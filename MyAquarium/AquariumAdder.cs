using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace MyAquarium
{
    [Activity(Label = "AquariumAdder")]
    public class AquariumAdder : Activity
    {
        Button sendButton;
        string email;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AquariumAdder);

            //Get button
            sendButton = FindViewById<Button>(Resource.Id.submitAquarium);

            //Get email from Intent
            email = Intent.GetStringExtra("Email");

            //Add listener to button
            sendButton.Click += AddAquarium;
        }

        private void AddAquarium(Object sender, EventArgs e)
        {
            int result;
            //Get parameters
            string nombre = FindViewById<EditText>(Resource.Id.aqNameTextEdit).Text, 
                tipo = FindViewById<EditText>(Resource.Id.aqTypeTextEdit).Text, 
                litros = FindViewById<EditText>(Resource.Id.aqLtsTextEdit).Text;

            if(string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(tipo) || string.IsNullOrEmpty(litros))
            {
                Toast.MakeText(this, "Alguno de los campos está vacío", ToastLength.Long).Show();
            }
            else
            {
                //Call to code for DB insertion
                BackEndServices backend = new BackEndServices();

                result = backend.AddAquarium(nombre, email, litros, tipo);

                if(result == 1)
                {
                    Toast.MakeText(this, "Acuario añadido exitosamente", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "Error al hacer la inserción", ToastLength.Long).Show();
                }

                Finish();
            }
        }
    }
}