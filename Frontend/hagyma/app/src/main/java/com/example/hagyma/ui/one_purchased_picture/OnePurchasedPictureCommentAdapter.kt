package com.example.hagyma.ui.one_purchased_picture

import android.annotation.SuppressLint
import android.content.Context
import android.os.Build
import android.util.Log
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.annotation.RequiresApi
import androidx.core.view.isVisible
import androidx.recyclerview.widget.RecyclerView
import com.auth0.android.jwt.JWT
import com.example.hagyma.data.Comment
import com.example.hagyma.databinding.CommentItemBinding
import com.example.hagyma.infrastructure.ApiClient
import kotlinx.coroutines.CoroutineScope
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.time.format.DateTimeFormatter

class OnePurchasedPictureCommentAdapter(private val context: Context?, private val onePurchasedPictureViewModel: OnePurchasedPictureViewModel?) :
    RecyclerView.Adapter<OnePurchasedPictureCommentAdapter.SearchedPictureItemViewHolder>() {

    private val commentsList: MutableList<Comment> = if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
        mutableListOf()
    } else {
        TODO("VERSION.SDK_INT < O")
    }

    // We need this boolean to hide and disable delete button when the logged in profile is not admin.
    private val isAdmin: Boolean = try {
        val jwt = ApiClient.accessToken?.let { JWT(it) }
        val role = jwt?.getClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
            ?.asString()

        role == "Admin"
    } catch (e: Exception) {
        e.message?.let { it1 -> Log.e("", it1) }
        false
    }

    class SearchedPictureItemViewHolder(val binding: CommentItemBinding): RecyclerView.ViewHolder(binding.root)

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): SearchedPictureItemViewHolder {
        return SearchedPictureItemViewHolder(
            CommentItemBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        )
    }

    @RequiresApi(Build.VERSION_CODES.O)
    override fun onBindViewHolder(holder: SearchedPictureItemViewHolder, position: Int) {
        val currListItem = commentsList[position]
        holder.binding.tvCommentText.text = currListItem.content
        holder.binding.tvCommentOwner.text = currListItem.creator

        val formatter = DateTimeFormatter.ofPattern("yyyy.MM.dd-HH:mm")
        val formatted = currListItem.creationTime!!.format(formatter)

        holder.binding.tvCommentTime.text = formatted


        holder.binding.btnDeleteComment.isEnabled = isAdmin
        holder.binding.btnDeleteComment.isVisible = isAdmin

        holder.binding.btnDeleteComment.setOnClickListener {
            CoroutineScope(Dispatchers.IO).launch {
                onePurchasedPictureViewModel?.deleteComment(currListItem.uuid)
            }
            deleteComment(currListItem)
        }
    }

    @SuppressLint("NotifyDataSetChanged")
    fun addComment(newComment: Comment){
        commentsList.add(newComment)
        notifyDataSetChanged()
    }

    @SuppressLint("NotifyDataSetChanged")
    fun deleteComment(newComment: Comment){
        commentsList.remove(newComment)
        notifyDataSetChanged()
    }

    fun clear(){
        commentsList.clear()
    }

    override fun getItemCount(): Int {
        return commentsList.size
    }
}