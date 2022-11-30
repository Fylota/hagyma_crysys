package com.example.hagyma.ui.purchased_pictures

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

class PurchasedPicturesAdapter(private val context: Context?) :
    RecyclerView.Adapter<PurchasedPicturesAdapter.PurchasedPictureItemViewHolder>() {

    private val testListItems: List<ListItem> = listOf(
        ListItem("test 1", UUID.randomUUID().toString()/*, UUID.randomUUID(), "picture1"*/),
        ListItem("test 2", UUID.randomUUID().toString()/*, UUID.randomUUID(), "picture2"*/),
        ListItem("test 3", UUID.randomUUID().toString()/*, UUID.randomUUID(), "picture3"*/),
        ListItem("test 4", UUID.randomUUID().toString()/*, UUID.randomUUID(), "picture4"*/),
        ListItem("test 5", UUID.randomUUID().toString()/*, UUID.randomUUID(), "picture5"*/),
        ListItem("test 6", UUID.randomUUID().toString()/*, UUID.randomUUID(), "picture6"*/),
        ListItem("test 7", UUID.randomUUID().toString()/*, UUID.randomUUID(), "picture7"*/),
    );

    class PurchasedPictureItemViewHolder(val binding: PictureItemBinding): RecyclerView.ViewHolder(binding.root){}

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): PurchasedPictureItemViewHolder {
        return PurchasedPictureItemViewHolder(
            PictureItemBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        )
    }

    override fun onBindViewHolder(holder: PurchasedPictureItemViewHolder, position: Int) {
        var currListItem = testListItems[position]
        holder.binding.let { binding ->
            binding.tvPictureName.text = currListItem.name
        }
        holder.binding.ivCheckPictureBtn.setOnClickListener { view ->
            val bundle = Bundle()
            bundle.putString("one_purchased_picture_uuid", currListItem.uuid.toString())
            view.findNavController().navigate(R.id.action_nav_purchased_pictures_to_nav_one_purchased_picture, bundle)
        }
    }

    override fun getItemCount(): Int {
        return testListItems.size;
    }
}