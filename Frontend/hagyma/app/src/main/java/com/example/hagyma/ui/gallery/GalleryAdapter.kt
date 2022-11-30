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

    private val listItems: List<ListItem> = emptyList()/*listOf(
        ListItem("test 1", UUID.randomUUID(), UUID.randomUUID(), "picture1"),
        ListItem("test 2", UUID.randomUUID(), UUID.randomUUID(), "picture2"),
        ListItem("test 3", UUID.randomUUID(), UUID.randomUUID(), "picture3"),
        ListItem("test 4", UUID.randomUUID(), UUID.randomUUID(), "picture4"),
        ListItem("test 5", UUID.randomUUID(), UUID.randomUUID(), "picture5"),
        ListItem("test 6", UUID.randomUUID(), UUID.randomUUID(), "picture6"),
        ListItem("test 7", UUID.randomUUID(), UUID.randomUUID(), "picture7"),
    );*/

    class GalleryItemViewHolder(val binding: PictureItemBinding): RecyclerView.ViewHolder(binding.root){}

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): GalleryItemViewHolder {
        return GalleryItemViewHolder(
            PictureItemBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        );
    }

    override fun onBindViewHolder(holder: GalleryItemViewHolder, position: Int) {
        var currListItem = listItems[position]
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
        return listItems.size;
    }

    @SuppressLint("NotifyDataSetChanged")
    fun addFile(newItem: ListItem){
        listItems.plus(newItem)
        notifyDataSetChanged()
    }
}