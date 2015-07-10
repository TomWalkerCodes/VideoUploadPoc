
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace videouploadservice
{
	[Activity (Label = "UploadActivity")]			
	public class UploadActivity : Activity
	{
		public const string BASE_URL = "https://api.ooyala.com/v2/";

		private const int UPLOAD_TIMEOUT = 1000 * 3600;

		public string apiKey;
		public string secretKey;

		private HttpWebResponse response;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Upload);

			string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/test.mp4";

			apiKey = "9mMWQyOuvIjKCgw1dwKgAv60UT18.YGeNZ";
			secretKey = "NVMG82H2ZMPKqeDKA472LYpMhVLGDJNEMkQ8d0zM";

			Dictionary<string, string> videoParams = new Dictionary<string, string> ();
			videoParams.Add("name", "Friday Video Test");
			videoParams.Add("file_name", "my_video.avi");
			videoParams.Add("asset_type", "video");
			videoParams.Add("file_size", "199895");
			videoParams.Add ("chunk_size", "100000");

			Dictionary<string, string> postParams = new Dictionary<string, string> ();
			postParams.Add ("body", JsonConvert.SerializeObject (videoParams));

			Hashtable stuff = new Hashtable ();

			var buttonUpload = FindViewById<Button> (Resource.Id.buttonUpload);       

			buttonUpload.Click += delegate {

				var test = Post("assets",postParams,stuff);
			};
		}

		public Object Get(string path, Dictionary<String, String> parameters)
		{
			var url = this.GenerateURL("GET", path, parameters, "");
			HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
			request.Method = "GET";

			if (this.GetResponse(request))
			{
			  return JsonConvert.DeserializeObject(new StreamReader(response.GetResponseStream()).ReadToEnd());
			}

			return null;
		}


		public Object Post(string path, Dictionary<String, String> parameters, Hashtable body)
		{
			String jsonBody = JsonConvert.SerializeObject(body);
			var url = this.GenerateURL("POST", path, parameters, jsonBody);

			HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
			request.Method = "POST";

			var data = System.Text.Encoding.Default.GetBytes(jsonBody);

			request.ContentLength = data.Length;

			var stream = request.GetRequestStream();

			stream.Write(data, 0, data.Length);

			stream.Close();

			if (this.GetResponse(request))
			{
				
				return JsonConvert.DeserializeObject(new StreamReader(response.GetResponseStream()).ReadToEnd());
			}

			return null;
		}

		public Object PostBytes(string path, Dictionary<String, String> parameters, System.Byte[] body, String fileName = null)
		{
			var url = this.GenerateURL("POST", path, parameters, System.Text.Encoding.Default.GetString(body));

			HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
			request.Method = "POST";
			request.AllowWriteStreamBuffering = false;
			request.Timeout = UPLOAD_TIMEOUT;
			request.SendChunked = false;

			request.ContentLength = body.Length;

			var stream = request.GetRequestStream();
			stream.Write(body, 0, body.Length);
			stream.Flush();
			stream.Close();

			if (this.GetResponse(request))
			{
				return JsonConvert.DeserializeObject(new StreamReader(response.GetResponseStream()).ReadToEnd());
			}

			return null;
		}

		public Object PutBytes(string path, Dictionary<String, String> parameters, System.Byte[] body, String fileName = null)
		{
			var url = this.GenerateURL("PUT", path, parameters, System.Text.Encoding.Default.GetString(body));

			System.Net.Cache.RequestCachePolicy requestCachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);

			HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
			request.CachePolicy = requestCachePolicy;
			request.Method = "PUT";
			request.AllowWriteStreamBuffering = false;
			request.Timeout = UPLOAD_TIMEOUT;
			request.SendChunked = false;

			request.ContentLength = body.Length;

			var stream = request.GetRequestStream();
			stream.Write(body, 0, body.Length);
			stream.Flush();
			stream.Close();

			if (this.GetResponse(request))
			{
				return JsonConvert.DeserializeObject(new StreamReader(response.GetResponseStream()).ReadToEnd());
			}

			return null;
		}

		public Object Patch(string path, Dictionary<String, String> parameters, Hashtable body)
		{
			var url = this.GenerateURL("PATCH", path, parameters, body);
			String jsonBody = JsonConvert.SerializeObject(body);

			HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
			request.Method = "PATCH";
			request.ContentType = "application/x-www-form-urlencoded";

			var data = System.Text.Encoding.UTF8.GetBytes(jsonBody);

			request.ContentLength = data.Length;

			var stream = request.GetRequestStream();

			stream.Write(data, 0, data.Length);

			stream.Close();

			if (this.GetResponse(request))
			{
				return JsonConvert.DeserializeObject(new StreamReader(response.GetResponseStream()).ReadToEnd());
			}

			return null;
		}


		public Object Put(string path, Dictionary<String, String> parameters, Hashtable body)
		{
			var url = this.GenerateURL("PUT", path, parameters, body);

			String jsonBody = JsonConvert.SerializeObject(body);

			HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
			request.Method = "PUT";
			request.ContentType = "application/x-www-form-urlencoded";

			var data = System.Text.Encoding.UTF8.GetBytes(jsonBody);

			request.ContentLength = data.Length;

			var stream = request.GetRequestStream();

			stream.Write(data, 0, data.Length);

			stream.Close();

			if (this.GetResponse(request))
			{
				return JsonConvert.DeserializeObject(new StreamReader(response.GetResponseStream()).ReadToEnd());
			}

			return null;
		}

		public Boolean Delete(string path, Dictionary<String, String> parameters)
		{
			var url = this.GenerateURL("DELETE", path, parameters, "");

			HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
			request.Method = "DELETE";

			return this.GetResponse(request);
		}

		private string GenerateURL(string HTTPMethod, string path, Dictionary<System.String, System.String> parameters, Hashtable body)
		{
			return GenerateURL(HTTPMethod, path, parameters, JsonConvert.SerializeObject(body));
		}

		private string GenerateURL(string HTTPMethod, string path, Dictionary<System.String, System.String> parameters, String body)
		{
			var url = BASE_URL + path;

			path = "/v2/" + path;

			if (!parameters.ContainsKey("api_key"))
			{
				parameters.Add("api_key", this.apiKey);
			}

			if (!parameters.ContainsKey("expires"))
			{
				DateTime now = DateTime.UtcNow;
				//Round up to the expiration to the next hour for higher caching performance
				DateTime expiresWindow = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
				expiresWindow = expiresWindow.AddHours(1);
				int expires = (int)(expiresWindow - new DateTime(1970, 1, 1)).TotalSeconds;
				parameters.Add("expires", expires.ToString());
			}

			//Sorting the keys
			var sortedKeys = new String[parameters.Keys.Count];
			parameters.Keys.CopyTo(sortedKeys, 0);
			Array.Sort(sortedKeys);

			for (int i = 0; i < sortedKeys.Length; i++)
			{
				url += (i == 0 && !url.Contains("?") ? "?" : "&") + sortedKeys[i] + "=" + parameters[sortedKeys[i]];
			}

			//url += "&signature=" + this.GenerateRequestSignature(HTTPMethod, path, sortedKeys, parameters, body);

			return url;
		}

		internal string GenerateRequestSignature(string HTTPMethod, String path, String[] sortedParameterKeys, Dictionary<String, String> parameters, String body)
		{
			var stringToSign = this.secretKey + HTTPMethod + path;

			for (int i = 0; i < sortedParameterKeys.Length; i++)
			{
				stringToSign += sortedParameterKeys[i] + "=" + parameters[sortedParameterKeys[i]];
			}

			stringToSign += body;

			var sha256 = new SHA256Managed();
			byte[] digest = sha256.ComputeHash(System.Text.Encoding.Default.GetBytes(stringToSign));
			string signedInput = Convert.ToBase64String(digest);

			//Removing the trailing = signs
			var lastEqualsSignindex = signedInput.Length - 1;
			while (signedInput[lastEqualsSignindex] == '=')
			{
				lastEqualsSignindex--;
			}

			signedInput = signedInput.Substring(0, lastEqualsSignindex + 1);

			return Uri.EscapeUriString(signedInput.Substring(0, 43));
		}

		private Boolean GetResponse(HttpWebRequest request)
		{
			try
			{
				response = request.GetResponse() as HttpWebResponse;
				return true;
			}
			catch (WebException e)
			{
				Console.WriteLine("Exception Message :" + e.Message);
				if (e.Status == WebExceptionStatus.ProtocolError)
				{
					var response = ((HttpWebResponse)e.Response);
					Console.WriteLine("Status Code : {0}", ((HttpWebResponse)e.Response).StatusCode);
					Console.WriteLine("Status Description : {0}", ((HttpWebResponse)e.Response).StatusDescription);

					var stream = response.GetResponseStream();
					var reader = new StreamReader(stream);
					var text = reader.ReadToEnd();
					Console.WriteLine("Response Description : {0}", text);

				}
				return false;
			}
		}

		protected override void OnDestroy ()
		{
			base.OnDestroy ();

		}
	}
}

                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    