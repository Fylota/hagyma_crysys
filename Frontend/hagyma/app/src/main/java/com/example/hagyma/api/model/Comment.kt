/**
 *
 * Please note:
 * This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * Do not edit this file manually.
 *
 */

@file:Suppress(
    "ArrayInDataClass",
    "EnumEntryName",
    "RemoveRedundantQualifierName",
    "UnusedImport"
)

package com.example.hagyma.api.model


import com.squareup.moshi.Json

/**
 * 
 *
 * @param content 
 * @param id 
 * @param creationTime 
 */


data class Comment (

    @Json(name = "content")
    val content: kotlin.String,

    @Json(name = "id")
    val id: kotlin.String,

    @Json(name = "creationTime")
    val creationTime: java.time.OffsetDateTime? = null

)

