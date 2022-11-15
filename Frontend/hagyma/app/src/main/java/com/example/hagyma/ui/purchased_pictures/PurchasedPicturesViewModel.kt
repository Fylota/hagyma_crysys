package com.example.hagyma.ui.purchased_pictures

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel

class PurchasedPicturesViewModel : ViewModel() {

    private val _text = MutableLiveData<String>().apply {
        value = "This is purchased pictures Fragment"
    }
    val text: LiveData<String> = _text
}