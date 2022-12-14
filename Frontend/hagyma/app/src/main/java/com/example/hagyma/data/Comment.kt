package com.example.hagyma.data

import java.time.OffsetDateTime

data class Comment (
    val uuid: String,
    val creator: String,
    val creationTime: OffsetDateTime?,
    val caffFileID: String,
    val content: String,
)