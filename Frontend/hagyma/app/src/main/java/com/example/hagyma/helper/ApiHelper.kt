package com.example.hagyma.helper

import android.util.Log
import com.example.hagyma.BuildConfig
import com.example.hagyma.api.AuthenticationApi
import com.example.hagyma.api.CaffApi
import com.example.hagyma.api.PaymentApi
import com.example.hagyma.api.UserApi

class ApiHelper {
    companion object {
        private val ADDRESS = (if(BuildConfig.DEBUG) "http" else "https") + "://10.0.2.2:"+ if(BuildConfig.DEBUG) "5226" else "7226" //Emulator
        //private val ADDRESS = (if(BuildConfig.DEBUG) "http" else "https") + "://192.168.1.103:"+ if(BuildConfig.DEBUG) "5226" else "7226"
//        private val ADDRESS = (if(BuildConfig.DEBUG) "http" else "https") + "://192.168.68.106:"+ if(BuildConfig.DEBUG) "5226" else "7226"
        fun getAuthenticationApi(): AuthenticationApi {
            Log.d("ApiHelper", ADDRESS)
            return AuthenticationApi(ADDRESS)
        }

        fun getUserApi(): UserApi {
            return UserApi(ADDRESS)
        }

        fun getPaymentApi(): PaymentApi {
            return PaymentApi(ADDRESS)
        }

        fun getCaffApi(): CaffApi {
            return CaffApi(ADDRESS)
        }
    }

}