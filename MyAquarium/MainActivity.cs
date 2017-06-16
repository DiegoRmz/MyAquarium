using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android;
using Android.Content;
using Android.Runtime;


namespace MyAquarium
{
    [Activity(Label = "MyAquarium", MainLauncher = true, Icon = "@drawable/icon")]

    public class MainActivity : Activity
    {
        Button login;
        Button fbLogin;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.AddParameters);

            //Get a login button reference
            login = FindViewById<Button>(Resource.Id.login);
            fbLogin = FindViewById<Button>(Resource.Id.fbLogin);

            //Check @startup of the application if there is connection
            Plugin.Connectivity.CrossConnectivity.Current.ConnectivityChanged += CurrentConnectivityChanged;

            login.Click += LoginClick;

        }

        private void CurrentConnectivityChanged(object sender, Plugin.Connectivity.Abstractions.ConnectivityChangedEventArgs e)
        {
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                Toast.MakeText(this, "Conectado a Internet", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, "Se requiere de conexión a internet", ToastLength.Long).Show();
            }
        }

        private void LoginClick(object sender, EventArgs e)
        {
            string email;
            string password;
            int    passed;

            //Check there is connectivity
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                //Get User Input
                email = FindViewById<EditText>(Resource.Id.editTextEmail).Text;
                password = FindViewById<EditText>(Resource.Id.editTextPassword).Text;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    Toast.MakeText(this, "Favor de llenar los campos", ToastLength.Long).Show();
                }
                else
                {
                    //Send the data to the backend
                    BackEndServices backend = new BackEndServices();

                    passed = backend.UserExist(email,password);

                    //Check result code
                    if (passed == 0)
                    {
                        Toast.MakeText(this, "Email o contraseña incorrectos", ToastLength.Long).Show();
                    }
                    else {
                        //Change view
                        var intent = new Intent(this, typeof(Switcher));

                        intent.PutExtra("Email",email);

                        //Start activity
                        StartActivity(intent);
                    }

                }
            }
                
            else
            {
                Toast.MakeText(this, "Esto no puede funcionar sin Internet", ToastLength.Short).Show();
            }

        }
    }
}

