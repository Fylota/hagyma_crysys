package com.example.hagyma.ui.admin_users

import android.content.Context
import android.os.Bundle
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.navigation.findNavController
import androidx.recyclerview.widget.RecyclerView
import com.example.hagyma.R
import com.example.hagyma.api.model.User
import com.example.hagyma.databinding.UserItemBinding

class AdminUsersAdapter(private val context: Context?) :
    RecyclerView.Adapter<AdminUsersAdapter.AdminUsersItemViewHolder>() {

    private var userList: MutableList<User> = mutableListOf()

    class AdminUsersItemViewHolder(val binding: UserItemBinding): RecyclerView.ViewHolder(binding.root)

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): AdminUsersItemViewHolder {
        return AdminUsersItemViewHolder(
            UserItemBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        )
    }

    override fun onBindViewHolder(holder: AdminUsersItemViewHolder, position: Int) {
        val currUser = userList[position]
        holder.binding.tvUsername.text = currUser.name
        if (currUser.isDeleted == true) {
            holder.binding.tvUsername.text = context?.resources
                ?.getString(R.string.deletedUserItem, currUser.name) ?: currUser.name
        }
        holder.binding.ivCheckUserBtn.setOnClickListener { view ->
            val bundle = Bundle()
            bundle.putString("userID", currUser.id)
            bundle.putString("userName", currUser.name)
            bundle.putString("email", currUser.email)
            currUser.isDeleted?.let { bundle.putBoolean("isDeleted", it) }
            view.findNavController().navigate(R.id.action_nav_admin_users_to_nav_profile, bundle)
        }
    }

    override fun getItemCount(): Int {
        return userList.size
    }

    fun addInitUsers(newItem: User){
        userList.add(newItem)
        notifyItemInserted(userList.size -1)
    }
}