package com.example.hagyma.data

import java.util.*

data class Comment (
    val uuid: UUID,
    val creatorID: UUID,
    val creationTime: String,
    val caffFileID: UUID,
    val message: String,
)