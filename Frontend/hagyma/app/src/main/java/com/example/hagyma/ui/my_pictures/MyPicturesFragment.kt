package com.example.hagyma.ui.my_pictures

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.lifecycle.lifecycleScope
import androidx.navigation.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.hagyma.R
import com.example.hagyma.data.ListItem
import com.example.hagyma.databinding.FragmentMyPicturesBinding
import com.example.hagyma.helper.ApiHelper
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import kotlinx.coroutines.withContext

class MyPicturesFragment : Fragment() {

    private var _binding: FragmentMyPicturesBinding? = null

    private val binding get() = _binding!!
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
    private lateinit var myPicturesAdapter: MyPicturesAdapter

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        _binding = FragmentMyPicturesBinding.inflate(inflater, container, false)
        val root: View = binding.root

        binding.fltBtnUploadPicture.setOnClickListener {
            val bundle = Bundle()
            bundle.putString("original_page", "my_pictures")
            root.findNavController().navigate(R.id.action_nav_my_pictures_to_nav_upload_caff, bundle)
        }

        myPicturesAdapter = MyPicturesAdapter(this.context)
        binding.rvPictures.adapter = myPicturesAdapter
        binding.rvPictures.layoutManager = LinearLayoutManager(this.context)

        lifecycleScope.launch {
            // Get My CAFF Files
            initMyCAFFFiles()
        }

        return root
    }

    private suspend fun initMyCAFFFiles(){
        val caffApi = ApiHelper.getCaffApi()
        try {
            withContext(ioDispatcher) {
                val pictures = caffApi.apiCaffUploadedImagesGet()
                println("PICTURES SIZE: ${pictures.size}")
                pictures.forEach { item ->
                    println("PICTURE: $item")
                    activity?.runOnUiThread {
                        myPicturesAdapter.addFile(ListItem(item.title, item.id,item.preview))
                    }
                }
            }
        } catch (e:Exception){
            println(e)
        }
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}