package com.example.hagyma.data

import java.time.OffsetDateTime
import java.util.*

data class Comment (
    val uuid: String,
    val creatorID: String,
    val creationTime: OffsetDateTime?,
    val caffFileID: String,
    val content: String,
)