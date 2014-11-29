using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace WallSwitch
{
	public class WallSwitchServiceManager : IDisposable
	{
		private const string k_url = "net.pipe://localhost/WallSwitch/service";

		private Thread _thread = null;
		private EventWaitHandle _killWait = new EventWaitHandle(false, EventResetMode.ManualReset);

		public void CreateService()
		{
			if (_thread != null)
			{
				_killWait.Set();
				_thread.Join();
			}

			_killWait.Reset();

			_thread = new Thread(new ThreadStart(ServiceThread));
			_thread.Name = "Service Thread";
			_thread.Priority = ThreadPriority.BelowNormal;
			_thread.Start();
		}

		public void Dispose()
		{
			if (_thread != null)
			{
				_killWait.Set();
				_thread.Join();
				_thread = null;
			}
		}

		private void ServiceThread()
		{
			try
			{
				Log.Write(LogLevel.Info, "Service thread is starting");

				var uri = new Uri(k_url);
				using (var host = new ServiceHost(typeof(WallSwitchService), new Uri[] { uri }))
				{
					var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
					host.AddServiceEndpoint(typeof(IWallSwitchService), binding, k_url);
					host.Open();

					_killWait.WaitOne();
				}

				Log.Write(LogLevel.Info, "Service thread is ending");
			}
			catch (Exception ex)
			{
				Log.Write(ex, "Exception in service thread.");
			}
		}

		public static IWallSwitchService GetClient()
		{
			try
			{
				var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
				var cf = new ChannelFactory<IWallSwitchService>(binding, k_url);
				return cf.CreateChannel();
			}
			catch (Exception ex)
			{
				//Log.Write(ex, "Error when attempting to get WallSwitchService client.");
				System.Windows.Forms.MessageBox.Show(ex.ToString());
				return null;
			}
		}
	}
}
