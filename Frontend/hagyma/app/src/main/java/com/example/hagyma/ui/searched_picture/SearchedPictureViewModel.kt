package com.example.hagyma.ui.searched_picture

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.map
import com.auth0.android.jwt.JWT
import com.example.hagyma.infrastructure.ApiClient
import java.text.SimpleDateFormat
import java.util.*

class SearchedPictureViewModel : ViewModel()  {

//    private val _text = MutableLiveData<String>().apply {
//        value = "This is searched picture Fragment"
//    }
//    val text: LiveData<String> = _text

    private val _jwt = MutableLiveData<JWT>().apply {
        value = ApiClient.accessToken?.let { JWT(it) }
    }

    val userId: LiveData<String> = _jwt.map { data -> data.getClaim("UserId")
        .asString()
        .toString()}

    val userName: LiveData<String> = _jwt.map { data ->  data.getClaim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
        .asString()
        .toString()}
}