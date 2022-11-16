package com.example.hagyma.ui.one_purchased_picture

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.hagyma.databinding.FragmentOnePurchasedPictureBinding

class OnePurchasedPictureFragment : Fragment() {

    private var _binding: FragmentOnePurchasedPictureBinding? = null

    private val binding get() = _binding!!

    private lateinit var onePurchasedPictureCommentAdapter: OnePurchasedPictureCommentAdapter

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val onePurchasedPictureViewModel =
            ViewModelProvider(this).get(OnePurchasedPictureViewModel::class.java)

        val onePurchasedPictureUUID = this.arguments?.getString("one_purchased_picture_uuid")

        _binding = FragmentOnePurchasedPictureBinding.inflate(inflater, container, false)
        val root: View = binding.root

//        val textView: TextView = binding.textPurchasedPictures
//        onePurchasedPictureViewModel.text.observe(viewLifecycleOwner) {
//            textView.text = it
//        }

        binding.textPictureName.text = onePurchasedPictureUUID  // TODO majd nev db-bol

        onePurchasedPictureCommentAdapter = OnePurchasedPictureCommentAdapter(this.context)
        binding.rvComments.adapter = onePurchasedPictureCommentAdapter
        binding.rvComments.layoutManager = LinearLayoutManager(this.context)

        binding.fltBtnDownloadPicture.setOnClickListener{ view ->
            val toast = Toast.makeText(context, "TODO DOWNLOAD", Toast.LENGTH_SHORT)
            toast.show()
        }

        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}