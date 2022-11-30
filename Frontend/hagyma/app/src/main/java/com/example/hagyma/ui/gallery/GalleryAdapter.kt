package com.example.hagyma.ui.gallery

import android.annotation.SuppressLint
import android.content.Context
import android.os.Bundle
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.navigation.findNavController
import androidx.recyclerview.widget.RecyclerView
import com.example.hagyma.R
import com.example.hagyma.data.ListItem
import com.example.hagyma.databinding.PictureItemBinding
import java.util.*

class GalleryAdapter(private val context: Context?) :
    RecyclerView.Adapter<GalleryAdapter.GalleryItemViewHolder>() {

    private val originalPictures: MutableList<ListItem> = mutableListOf()
    private var listItems: MutableList<ListItem> = mutableListOf()

    class GalleryItemViewHolder(val binding: PictureItemBinding): RecyclerView.ViewHolder(binding.root){}

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): GalleryItemViewHolder {
        return GalleryItemViewHolder(
            PictureItemBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        );
    }

    override fun onBindViewHolder(holder: GalleryItemViewHolder, position: Int) {
        val currListItem = listItems[position]
        holder.binding.let { binding ->
            binding.tvPictureName.text = currListItem.name
        }
        holder.binding.ivCheckPictureBtn.setOnClickListener { view ->
            val bundle = Bundle()
            bundle.putString("searched_picture_uuid", currListItem.uuid)
            view.findNavController().navigate(R.id.action_nav_gallery_to_nav_searched_picture, bundle)
        }
    }

    override fun getItemCount(): Int {
        return listItems.size
    }

    @SuppressLint("NotifyDataSetChanged")
    fun addInitFile(newItem: ListItem){
        listItems.add(newItem)
        originalPictures.add(newItem)
        notifyDataSetChanged()
    }

    @SuppressLint("NotifyDataSetChanged")
    fun addFile(newItem: ListItem){
        listItems.add(newItem)
        notifyDataSetChanged()
    }

    @SuppressLint("NotifyDataSetChanged")
    fun deleteFile(file: ListItem){
        listItems.remove(file)
        notifyDataSetChanged()
    }

    fun getOriginalPictures(): MutableList<ListItem>{
        return originalPictures
    }

    fun getActualPictures(): MutableList<ListItem>{
        return listItems
    }
}