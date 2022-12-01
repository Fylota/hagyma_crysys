package com.example.hagyma.ui.one_purchased_picture

import android.graphics.BitmapFactory
import android.os.Build
import android.os.Bundle
import android.util.Base64
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import android.widget.Toast
import androidx.annotation.RequiresApi
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.hagyma.data.Comment
import com.example.hagyma.databinding.FragmentOnePurchasedPictureBinding
import com.example.hagyma.ui.searched_picture.SearchedPictureViewModel
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.time.OffsetDateTime
import java.util.*

class OnePurchasedPictureFragment : Fragment() {

    private var _binding: FragmentOnePurchasedPictureBinding? = null

    private val binding get() = _binding!!

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

        val root: View = binding.root

        onePurchasedPictureCommentAdapter = OnePurchasedPictureCommentAdapter(this.context, _viewModel)
        binding.rvComments.adapter = onePurchasedPictureCommentAdapter
        binding.rvComments.layoutManager = LinearLayoutManager(this.context)

        lifecycleScope.launch(Dispatchers.IO) {
            // Get CAFF File
            viewModel.getCAFF(onePurchasedPictureUUID!!)
        }

        binding.addCommentButton.setOnClickListener {
            lifecycleScope.launch(Dispatchers.IO) {
                if(onePurchasedPictureUUID != null){
                    saveComment(onePurchasedPictureUUID, binding.editTextNewComment.text.toString())
                }
            }
        }

        // TODO: DOWNLOAD!!!
        binding.fltBtnDownloadPicture.setOnClickListener{ view ->
            val toast = Toast.makeText(context, "TODO DOWNLOAD", Toast.LENGTH_SHORT)
            toast.show()
        }

        return root
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
            System.out.println(e)
        }
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}