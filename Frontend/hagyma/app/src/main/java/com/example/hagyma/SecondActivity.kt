package com.example.hagyma

import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.view.MenuItem
import androidx.appcompat.app.AppCompatActivity
import androidx.core.view.GravityCompat
import androidx.drawerlayout.widget.DrawerLayout
import androidx.navigation.findNavController
import androidx.navigation.ui.AppBarConfiguration
import androidx.navigation.ui.navigateUp
import androidx.navigation.ui.setupActionBarWithNavController
import androidx.navigation.ui.setupWithNavController
import com.auth0.android.jwt.JWT
import com.example.hagyma.api.AuthenticationApi
import com.example.hagyma.api.UserApi
import com.example.hagyma.databinding.ActivitySecondBinding
import com.example.hagyma.helper.ApiHelper
import com.example.hagyma.infrastructure.ApiClient
import com.google.android.material.navigation.NavigationView


class SecondActivity : AppCompatActivity(), NavigationView.OnNavigationItemSelectedListener {

    private lateinit var appBarConfiguration: AppBarConfiguration
    private lateinit var binding: ActivitySecondBinding
    private lateinit var authenticationApi: AuthenticationApi
    private lateinit var userApi : UserApi

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        authenticationApi = ApiHelper.getAuthenticationApi()
        userApi = ApiHelper.getUserApi()
        binding = ActivitySecondBinding.inflate(layoutInflater)
        setContentView(binding.root)

        setSupportActionBar(binding.appBarMain.toolbar)

        val drawerLayout: DrawerLayout = binding.drawerLayout
        val navView: NavigationView = binding.navView
        val navController = findNavController(R.id.nav_host_fragment_content_main)
        // Passing each menu ID as a set of Ids because each
        // menu should be considered as top level destinations.
        appBarConfiguration = AppBarConfiguration(
            setOf(
                R.id.nav_gallery, R.id.nav_my_pictures, R.id.nav_purchased_pictures,
                R.id.nav_profile, R.id.nav_admin_users, R.id.nav_logout
            ), drawerLayout
        )
        setupActionBarWithNavController(navController, appBarConfiguration)
        navView.setupWithNavController(navController)
        navView.setNavigationItemSelectedListener(this)

        // Hide and disable users menu group when the logged in profile is not admin.

        val isAdmin: Boolean = try {
            val jwt = ApiClient.accessToken?.let { JWT(it) }
            val role = jwt?.getClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                ?.asString()

            role == "Admin"
        } catch (e: Exception) {
            e.message?.let { it1 -> Log.e("", it1) }
            false
        }

        navView.menu.setGroupVisible(R.id.group_admin, isAdmin)
        navView.menu.setGroupEnabled(R.id.group_admin, isAdmin)
    }

    override fun onSupportNavigateUp(): Boolean {
        val navController = findNavController(R.id.nav_host_fragment_content_main)
        return navController.navigateUp(appBarConfiguration) || super.onSupportNavigateUp()
    }

    override fun onNavigationItemSelected(item: MenuItem): Boolean {

        when (item.itemId) {
            R.id.nav_gallery -> {
                findNavController(R.id.nav_host_fragment_content_main).navigate(R.id.action_global_nav_gallery)
            }
            R.id.nav_my_pictures -> {
                findNavController(R.id.nav_host_fragment_content_main).navigate(R.id.action_global_nav_my_pictures)
            }
            R.id.nav_purchased_pictures -> {
                findNavController(R.id.nav_host_fragment_content_main).navigate(R.id.action_global_nav_purchased_pictures)
            }
            R.id.nav_profile -> {
                findNavController(R.id.nav_host_fragment_content_main).navigate(R.id.action_global_nav_profile)
            }
            R.id.nav_admin_users -> {
                findNavController(R.id.nav_host_fragment_content_main).navigate(R.id.action_global_nav_admin_users)
            }
            R.id.nav_logout -> {
                ApiClient.accessToken = null
                startActivity(Intent(this, MainActivity::class.java))
                finish()
            }
        }

        binding.drawerLayout.closeDrawer(GravityCompat.START)
        return true
    }
}