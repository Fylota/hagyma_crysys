package com.example.hagyma.ui.searched_picture

import android.graphics.drawable.BitmapDrawable
import android.util.Base64
import androidx.lifecycle.*
import com.auth0.android.jwt.JWT
import com.example.hagyma.api.model.CaffDetails
import com.example.hagyma.data.CAFF
import com.example.hagyma.data.Comment
import com.example.hagyma.helper.ApiHelper
import com.example.hagyma.infrastructure.ApiClient
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch
import java.io.ByteArrayInputStream
import java.text.SimpleDateFormat
import java.time.Instant.now
import java.util.*
import androidx.lifecycle.lifecycleScope
import com.example.hagyma.api.model.CommentRequest
import kotlinx.coroutines.withContext
import java.time.OffsetDateTime

class SearchedPictureViewModel : ViewModel()  {

    private val _caff = MutableLiveData<CaffDetails>().apply {
        value = CaffDetails("","","",null,null)
    }
    val caff: LiveData<CaffDetails> = _caff
    private val caffApi = ApiHelper.getCaffApi()
    private val paymentApi = ApiHelper.getPaymentApi()

    suspend fun getCAFF(uuid:String){
        try {
            withContext(Dispatchers.Main){
                _caff.value = caffApi.apiCaffGetImageGet(uuid)
            }
        }catch (e:Exception){
            System.out.println("SearchedPicture getCaff " + e)
        }
    }

    suspend fun saveComment(uuid: String, newCommentText: String){
        caffApi.apiCaffAddCommentPost(uuid, CommentRequest(newCommentText))
    }

    suspend fun deleteComment(commentId: String){
        caffApi.apiCaffDeleteCommentDelete(commentId)
    }

    suspend fun purchaseCaff(imageId: String) {
        withContext(Dispatchers.IO) {
            paymentApi.apiPaymentPurchasePost(imageId)
        }
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