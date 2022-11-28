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
import hu.bme.aut.android.onlab.extensions.validateNonEmpty
import kotlinx.coroutines.Dispatchers
import kotlinx.coroutines.launch


class MainActivity : BaseActivity() {

//    private lateinit var firebaseAuth: FirebaseAuth
    private lateinit var authenticationApi: AuthenticationApi
    private lateinit var userApi : UserApi
    private lateinit var binding: ActivityMainBinding

    override fun onCreate(savedInstanceState: Bundle?) {
//        val currentTheme = PreferenceManager.getDefaultSharedPreferences(this).getInt(
//            SettingsFragment.KEY_THEME,
//            SettingsFragment.PURPLE
//        )
//        setTheme(currentTheme)
        super.onCreate(savedInstanceState)
        authenticationApi = ApiHelper.getAuthenticationApi()
        userApi = ApiHelper.getUserApi()
        binding = ActivityMainBinding.inflate(layoutInflater)
        setContentView(binding.root)

//        firebaseAuth = FirebaseAuth.getInstance()

        binding.btnRegister.setOnClickListener { registerClick() }
        binding.btnLogin.setOnClickListener { loginClick() }

    }

    // Check if there's a signed-in user
    override fun onStart(){
        super.onStart()
//        val user: FirebaseUser? = firebaseAuth.currentUser
//        user?.let {
//            startActivity(Intent(this@MainActivity, SecondActivity::class.java))
//            toast("Welcome back!")
//        }
    }

    // If someone wants to register then the application calls this function
    private fun registerClick() {

        val v = LayoutInflater.from(this).inflate(R.layout.registration, null)

        val email = v.findViewById<EditText>(R.id.etregEmail)
        val username = v.findViewById<EditText>(R.id.etregUsername)
        val pass1 = v.findViewById<EditText>(R.id.etregPassword)
        val pass2 = v.findViewById<EditText>(R.id.etregPassword2)
        val add_dialog = AlertDialog.Builder(this)
        add_dialog.setView(v)

        add_dialog.setPositiveButton("Ok") { dialog, _ ->  // TODO: After validation we save the new user data to the DB
            val handler = Handler(Looper.getMainLooper()!!)
            if (!validateRegisterForm(email, username, pass1, pass2)) {
                Toast.makeText(this, "Invalid form", Toast.LENGTH_SHORT).show()
            } else {
                lifecycleScope.launch(Dispatchers.IO) {
                    try {
                        val result = authenticationApi.authRegisterPost(
                            RegisterRequest(
                                email.text.toString(),
                                pass1.text.toString(),
                                username.text.toString()
                            )
                        )
                        handler.post {
                            Toast.makeText(
                                baseContext,
                                "Registration successful!, ${result.toString()}",
                                Toast.LENGTH_SHORT
                            ).show()
                        }
                        try {
                            val userInfo = userApi.apiUserGetUserGet()
                        } catch (e: Exception) {
                            handler.post {
                                Toast.makeText(
                                    baseContext,
                                    "Registration failed!, ${e.message}",
                                    Toast.LENGTH_SHORT
                                ).show()
                                onLoginFailed()
                            }
                        }
                        handler.post {
                            onLoginSuccess()
                        }
                    } catch (e: Exception) {
                        e.message?.let { it1 -> Log.e("", it1) }
                        handler.post {
                            Toast.makeText(
                                baseContext,
                                "Registration failed!, ${e.message}",
                                Toast.LENGTH_SHORT
                            ).show()
                            onLoginFailed()
                        }
                    }
                }
            }
//            if (email.validateNonEmpty() && pass1.validateNonEmpty() && pass2.validateNonEmpty() && pass1.text.toString() == pass2.text.toString()) {
//                firebaseAuth
//                    .createUserWithEmailAndPassword(email.text.toString(), pass1.text.toString())
//                    .addOnSuccessListener { result ->
//                        hideProgressDialog()
//
//                        val firebaseUser = result.user
//                        val profileChangeRequest = UserProfileChangeRequest.Builder()
//                            .setDisplayName(firebaseUser?.email?.substringBefore('@'))
//                            .build()
//                        firebaseUser?.updateProfile(profileChangeRequest)
//
//                        toast("Registration successful")
//                    }
//                    .addOnFailureListener { exception ->
//                        hideProgressDialog()
//
//                        toast(exception.message)
//                    }
//                Toast.makeText(this, "Registration successful", Toast.LENGTH_SHORT).show()
//                dialog.dismiss()
//            }else{
//                Toast.makeText(this, "Incorrect registration", Toast.LENGTH_SHORT).show()
//            }

        }
        add_dialog.setNegativeButton("Cancel") { dialog, _ ->
            Toast.makeText(this, "Cancel", Toast.LENGTH_SHORT).show()
        }
        add_dialog.create()
        add_dialog.show()

//        showProgressDialog()

//        firebaseAuth
//            .createUserWithEmailAndPassword(binding.etEmail.text.toString(), binding.etPassword.text.toString())
//            .addOnSuccessListener { result ->
//                hideProgressDialog()
//
//                val firebaseUser = result.user
//                val profileChangeRequest = UserProfileChangeRequest.Builder()
//                    .setDisplayName(firebaseUser?.email?.substringBefore('@'))
//                    .build()
//                firebaseUser?.updateProfile(profileChangeRequest)
//
//                toast("Registration successful")
//            }
//            .addOnFailureListener { exception ->
//                hideProgressDialog()
//
//                toast(exception.message)
//            }
    }

