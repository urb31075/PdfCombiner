// --------------------------------------------------------------------------------------------------------------------
// <copyright File="MainService.cs" company="urb31075">
//  All Right Reserved 
// </copyright>
// <summary>
//   The main service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace PdfCombiner
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using System.ServiceProcess;

    using PdfCombinerWcf;

    // https://msdn.microsoft.com/ru-ru/library/vstudio/zt39148a%28v=vs.110%29.aspx
    // installutil.exe Service.exe

    /// <summary>
    /// The main service.
    /// </summary>
    public partial class PdfCombinerWindowsService : ServiceBase
    {
        private const string HostUri = @"http://localhost:47523/PdfCombineService";

        private ServiceHost host;

        public enum ServiceState
        {
            ServiceStopped = 0x00000001,
            ServiceStartPending = 0x00000002,
            ServiceStopPending = 0x00000003,
            ServiceRunning = 0x00000004,
            ServiceContinuePending = 0x00000005,
            ServicePausePending = 0x00000006,
            ServicePaused = 0x00000007,
        }

        public System.Timers.Timer WorkTimer { get; set; }

        public bool WorkFlag { get; set; }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

        /// <summary>
        /// The event log.
        /// </summary>
        private readonly EventLog eventLog;

        /// <summary>
        /// The event id.
        /// </summary>
        private int eventId = 1;

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        private readonly string debugLogFileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfCombinerWindowsService"/> class.
        /// </summary>
        public PdfCombinerWindowsService(string[] args)
        {
            this.InitializeComponent();
            var eventSourceName = "MySource";
            var logName = "MyNewLog";
            if (args.Any())
            {
                eventSourceName = args[0];
            }
            if (args.Length > 1)
            {
                logName = args[1];
            }


            this.eventLog = new EventLog();
            if (!EventLog.SourceExists("ControlSPSource"))
            {
                EventLog.CreateEventSource("ControlSPSource", "ControlSPNewLog");
            }

            this.eventLog.Source = eventSourceName;
            this.eventLog.Log = logName;
            this.debugLogFileName = @"C:\\PdfCombinerDebugLog.txt";
        }

        /// <summary>
        /// The on timer.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="args">
        /// The args.
        /// </param>
        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            this.eventLog.WriteEntry("ControlSP Monitoring the System", EventLogEntryType.Information, this.eventId++);
            this.WriteDebugLog($"ControlSP Monitoring the System {DateTime.Now}");
        }

        /// <summary>
        /// The on start.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.  
            var serviceStatus = new ServiceStatus
                {
                    dwCurrentState = ServiceState.ServiceStartPending,
                    dwWaitHint = 100000
                };
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            this.eventLog.WriteEntry("In OnStart");
            this.WriteDebugLog("OnStart");
            this.WorkTimer = new System.Timers.Timer { Interval = 60000 }; // 60 seconds  
            this.WorkTimer.Elapsed += this.OnTimer;
            this.WorkTimer.Start();

            // Update the service state to Running.  
            serviceStatus.dwCurrentState = ServiceState.ServiceRunning;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
            this.WorkFlag = true;

            if (this.host != null)
            {
                return;
            }

            var baseAddress = new Uri(HostUri);
            this.host = new ServiceHost(typeof(PdfCombinerCommunication), baseAddress);

            var smb = new ServiceMetadataBehavior
            {
                HttpGetEnabled = true,
                MetadataExporter = { PolicyVersion = PolicyVersion.Policy15 }
            };
            this.host.Description.Behaviors.Add(smb);
            this.host.Open();
            this.WriteDebugLog($"The service is ready at {HostUri}");

        }

        /// <summary>
        /// The on stop.
        /// </summary>
        protected override void OnStop()
        {
            this.WorkFlag = false;
            this.WorkTimer.Stop();
            this.eventLog.WriteEntry("In OnStop");
            this.WriteDebugLog("OnStop");
            if (this.host == null)
            {
                return;
            }

            this.host.Close();
            this.WriteDebugLog($"The service is closed at {HostUri}");
        }

        /// <summary>
        /// The on pause.
        /// </summary>
        protected override void OnPause()
        {
            this.WorkTimer.Stop();
            this.eventLog.WriteEntry("In OnPause");
            this.WriteDebugLog("OnPause");
        }

        /// <summary>
        /// The on continue.
        /// </summary>
        protected override void OnContinue()
        {
            this.WorkTimer.Start();
            this.eventLog.WriteEntry("In OnContinue");
            this.WriteDebugLog("OnContinue");
        }

        public void WriteDebugLog(string text)
        {
            var file = File.AppendText(this.debugLogFileName);
            file.WriteLine(text);
            file.Close();
        }
    }
}
