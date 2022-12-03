package com.example.hagyma.ui.gallery

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.lifecycleScope
import androidx.navigation.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.hagyma.R
import com.example.hagyma.data.ListItem
import com.example.hagyma.databinding.FragmentGalleryBinding
import com.example.hagyma.helper.ApiHelper
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.util.*

class GalleryFragment : Fragment() {

    private var _binding: FragmentGalleryBinding? = null

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = _binding!!

    private lateinit var galleryAdapter: GalleryAdapter

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val galleryViewModel =
            ViewModelProvider(this)[GalleryViewModel::class.java]

        _binding = FragmentGalleryBinding.inflate(inflater, container, false)
        val root: View = binding.root

        binding.fltBtnUploadPicture.setOnClickListener {
            val bundle = Bundle()
            bundle.putString("original_page", "gallery")
            root.findNavController().navigate(R.id.action_nav_gallery_to_nav_upload_caff, bundle)
        }

        galleryAdapter = GalleryAdapter(this.context)
        binding.rvPictures.adapter = galleryAdapter
        binding.rvPictures.layoutManager = LinearLayoutManager(this.context)

        binding.searchButton.setOnClickListener {
            searchRefreshList(binding.etSearchingText.text.toString())
        }

        lifecycleScope.launch(Dispatchers.IO) {
            // Get CAFF Files
            initCAFFFiles()
        }

        return root
    }

    private fun searchRefreshList(keyString: String){
        if (keyString != ""){
            val actualItems = galleryAdapter.getActualPictures()
            galleryAdapter.getOriginalPictures().forEach { originalPicture ->
                if (originalPicture.name.lowercase(Locale.ROOT).contains(keyString.lowercase(Locale.ROOT))) {
                    if(!actualItems.contains(originalPicture)){
                        galleryAdapter.addFile(originalPicture)
                    }
                } else {
                    if(actualItems.contains(originalPicture)){
                        galleryAdapter.deleteFile(originalPicture)
                    }
                }
            }
        }
        binding.etSearchingText.text.clear()
    }

    private suspend fun initCAFFFiles(){
        val caffApi = ApiHelper.getCaffApi()
        try {
            val pictures = caffApi.apiCaffListImagesGet()
            pictures.forEach { item -> activity?.runOnUiThread {
                galleryAdapter.addInitFile(ListItem(item.title, item.id,item.preview))
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