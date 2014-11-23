using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using WidgetInterface;

namespace WallSwitchWidgets
{
	public class SysInfo : IWidget
	{
		private int _row;
		private Rectangle _bounds;
		private Graphics _g;
		private Font _labelFont;
		private Font _valueFont;
		private bool _sample;

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
		}

		public void Save(WidgetConfig config)
		{
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

			/*
			Boot Time:			<Boot Time>				done
			CPU:				<CPU>					done
			Default Gateway:	<Default Gateway>		done
			DHCP Server:		<DHCP Server>			done
			DNS Server:			<DNS Server>			done
			Free Space:			<Free Space>			done
			Host Name:			<Host Name>				done
			IE Version:			<IE Version>
			IP Address:			<IP Address>			done
			Logon Domain:		<Logon Domain>			done
			Logon Server:		<Logon Server>
			MAC Address:		<MAC Address>			done
			Machine Domain:		<Machine Domain>		done
			Memory:				<Memory>
			Network Card:		<Network Card>
			Network Speed:		<Network Speed>
			Network Type:		<Network Type>
			OS Version:			<OS Version>
			Service Pack:		<Service Pack>
			Snapshot Time:		<Snapshot Time>
			Subnet Mask:		<Subnet Mask>
			System Type:		<System Type>
			User Name:			<User Name>
			Volumes:			<Volumes>
			*/

			DrawItem("Boot Time:", ValueType.BootTime);
			DrawItem("CPU:", ValueType.CPU);
			DrawItem("Default Gateway:", ValueType.DefaultGateway);
			DrawItem("DHCP Server:", ValueType.DhcpServer);
			DrawItem("DNS Server:", ValueType.DnsServer);
			DrawItem("Free Space:", ValueType.FreeSpace);
			DrawItem("Host Name:", ValueType.HostName);
			DrawItem("IP Address:", ValueType.IpAddress);
			DrawItem("Logon Domain:", ValueType.LogonDomain);
			DrawItem("MAC Address:", ValueType.MacAddress);
			DrawItem("Machine Domain:", ValueType.MachineDomain);
			

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

			_g.DrawString(label, _labelFont, Brushes.White, new PointF(_bounds.Left, _row));

			var gotValue = false;
			foreach (var value in values)
			{
				if (string.IsNullOrWhiteSpace(value)) continue;

				gotValue = true;

				_g.DrawString(value, _valueFont, Brushes.White, new PointF(valueLeft, _row));
				_row += k_rowHeight;
			}

			if (!gotValue) _row += k_rowHeight;
		}

		public bool IsFixedSize
		{
			get { return false; }
		}

		public void OnSizeChanged(WidgetSizeChangedArgs args)
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
		private string _logonDomain;
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
