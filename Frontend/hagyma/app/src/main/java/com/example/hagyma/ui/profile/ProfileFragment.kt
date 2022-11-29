package com.example.hagyma.ui.profile

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.findNavController
import com.example.hagyma.R
import com.example.hagyma.databinding.FragmentProfileBinding
import java.util.*

class ProfileFragment : Fragment() {

    private var _binding: FragmentProfileBinding? = null

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = _binding!!

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val viewModel =
            ViewModelProvider(this).get(ProfileViewModel::class.java)

        var userID = this.arguments?.getString("userID")
        if(userID == null) { // Akkor nem admin van belepve es egy felhasznalo oldalara akar menni, hanem az adott user a sajat oldalara
            userID = UUID.randomUUID().toString()
            viewModel.userId.observe(viewLifecycleOwner) {
                userID = it
            }
        }

        _binding = FragmentProfileBinding.inflate(inflater, container, false)
        val root: View = binding.root

        val textViewUsername: TextView = binding.tvUsername
        viewModel.userName.observe(viewLifecycleOwner) {
            textViewUsername.text = it
        }

        val textViewEmail: TextView = binding.tvEmail
        viewModel.email.observe(viewLifecycleOwner) {
            textViewEmail.text = it
        }

        val textViewDate: TextView = binding.tvRegistrationDate
        viewModel.regDate.observe(viewLifecycleOwner) {
            textViewDate.text = it
        }

        binding.editButton.setOnClickListener {
            val bundle = Bundle()
            bundle.putString("editing_userid", userID)
            root.findNavController().navigate(R.id.action_nav_profile_to_nav_edit_profile, bundle)
        }

        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}