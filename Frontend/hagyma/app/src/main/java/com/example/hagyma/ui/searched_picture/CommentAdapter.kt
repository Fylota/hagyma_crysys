package com.example.hagyma.ui.searched_picture

import android.annotation.SuppressLint
import android.content.Context
import android.os.Build
import android.util.Log
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.core.view.isVisible
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.RecyclerView
import com.auth0.android.jwt.JWT
import com.example.hagyma.R
import com.example.hagyma.data.Comment
import com.example.hagyma.databinding.CommentItemBinding
import com.example.hagyma.infrastructure.ApiClient
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.util.*

class CommentAdapter(private val context: Context?) :
    RecyclerView.Adapter<CommentAdapter.SearchedPictureItemViewHolder>() {

    private val commentsList: MutableList<Comment> = if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
        mutableListOf()
    /*listOf(
            Comment(UUID.randomUUID().toString(), UUID.randomUUID(),  LocalDate.now().toString(), UUID.randomUUID(),"comment1\nline2\nline3"),
            Comment(UUID.randomUUID(), UUID.randomUUID(),  LocalDate.now().toString(), UUID.randomUUID(),"comment2"),
            Comment(UUID.randomUUID(), UUID.randomUUID(),  LocalDate.now().toString(), UUID.randomUUID(),"comment3"),
            Comment(UUID.randomUUID(), UUID.randomUUID(),  LocalDate.now().toString(), UUID.randomUUID(),"comment4"),
            Comment(UUID.randomUUID(), UUID.randomUUID(),  LocalDate.now().toString(), UUID.randomUUID(),"comment5"),
            Comment(UUID.randomUUID(), UUID.randomUUID(),  LocalDate.now().toString(), UUID.randomUUID(),"comment6"),
        )*/
    } else {
        TODO("VERSION.SDK_INT < O")
    }

    // We need this boolean to hide and disable delete button when the logged in profile is not admin.
    val isAdmin: Boolean = try {
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

    override fun onBindViewHolder(holder: SearchedPictureItemViewHolder, position: Int) {
        val currListItem = commentsList[position]
        holder.binding.tvCommentOwner.text = currListItem.creator
        holder.binding.tvCommentTime.text = currListItem.creationTime.toString()
        //        holder.binding.tvCommentTime.text = currListItem.creationTime.toString().format(DateTimeFormatter.ofPattern("dd-MM-yyyy HH:mm"))
//        holder.binding.tvCommentTime.text = LocalDate.parse(currListItem.creationTime.toString(), DateTimeFormatter.ofPattern("dd-MM-yyyy HH:mm")).toString()
        holder.binding.tvCommentText.text = currListItem.content

        holder.binding.btnDeleteComment.isEnabled = isAdmin
        holder.binding.btnDeleteComment.isVisible = isAdmin
        holder.binding.btnDeleteComment.setOnClickListener {
            // TODO Comment torlese!!
                // TODO: 1. DB-bol - valahogy meghivni a SearchedPictureViewModel.deleteComment(commentId: String) FV-t!!!
                // TODO: 2. Innen a listabol -> deleteComment(currListItem)
        }
//        if(isAdmin){
//
//        }
    }

    override fun getItemCount(): Int {
        return commentsList.size
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
}