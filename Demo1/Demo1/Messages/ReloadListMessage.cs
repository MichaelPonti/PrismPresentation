using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.Messages
{
	public enum OperationEnum
	{
		Add,
		Delete,
		Update,
		Refresh,
	}

	public class ReloadListMessageArgs
	{
		public OperationEnum Operation { get; private set; }
		public string OriginatingPage { get; private set; }

		public ReloadListMessageArgs(string origPage, OperationEnum op)
		{
			OriginatingPage = origPage;
			Operation = op;
		}
	}

	public class ReloadListMessage : PubSubEvent<ReloadListMessageArgs>
	{
	}
}
