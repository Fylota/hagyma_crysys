package com.example.hagyma.ui.one_purchased_picture

import android.content.Context
import android.os.Build
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.RecyclerView
import com.example.hagyma.data.Comment
import com.example.hagyma.databinding.CommentItemBinding
import java.time.LocalDate
import java.util.*

class OnePurchasedPictureCommentAdapter(private val context: Context?) :
    RecyclerView.Adapter<OnePurchasedPictureCommentAdapter.SearchedPictureItemViewHolder>() {

    private val testListComments: MutableList<Comment> = if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
    mutableListOf()
    /*listOf(
            Comment(UUID.randomUUID(), UUID.randomUUID(),  LocalDate.now().toString(), UUID.randomUUID(),"comment1"),
            Comment(UUID.randomUUID(), UUID.randomUUID(),  LocalDate.now().toString(), UUID.randomUUID(),"comment2"),
            Comment(UUID.randomUUID(), UUID.randomUUID(),  LocalDate.now().toString(), UUID.randomUUID(),"comment3"),
            Comment(UUID.randomUUID(), UUID.randomUUID(),  LocalDate.now().toString(), UUID.randomUUID(),"comment4"),
            Comment(UUID.randomUUID(), UUID.randomUUID(),  LocalDate.now().toString(), UUID.randomUUID(),"comment5"),
            Comment(UUID.randomUUID(), UUID.randomUUID(),  LocalDate.now().toString(), UUID.randomUUID(),"comment6"),
        )*/
    } else {
        TODO("VERSION.SDK_INT < O")
    };

    class SearchedPictureItemViewHolder(val binding: CommentItemBinding): RecyclerView.ViewHolder(binding.root){}

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): SearchedPictureItemViewHolder {
        return SearchedPictureItemViewHolder(
            CommentItemBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        );
    }

    override fun onBindViewHolder(holder: SearchedPictureItemViewHolder, position: Int) {
        val currListItem = testListComments[position]
        holder.binding.tvCommentText.text = currListItem.content
        holder.binding.tvCommentTime.text = currListItem.creationTime.toString()
    }

    override fun getItemCount(): Int {
        return testListComments.size;
    }
}