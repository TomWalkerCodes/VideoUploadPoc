package md56a95e70f3ac4629860bfba473dd6cddd;


public class AdminActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("videouploadservice.AdminActivity, videouploadservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AdminActivity.class, __md_methods);
	}


	public AdminActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == AdminActivity.class)
			mono.android.TypeManager.Activate ("videouploadservice.AdminActivity, videouploadservice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
