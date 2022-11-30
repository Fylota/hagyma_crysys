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
    private var listItems: MutableList<ListItem> = mutableListOf()/*listOf(
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
        return listItems.size
    }

//    fun getOriginalCount(): Int {
//        return originalPictures.size
//    }

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

//    fun clearList() {
//        listItems = mutableListOf()
//    }

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

//    @SuppressLint("NotifyDataSetChanged")
//    fun searchRefreshList(keyString: String){
//        listItems = allPictures
//        listItems.forEach { item ->
//            if (!item.name.contains(keyString)) {
//                listItems.remove(item)
//                notifyDataSetChanged()
//            }
//        }
//    }
}