package com.example.hagyma.ui.upload_caff

import android.Manifest
import android.app.Activity
import android.content.ContentResolver
import android.content.Intent
import android.content.pm.PackageManager
import android.net.Uri
import android.os.Bundle
import android.os.Handler
import android.os.Looper
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.EditText
import android.widget.TextView
import android.widget.Toast
import androidx.activity.result.contract.ActivityResultContracts
import androidx.core.app.ActivityCompat
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.lifecycle.lifecycleScope
import androidx.navigation.findNavController
import com.example.hagyma.R
import com.example.hagyma.api.CaffApi
import com.example.hagyma.databinding.FragmentUploadCaffBinding
import com.example.hagyma.helper.ApiHelper
import com.example.hagyma.extensions.validateNonEmpty
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.io.File


class UploadCaffFragment : Fragment() {

    private var _binding: FragmentUploadCaffBinding? = null

    // This property is only valid between onCreateView and
    // onDestroyView.
    private val binding get() = _binding!!

    private lateinit var caffApi : CaffApi
    private lateinit var tvAttachedFile : TextView
    private var caffFile: File? = null

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val uploadCaffViewModel =
            ViewModelProvider(this)[UploadCaffViewModel::class.java]

        val originalPageName = this.arguments?.getString("original_page")

        caffApi = ApiHelper.getCaffApi()
        _binding = FragmentUploadCaffBinding.inflate(inflater, container, false)
        val root: View = binding.root

        val etTitle = binding.etCaffName
        val etDescription = binding.etCaffDescription
        tvAttachedFile = binding.tvAttachedFile

        binding.attachBtn.setOnClickListener {
            openFileSelectorActivityForResult()
        }

        binding.uploadBtn.setOnClickListener {
            if (!validForm(etTitle, etDescription, caffFile)) {
                Toast.makeText(context, "Please fill every field.", Toast.LENGTH_SHORT).show()
            }

            else {
                Toast.makeText(context, "Uploading file...", Toast.LENGTH_SHORT).show()
                val handler = Handler(Looper.getMainLooper()!!)
                lifecycleScope.launch(Dispatchers.IO) {
                    try {
                        caffFile?.let { it1 ->
                            caffApi.apiCaffUploadImagePost(
                                it1,
                                etDescription.text.toString(),
                                etTitle.text.toString()
                            )
                        }
                        handler.post {
                            Toast.makeText(context, "file uploaded", Toast.LENGTH_SHORT).show()
                            if(originalPageName == "gallery"){
                                root.findNavController().navigate(R.id.action_nav_upload_caff_to_nav_gallery)
                            } else {
                                root.findNavController().navigate(R.id.action_nav_upload_caff_to_nav_my_pictures)
                            }
                        }
                    } catch (e: Exception) {
                        e.message?.let { it1 -> Log.e(tag, it1) }
                    }
                }
            }
        }

        return root
    }

    private fun validForm(title: EditText, description: EditText, caff: File?): Boolean {
        return title.validateNonEmpty() &&
                description.validateNonEmpty() &&
                caff != null
    }

    private fun openFileSelectorActivityForResult() {
        verifyStoragePermissions(this.activity)
        val intent = Intent(Intent.ACTION_GET_CONTENT).apply {
            addCategory(Intent.CATEGORY_OPENABLE)
            type = "*/*"
        }
        resultLauncher.launch(intent)
    }

    private var resultLauncher = registerForActivityResult(ActivityResultContracts.StartActivityForResult()) { result ->
        if (result.resultCode == Activity.RESULT_OK) {
            val uri: Uri? = result.data?.data
            caffFile = getFile(requireContext().contentResolver, uri, requireContext().cacheDir)
        }
    }

    private fun getFile(contentResolver: ContentResolver, uri: Uri?, directory: File): File? {
        val file =
            File.createTempFile("caff", ".caff", directory)
        file.outputStream().use {
            if (uri != null) {
                contentResolver.openInputStream(uri)?.copyTo(it)
            }
        }
        Toast.makeText(context, "retrieved data, ${file.path}", Toast.LENGTH_SHORT).show()
        tvAttachedFile.text = file.path.substring(file.path.lastIndexOf("/")+1)
        return file
    }

    private fun verifyStoragePermissions(activity: Activity?) {
        // Check if we have write permission
        val permission = ActivityCompat.checkSelfPermission(
            requireActivity(),
            Manifest.permission.READ_EXTERNAL_STORAGE
        )
        if (permission != PackageManager.PERMISSION_GRANTED) {
            // We don't have permission so prompt the user
            if (activity != null) {
                ActivityCompat.requestPermissions(
                    activity,
                    arrayOf(Manifest.permission.READ_EXTERNAL_STORAGE),1)
            }
        }
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}