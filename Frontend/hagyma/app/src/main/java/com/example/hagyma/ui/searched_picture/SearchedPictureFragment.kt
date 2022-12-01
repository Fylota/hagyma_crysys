package com.example.hagyma.ui.searched_picture

import android.graphics.BitmapFactory
import android.os.Build
import android.os.Bundle
import android.util.Base64
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.annotation.RequiresApi
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.hagyma.data.Comment
import com.example.hagyma.databinding.FragmentSearchedPictureBinding
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.time.OffsetDateTime
import java.util.*


class SearchedPictureFragment: Fragment() {

    private var _binding: FragmentSearchedPictureBinding? = null

    private val binding get() = _binding!!

    private var _viewModel: SearchedPictureViewModel?=null

    private val viewModel
        get() = _viewModel!!

    private lateinit var commentAdapter: CommentAdapter

    @RequiresApi(Build.VERSION_CODES.O)
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {

        val searchedPictureUUID = this.arguments?.getString("searched_picture_uuid")  // TODO hasznalni

        _binding = FragmentSearchedPictureBinding.inflate(inflater, container, false)
        _viewModel = ViewModelProvider(requireActivity())[SearchedPictureViewModel::class.java]

        viewModel.caff.observe(viewLifecycleOwner){
            binding.textPictureName.text = it.title

            commentAdapter.clear()
            it.comments?.forEach { comment ->
                commentAdapter.addComment(
                    Comment(comment.id, comment.creatorName, comment.creationTime, it.id, comment.content)
                )
            }
            val decodedString = Base64.decode(it.preview.toByteArray(), Base64.DEFAULT)
            val decodedByte = BitmapFactory.decodeByteArray(decodedString,0,decodedString.size)

            binding.ivPicture.setImageBitmap(decodedByte)
        }

        val root: View = binding.root

//        binding.textPictureName.text = searchedPictureUUID  // TODO majd nev db-bol

        commentAdapter = CommentAdapter(this.context)
        binding.rvComments.adapter = commentAdapter
        binding.rvComments.layoutManager = LinearLayoutManager(this.context)



        lifecycleScope.launch(Dispatchers.IO) {
            // Get CAFF File
            viewModel.getCAFF(searchedPictureUUID!!)
        }


        binding.addCommentButton.setOnClickListener {
            lifecycleScope.launch(Dispatchers.IO) {
                if(searchedPictureUUID != null){
                    saveComment(searchedPictureUUID, binding.editTextNewComment.text.toString())
                }
            }
        }

        return root
    }


    @RequiresApi(Build.VERSION_CODES.O)
    suspend fun saveComment(uuid: String, newCommentText: String){
        // TODO: Uj komment feltoltese db-be
        val newComment = Comment(
            UUID.randomUUID().toString(),
            viewModel.userName.toString(),
            OffsetDateTime.now(),
            uuid,
            newCommentText
        )
        try {
            viewModel.saveComment(uuid, newCommentText)
            commentAdapter.addComment(newComment)
            requireActivity().runOnUiThread {
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