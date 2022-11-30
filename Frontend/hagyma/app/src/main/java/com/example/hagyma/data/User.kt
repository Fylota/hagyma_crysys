package com.example.hagyma.data

import java.util.*

data class User (
    val uuid: UUID,
    val isAdmin: Boolean,
    val name: String,
    val email: String,
    val regDate: Date,
    var caffFiles: List<CAFF>?,
    var comments: List<Comment>?,
    var purchasedCaffFiles: List<CAFF>?,
)