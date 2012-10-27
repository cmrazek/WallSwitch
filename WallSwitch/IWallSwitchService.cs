using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WallSwitch
{
	[ServiceContract]
	public interface IWallSwitchService
	{
		[OperationContract]
		void Activate();
	}
}
