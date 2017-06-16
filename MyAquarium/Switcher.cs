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
    [Activity(Label = "Switcher")]
    class Switcher : Activity
    {
        Button search;
        Button addAquarium;
        Button addParameters;
        string email;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //This sets the layout as the next one
            SetContentView(Resource.Layout.Switcher);

            search = FindViewById<Button>(Resource.Id.search);
            addAquarium = FindViewById<Button>(Resource.Id.gotoAddAq);
            addParameters = FindViewById<Button>(Resource.Id.gotoAddPar);

            email = Intent.GetStringExtra("Email");

            search.Click += SearchAquarium;

            addAquarium.Click += AquariumAdder;

            addParameters.Click += ParameterAdder;
        }

        private void SearchAquarium(Object sender, EventArgs e)
        {
            string nombre = FindViewById<EditText>(Resource.Id.searchAqTextEdit).Text;
            string data;

            if (string.IsNullOrEmpty(nombre))
            {
                Toast.MakeText(this, "Nombre de acuario vacío", ToastLength.Long).Show();
            }
            else
            {
                BackEndServices backend = new BackEndServices();

                data = backend.GetAquariumData(email, nombre);

                //Display data obtained
            }
        }

        private void AquariumAdder(Object sender, EventArgs e)
        {
            //Create intent
            var intent = new Intent(this, typeof(AquariumAdder));

            //Send email as it is basically the key to everything
            intent.PutExtra("Email", email);

            //Start activity
            StartActivity(intent);
        }

        private void ParameterAdder(Object sender, EventArgs e)
        {
            //Create intent
            var intent = new Intent(this, typeof(ParameterAdder));

            //Send email as it is basically the key to everything
            intent.PutExtra("Email", email);

            //Start activity
            StartActivity(intent);
        }
    }
}