package crc646b66e06abe9314ff;


public class ServiceMusicHandler
	extends android.os.Handler
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Tests_Program.ServiceMusicHandler, Tests Program", ServiceMusicHandler.class, __md_methods);
	}


	public ServiceMusicHandler ()
	{
		super ();
		if (getClass () == ServiceMusicHandler.class)
			mono.android.TypeManager.Activate ("Tests_Program.ServiceMusicHandler, Tests Program", "", this, new java.lang.Object[] {  });
	}


	public ServiceMusicHandler (android.os.Handler.Callback p0)
	{
		super (p0);
		if (getClass () == ServiceMusicHandler.class)
			mono.android.TypeManager.Activate ("Tests_Program.ServiceMusicHandler, Tests Program", "Android.OS.Handler+ICallback, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public ServiceMusicHandler (android.os.Looper p0)
	{
		super (p0);
		if (getClass () == ServiceMusicHandler.class)
			mono.android.TypeManager.Activate ("Tests_Program.ServiceMusicHandler, Tests Program", "Android.OS.Looper, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public ServiceMusicHandler (android.os.Looper p0, android.os.Handler.Callback p1)
	{
		super (p0, p1);
		if (getClass () == ServiceMusicHandler.class)
			mono.android.TypeManager.Activate ("Tests_Program.ServiceMusicHandler, Tests Program", "Android.OS.Looper, Mono.Android:Android.OS.Handler+ICallback, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}

	public ServiceMusicHandler (android.content.Context p0)
	{
		super ();
		if (getClass () == ServiceMusicHandler.class)
			mono.android.TypeManager.Activate ("Tests_Program.ServiceMusicHandler, Tests Program", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
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
