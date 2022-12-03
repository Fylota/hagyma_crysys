package com.example.hagyma.ui.purchased_pictures

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.hagyma.data.ListItem
import com.example.hagyma.databinding.FragmentPurchasedPicturesBinding
import com.example.hagyma.helper.ApiHelper
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class PurchasedPicturesFragment : Fragment() {

    private var _binding: FragmentPurchasedPicturesBinding? = null

    private val binding get() = _binding!!

    private lateinit var purchasedPicturesAdapter: PurchasedPicturesAdapter

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = FragmentPurchasedPicturesBinding.inflate(inflater, container, false)
        val root: View = binding.root

        purchasedPicturesAdapter = PurchasedPicturesAdapter(this.context)
        binding.rvPictures.adapter = purchasedPicturesAdapter
        binding.rvPictures.layoutManager = LinearLayoutManager(this.context)

        lifecycleScope.launch {
            // Get Purchased CAFF Files
            initPurchasedCAFFFiles()
        }

        return root
    }

    private suspend fun initPurchasedCAFFFiles(){
        val caffApi = ApiHelper.getCaffApi()
        try {
            withContext(Dispatchers.IO) {
                val pictures = caffApi.apiCaffPurchasedImagesGet()
                pictures.forEach { item -> activity?.runOnUiThread {
                    purchasedPicturesAdapter.addFile(ListItem(item.title, item.id,item.preview)) }
                }
            }
        }catch (e:Exception){
            println(e)
        }
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}