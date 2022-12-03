package com.example.hagyma.ui.edit_my_picture

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.navigation.findNavController
import com.example.hagyma.R
import com.example.hagyma.databinding.FragmentEditMyPictureBinding

class EditMyPictureFragment : Fragment() {

    private var _binding: FragmentEditMyPictureBinding? = null

    private val binding get() = _binding!!

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val editMyPictureViewModel =
            ViewModelProvider(this)[EditMyPictureViewModel::class.java]

        val editingPictureUUID = this.arguments?.getString("my_editing_picture_uuid")

        _binding = FragmentEditMyPictureBinding.inflate(inflater, container, false)
        val root: View = binding.root

        binding.cancelButton.setOnClickListener { view ->
            val toast = Toast.makeText(context, "Editing cancelled", Toast.LENGTH_SHORT)
            toast.show()

            val bundle = Bundle()
            bundle.putString("my_searched_picture_uuid", editingPictureUUID)
            view.findNavController().navigate(R.id.action_nav_edit_my_picture_to_nav_my_one_picture, bundle)
        }

        binding.textPictureName.text = editingPictureUUID // TODO caff nev db-bol

        binding.saveButton.setOnClickListener { view ->
            val toast = Toast.makeText(context, "TODO SAVE", Toast.LENGTH_SHORT)
            toast.show()

            val bundle = Bundle()
            bundle.putString("my_searched_picture_uuid", editingPictureUUID) // TODO modositas utani nevet atadni
            view.findNavController().navigate(R.id.action_nav_edit_my_picture_to_nav_my_one_picture, bundle)
        }

        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}