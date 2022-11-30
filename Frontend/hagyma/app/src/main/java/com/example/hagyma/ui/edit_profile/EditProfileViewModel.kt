package com.example.hagyma.ui.edit_profile

import androidx.lifecycle.LiveData
import androidx.lifecycle.MutableLiveData
import androidx.lifecycle.ViewModel

class EditProfileViewModel : ViewModel() {

    private val _text = MutableLiveData<String>().apply {
        value = "This is edit profile Fragment"
    }
    val text: LiveData<String> = _text
}