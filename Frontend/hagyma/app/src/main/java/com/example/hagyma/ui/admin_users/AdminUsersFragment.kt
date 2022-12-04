package com.example.hagyma.ui.admin_users

import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.hagyma.api.UserApi
import com.example.hagyma.databinding.FragmentAdminUsersBinding
import com.example.hagyma.helper.ApiHelper
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class AdminUsersFragment : Fragment() {

    private var _binding: FragmentAdminUsersBinding? = null

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = _binding!!
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
    private lateinit var adminUsersAdapter: AdminUsersAdapter
    private lateinit var userApi : UserApi

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        userApi = ApiHelper.getUserApi()

        _binding = FragmentAdminUsersBinding.inflate(inflater, container, false)
        val root: View = binding.root

        adminUsersAdapter = AdminUsersAdapter(this.context)
        binding.rvPictures.adapter = adminUsersAdapter
        binding.rvPictures.layoutManager = LinearLayoutManager(this.context)

        lifecycleScope.launch {
            initUsers()
        }

        return root
    }

    private suspend fun initUsers() {
        withContext(ioDispatcher) {
            try {
                val users = userApi.apiUserGetUsersGet()
                users.forEach { item ->
                    activity?.runOnUiThread {
                        adminUsersAdapter.addInitUsers(
                            item
                        )
                    }
                }
            } catch (e: Exception) {
                e.message?.let { it1 -> Log.e(tag, it1) }
            }
        }
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}