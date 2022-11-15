package com.example.hagyma.ui.one_purchased_picture

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel

class OnePurchasedPictureViewModel : ViewModel() {

    private val _text = MutableLiveData<String>().apply {
        value = "This is one purchased picture Fragment"
    }
    val text: LiveData<String> = _text
}