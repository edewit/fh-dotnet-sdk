using System;

namespace tests
{
    /// <summary>
    /// xUnit used for xamarin doesn't need / have a TestClass attribute, this dummy is used
    /// for xamarin so that we can reuse the tests on all platforms.
    /// </summary>
	public class TestClass : System.Attribute
	{
		public TestClass ()
		{
		}
	}
}

