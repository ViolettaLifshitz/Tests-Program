package crc646b66e06abe9314ff;


public class MusicService_FirstServiceBinder
	extends android.os.Binder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Tests_Program.MusicService+FirstServiceBinder, Tests Program", MusicService_FirstServiceBinder.class, __md_methods);
	}


	public MusicService_FirstServiceBinder ()
	{
		super ();
		if (getClass () == MusicService_FirstServiceBinder.class)
			mono.android.TypeManager.Activate ("Tests_Program.MusicService+FirstServiceBinder, Tests Program", "", this, new java.lang.Object[] {  });
	}


	public MusicService_FirstServiceBinder (java.lang.String p0)
	{
		super (p0);
		if (getClass () == MusicService_FirstServiceBinder.class)
			mono.android.TypeManager.Activate ("Tests_Program.MusicService+FirstServiceBinder, Tests Program", "System.String, mscorlib", this, new java.lang.Object[] { p0 });
	}

	public MusicService_FirstServiceBinder (crc646b66e06abe9314ff.MusicService p0)
	{
		super ();
		if (getClass () == MusicService_FirstServiceBinder.class)
			mono.android.TypeManager.Activate ("Tests_Program.MusicService+FirstServiceBinder, Tests Program", "Tests_Program.MusicService, Tests Program", this, new java.lang.Object[] { p0 });
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
