using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using WallSwitch.WidgetInterface;

namespace WallSwitch.Widgets
{
	[DisplayName("Sys Info")]
	public class SysInfo : IWidget
	{
		private SysInfoProperties _props = new SysInfoProperties();

		private int _row;
		private Rectangle _bounds;
		private Graphics _g;
		private Font _labelFont;
		private Font _valueFont;
		private bool _sample;
		private Brush _labelBrush;
		private Brush _valueBrush;

		private const int k_defaultWidth = 500;
		private const int k_defaultHeight = 500;
		private const int k_rowHeight = 20;
		private const string k_error = "(error)";
		private const string k_unknown = "(unknown)";
		private const string k_sample = "(???)";

		private enum ValueType
		{
			None,
			BootTime,
			CPU,
			DefaultGateway,
			DhcpServer,
			DnsServer,
			FreeSpace,
			HostName,
			IeVersion,
			IpAddress,
			LogonDomain,
			LogonServer,
			MacAddress,
			MachineDomain,
			Memory,
			NetworkCard,
			NetworkSpeed,
			NetworkType,
			OsVersion,
			ServicePack,
			SnapshotTime,
			SubnetMask,
			SystemType,
			UserName,
			Volumes
		}

		public void Load(WidgetConfig config)
		{
			_props.Load(config);
		}

		public void Save(WidgetConfig config)
		{
			_props.Save(config);
		}

		public Rectangle GetPreferredBounds(ScreenList screens)
		{
			var prim = screens.Primary.Bounds;
			return new Rectangle((prim.Width - k_defaultWidth) / 2 + prim.Left, (prim.Height - k_defaultHeight) / 2 + prim.Top, k_defaultWidth, k_defaultHeight);
		}

		public void Draw(WidgetDrawArgs args)
		{
			_bounds = args.Bounds;
			_row = _bounds.Top;
			_g = args.Graphics;
			_sample = args.Sample;

			_labelFont = new Font(FontFamily.GenericSansSerif, k_rowHeight, FontStyle.Bold, GraphicsUnit.Pixel);
			_valueFont = new Font(FontFamily.GenericSansSerif, k_rowHeight, FontStyle.Regular, GraphicsUnit.Pixel);
			_labelBrush = new SolidBrush(_props.LabelColor);
			_valueBrush = new SolidBrush(_props.ValueColor);

			if (_props.HostName) DrawItem("Host Name:", ValueType.HostName);
			if (_props.LogonDomain) DrawItem("Logon Domain:", ValueType.LogonDomain);
			if (_props.IpAddress) DrawItem("IP Address:", ValueType.IpAddress);
			if (_props.MachineDomain) DrawItem("Machine Domain:", ValueType.MachineDomain);
			if (_props.MacAddress) DrawItem("MAC Address:", ValueType.MacAddress);
			if (_props.BootTime) DrawItem("Boot Time:", ValueType.BootTime);
			if (_props.Cpu) DrawItem("CPU:", ValueType.CPU);
			if (_props.DefaultGateway) DrawItem("Default Gateway:", ValueType.DefaultGateway);
			if (_props.DhcpServer) DrawItem("DHCP Server:", ValueType.DhcpServer);
			if (_props.DnsServer) DrawItem("DNS Server:", ValueType.DnsServer);
			if (_props.FreeSpace) DrawItem("Free Space:", ValueType.FreeSpace);

			_labelFont = null;
			_valueFont = null;
			_labelBrush = null;
			_valueBrush = null;
			_g = null;
		}

		private void DrawItem(string label, ValueType vtype)
		{
			var values = GetValue(vtype);
			if (values == null || !values.Any()) values = new string[] { k_unknown };
			DrawItemWithValues(label, values);
		}

		private void DrawItemWithValues(string label, IEnumerable<string> values)
		{
			var valueLeft = _bounds.Width / 2 + _bounds.Left;

			_g.DrawString(label, _labelFont, _labelBrush, new PointF(_bounds.Left, _row));

			var gotValue = false;
			foreach (var value in values)
			{
				if (string.IsNullOrWhiteSpace(value)) continue;

				gotValue = true;

				_g.DrawString(value, _valueFont, _valueBrush, new PointF(valueLeft, _row));
				_row += k_rowHeight;
			}

			if (!gotValue) _row += k_rowHeight;
		}

		public bool IsFixedSize
		{
			get { return false; }
		}

		public void OnBoundsChanged(WidgetBoundsChangedArgs args)
		{
		}

		private IEnumerable<string> GetValue(ValueType vtype)
		{
			if (_sample)
			{
				return new string[] { k_sample };
			}

			switch (vtype)
			{
				case ValueType.BootTime:
					return new string[] { GetBootTime().ToString() };
				case ValueType.CPU:
					return new string[] { GetCpu() };
				case ValueType.DefaultGateway:
					return new string[] { GetDefaultGateway() };
				case ValueType.DhcpServer:
					return GetDhcpServer();
				case ValueType.DnsServer:
					return GetDnsServer();
				case ValueType.MacAddress:
					return GetMacAddress();
				case ValueType.FreeSpace:
					return GetFreeSpace();
				case ValueType.HostName:
					return new string[] { Environment.MachineName };
				case ValueType.IpAddress:
					return GetIpAddress();
				case ValueType.LogonDomain:
					return new string[] { GetUserDomain() };
				case ValueType.MachineDomain:
					return new string[] { GetMachineDomain() };
			}

			return new string[0];
		}

