
using Android.App;
using Android.Content;
using Android.Telephony;
using Android.Widget;
using Grossbuch.Models;
using Grossbuch.Repositories;
using Java.Lang;
using System;

namespace Grossbuch.Droid
{
    [BroadcastReceiver]
    [IntentFilter(new[] { "android.provider.Telephony.SMS_RECEIVED" }, Priority = (int)IntentFilterPriority.HighPriority)]
    class SmsReceiver : BroadcastReceiver
    {
        public static readonly string INTENT_ACTION = "android.provider.Telephony.SMS_RECEIVED";
        protected string message, address = "";

        public override void OnReceive(Android.Content.Context context, Intent intent)
        {
            if (intent.HasExtra("pdus"))
            {
                var smsArray = (Java.Lang.Object[])intent.Extras.Get("pdus");
                foreach (var item in smsArray)
                {
                    var sms = SmsMessage.CreateFromPdu((byte[])item);
                    address = sms.OriginatingAddress;
                    message = sms.MessageBody;
                    Toast.MakeText(context, "Number: " + address + "Message: " + message, ToastLength.Short).Show();
                    ParseSMS(message);
                }
            }
        }

        private void ParseSMS(string sms)
        {
            var words = sms.Split(' ');

            var card = words[0];
            var time = words[1];
            //var desciption = words[2];
            //var sum = words[3];
            var purpose = words[4];

            Operation operation = new Operation();
            //operation.Account =
            operation.Date = DateTime.Now;
            if (words[2] == "зачисление") { operation.Type = 1; }
            else
            {
                if (words[2] == "перевод") { operation.Type = 3; }
                else { operation.Type = 2; }
            }
            operation.Summ = ParseToSum(words[3]);
            //operation.Purpose =
            operation.Description = sms;
            operation.Performed = true;

            AddOperation(operation);
        }

        private async void AddOperation(Operation operation)
        {
            OperationRepository opRep = new OperationRepository(null);
            await opRep.AddItemAsync(operation, 1);
            await opRep.AddItemAsync(operation, 2);
        }

        private float ParseToSum(string sumString)
        {
            string sum = "";

            for(int i=0; i<sumString.Length; i++)
            {
                if (!char.IsLetter(sumString[i])) { sum = sum + sumString[i]; }
                else { break; }
            }

            return float.Parse(sum);
        }
    }
}