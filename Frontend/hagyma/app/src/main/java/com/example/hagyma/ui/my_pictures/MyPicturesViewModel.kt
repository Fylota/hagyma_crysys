package com.example.hagyma.ui.my_pictures

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel

class MyPicturesViewModel : ViewModel() {

    private val _text = MutableLiveData<String>().apply {
        value = "This is my pictures Fragment"
    }
    val text: LiveData<String> = _text
}