		private string StringListToString(IEnumerable<string> list)
		{
			var sb = new StringBuilder();
			foreach (var item in list)
			{
				if (string.IsNullOrEmpty(item)) continue;
				if (sb.Length > 0) sb.Append(", ");
				sb.Append(item);
			}
			return sb.ToString();
		}

		public object Properties
		{
			get { return _props; }
		}

		public class SysInfoProperties
		{
			[Category("Data")]
			public bool HostName { get; set; }

			[Category("Data")]
			public bool LogonDomain { get; set; }

			[Category("Data")]
			public bool IpAddress { get; set; }

			[Category("Data")]
			public bool MachineDomain { get; set; }

			[Category("Data")]
			public bool MacAddress { get; set; }

			[Category("Data")]
			public bool BootTime { get; set; }

			[Category("Data")]
			public bool Cpu { get; set; }

			[Category("Data")]
			public bool DefaultGateway { get; set; }

			[Category("Data")]
			public bool DhcpServer { get; set; }

			[Category("Data")]
			public bool DnsServer { get; set; }

			[Category("Data")]
			public bool FreeSpace { get; set; }

			[Category("Appearance")]
			public Color LabelColor { get; set; }

			[Category("Appearance")]
			public Color ValueColor { get; set; }

			public SysInfoProperties()
			{
				HostName = true;
				LogonDomain = true;
				IpAddress = true;
				MachineDomain = true;
				MacAddress = true;
				BootTime = true;
				Cpu = true;
				DefaultGateway = true;
				DhcpServer = true;
				DnsServer = true;
				FreeSpace = true;

				LabelColor = Color.Silver;
				ValueColor = Color.White;
			}

			public void Save(WidgetConfig config)
			{
				config["HostName"] = HostName.ToString();
				config["LogonDomain"] = LogonDomain.ToString();
				config["IpAddress"] = IpAddress.ToString();
				config["MachineDomain"] = MachineDomain.ToString();
				config["MacAddress"] = MacAddress.ToString();
				config["BootTime"] = BootTime.ToString();
				config["Cpu"] = Cpu.ToString();
				config["DefaultGateway"] = DefaultGateway.ToString();
				config["DhcpServer"] = DhcpServer.ToString();
				config["DnsServer"] = DnsServer.ToString();
				config["FreeSpace"] = FreeSpace.ToString();

				config["LabelColor"] = ColorTranslator.ToHtml(LabelColor);
				config["ValueColor"] = ColorTranslator.ToHtml(ValueColor);
			}

			public void Load(WidgetConfig config)
			{
				HostName = GetConfigBool(config, "HostName", true);
				LogonDomain = GetConfigBool(config, "LogonDomain", true);
				IpAddress = GetConfigBool(config, "IpAddress", true);
				MachineDomain = GetConfigBool(config, "MachineDomain", true);
				MacAddress = GetConfigBool(config, "MacAddress", true);
				BootTime = GetConfigBool(config, "BootTime", true);
				Cpu = GetConfigBool(config, "Cpu", true);
				DefaultGateway = GetConfigBool(config, "DefaultGateway", true);
				DhcpServer = GetConfigBool(config, "DhcpServer", true);
				DnsServer = GetConfigBool(config, "DnsServer", true);
				FreeSpace = GetConfigBool(config, "FreeSpace", true);

				LabelColor = GetConfigColor(config, "LabelColor", Color.Silver);
				ValueColor = GetConfigColor(config, "ValueColor", Color.White);
			}

			private bool GetConfigBool(WidgetConfig config, string name, bool defVal)
			{
				var item = config.TryGetItem(name);
				if (item == null) return defVal;

				bool value;
				if (!bool.TryParse(item.Value, out value)) return defVal;

				return value;
			}

			private Color GetConfigColor(WidgetConfig config, string name, Color defVal)
			{
				var item = config.TryGetItem(name);
				if (item == null) return defVal;

				try
				{
					return ColorTranslator.FromHtml(item.Value);
				}
				catch (Exception)
				{
					return defVal;
				}
			}
		}

		#region O/S
		[DllImport("kernel32")]
		extern static UInt64 GetTickCount64();

		private DateTime? _bootTime;

		public DateTime GetBootTime()
		{
			if (!_bootTime.HasValue)
			{
				try
				{
					var upTime = TimeSpan.FromMilliseconds(GetTickCount64());
					_bootTime = DateTime.Now.Subtract(upTime);
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.ToString());
					_bootTime = DateTime.MinValue;
				}
			}
			return _bootTime.Value;
		}
		#endregion

		#region CPU
		private string _cpu;

