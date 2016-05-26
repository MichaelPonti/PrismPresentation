using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.Messages
{
	public class NavigationMessageArgs
	{
		public string PageToken { get; private set; } = null;
		public object NavParameters { get; private set; } = null;

		public NavigationMessageArgs(string pageToken, object navParameters = null)
		{
			PageToken = pageToken;
			NavParameters = navParameters;
		}
	}


	public class NavigationMessage : PubSubEvent<NavigationMessageArgs>
	{
	}
}
