package com.example.hagyma.ui.edit_my_picture

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel

class EditMyPictureViewModel : ViewModel() {

    private val _text = MutableLiveData<String>().apply {
        value = "This is edit my picture Fragment"
    }
    val text: LiveData<String> = _text
}