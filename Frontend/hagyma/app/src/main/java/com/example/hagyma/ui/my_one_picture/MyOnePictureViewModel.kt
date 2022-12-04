package com.example.hagyma.ui.my_one_picture

import androidx.lifecycle.*
import com.auth0.android.jwt.JWT
import com.example.hagyma.api.model.CaffDetails
import com.example.hagyma.api.model.CommentRequest
import com.example.hagyma.helper.ApiHelper
import com.example.hagyma.infrastructure.ApiClient
import kotlinx.coroutines.*
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
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO

    fun getCAFF(uuid:String) {
        viewModelScope.launch(ioDispatcher) {
            try {
                _caff.postValue(caffApi.apiCaffGetImageGet(uuid))
            }catch (e:Exception){
                println("SearchedPicture getCaff $e")
            }
        }
    }

    fun getDownloadCAFF(uuid:String) {
        viewModelScope.launch(ioDispatcher) {
            _caffFile.postValue(caffApi.apiCaffDownloadImageGet(uuid))
        }
    }

    suspend fun getUserName(): String {
        return userApi.apiUserGetUserGet().name
    }

    fun saveComment(uuid: String, newCommentText: String) {
        viewModelScope.launch(ioDispatcher) {
            caffApi.apiCaffAddCommentPost(uuid, CommentRequest(newCommentText))
        }
    }

    fun deleteComment(commentId: String) {
        viewModelScope.launch {
            caffApi.apiCaffDeleteCommentDelete(commentId)
        }
    }

    fun deletePicture(imageId: String) {
        viewModelScope.launch {
            caffApi.apiCaffDeleteImageDelete(imageId)
        }
    }

    private val _jwt = MutableLiveData<JWT>().apply {
        value = ApiClient.accessToken?.let { JWT(it) }
    }

    val userName: LiveData<String> = _jwt.map { data ->  data.getClaim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")
        .asString()
        .toString()}
}