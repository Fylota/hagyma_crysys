package com.example.hagyma.ui.searched_picture

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.hagyma.data.Comment
import com.example.hagyma.data.ListItem
import com.example.hagyma.databinding.FragmentSearchedPictureBinding
import com.example.hagyma.helper.ApiHelper
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch


class SearchedPictureFragment: Fragment() {

    private var _binding: FragmentSearchedPictureBinding? = null

    private val binding get() = _binding!!

    private lateinit var commentAdapter: CommentAdapter

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

        binding.textPictureName.text =searchedPictureUUID  // TODO majd nev db-bol

        commentAdapter = CommentAdapter(this.context)
        binding.rvComments.adapter = commentAdapter
        binding.rvComments.layoutManager = LinearLayoutManager(this.context)

        lifecycleScope.launch(Dispatchers.IO) {
            // Get CAFF File
            if(searchedPictureUUID != null){
                initCAFFFile(searchedPictureUUID)
            }
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

    suspend fun initCAFFFile(uuid: String) {
        val caffApi = ApiHelper.getCaffApi()
        try {
            val picture = caffApi.apiCaffGetImageGet(uuid)
            picture.comments?.forEach { comment ->
                commentAdapter.addComment(Comment(comment.id, "todoUserID",
                    comment.creationTime, picture.id, comment.content))
            }
            println("PICTURE: $picture")
            binding.textPictureName.text = picture.title
            // TODO: kep berakasa
            //TODO: uj komment eseten a komment hozzaadasa a cucchoz
        }catch (e:Exception){
            System.out.println(e)
        }
    }

    suspend fun saveComment(uuid: String, newCommentText: String){
        // TODO: Uj komment keszitese a szovegbol a felhaszalo stb adatok alapjan
        // TODO: Uj komment feltoltese db-be
//        commentAdapter.addComment(//TODO: Uj komment)
        binding.editTextNewComment.text.clear()
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}