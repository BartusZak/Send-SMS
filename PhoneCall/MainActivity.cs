using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Views;
using System;
using Android.Telephony;

namespace PhoneCall
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
             SetContentView (Resource.Layout.Main);

            // Użycie Delegata
            EditText phoneNumber = FindViewById<EditText>(Resource.Id.PhoneNumber);
            EditText phoneMsg = FindViewById<EditText>(Resource.Id.PhoneMsg);
            Button callButton = FindViewById<Button>(Resource.Id.CallButton);

            callButton.Click += (object sender, EventArgs e) =>
            {
                var callDialog = new AlertDialog.Builder(this);
                callDialog.SetMessage(Resources.GetText(Resource.String.AlertInfo));
                callDialog.SetNeutralButton(Resources.GetText(Resource.String.CallText), delegate
                {
                //Wysyłanie SMS'a natychmiastowo w tle!!
                SmsManager.Default.SendTextMessage(phoneNumber.Text, null, phoneMsg.Text, null, null);
                {
                    var callDialog2 = new AlertDialog.Builder(this);
                    callDialog2.SetMessage(Resources.GetText(Resource.String.MessageSent));
                    callDialog2.SetNeutralButton(Resources.GetText(Resource.String.Ok), delegate { });
                    callDialog2.Show();
                        phoneMsg.Text = "";
                        phoneNumber.Text = "";
                    }
                    //Wysyłanie SMS'a za pomocą INTENT (3ba jeszcze potwierdzic wysłanie)
                    //var smsUri = Android.Net.Uri.Parse("smsto:" + phoneNumber.Text);
                    //var smsIntent = new Intent(Intent.ActionSendto, smsUri);
                    //smsIntent.PutExtra("sms_body", phoneMsg.Text );
                    //StartActivity(smsIntent);

                    //Dzwonienie
                    //var callIntent = new Intent(Intent.ActionCall);
                    //callIntent.SetData(Android.Net.Uri.Parse("tel:" + phoneNumber.Text));
                    //StartActivity(callIntent);
                });
                callDialog.SetNegativeButton(Resources.GetText(Resource.String.Cancel), delegate { });
                callDialog.Show();
            };

        }
        //   [java.interop.export("call")]
        //    public void call(view view)
        //     {
        //        edittext phonenumber = findviewbyid<edittext>(resource.id.phonenumber);
        //
        //     var callintent = new intent(intent.actioncall);
        //       callintent.setdata(android.net.uri.parse("tel:" + phonenumber.text));
        //        startactivity(callintent);
        //     }

      
}
}

