package com.example.hagyma.ui.edit_profile

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.findNavController
import com.example.hagyma.R
import com.example.hagyma.databinding.FragmentEditProfileBinding
import com.example.hagyma.databinding.FragmentProfileBinding

class EditProfileFragment : Fragment() {

    private var _binding: FragmentEditProfileBinding? = null

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = _binding!!

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val editProfileViewModel =
            ViewModelProvider(this).get(EditProfileViewModel::class.java)

        val userID = this.arguments?.getString("editing_userid");

        _binding = FragmentEditProfileBinding.inflate(inflater, container, false)
        val root: View = binding.root

        binding.tvUsername.text = userID;

        binding.cancelButton.setOnClickListener {
            val toast = Toast.makeText(context, "Editing cancelled", Toast.LENGTH_SHORT)
            toast.show()

            val bundle = Bundle()
            bundle.putString("userID", userID);
            root.findNavController().navigate(R.id.action_nav_edit_profile_to_nav_profile, bundle)
        }

        binding.saveButton.setOnClickListener {
            val toast = Toast.makeText(context, "TODO save", Toast.LENGTH_SHORT)
            toast.show()

            val bundle = Bundle()
            bundle.putString("userID", userID);
            root.findNavController().navigate(R.id.action_nav_edit_profile_to_nav_profile, bundle)
        }

        binding.deleteButton.setOnClickListener {
            val toast = Toast.makeText(context, "Todo delete", Toast.LENGTH_SHORT)
            toast.show()

            val bundle = Bundle()
            bundle.putString("userID", userID);
            root.findNavController().navigate(R.id.action_nav_edit_profile_to_nav_profile, bundle)
        }

//        val textView: TextView = binding.textProfile
//        slideshowViewModel.text.observe(viewLifecycleOwner) {
//            textView.text = it
//        }
//        binding.tvUsername.text = userID;
//
//        binding.editButton.setOnClickListener {
//            val bundle = Bundle()
//            bundle.putString("editing_userid", userID);
//            root.findNavController().navigate(R.id.action_nav_gallery_to_nav_upload_caff, bundle)
//        }

        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}