		public string GetCpu()
		{
			if (_cpu == null)
			{
				if (_sample) return k_sample;
				try
				{
					var mgmtClass = new ManagementClass("Win32_Processor");
					var mgmtObjects = mgmtClass.GetInstances();
					foreach (var obj in mgmtObjects)
					{
						_cpu = obj.Properties["Description"].Value.ToString();
						return _cpu;
					}
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.ToString());
					_cpu = k_error;
				}
			}
			return _cpu;
		}
		#endregion

		#region Network
		private bool _networkInfoInit;
		private string _defaultGateway;
		private string[] _dhcpServer;
		private string[] _dnsServer;
		private string[] _macAddress;
		private string[] _ipAddress;

		private void GetNetworkInfo()
		{
			try
			{
				var netInts = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
				var defInt = (from n in netInts where n.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up select n).FirstOrDefault();
				if (defInt == null)
				{
					_defaultGateway = k_unknown;
				}
				else
				{
					var gatewayAddr = defInt.GetIPProperties().GatewayAddresses.FirstOrDefault();
					if (gatewayAddr == null) _defaultGateway = k_unknown;
					else _defaultGateway = gatewayAddr.Address.ToString();
				}

				var dhcpServers = new List<string>();
				var dnsAddrs = new List<string>();
				var macAddrs = new List<string>();
				var ipAddrs = new List<string>();

				foreach (var netInt in (from n in netInts where n.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up select n))
				{
					var ipProps = netInt.GetIPProperties();

					foreach (var dhcpAddr in ipProps.DhcpServerAddresses)
					{
						var addr = dhcpAddr.ToString();
						if (!dhcpServers.Contains(addr)) dhcpServers.Add(addr);
					}

					foreach (var dnsAddr in ipProps.DnsAddresses)
					{
						var addr = dnsAddr.ToString();
						if (!dnsAddrs.Contains(addr)) dnsAddrs.Add(addr);
					}

					macAddrs.Add(FormatPhysicalAddress(netInt.GetPhysicalAddress().GetAddressBytes()));

					foreach (var uniAddr in ipProps.UnicastAddresses)
					{
						if (uniAddr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork ||
							uniAddr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
						{
							ipAddrs.Add(uniAddr.Address.ToString());
						}
					}
				}

				_dhcpServer = dhcpServers.ToArray();
				_dnsServer = dnsAddrs.ToArray();
				_macAddress = macAddrs.ToArray();
				_ipAddress = ipAddrs.ToArray();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				_defaultGateway = k_error;
			}
			_networkInfoInit = true;
		}

		private string GetDefaultGateway()
		{
			if (_networkInfoInit == false) GetNetworkInfo();
			return _defaultGateway;
		}

		private string[] GetDhcpServer()
		{
			if (_networkInfoInit == false) GetNetworkInfo();
			return _dhcpServer;
		}

		private string[] GetMacAddress()
		{
			if (_networkInfoInit == false) GetNetworkInfo();
			return _macAddress;
		}

		private string[] GetDnsServer()
		{
			if (_networkInfoInit == false) GetNetworkInfo();
			return _dnsServer;
		}

		private string[] GetIpAddress()
		{
			if (_networkInfoInit == false) GetNetworkInfo();
			return _ipAddress;
		}

		private string FormatPhysicalAddress(IEnumerable<byte> bytes)
		{
			var sb = new StringBuilder();
			foreach (var b in bytes)
			{
				if (sb.Length > 0) sb.Append("-");
				sb.Append(b.ToString("X2"));
			}
			return sb.ToString();
		}
		#endregion

		#region Disk
		private string[] _freeSpace;

		private string[] GetFreeSpace()
		{
			if (_freeSpace == null)
			{
				try
				{
					var freeSpace = new List<string>();
					foreach (var driveInfo in DriveInfo.GetDrives())
					{
						if (driveInfo.IsReady) freeSpace.Add(string.Concat(driveInfo.Name, " ", driveInfo.TotalFreeSpace.ToDataSizeString()));
					}
					_freeSpace = freeSpace.ToArray();
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.ToString());
					_freeSpace = new string[0];
				}
			}
			return _freeSpace;
		}
		#endregion

		#region Domains
		private string _machineDomain;

		private string GetUserDomain()
		{
			return Environment.UserDomainName;
		}

		[DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		static extern int NetGetJoinInformation(
		  string server,
		  out IntPtr domain,
		  out NetJoinStatus status);

		// Win32 Result Code Constant
		const int ErrorSuccess = 0;

		// NetGetJoinInformation() Enumeration
		public enum NetJoinStatus
		{
			NetSetupUnknownStatus = 0,
			NetSetupUnjoined,
			NetSetupWorkgroupName,
			NetSetupDomainName
		} // NETSETUP_JOIN_STATUS

		private string GetMachineDomain()
		{
			if (_machineDomain == null)
			{
				try
				{
					IntPtr pDomain;
					NetJoinStatus status;
					var result = NetGetJoinInformation(null, out pDomain, out status);
					if (result == 0) _machineDomain = Marshal.PtrToStringUni(pDomain);
					else _machineDomain = k_unknown;
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.ToString());
					_machineDomain = k_error;
				}
			}
			return _machineDomain;
		}
		#endregion

	}
}
