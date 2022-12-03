package com.example.hagyma.ui.my_one_picture

import android.util.Log
import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel
import androidx.lifecycle.map
import com.auth0.android.jwt.JWT
import com.example.hagyma.api.model.CaffDetails
import com.example.hagyma.api.model.CommentRequest
import com.example.hagyma.helper.ApiHelper
import com.example.hagyma.infrastructure.ApiClient
import java.io.File

class MyOnePictureViewModel : ViewModel() {

    private val _caff = MutableLiveData<CaffDetails>().apply {
        value = CaffDetails("","","",null,null)
    }
    val caff: LiveData<CaffDetails> = _caff
    private var _caffFile = MutableLiveData<File>().apply {
        value = File("example.caff")
    }
    val caffFile: LiveData<File> = _caffFile
    private val caffApi = ApiHelper.getCaffApi()
    private val userApi = ApiHelper.getUserApi()

    suspend fun getCAFF(uuid:String) {
        runCatching {
            _caff.value = caffApi.apiCaffGetImageGet(uuid)
        }.onFailure { error: Throwable ->
            error.message?.let { it1 -> Log.e("", it1) }
        }
    }

    suspend fun getDownloadCAFF(uuid:String) {
        runCatching {
            _caffFile.value = caffApi.apiCaffDownloadImageGet(uuid)
        }.onFailure { error: Throwable ->
            error.message?.let { it1 -> Log.e("", it1) }
        }
    }

    suspend fun getUserName(): String {
        return userApi.apiUserGetUserGet().name
    }

    suspend fun saveComment(uuid: String, newCommentText: String) {
        runCatching {
        caffApi.apiCaffAddCommentPost(uuid, CommentRequest(newCommentText))
        }.onFailure { error: Throwable ->
            error.message?.let { it1 -> Log.e("", it1) }
        }
    }

    suspend fun deleteComment(commentId: String) {
        runCatching {
            caffApi.apiCaffDeleteCommentDelete(commentId)
        }.onFailure { error: Throwable ->
            error.message?.let { it1 -> Log.e("", it1) }
        }
    }

    suspend fun deletePicture(imageId: String) {
        runCatching {
            caffApi.apiCaffDeleteImageDelete(imageId)
        }.onFailure { error: Throwable ->
            error.message?.let { it1 -> Log.e("", it1) }
        }
    }


    private val _jwt = MutableLiveData<JWT>().apply {
        value = ApiClient.accessToken?.let { JWT(it) }
    }

    val userName: LiveData<String> = _jwt.map { data ->  data.getClaim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
        .asString()
        .toString()}
}