package com.example.hagyma.ui.upload_caff

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel

class UploadCaffViewModel : ViewModel() {

    private val _text = MutableLiveData<String>().apply {
        value = "This is upload caff Fragment"
    }
    val text: LiveData<String> = _text
}