package com.example.hagyma.ui.my_one_picture

import android.Manifest
import android.app.Activity
import android.content.Intent
import android.content.pm.PackageManager
import android.graphics.BitmapFactory
import android.net.Uri
import android.os.Build
import android.os.Bundle
import android.os.Handler
import android.os.Looper
import android.provider.DocumentsContract
import android.util.Base64
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.activity.result.contract.ActivityResultContracts
import androidx.annotation.RequiresApi
import androidx.core.app.ActivityCompat
import androidx.core.net.toUri
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.hagyma.R
import com.example.hagyma.data.Comment
import com.example.hagyma.databinding.FragmentMyOnePictureBinding
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.io.File
import java.time.OffsetDateTime
import java.util.*

class MyOnePictureFragment : Fragment() {

    private var _binding: FragmentMyOnePictureBinding? = null
    private val binding get() = _binding!!
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
    private var caffFile: File? = null
    private var _viewModel: MyOnePictureViewModel?=null
    private val viewModel
        get() = _viewModel!!

    private lateinit var myPictureCommentAdapter: MyPictureCommentAdapter

    @RequiresApi(Build.VERSION_CODES.O)
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val mySearchedPictureUUID = this.arguments?.getString("my_searched_picture_uuid")

        _binding = FragmentMyOnePictureBinding.inflate(inflater, container, false)
        _viewModel = MyOnePictureViewModel()

        viewModel.caff.observe(viewLifecycleOwner){
            binding.textPictureName.text = it.title

            myPictureCommentAdapter.clear()
            it.comments?.forEach { comment ->
                myPictureCommentAdapter.addComment(
                    Comment(comment.id, comment.creatorName, comment.creationTime, it.id, comment.content)
                )
            }
            val decodedString = Base64.decode(it.preview.toByteArray(), Base64.DEFAULT)
            val decodedByte = BitmapFactory.decodeByteArray(decodedString,0,decodedString.size)

            binding.ivPicture.setImageBitmap(decodedByte)
        }

        viewModel.caffFile.observe(viewLifecycleOwner) {
            caffFile = it
        }

        val root: View = binding.root

        myPictureCommentAdapter = MyPictureCommentAdapter(this.context, _viewModel)
        binding.rvComments.adapter = myPictureCommentAdapter
        binding.rvComments.layoutManager = LinearLayoutManager(this.context)

        // Get CAFF File
        viewModel.getCAFF(mySearchedPictureUUID!!)

        viewModel.getDownloadCAFF(mySearchedPictureUUID)

        binding.addCommentButton.setOnClickListener {
            lifecycleScope.launch(ioDispatcher) {
                saveComment(mySearchedPictureUUID, binding.editTextNewComment.text.toString())
            }
        }

        val handler = Handler(Looper.getMainLooper()!!)
        binding.deleteButton.setOnClickListener { view ->
            lifecycleScope.launch(ioDispatcher) {
                try {
                    viewModel.deletePicture(mySearchedPictureUUID)
                    handler.post {
                        view.findNavController().navigate(R.id.action_nav_my_one_picture_to_nav_my_pictures)
                    }
                } catch (e: Exception) {
                    e.message?.let { it1 -> Log.e(tag, it1) }
                }
            }
        }

        binding.fltBtnDownloadPicture.setOnClickListener {
            createFileWithActivityForResult()
        }

        return root
    }

    private fun createFileWithActivityForResult() {
        verifyStoragePermissions(this.activity)
        val intent = Intent(Intent.ACTION_CREATE_DOCUMENT).apply {
            addCategory(Intent.CATEGORY_OPENABLE)
            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
                putExtra(DocumentsContract.EXTRA_INITIAL_URI, Uri.parse("/Downloads"))
            }
            type = "*/*"
        }
        resultLauncher.launch(intent)
    }

    private var resultLauncher = registerForActivityResult(ActivityResultContracts.StartActivityForResult()) { result ->
        if (result.resultCode == Activity.RESULT_OK) {
            val uri: Uri?
            if (result.data != null) {
                uri = result.data!!.data
                // Perform operations on the document using its URI.
                createFile(uri)
            }
        }
    }

    private fun createFile(uri: Uri?) {
        if (uri != null) {
            try {
                caffFile?.let { it1 -> requireContext().contentResolver.openOutputStream(uri)
                    ?.let { it2 ->
                        requireContext().contentResolver.openInputStream(it1.toUri())?.copyTo(it2)
                    } }
                val toast = Toast.makeText(context, "Download successful", Toast.LENGTH_SHORT)
                toast.show()
            } catch (e: Exception) {
                e.message?.let { it1 -> Log.e(tag, it1) }
            }
        }
    }

    private fun verifyStoragePermissions(activity: Activity?) {
        // Check if we have write permission
        val permission = ActivityCompat.checkSelfPermission(
            requireActivity(),
            Manifest.permission.WRITE_EXTERNAL_STORAGE
        )
        if (permission != PackageManager.PERMISSION_GRANTED && activity != null) {
            // We don't have permission so prompt the user
            ActivityCompat.requestPermissions(
                activity,
                arrayOf(Manifest.permission.WRITE_EXTERNAL_STORAGE),1
            )
        }
    }

    @RequiresApi(Build.VERSION_CODES.O)
    suspend fun saveComment(uuid: String, newCommentText: String){
        val newComment = Comment(
            UUID.randomUUID().toString(),
            viewModel.getUserName(),
            OffsetDateTime.now(),
            uuid,
            newCommentText
        )
        try {
            viewModel.saveComment(uuid, newCommentText)
            requireActivity().runOnUiThread {
                myPictureCommentAdapter.addComment(newComment)
                binding.editTextNewComment.text.clear()
            }
        }catch (e:Exception){
            e.message?.let { it1 -> Log.e(tag, it1) }
        }
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}