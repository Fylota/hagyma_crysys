package com.example.hagyma.data

import java.util.*

data class CAFF (
    val name: String,
    val creationTime: Date,
    val creatorID: UUID,
    val description: String?,
    var commentIDs: List<Comment>?,
)