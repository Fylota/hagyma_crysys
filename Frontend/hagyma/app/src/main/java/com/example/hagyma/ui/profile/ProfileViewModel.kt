package com.example.hagyma.ui.profile

import androidx.lifecycle.*
import com.auth0.android.jwt.JWT
import com.example.hagyma.infrastructure.ApiClient
import java.text.SimpleDateFormat
import java.util.*


class ProfileViewModel : ViewModel() {

    private val _jwt = MutableLiveData<JWT>().apply {
        value = ApiClient.accessToken?.let { JWT(it) }
    }

    val userId: LiveData<String> = _jwt.map { data -> data.getClaim("UserId")
        .asString()
        .toString()}

    val userName: LiveData<String> = _jwt.map { data ->  data.getClaim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
        .asString()
        .toString()}

    val email: LiveData<String> = _jwt.map { data -> data.getClaim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")
        .asString()
        .toString()}

    val isAdmin: LiveData<String> = _jwt.map { data -> data.getClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
        .asString()
        .toString()}

    var sdfDate: SimpleDateFormat = SimpleDateFormat(
        "dd-MM-yyyy",
        Locale.getDefault()
    )
    // todo change claim to regDate
    val regDate: LiveData<String> = _jwt.map { data ->
        sdfDate.format(
            Date(((data.getClaim("iat").asString())?.toLong() ?: 1) * 1000))
        }
}