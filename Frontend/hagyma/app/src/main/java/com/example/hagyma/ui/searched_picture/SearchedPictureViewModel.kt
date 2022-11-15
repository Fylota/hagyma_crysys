package com.example.hagyma.ui.searched_picture

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel

class SearchedPictureViewModel : ViewModel()  {

    private val _text = MutableLiveData<String>().apply {
        value = "This is searched picture Fragment"
    }
    val text: LiveData<String> = _text
}