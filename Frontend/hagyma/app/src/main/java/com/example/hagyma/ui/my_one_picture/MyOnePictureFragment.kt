package com.example.hagyma.ui.my_one_picture

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.hagyma.R
import com.example.hagyma.databinding.FragmentMyOnePictureBinding

class MyOnePictureFragment : Fragment() {

    private var _binding: FragmentMyOnePictureBinding? = null

    private val binding get() = _binding!!

    private lateinit var myPictureCommentAdapter: MyPictureCommentAdapter

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
//        val myOnePictureViewModel =
//            ViewModelProvider(this).get(MyOnePictureViewModel::class.java)

        val mySearchedPictureUUID = this.arguments?.getString("my_searched_picture_uuid")  // TODO hasznalni

        _binding = FragmentMyOnePictureBinding.inflate(inflater, container, false)
        val root: View = binding.root

//        val textView: TextView = binding.textPictureName
//        myOnePictureViewModel.text.observe(viewLifecycleOwner) {
//            textView.text = it
//        }

        binding.textPictureName.text = mySearchedPictureUUID  // TODO majd nev db-bol

        myPictureCommentAdapter = MyPictureCommentAdapter(this.context)
        binding.rvComments.adapter = myPictureCommentAdapter
        binding.rvComments.layoutManager = LinearLayoutManager(this.context)

        binding.editButton.setOnClickListener { view ->
            val bundle = Bundle()
            bundle.putString("my_editing_picture_uuid", mySearchedPictureUUID)
            view.findNavController().navigate(R.id.action_nav_my_one_picture_to_nav_edit_my_picture, bundle)
        }

        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}