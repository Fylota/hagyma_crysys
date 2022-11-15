package com.example.hagyma.ui.purchased_pictures

import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.fragment.app.Fragment
import androidx.lifecycle.ViewModelProvider
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.hagyma.databinding.FragmentPurchasedPicturesBinding

class PurchasedPicturesFragment : Fragment() {

    private var _binding: FragmentPurchasedPicturesBinding? = null

    private val binding get() = _binding!!

    private lateinit var purchasedPicturesAdapter: PurchasedPicturesAdapter

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        val purchasedPicturesViewModel =
            ViewModelProvider(this).get(PurchasedPicturesViewModel::class.java)

        _binding = FragmentPurchasedPicturesBinding.inflate(inflater, container, false)
        val root: View = binding.root

//        val textView: TextView = binding.textPurchasedPictures
//        purchasedPicturesViewModel.text.observe(viewLifecycleOwner) {
//            textView.text = it
//        }

        purchasedPicturesAdapter = PurchasedPicturesAdapter(this.context)
        binding.rvPictures.adapter = purchasedPicturesAdapter
        binding.rvPictures.layoutManager = LinearLayoutManager(this.context)

        return root
    }

    override fun onDestroyView() {
        super.onDestroyView()
        _binding = null
    }
}