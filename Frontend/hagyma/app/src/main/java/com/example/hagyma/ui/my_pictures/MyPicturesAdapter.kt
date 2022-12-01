package com.example.hagyma.ui.my_pictures

import android.annotation.SuppressLint
import android.content.Context
import android.graphics.BitmapFactory
import android.os.Bundle
import android.util.Base64
import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.navigation.findNavController
import androidx.recyclerview.widget.RecyclerView
import com.example.hagyma.R
import com.example.hagyma.data.ListItem
import com.example.hagyma.databinding.PictureItemBinding

class MyPicturesAdapter(private val context: Context?) :
    RecyclerView.Adapter<MyPicturesAdapter.MyPicturesItemViewHolder>() {

    private val listItems: MutableList<ListItem> = mutableListOf()/*listOf(
        ListItem("test 1", UUID.randomUUID().toString()/*, UUID.randomUUID(), "picture1"*/),
        ListItem("test 2", UUID.randomUUID().toString()/*, UUID.randomUUID(), "picture2"*/),
        ListItem("test 3", UUID.randomUUID().toString()/*, UUID.randomUUID(), "picture3"*/),
        ListItem("test 4", UUID.randomUUID().toString()/*, UUID.randomUUID(), "picture4"*/),
        ListItem("test 5", UUID.randomUUID().toString()/*, UUID.randomUUID(), "picture5"*/),
        ListItem("test 6", UUID.randomUUID().toString()/*, UUID.randomUUID(), "picture6"*/),
        ListItem("test 7", UUID.randomUUID().toString()/*, UUID.randomUUID(), "picture7"*/),
    );*/

    class MyPicturesItemViewHolder(val binding: PictureItemBinding): RecyclerView.ViewHolder(binding.root){}

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): MyPicturesItemViewHolder {
        return MyPicturesItemViewHolder(
            PictureItemBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        );
    }

    override fun onBindViewHolder(holder: MyPicturesItemViewHolder, position: Int) {
        val currListItem = listItems[position]
        holder.binding.let { binding ->
            binding.tvPictureName.text = currListItem.name

            val encodedString = Base64.decode(currListItem.picture, Base64.DEFAULT)
            val bitmap = BitmapFactory.decodeByteArray(encodedString,0,encodedString.size)
            binding.ivPicture.setImageBitmap(bitmap)
        }
        holder.binding.ivCheckPictureBtn.setOnClickListener { view ->
            val bundle = Bundle()
            bundle.putString("my_searched_picture_uuid", currListItem.uuid)
            view.findNavController().navigate(R.id.action_nav_my_pictures_to_nav_my_one_picture, bundle)
        }
    }

    override fun getItemCount(): Int {
        return listItems.size;
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
}