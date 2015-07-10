
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

namespace videouploadservice
{
	[Activity (Label = "@string/app_name", MainLauncher = true, Icon="@drawable/ic_launcher")]		
	public class MainActivity : TabActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView(Resource.Layout.Main);
			CreateTab(typeof(VideoActivity), "video", "Video", Resource.Drawable.ic_tab_video);
			CreateTab(typeof(UploadActivity), "upload", "Upload", Resource.Drawable.ic_tab_upload);
			CreateTab(typeof(PlayVideoActivity), "play", "play", Resource.Drawable.ic_tab_video);
			CreateTab(typeof(AdminActivity), "admin", "Admin", Resource.Drawable.ic_tab_admin);

		}

		private void CreateTab(Type activityType, string tag, string label, int drawableId )
		{
			var intent = new Intent(this, activityType);
			intent.AddFlags(ActivityFlags.NewTask);

			var spec = TabHost.NewTabSpec(tag);
			var drawableIcon = Resources.GetDrawable(drawableId);
			spec.SetIndicator(label, drawableIcon);
			spec.SetContent(intent);

			TabHost.AddTab(spec);
		}
	}
}