    private fun loginClick() { // TODO: The real login with check the given username/email and password

        if (!validateForm()) {
            onLoginFailed()
            return
        }
        binding.btnLogin.isEnabled = false
        val email = findViewById<EditText>(R.id.etEmail).text.toString()
        val password = findViewById<EditText>(R.id.etPassword).text.toString()
        showProgressDialog()


        val handler = Handler(Looper.getMainLooper()!!)
        lifecycleScope.launch(Dispatchers.IO) {
            try {
                val result = authenticationApi.authLoginPost(LoginRequest(email,password))
                ApiClient.accessToken = result
                try {
                    val userInfo = userApi.apiUserGetUserGet()
                } catch (e: Exception){
                    handler.post{
                        onLoginFailed()
                    }
                }
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

//
//        showProgressDialog()

//        firebaseAuth
//            .signInWithEmailAndPassword(binding.etEmail.text.toString(), binding.etPassword.text.toString())
//            .addOnSuccessListener {
//                hideProgressDialog()
//                startActivity(Intent(this@MainActivity, SecondActivity::class.java))
//                finish()
//            }
//            .addOnFailureListener { exception ->
//                hideProgressDialog()
//
//                toast(exception.localizedMessage)
//            }


    private fun validateForm(): Boolean = binding.etEmail.validateNonEmpty()
            && binding.etPassword.validateNonEmpty()


    // OLD PAGE: (now this is in the secondActivity)
//    private lateinit var appBarConfiguration: AppBarConfiguration
//    private lateinit var binding: ActivityMainBinding
//
//    override fun onCreate(savedInstanceState: Bundle?) {
//        super.onCreate(savedInstanceState)
//
//        binding = ActivityMainBinding.inflate(layoutInflater)
//        setContentView(binding.root)
//
//        //val isAdmin = false;
//        setSupportActionBar(binding.appBarMain.toolbar)
//
//        val drawerLayout: DrawerLayout = binding.drawerLayout
//        val navView: NavigationView = binding.navView
//        val navController = findNavController(R.id.nav_host_fragment_content_main)
//        // Passing each menu ID as a set of Ids because each
//        // menu should be considered as top level destinations.
//        appBarConfiguration = AppBarConfiguration(
//            setOf(
//                R.id.nav_gallery, R.id.nav_my_pictures, R.id.nav_purchased_pictures, R.id.nav_profile, R.id.nav_admin_users
//            ), drawerLayout
//        )
//        setupActionBarWithNavController(navController, appBarConfiguration)
//        navView.setupWithNavController(navController)
//
//        // Hide and disable users menu group when the logged in profile is not admin.
//        // TODO get isAdmin from logged in user.
//        val isAdmin = true;
//        navView.menu.setGroupVisible(R.id.group_admin, isAdmin)
//        navView.menu.setGroupEnabled(R.id.group_admin, isAdmin)
//    }
//
//    override fun onSupportNavigateUp(): Boolean {
//        val navController = findNavController(R.id.nav_host_fragment_content_main)
//        return navController.navigateUp(appBarConfiguration) || super.onSupportNavigateUp()
//    }

    private fun validateRegisterForm(email: EditText, username: EditText, pass1: EditText, pass2: EditText): Boolean =
        email.validateNonEmpty() &&
        username.validateNonEmpty() &&
        pass1.validateNonEmpty() &&
        pass1.text.toString() == pass2.text.toString()

    private fun onLoginSuccess() {
        hideProgressDialog()
        binding.btnLogin.isEnabled = true
        Toast.makeText(baseContext, "Login Success", Toast.LENGTH_LONG).show()
        startActivity(Intent(baseContext, SecondActivity::class.java))
    }

    private fun onLoginFailed() {
        hideProgressDialog()
        Toast.makeText(baseContext, "Login failed", Toast.LENGTH_LONG).show()
        binding.btnLogin.isEnabled = true
    }

    private fun onRegisterSuccess() {}
    private fun onRegisterFailed() {}
}