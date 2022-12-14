package com.example.hagyma

import android.app.AlertDialog
import android.content.Intent
import android.os.Bundle
import android.os.Handler
import android.os.Looper
import android.util.Log
import android.view.LayoutInflater
import android.widget.EditText
import android.widget.Toast
import androidx.lifecycle.lifecycleScope

import com.example.hagyma.api.AuthenticationApi
import com.example.hagyma.api.UserApi
import com.example.hagyma.api.model.LoginRequest
import com.example.hagyma.api.model.RegisterRequest
import com.example.hagyma.databinding.ActivityMainBinding
import com.example.hagyma.helper.ApiHelper
import com.example.hagyma.infrastructure.ApiClient
import com.example.hagyma.extensions.validateNonEmpty
import kotlinx.coroutines.CoroutineDispatcher
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch


class MainActivity : BaseActivity() {

    private lateinit var authenticationApi: AuthenticationApi
    private lateinit var userApi : UserApi
    private lateinit var binding: ActivityMainBinding
    private val ioDispatcher: CoroutineDispatcher = Dispatchers.IO
    private lateinit var progressBar: AlertDialog

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        authenticationApi = ApiHelper.getAuthenticationApi()
        userApi = ApiHelper.getUserApi()
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

        binding.btnRegister.setOnClickListener { registerClick() }
        binding.btnLogin.setOnClickListener { loginClick() }

        val builder  = AlertDialog.Builder(this)
        builder.setCancelable(false)
        builder.setView(R.layout.layout_loading_dialog)
        progressBar = builder.create()
    }

    // If someone wants to register then the application calls this function
    private fun registerClick() {

        val v = LayoutInflater.from(this).inflate(R.layout.registration, null)

        val email = v.findViewById<EditText>(R.id.etregEmail)
        val username = v.findViewById<EditText>(R.id.etregUsername)
        val pass1 = v.findViewById<EditText>(R.id.etregPassword)
        val pass2 = v.findViewById<EditText>(R.id.etregPassword2)
        val alertDialog = AlertDialog.Builder(this)
        alertDialog.setView(v)

        alertDialog.setPositiveButton("Ok") { _, _ ->
            val handler = Handler(Looper.getMainLooper()!!)
            if (!validateRegisterForm(email, username, pass1, pass2)) {
                Toast.makeText(this, "Invalid form", Toast.LENGTH_SHORT).show()
            } else {
                lifecycleScope.launch(ioDispatcher) {
                    try {
                        authenticationApi.authRegisterPost(
                            RegisterRequest(
                                email.text.toString(),
                                pass1.text.toString(),
                                username.text.toString()
                            )
                        )
                        handler.post {
                            onRegisterSuccess()
                        }
                    } catch (e: Exception) {
                        e.message?.let { it1 -> Log.e("", it1) }
                        handler.post {
                            Toast.makeText(
                                baseContext,
                                "Registration failed!, ${e.message}",
                                Toast.LENGTH_SHORT
                            ).show()
                            onRegisterFailed()
                        }
                    }
                }
            }

        }
        alertDialog.setNegativeButton("Cancel") { _, _ ->
            Toast.makeText(this, "Cancel", Toast.LENGTH_SHORT).show()
        }
        alertDialog.create()
        alertDialog.show()
    }

    private fun loginClick() {

        if (!validateForm()) {
            onLoginFailed()
            return
        }
        binding.btnLogin.isEnabled = false
        val email = findViewById<EditText>(R.id.etEmail).text.toString()
        val password = findViewById<EditText>(R.id.etPassword).text.toString()
        progressBar.show()


        val handler = Handler(Looper.getMainLooper()!!)
        lifecycleScope.launch(ioDispatcher) {
            try {
                val result = authenticationApi.authLoginPost(LoginRequest(email,password))
                ApiClient.accessToken = result
                handler.post{
                    onLoginSuccess()
                }
            } catch (e: Exception){
                e.message?.let { it1 -> Log.e("", it1) }
                handler.post{
                    onLoginFailed()
                }
            }
        }
    }

    private fun validateForm(): Boolean = binding.etEmail.validateNonEmpty()
            && binding.etPassword.validateNonEmpty()


    private fun validateRegisterForm(email: EditText, username: EditText, pass1: EditText, pass2: EditText): Boolean =
        email.validateNonEmpty() &&
        android.util.Patterns.EMAIL_ADDRESS.matcher(email.text.toString()).matches() &&
        username.validateNonEmpty() &&
        pass1.validateNonEmpty() &&
        pass1.text.toString().length >= 6 &&
        pass1.text.toString().any(Char::isDigit) &&
        pass1.text.toString().any(Char::isLowerCase) &&
        pass1.text.toString().any(Char::isUpperCase) &&
        pass1.text.toString().any { it in "!,+^" } &&
        pass1.text.toString() == pass2.text.toString()

    private fun onLoginSuccess() {
        progressBar.hide()
        binding.btnLogin.isEnabled = true
        Toast.makeText(baseContext, "Login Success", Toast.LENGTH_LONG).show()
        startActivity(Intent(baseContext, SecondActivity::class.java))
    }

    private fun onLoginFailed() {
        progressBar.hide()
        Toast.makeText(baseContext, "Login failed", Toast.LENGTH_LONG).show()
        binding.btnLogin.isEnabled = true
    }

    private fun onRegisterSuccess() {
        Toast.makeText(
            baseContext,
            "Registration successful. Please log in!",
            Toast.LENGTH_SHORT
        ).show()
    }
    private fun onRegisterFailed() {
        Toast.makeText(
            baseContext,
            "Registration Failed",
            Toast.LENGTH_SHORT
        ).show()
    }
}