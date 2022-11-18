package com.example.hagyma.ui.admin_users

import android.content.Context
import android.os.Bundle
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.navigation.findNavController
import androidx.recyclerview.widget.RecyclerView
import com.example.hagyma.R
import com.example.hagyma.data.User
import com.example.hagyma.databinding.UserItemBinding
import java.util.*

class AdminUsersAdapter(private val context: Context?) :
    RecyclerView.Adapter<AdminUsersAdapter.AdminUsersItemViewHolder>() {

    private val testUsersList: List<User> = listOf(
        User(UUID.randomUUID(), false, "user1", "email@email.com", Date(), null, null, null),
        User(UUID.randomUUID(), false, "user2", "email@email.com", Date(),null, null, null),
        User(UUID.randomUUID(), false, "user3", "email@email.com", Date(),null, null, null),
        User(UUID.randomUUID(), false, "user4", "email@email.com", Date(),null, null, null),
        User(UUID.randomUUID(), false, "user5", "email@email.com", Date(),null, null, null),
        User(UUID.randomUUID(), true, "admin1", "email@email.com", Date(),null, null, null),
    );

    class AdminUsersItemViewHolder(val binding: UserItemBinding): RecyclerView.ViewHolder(binding.root){}

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): AdminUsersAdapter.AdminUsersItemViewHolder {
        return AdminUsersAdapter.AdminUsersItemViewHolder(
            UserItemBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        );
    }

    override fun onBindViewHolder(holder: AdminUsersAdapter.AdminUsersItemViewHolder, position: Int) {
        var currUser = testUsersList[position]
        holder.binding.let { binding ->
            binding.tvUsername.text = currUser.name
        }
        holder.binding.ivCheckUserBtn.setOnClickListener { view ->
            val bundle = Bundle()
            bundle.putString("userID", currUser.uuid.toString())
            view.findNavController().navigate(R.id.action_nav_admin_users_to_nav_profile, bundle)
        }
    }

    override fun getItemCount(): Int {
        return testUsersList.size;
    }
}