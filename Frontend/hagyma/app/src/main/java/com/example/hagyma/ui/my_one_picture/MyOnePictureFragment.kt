package com.example.hagyma.ui.my_one_picture

import android.graphics.BitmapFactory
import android.os.Build
import android.os.Bundle
import android.util.Base64
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.annotation.RequiresApi
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.hagyma.R
import com.example.hagyma.data.Comment
import com.example.hagyma.databinding.FragmentMyOnePictureBinding
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.time.OffsetDateTime
import java.util.*

class MyOnePictureFragment : Fragment() {

    private var _binding: FragmentMyOnePictureBinding? = null

    private val binding get() = _binding!!

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
        val mySearchedPictureUUID = this.arguments?.getString("my_searched_picture_uuid")  // TODO hasznalni

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

        val root: View = binding.root

        myPictureCommentAdapter = MyPictureCommentAdapter(this.context, _viewModel)
        binding.rvComments.adapter = myPictureCommentAdapter
        binding.rvComments.layoutManager = LinearLayoutManager(this.context)

        lifecycleScope.launch(Dispatchers.IO) {
            // Get CAFF File
            viewModel.getCAFF(mySearchedPictureUUID!!)
        }

        binding.addCommentButton.setOnClickListener {
            lifecycleScope.launch(Dispatchers.IO) {
                if(mySearchedPictureUUID != null){
                    saveComment(mySearchedPictureUUID, binding.editTextNewComment.text.toString())
                }
            }
        }

        binding.editButton.setOnClickListener { view ->
            val bundle = Bundle()
            bundle.putString("my_editing_picture_uuid", mySearchedPictureUUID)
            view.findNavController().navigate(R.id.action_nav_my_one_picture_to_nav_edit_my_picture, bundle)
        }

        binding.editButton.setOnClickListener { view ->
            lifecycleScope.launch(Dispatchers.IO) {
                if(mySearchedPictureUUID != null){
                    viewModel.deletePicture(mySearchedPictureUUID)
                    view.findNavController().navigate(R.id.action_nav_my_one_picture_to_nav_my_pictures)
                }
            }
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
                myPictureCommentAdapter.addComment(newComment)
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