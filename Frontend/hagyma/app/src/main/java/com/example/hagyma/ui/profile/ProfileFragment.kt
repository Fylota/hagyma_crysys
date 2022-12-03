package com.example.hagyma.ui.profile

import android.app.AlertDialog
import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.lifecycleScope
import androidx.navigation.findNavController
import com.example.hagyma.R
import com.example.hagyma.api.UserApi
import com.example.hagyma.databinding.FragmentProfileBinding
import com.example.hagyma.helper.ApiHelper
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch

class ProfileFragment : Fragment() {

    private var _binding: FragmentProfileBinding? = null

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = _binding!!
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
    private lateinit var userApi : UserApi

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val viewModel =
            ViewModelProvider(this)[ProfileViewModel::class.java]

        userApi = ApiHelper.getUserApi()
        _binding = FragmentProfileBinding.inflate(inflater, container, false)
        val root: View = binding.root

        val textViewUsername: TextView = binding.tvUsername
        val textViewEmail: TextView = binding.tvEmail
        val textViewDate: TextView = binding.tvRegistrationDate

        var userID = this.arguments?.getString("userID")
        val userName = this.arguments?.getString("userName")
        val email = this.arguments?.getString("email")
        val regDate = this.arguments?.getString("regDate")
        val isDeleted = this.arguments?.getBoolean("isDeleted")
        var isAdminViewing = true

        if (userID == null) { // viewing own profile
            viewModel.userId.observe(viewLifecycleOwner) {
                userID = it
            }
            isAdminViewing = false
        }

        if (userName == null) {
            viewModel.userName.observe(viewLifecycleOwner) {
                textViewUsername.text = it
            }
        } else {
            textViewUsername.text = userName
        }

        if (email == null) {
            viewModel.email.observe(viewLifecycleOwner) {
                textViewEmail.text = it
            }
        } else {
            textViewEmail.text = email
        }

        if (regDate == null) {
            viewModel.regDate.observe(viewLifecycleOwner) {
                textViewDate.text = it
            }
        } else {
            textViewDate.text = regDate
        }

        // If an admin is viewing a user's profile they can delete the user profile here,
        // but can not edit the password.
        // If user is viewing their own profile,
        // only the edit button is visible.
        if (isAdminViewing) {
            binding.editButton.visibility = View.INVISIBLE
            if (isDeleted == true) {
                binding.tvDeletedUserLabel.visibility = View.VISIBLE
            } else {
                binding.deleteProfileButton.visibility = View.VISIBLE
            }
        }

        binding.editButton.setOnClickListener {
            val bundle = Bundle()
            bundle.putString("editing_userid", userID)
            bundle.putString("editing_userName", userName)
            bundle.putString("editing_email", email)
            bundle.putString("editing_regDate", regDate)
            root.findNavController().navigate(R.id.action_nav_profile_to_nav_edit_profile, bundle)
        }

        val builder  = AlertDialog.Builder(context)
        builder.setTitle("Deleting user")
        builder.setMessage("Dou you want to delete this user?")
        builder.setPositiveButton(android.R.string.ok) { _, _ ->
            userID?.let { deleteUser(it, root) }
        }
        builder.setNegativeButton(android.R.string.cancel) { _, _ ->
            Toast.makeText(context,"Canceled", Toast.LENGTH_SHORT).show()
        }

        binding.deleteProfileButton.setOnClickListener {
            builder.show()
        }
        return root
    }

    private fun deleteUser(userId: String, root: View) {
        lifecycleScope.launch(ioDispatcher) {
            try {
                userApi.apiUserDeleteUserDelete(userId)
                root.findNavController().navigate(R.id.action_nav_profile_to_nav_admin_users)
            } catch (e: Exception){
                e.message?.let { it1 -> Log.e(tag, it1) }
            }
        }
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}