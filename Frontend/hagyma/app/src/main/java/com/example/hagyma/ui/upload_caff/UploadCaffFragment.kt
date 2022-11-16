package com.example.hagyma.ui.upload_caff

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.findNavController
import com.example.hagyma.R
import com.example.hagyma.databinding.FragmentPurchasedPicturesBinding
import com.example.hagyma.databinding.FragmentUploadCaffBinding

class UploadCaffFragment : Fragment() {

    private var _binding: FragmentUploadCaffBinding? = null

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = _binding!!

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val slideshowViewModel =
            ViewModelProvider(this).get(UploadCaffViewModel::class.java)

        val originalPageName = this.arguments?.getString("original_page")

        _binding = FragmentUploadCaffBinding.inflate(inflater, container, false)
        val root: View = binding.root

        binding.uploadBtn.setOnClickListener {
            val toast = Toast.makeText(context, "TOTO UPLOAD", Toast.LENGTH_SHORT)
            toast.show()
            if(originalPageName == "gallery"){
                root.findNavController().navigate(R.id.action_nav_upload_caff_to_nav_gallery)
            } else {
                root.findNavController().navigate(R.id.action_nav_upload_caff_to_nav_my_pictures)
            }
        }

//        val textView: TextView = binding.textUploadCaff
//        slideshowViewModel.text.observe(viewLifecycleOwner) {
//            textView.text = it
//        }
        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}