﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Firebase;

using LetMeKnow.Droid.Database;

using Firebase.Auth;

namespace LetMeKnow.Droid
{
    [Activity(Label = "LetMeKnow", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            FirebaseApp.InitializeApp(Application.Context);

            // Handle any intent here
            DatabaseAuthenticator.HandleEmailVerificationLink(this.Intent);

            LoadApplication(new App(new DatabaseManager()));
        }

        public override void OnBackPressed() {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed)) {
                // Do something if there are some pages in the `PopupStack`
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults) {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}