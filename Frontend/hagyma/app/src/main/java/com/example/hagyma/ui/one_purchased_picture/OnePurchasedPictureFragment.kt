package com.example.hagyma.ui.one_purchased_picture

import android.Manifest
import android.app.Activity
import android.content.Intent
import android.content.pm.PackageManager
import android.graphics.BitmapFactory
import android.net.Uri
import android.os.Build
import android.os.Bundle
import android.provider.DocumentsContract
import android.util.Base64
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
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.hagyma.data.Comment
import com.example.hagyma.databinding.FragmentOnePurchasedPictureBinding
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.io.File
import java.time.OffsetDateTime
import java.util.*


class OnePurchasedPictureFragment : Fragment() {

    private var _binding: FragmentOnePurchasedPictureBinding? = null

    private val binding get() = _binding!!

    private var caffFile: File? = null

    private var _viewModel: OnePurchasedPictureViewModel?=null

    private val viewModel
        get() = _viewModel!!

    private lateinit var onePurchasedPictureCommentAdapter: OnePurchasedPictureCommentAdapter

    @RequiresApi(Build.VERSION_CODES.O)
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val onePurchasedPictureUUID = this.arguments?.getString("one_purchased_picture_uuid")

        _binding = FragmentOnePurchasedPictureBinding.inflate(inflater, container, false)
        _viewModel = OnePurchasedPictureViewModel()

        viewModel.caff.observe(viewLifecycleOwner){
            binding.textPictureName.text = it.title

            onePurchasedPictureCommentAdapter.clear()
            it.comments?.forEach { comment ->
                onePurchasedPictureCommentAdapter.addComment(
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

        onePurchasedPictureCommentAdapter = OnePurchasedPictureCommentAdapter(this.context, _viewModel)
        binding.rvComments.adapter = onePurchasedPictureCommentAdapter
        binding.rvComments.layoutManager = LinearLayoutManager(this.context)

        lifecycleScope.launch(Dispatchers.IO) {
            // Get CAFF File
            viewModel.getCAFF(onePurchasedPictureUUID!!)
        }

        lifecycleScope.launch(Dispatchers.IO) {
            viewModel.getDownloadCAFF(onePurchasedPictureUUID!!)
        }

        binding.addCommentButton.setOnClickListener {
            lifecycleScope.launch(Dispatchers.IO) {
                if(onePurchasedPictureUUID != null){
                    saveComment(onePurchasedPictureUUID, binding.editTextNewComment.text.toString())
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
            // putExtra(Intent.EXTRA_TITLE, "downloadCaff.caff")
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
            caffFile?.let { it1 -> requireContext().contentResolver.openOutputStream(uri)
                ?.let { it2 ->
                    requireContext().contentResolver.openInputStream(it1.toUri())?.copyTo(it2)
                } }
            val toast = Toast.makeText(context, "Download successful", Toast.LENGTH_SHORT)
            toast.show()
        }
    }

    private fun verifyStoragePermissions(activity: Activity?) {
        // Check if we have write permission
        val permission = ActivityCompat.checkSelfPermission(
            requireActivity(),
            Manifest.permission.WRITE_EXTERNAL_STORAGE
        )
        if (permission != PackageManager.PERMISSION_GRANTED) {
            // We don't have permission so prompt the user
            if (activity != null) {
                ActivityCompat.requestPermissions(
                    activity,
                    arrayOf(Manifest.permission.WRITE_EXTERNAL_STORAGE),1)
            }
        }
    }

    @RequiresApi(Build.VERSION_CODES.O)
    suspend fun saveComment(uuid: String, newCommentText: String){
        val newComment = Comment(
            UUID.randomUUID().toString(),
//            viewModel.userName.value.toString(),
            viewModel.getUserName(),
            OffsetDateTime.now(),
            uuid,
            newCommentText
        )
        try {
            viewModel.saveComment(uuid, newCommentText)
            requireActivity().runOnUiThread {
                onePurchasedPictureCommentAdapter.addComment(newComment)
                binding.editTextNewComment.text.clear()
            }
        }catch (e:Exception){
            println(e)
        }
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}