package com.example.hagyma.ui.searched_picture

import android.graphics.BitmapFactory
import android.graphics.drawable.BitmapDrawable
import android.graphics.drawable.Drawable
import android.os.Build
import android.os.Bundle
import android.util.Base64
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.annotation.RequiresApi
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.hagyma.api.model.CommentRequest
import com.example.hagyma.data.Comment
import com.example.hagyma.data.ListItem
import com.example.hagyma.databinding.FragmentSearchedPictureBinding
import com.example.hagyma.helper.ApiHelper
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.io.ByteArrayInputStream
import java.time.OffsetDateTime
import java.util.*


class SearchedPictureFragment: Fragment() {

    private var _binding: FragmentSearchedPictureBinding? = null

    private val binding get() = _binding!!

    private lateinit var commentAdapter: CommentAdapter

    @RequiresApi(Build.VERSION_CODES.O)
    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val searchedPictureViewModel =
            ViewModelProvider(this).get(SearchedPictureViewModel::class.java)

        val searchedPictureUUID = this.arguments?.getString("searched_picture_uuid")  // TODO hasznalni

        _binding = FragmentSearchedPictureBinding.inflate(inflater, container, false)
        val root: View = binding.root

//        binding.textPictureName.text = searchedPictureUUID  // TODO majd nev db-bol

        commentAdapter = CommentAdapter(this.context)
        binding.rvComments.adapter = commentAdapter
        binding.rvComments.layoutManager = LinearLayoutManager(this.context)

        lifecycleScope.launch(Dispatchers.IO) {
            // Get CAFF File
            initCAFFFile(searchedPictureUUID!!, searchedPictureViewModel)
        }

        binding.addCommentButton.setOnClickListener {
            lifecycleScope.launch(Dispatchers.IO) {
                if(searchedPictureUUID != null){
                    saveComment(searchedPictureUUID, binding.editTextNewComment.text.toString(),
                        searchedPictureViewModel)
                }
            }
        }

        return root
    }

    suspend fun initCAFFFile(uuid: String, viewModel: SearchedPictureViewModel) {
        val caffApi = ApiHelper.getCaffApi()
        try {
            val picture = caffApi.apiCaffGetImageGet(uuid)
            // Set name
            binding.textPictureName.text = picture.title

            // Save comments
            picture.comments?.forEach { comment ->
                commentAdapter.addComment(Comment(comment.id, viewModel.userId.toString(),
                    viewModel.userName.toString(), comment.creationTime, picture.id, comment.content))
            }

            binding.ivPicture.setBackgroundDrawable(
                BitmapDrawable(
                    requireActivity().resources,
                    ByteArrayInputStream(Base64.decode(picture.preview.toByteArray(),Base64.DEFAULT))
                )
            )
            // TODO: kep berakasa
        }catch (e:Exception){
            System.out.println(e)
        }
    }

    @RequiresApi(Build.VERSION_CODES.O)
    suspend fun saveComment(uuid: String, newCommentText: String, viewModel: SearchedPictureViewModel){
        // TODO: Uj komment feltoltese db-be
        val newComment = Comment(
            UUID.randomUUID().toString(),
            viewModel.userId.toString(),
            viewModel.userName.toString(),
            OffsetDateTime.now(),
            uuid,
            newCommentText
        )
        try {
            val caffApi = ApiHelper.getCaffApi()
            caffApi.apiCaffAddCommentPost(uuid, CommentRequest(newCommentText))
            commentAdapter.addComment(newComment)
            binding.editTextNewComment.text.clear()
        }catch (e:Exception){
            System.out.println(e)
        }
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}