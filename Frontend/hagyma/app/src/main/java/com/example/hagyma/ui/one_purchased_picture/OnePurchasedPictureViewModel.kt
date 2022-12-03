package com.example.hagyma.ui.one_purchased_picture

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
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.withContext
import java.io.File

class OnePurchasedPictureViewModel : ViewModel() {

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

    suspend fun getCAFF(uuid:String){
        try {
            withContext(Dispatchers.Main){
                _caff.value = caffApi.apiCaffGetImageGet(uuid)
            }
        }catch (e:Exception){
            System.out.println("SearchedPicture getCaff " + e)
        }
    }

    suspend fun getDownloadCAFF(uuid:String){
        try {
            withContext(Dispatchers.Main){
                _caffFile.value = caffApi.apiCaffDownloadImageGet(uuid)
            }
        }catch (e:Exception){
            e.message?.let { it1 -> Log.e("", it1) }
        }
    }

    suspend fun getUserName(): String {
        return userApi.apiUserGetUserGet().name
    }

    suspend fun saveComment(uuid: String, newCommentText: String){
        caffApi.apiCaffAddCommentPost(uuid, CommentRequest(newCommentText))
    }

    suspend fun deleteComment(commentId: String){
        caffApi.apiCaffDeleteCommentDelete(commentId)
    }


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