package com.example.hagyma.ui.searched_picture

import android.app.AlertDialog
import android.graphics.BitmapFactory
import android.os.Build
import android.os.Bundle
import android.os.Handler
import android.os.Looper
import android.util.Base64
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.annotation.RequiresApi
import androidx.fragment.app.Fragment
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
        _viewModel = SearchedPictureViewModel()

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

        commentAdapter = CommentAdapter(this.context, _viewModel)
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

        val builder  = AlertDialog.Builder(context)
        builder.setTitle("Purchase")
        builder.setMessage("Would you like to purchase this CAFF?")
        builder.setPositiveButton(android.R.string.ok) { dialog, which ->
            purchaseImage(searchedPictureUUID)
        }
        builder.setNegativeButton(android.R.string.cancel) { dialog, which ->
            Toast.makeText(context,"Payment canceled", Toast.LENGTH_SHORT).show()
        }

        binding.purchaseButton.setOnClickListener {
            builder.show()
        }

        return root
    }
    // todo dont let owner try to purchase own image
    private fun purchaseImage(searchedPictureUUID: String?) {
        val handler = Handler(Looper.getMainLooper()!!)
        lifecycleScope.launch(Dispatchers.IO) {
            if(searchedPictureUUID != null){
                try {
                    viewModel.purchaseCaff(searchedPictureUUID)
                    handler.post {
                        Toast.makeText(context, "CAFF purchased", Toast.LENGTH_SHORT).show()
                    }
                } catch (e: Exception) {
                    handler.post {
                        Toast.makeText(context, "Purchase failed", Toast.LENGTH_SHORT).show()
                    }
                    e.message?.let { it1 -> Log.e(tag, it1) }
                }
            }
        }
    }


    @RequiresApi(Build.VERSION_CODES.O)
    suspend fun saveComment(uuid: String, newCommentText: String){
        val newComment = Comment(
            UUID.randomUUID().toString(),
            viewModel.getUserName(),
//            viewModel.userName.value.toString(),
            OffsetDateTime.now(),
            uuid,
            newCommentText
        )
        try {
            viewModel.saveComment(uuid, newCommentText)
            requireActivity().runOnUiThread {
                commentAdapter.addComment(newComment)
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