package com.example.hagyma.ui.admin_users

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.hagyma.api.AuthenticationApi
import com.example.hagyma.api.UserApi
import com.example.hagyma.databinding.FragmentAdminUsersBinding
import com.example.hagyma.helper.ApiHelper

class AdminUsersFragment : Fragment() {

    private var _binding: FragmentAdminUsersBinding? = null

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = _binding!!
    private lateinit var adminUsersAdapter: AdminUsersAdapter

    private lateinit var authenticationApi: AuthenticationApi
    private lateinit var userApi : UserApi

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val slideshowViewModel =
            ViewModelProvider(this).get(AdminUsersViewModel::class.java)

        authenticationApi = ApiHelper.getAuthenticationApi()
        userApi = ApiHelper.getUserApi()

        _binding = FragmentAdminUsersBinding.inflate(inflater, container, false)
        val root: View = binding.root

//        val textView: TextView = binding.textAdminUsers
//        slideshowViewModel.text.observe(viewLifecycleOwner) {
//            textView.text = it
//        }

        adminUsersAdapter = AdminUsersAdapter(this.context)
        binding.rvPictures.adapter = adminUsersAdapter
        binding.rvPictures.layoutManager = LinearLayoutManager(this.context)

        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}