
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
using Android.Media;

namespace videouploadservice
{
	[Activity (Label = "PlayVideoActivity")]			
	public class PlayVideoActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.PlayVideo);

			string path = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/test.mp4";
		
			var play = FindViewById<Button> (Resource.Id.Play);       
			var video = FindViewById<VideoView> (Resource.Id.SampleVideoView);

			play.Click += delegate {

				var uri = Android.Net.Uri.Parse (path);        
				video.SetVideoURI (uri);
				video.Start ();   
			};
		}

		protected override void OnDestroy ()
		{
			base.OnDestroy ();

		}
	}
}

