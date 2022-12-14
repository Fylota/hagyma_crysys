package com.example.hagyma.ui.edit_profile

import android.app.AlertDialog
import android.content.Intent
import android.os.Bundle
import android.os.Handler
import android.os.Looper
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.lifecycleScope
import androidx.navigation.findNavController
import com.example.hagyma.MainActivity
import com.example.hagyma.R
import com.example.hagyma.api.UserApi
import com.example.hagyma.api.model.UserChangeRequest
import com.example.hagyma.databinding.FragmentEditProfileBinding
import com.example.hagyma.helper.ApiHelper
import com.example.hagyma.infrastructure.ApiClient
import com.example.hagyma.extensions.validateNonEmpty
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch


class EditProfileFragment : Fragment() {

    private var _binding: FragmentEditProfileBinding? = null

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
        val editProfileViewModel =
            ViewModelProvider(this)[EditProfileViewModel::class.java]

        userApi = ApiHelper.getUserApi()
        _binding = FragmentEditProfileBinding.inflate(inflater, container, false)
        val root: View = binding.root

        val textViewUsername: TextView = binding.tvUsername

        val userID = this.arguments?.getString("editing_userid")

        editProfileViewModel.userName.observe(viewLifecycleOwner) {
            textViewUsername.text = it
        }

        binding.cancelButton.setOnClickListener {
            val toast = Toast.makeText(context, "Editing cancelled", Toast.LENGTH_SHORT)
            toast.show()

            val bundle = Bundle()
            root.findNavController().navigate(R.id.action_nav_edit_profile_to_nav_profile, bundle)
        }

        binding.saveButton.setOnClickListener {
            val currentPassword = binding.etCurrentPassword.text.toString()
            val newPassword = binding.etNewPassword.text.toString()
            if(!validateNewPassword(binding.etNewPassword, binding.etNewPassword2)) {
                Toast.makeText(context, "invalid new password", Toast.LENGTH_LONG).show()
            } else {
                val handler = Handler(Looper.getMainLooper()!!)
                lifecycleScope.launch(ioDispatcher) {
                    try {
                        userApi.apiUserUpdateUserPut(
                            UserChangeRequest(currentPassword, newPassword))
                        handler.post {
                            Toast.makeText(context, "Saving...", Toast.LENGTH_SHORT).show()
                            val bundle = Bundle()
                            root.findNavController().navigate(R.id.action_nav_edit_profile_to_nav_profile, bundle)
                        }
                    } catch (e: Exception){
                        e.message?.let { it1 -> Log.e(tag, it1) }
                    }
                }
            }
        }

        val builder  = AlertDialog.Builder(context)
        builder.setTitle("Deleting user")
        builder.setMessage("Dou you want to delete your profile?")
        builder.setPositiveButton(android.R.string.ok) { _, _ ->
            userID?.let { deleteUser(it) }
        }
        builder.setNegativeButton(android.R.string.cancel) { _, _ ->
            Toast.makeText(context,"Canceled", Toast.LENGTH_SHORT).show()
        }

        binding.deleteButton.setOnClickListener {
            builder.show()
        }

        return root
    }

    private fun deleteUser(id: String) {
        val toast = Toast.makeText(context, "Deleting user...", Toast.LENGTH_SHORT)
        toast.show()
        val handler = Handler(Looper.getMainLooper()!!)
        lifecycleScope.launch(ioDispatcher) {
            try {
                userApi.apiUserDeleteUserDelete(id)
                ApiClient.accessToken = null
                handler.post {
                    Toast.makeText(context, "User deleted", Toast.LENGTH_SHORT).show()
                    val intent = Intent(activity, MainActivity::class.java)
                    startActivity(intent)
                    activity?.finish()
                }
            } catch (e: Exception) {
                Toast.makeText(context, "Delete failed", Toast.LENGTH_SHORT).show()
                e.message?.let { it1 -> Log.e(tag, it1) }
            }
        }
    }

    private fun validateNewPassword(pass1: EditText, pass2: EditText): Boolean =
        pass1.validateNonEmpty() &&
        pass1.text.toString().length >= 6 &&
        pass1.text.toString().any(Char::isDigit) &&
        pass1.text.toString().any(Char::isLowerCase) &&
        pass1.text.toString().any(Char::isUpperCase) &&
        pass1.text.toString().any { it in "!,+^" } &&
        pass1.text.toString() == pass2.text.toString()

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}