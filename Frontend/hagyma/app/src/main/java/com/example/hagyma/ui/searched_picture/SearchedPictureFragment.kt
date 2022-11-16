package com.example.hagyma.ui.searched_picture

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.hagyma.databinding.FragmentSearchedPictureBinding


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

//        val textView: TextView = binding.textPictureName
//        searchedPictureViewModel.text.observe(viewLifecycleOwner) {
//            textView.text = it
//        }

        commentAdapter = CommentAdapter(this.context)
        binding.rvComments.adapter = commentAdapter
        binding.rvComments.layoutManager = LinearLayoutManager(this.context)

        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}