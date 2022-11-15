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

import com.example.hagyma.api.model.FileAttributes

import com.squareup.moshi.Json

/**
 * 
 *
 * @param parent 
 * @param root 
 * @param fullName 
 * @param extension 
 * @param name 
 * @param exists 
 * @param creationTime 
 * @param creationTimeUtc 
 * @param lastAccessTime 
 * @param lastAccessTimeUtc 
 * @param lastWriteTime 
 * @param lastWriteTimeUtc 
 * @param linkTarget 
 * @param attributes 
 */


data class DirectoryInfo (

    @Json(name = "parent")
    val parent: DirectoryInfo? = null,

    @Json(name = "root")
    val root: DirectoryInfo? = null,

    @Json(name = "fullName")
    val fullName: kotlin.String? = null,

    @Json(name = "extension")
    val extension: kotlin.String? = null,

    @Json(name = "name")
    val name: kotlin.String? = null,

    @Json(name = "exists")
    val exists: kotlin.Boolean? = null,

    @Json(name = "creationTime")
    val creationTime: java.time.OffsetDateTime? = null,

    @Json(name = "creationTimeUtc")
    val creationTimeUtc: java.time.OffsetDateTime? = null,

    @Json(name = "lastAccessTime")
    val lastAccessTime: java.time.OffsetDateTime? = null,

    @Json(name = "lastAccessTimeUtc")
    val lastAccessTimeUtc: java.time.OffsetDateTime? = null,

    @Json(name = "lastWriteTime")
    val lastWriteTime: java.time.OffsetDateTime? = null,

    @Json(name = "lastWriteTimeUtc")
    val lastWriteTimeUtc: java.time.OffsetDateTime? = null,

    @Json(name = "linkTarget")
    val linkTarget: kotlin.String? = null,

    @Json(name = "attributes")
    val attributes: FileAttributes? = null

)

