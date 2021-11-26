package crc64fd1c931c7ceb70db;


public class NumericUpDown
	extends crc647afef5a3fc29c4f6.SfNumericUpDownRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("SEMS_APP.Droid.Renderer.NumericUpDown, SEMS_APP.Android", NumericUpDown.class, __md_methods);
	}


	public NumericUpDown (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == NumericUpDown.class)
			mono.android.TypeManager.Activate ("SEMS_APP.Droid.Renderer.NumericUpDown, SEMS_APP.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public NumericUpDown (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == NumericUpDown.class)
			mono.android.TypeManager.Activate ("SEMS_APP.Droid.Renderer.NumericUpDown, SEMS_APP.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public NumericUpDown (android.content.Context p0)
	{
		super (p0);
		if (getClass () == NumericUpDown.class)
			mono.android.TypeManager.Activate ("SEMS_APP.Droid.Renderer.NumericUpDown, SEMS_APP.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
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
