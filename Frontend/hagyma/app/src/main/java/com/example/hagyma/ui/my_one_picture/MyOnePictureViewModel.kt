package com.example.hagyma.ui.my_one_picture

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel

class MyOnePictureViewModel : ViewModel()  {

    private val _text = MutableLiveData<String>().apply {
        value = "This is my one picture Fragment"
    }
    val text: LiveData<String> = _text
}