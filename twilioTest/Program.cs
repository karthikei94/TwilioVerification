using Authy.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace twilioTest
{
    class Program
    {
        public static string AuthyAPIKey = "ObFyJkK1sUNl7hYk9Hw8pZ0qrUqM0YBB";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // Find your Account Sid and Auth Token at twilio.com/console
            const string accountSid = "AC6e08147104ffc4a8a1c9ad142ade2cc8";
            //const string authToken = "126c98b5de29aae057cbf8302cd2e15d";
            //TwilioClient.Init(accountSid, authToken);

            //var key = KeyResource.Fetch("SK2a0747eba6abf96b7e3c3ff0b4530f6e");

            //Console.WriteLine(key.FriendlyName);

            //AuthyClient _client = new AuthyClient(AuthyAPIKey, false);
            //var t = _client.RegisterUser("karthikei94@gmail.com", "8897322553", Convert.ToInt32("91"));
            List<KeyValuePair<string, string>> values = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string,string>("via", "sms"),
              new KeyValuePair<string,string>("phone_number", "8897322553"),
              new KeyValuePair<string,string>("country_code", "91")
            };
            //Console.WriteLine(t.UserId);
            //StartPhoneVerificationAsync(values); // for triggering messages
            VerifyPhoneAsync(); //for verifying the phone
            Console.ReadLine();
        }

        public static async Task StartPhoneVerificationAsync(List<KeyValuePair<string, string>> value)
        {
            // Create client
            var client = new HttpClient();

            var requestContent = new FormUrlEncodedContent(value);

            // https://api.authy.com/protected/$AUTHY_API_FORMAT/phones/verification/start?via=$VIA&country_code=$USER_COUNTRY&phone_number=$USER_PHONE
            HttpResponseMessage response = await client.PostAsync(
              "https://api.authy.com/protected/json/phones/verification/start?api_key=" + AuthyAPIKey,
              requestContent);

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Write the output.
                Console.WriteLine(await reader.ReadToEndAsync());
            }
            Console.WriteLine("status code: " + response.StatusCode.ToString() + "Content:" + response.Content);
            Console.ReadLine();
        }

        public static async Task VerifyPhoneAsync()
        {
            // Create client
            var client = new HttpClient();

            // Add authentication header
            client.DefaultRequestHeaders.Add("X-Authy-API-Key", AuthyAPIKey);

            // https://api.authy.com/protected/$AUTHY_API_FORMAT/phones/verification/check?phone_number=$USER_PHONE&country_code=$USER_COUNTRY&verification_code=$VERIFY_CODE
            HttpResponseMessage response = await client.GetAsync(
              "https://api.authy.com/protected/json/phones/verification/check?phone_number=8897322553&country_code=91&verification_code=8949");

            // Get the response content.
            HttpContent responseContent = response.Content;

            // Get the stream of the content.
            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                // Write the output.
                Console.WriteLine(await reader.ReadToEndAsync());
            }
            Console.WriteLine("status code: " + response.StatusCode.ToString() + "Content:" + response.Content);
            Console.ReadLine();
        }


    }
}
