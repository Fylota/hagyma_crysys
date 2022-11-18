package com.example.hagyma.helper

import androidx.viewbinding.BuildConfig
import com.example.hagyma.api.AuthenticationApi
import com.example.hagyma.api.CaffApi
import com.example.hagyma.api.PaymentApi
import com.example.hagyma.api.UserApi
import com.example.hagyma.http.CustomClientFactory

class ApiHelper {
    companion object {
        private val ADDRESS = (if(BuildConfig.DEBUG) "http" else "https") + "://10.0.2.2:" + (if(BuildConfig.DEBUG) "5226" else "7226")
        //private const val ADDRESS = "https://192.168.1.103:7226"
        fun getAuthenticationApi(): AuthenticationApi {
            return AuthenticationApi(ADDRESS, CustomClientFactory().createNewNetworkModuleClient())
        }

        fun getUserApi(): UserApi {
            return UserApi(ADDRESS, CustomClientFactory().createNewNetworkModuleClient())
        }

        fun getPaymentApi(): PaymentApi {
            return PaymentApi(ADDRESS, CustomClientFactory().createNewNetworkModuleClient())
        }

        fun getCaffApi(): CaffApi {
            return CaffApi(ADDRESS, CustomClientFactory().createNewNetworkModuleClient())
        }
    }

}