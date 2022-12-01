package com.example.hagyma.ui.searched_picture

import android.annotation.SuppressLint
import android.content.Context
import android.os.Build
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.example.hagyma.data.Comment
import com.example.hagyma.databinding.CommentItemBinding
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
        holder.binding.tvCommentText.text = currListItem.content
    }

    override fun getItemCount(): Int {
        return commentsList.size
    }

    @SuppressLint("NotifyDataSetChanged")
    fun addComment(newComment: Comment){
        commentsList.add(newComment)
        notifyDataSetChanged()
    }
    fun clear(){
        commentsList.clear()
    }
}