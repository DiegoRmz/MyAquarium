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
    [Activity(Label = "ParameterAdder")]
    public class ParameterAdder : Activity
    {
        Button sendButton;
        string email;

        TextView _dateDisplay;
        Button _dateSelectButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ParameterAdder);

            //Get button
            sendButton = FindViewById<Button>(Resource.Id.submitParameters);

            //Get email from Intent
            email = Intent.GetStringExtra("Email");

            sendButton.Click += AddParameters;
        }

        private void AddParameters(object sender, EventArgs e)
        {
            int result;

            string nombre = FindViewById<EditText>(Resource.Id.nom).Text,
                dia=FindViewById<EditText>(Resource.Id.dayTextEdit).Text,
                mes = FindViewById<EditText>(Resource.Id.monthTextEdit).Text,
                anio = FindViewById<EditText>(Resource.Id.yearTextEdit).Text,
                ph =FindViewById<EditText>(Resource.Id.phTextEdit).Text,
                salinidad=FindViewById<EditText>(Resource.Id.salinityTextEdit).Text,
                amonia=FindViewById<EditText>(Resource.Id.ammoniaTextEdit).Text,
                nitrite=FindViewById<EditText>(Resource.Id.nitriteTextEdit).Text,
                nitrate=FindViewById<EditText>(Resource.Id.nitrateTextEdit).Text;

            if(string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(dia) || string.IsNullOrEmpty(mes) || string.IsNullOrEmpty(anio) || string.IsNullOrEmpty(ph) || string.IsNullOrEmpty(salinidad) || string.IsNullOrEmpty(amonia) || string.IsNullOrEmpty(nitrite) || string.IsNullOrEmpty(nitrate))
            {
                Toast.MakeText(this, "Alguno de los campos está vacío", ToastLength.Long).Show();
            }
            else
            {
                //Call to code for DB insertion
                BackEndServices backend = new BackEndServices();
                int d = int.Parse(dia);
                int m = int.Parse(mes);
                int a = int.Parse(anio);

                if (d > 31 || d < 1 || m>12 || m<1 || a < 2017)
                {
                    Toast.MakeText(this, "Problemas en el formato de la fecha", ToastLength.Long).Show();
                }
                else
                {
                    string fecha = anio + "-" + mes + "-" + dia;
                    result = backend.AddParameters(nombre, email, fecha, salinidad, amonia, nitrite, nitrate);

                    if (result == 1)
                    {
                        Toast.MakeText(this, "Parámetros añadidos exitosamente", ToastLength.Long).Show();
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
}