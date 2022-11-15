package com.example.hagyma.ui.admin_users

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel

class AdminUsersViewModel : ViewModel() {

    private val _text = MutableLiveData<String>().apply {
        value = "This is admin user Fragment"
    }
    val text: LiveData<String> = _text
}