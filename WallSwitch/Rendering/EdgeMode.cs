using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace WallSwitch
{
	public enum EdgeMode
	{
		[Description("Hard Edge")]
		None,
		[Description("Feathered Edge")]
		Feather,
		[Description("Solid Border")]
		SolidBorder
	}
}
