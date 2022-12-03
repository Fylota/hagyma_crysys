package com.example.hagyma.ui.purchased_pictures

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

class PurchasedPicturesAdapter(private val context: Context?) :
    RecyclerView.Adapter<PurchasedPicturesAdapter.PurchasedPictureItemViewHolder>() {

    private val listItems: MutableList<ListItem> = mutableListOf()

    class PurchasedPictureItemViewHolder(val binding: PictureItemBinding): RecyclerView.ViewHolder(binding.root)

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): PurchasedPictureItemViewHolder {
        return PurchasedPictureItemViewHolder(
            PictureItemBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        )
    }

    override fun onBindViewHolder(holder: PurchasedPictureItemViewHolder, position: Int) {
        val currListItem = listItems[position]
        holder.binding.let { binding ->
            binding.tvPictureName.text = currListItem.name

            val encodedString = Base64.decode(currListItem.picture,Base64.DEFAULT)
            val bitmap = BitmapFactory.decodeByteArray(encodedString,0,encodedString.size)
            binding.ivPicture.setImageBitmap(bitmap)
        }
        holder.binding.ivCheckPictureBtn.setOnClickListener { view ->
            val bundle = Bundle()
            bundle.putString("one_purchased_picture_uuid", currListItem.uuid)
            view.findNavController().navigate(R.id.action_nav_purchased_pictures_to_nav_one_purchased_picture, bundle)
        }
    }

    @SuppressLint("NotifyDataSetChanged")
    fun addFile(newItem: ListItem){
        listItems.add(newItem)
        notifyDataSetChanged()
    }

    override fun getItemCount(): Int {
        return listItems.size
    }
}