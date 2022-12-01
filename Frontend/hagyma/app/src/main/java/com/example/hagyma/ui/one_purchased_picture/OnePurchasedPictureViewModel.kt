package com.example.hagyma.ui.one_purchased_picture

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

class OnePurchasedPictureViewModel : ViewModel() {

    private val _caff = MutableLiveData<CaffDetails>().apply {
        value = CaffDetails("","","",null,null)
    }
    val caff: LiveData<CaffDetails> = _caff
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