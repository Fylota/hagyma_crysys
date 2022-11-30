package com.example.hagyma.ui.gallery

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.map
import com.auth0.android.jwt.JWT
import com.example.hagyma.api.CaffApi
import com.example.hagyma.infrastructure.ApiClient
import java.text.SimpleDateFormat
import java.util.*

class GalleryViewModel : ViewModel() {

    private val _text = MutableLiveData<String>().apply {
        value = "This is gallery Fragment"
    }
    val text: LiveData<String> = _text
}