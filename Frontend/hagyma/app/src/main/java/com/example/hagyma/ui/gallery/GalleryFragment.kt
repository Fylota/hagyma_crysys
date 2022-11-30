package com.example.hagyma.ui.gallery

import android.os.Bundle
import android.os.Handler
import android.os.Looper
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.lifecycleScope
import androidx.navigation.findNavController
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.hagyma.R
import com.example.hagyma.api.AuthenticationApi
import com.example.hagyma.api.CaffApi
import com.example.hagyma.api.UserApi
import com.example.hagyma.api.model.LoginRequest
import com.example.hagyma.data.ListItem
import com.example.hagyma.databinding.FragmentGalleryBinding
import com.example.hagyma.helper.ApiHelper
import com.example.hagyma.infrastructure.ApiClient
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.coroutineScope
import kotlinx.coroutines.launch

class GalleryFragment : Fragment() {

    private var _binding: FragmentGalleryBinding? = null

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = _binding!!

    private lateinit var galleryAdapter: GalleryAdapter

    private lateinit var authenticationApi: AuthenticationApi
    private lateinit var userApi : UserApi

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val galleryViewModel =
            ViewModelProvider(this).get(GalleryViewModel::class.java)

        authenticationApi = ApiHelper.getAuthenticationApi()
        userApi = ApiHelper.getUserApi()

        _binding = FragmentGalleryBinding.inflate(inflater, container, false)
        val root: View = binding.root

        binding.searchButton.setOnClickListener {
            val handler = Handler(Looper.getMainLooper()!!)
            lifecycleScope.launch(Dispatchers.IO) {
                try {
                    var result = authenticationApi.authLoginPost(LoginRequest("admin@admin.com","Admin1!"))
                    handler.post {
                        Toast.makeText(context,result,Toast.LENGTH_LONG).show()
                    }
                    ApiClient.accessToken = result;
                    try {
                        var userinfowithAccessToken = userApi.apiUserGetUserGet();
                        handler.post{
                            Toast.makeText(context,"Try with accesstoken: ${userinfowithAccessToken.email}",Toast.LENGTH_SHORT).show()
                        }
                    } catch (e: Exception){
                        handler.post{
                            Toast.makeText(context,"Try with accesstoken: ${e.message}",Toast.LENGTH_SHORT).show();
                        }
                    }
                } catch (e: Exception){
                    e.message?.let { it1 -> Log.e(tag, it1) }
                }

            }

        }
        binding.fltBtnUploadPicture.setOnClickListener {
            val bundle = Bundle()
            bundle.putString("original_page", "gallery");
            root.findNavController().navigate(R.id.action_nav_gallery_to_nav_upload_caff, bundle)
        }

        galleryAdapter = GalleryAdapter(this.context)
        binding.rvPictures.adapter = galleryAdapter
        binding.rvPictures.layoutManager = LinearLayoutManager(this.context)


        viewLifecycleOwner.lifecycleScope.launch {
            // Get CAFF Files
            initCAFFFiles()
        }

        return root
    }

    suspend fun initCAFFFiles(){
        val caffApi = ApiHelper.getCaffApi()
        try {
            caffApi.apiCaffListImagesGet().forEach { item ->
                println("picture name: " + item.title)
                galleryAdapter.addFile(ListItem(item.title, item.id))
            }
        }catch (e:Exception){
            System.out.println(e)
        }

    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}