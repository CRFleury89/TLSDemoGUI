using System;
using System.ComponentModel;
using System.Windows.Forms;
using TLSDemonstration.ViewModel;
using System.Net.Sockets;
using System.Net;
using TLSDemonstration.PacketData;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using SharpPcap.LibPcap;
using SharpPcap;
using PacketDotNet;
using System.Threading;

namespace TLSDemonstration.UserControls
{
    public partial class PacketCaptureUserControl : UserControl
    {
        List<LibPcapLiveDevice> Interfaces = new List<LibPcapLiveDevice>();
        LibPcapLiveDevice wifi_device;
        
        Thread sniffing;

        IPAddress webIp;

        private bool Stopped = true;

        private BackgroundWorker Worker;
        private string process;
        Regex regex = new Regex(@"([0-9]{1,3}\.){3}[0-9]{1,3}");

        private List<IpPacket> testList = new List<IpPacket>();

        public PacketCaptureUserControl()
        {
            InitializeComponent();

            lvMessages.DoubleBuffered(true);

            Worker = new BackgroundWorker();
            Worker.DoWork += new DoWorkEventHandler(DoWork);
            Worker.ProgressChanged += new ProgressChangedEventHandler(ProgressChanged);
            Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);
            Worker.WorkerReportsProgress = true;
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            switch (process)
            {
                case "start":
                    ExecuteCommand();
                    break;
                case "update":
                    while (!Worker.CancellationPending) ;
                    break;
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (process)
            {
                case "start":
                    if (webIp == null)
                    {
                        btnStop.PerformClick();
                        return;
                    }

                    lblWebIP.Text = webIp.ToString();
                    process = "update";
                    break;
                case "update":
                    lvMessages.Items.Add((ListViewItem)e.UserState);
                    break;
            }
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!Stopped) Worker.RunWorkerAsync();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (Stopped)
            {
                Stopped = false;
                lvMessages.Items.Clear();
            }
            process = "start";

            wifi_device.OnPacketArrival += new PacketArrivalEventHandler(DeviceOnPacketArrival);
            sniffing = new Thread(new ThreadStart(ProcessReceived));

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            Worker.RunWorkerAsync();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Stopped = true;
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            sniffing.Abort();
            wifi_device.Close();
        }

        private void ExecuteCommand()
        {
            ProcessStartInfo processinfo;
            Process process;

            processinfo = new ProcessStartInfo("cmd.exe", "/c " + "nslookup " + tbWebAddress.Text);
            processinfo.CreateNoWindow = false;
            processinfo.UseShellExecute = false;
            processinfo.RedirectStandardOutput = true;
            process = Process.Start(processinfo);

            var output = process.StandardOutput.ReadToEnd();
            
            if (regex.Matches(output).Count > 1)
            {
                webIp = IPAddress.Parse(regex.Matches(output)[1].ToString());
            }
            else
            {
                webIp = null;
            }

            process.WaitForExit();
            process.Close();
            Worker.ReportProgress(1);
            sniffing.Start();
        }

        private void DeviceOnPacketArrival(object sender, CaptureEventArgs e)
        {
            IpPacket ipPacket = (IpPacket)Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data).Extract(typeof(IpPacket));
            ListViewItem item = new ListViewItem();
            testList.Add(ipPacket);

            if (ipPacket.Protocol.ToString() == "TCP")
            {
                PacketTcp tcp = new PacketTcp(e.Packet.Data, e.Packet.Data.Length);
                item.Text = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString();
                item.SubItems.Add(ipPacket.SourceAddress.ToString());
                item.SubItems.Add(tcp.SourcePort);
                item.SubItems.Add(ipPacket.DestinationAddress.ToString());
                item.SubItems.Add(tcp.DestinationPort);
                item.SubItems.Add(ipPacket.Protocol.ToString());
                item.SubItems.Add(ipPacket.TotalLength.ToString());
                ipPacket.

                Worker.ReportProgress(1, item);
            }
        }

        private void ProcessReceived()
        {
            wifi_device.Open(DeviceMode.Promiscuous, 1000);

            if (wifi_device.Opened)
            {
                wifi_device.Filter = "tcp and host " + webIp.ToString();
            }
            wifi_device.Capture();
        }

        private void PacketCaptureUserControl_Load(object sender, EventArgs e)
        {
            LibPcapLiveDeviceList devices = LibPcapLiveDeviceList.Instance;

            foreach (LibPcapLiveDevice device in devices)
            {
                if (!device.Interface.Addresses.Exists(a => a != null && a.Addr != null && a.Addr.ipAddress != null)) continue;
                var devInterface = device.Interface;
                var friendlyName = devInterface.FriendlyName;
                var description = devInterface.Description;

                Interfaces.Add(device);
                cobInterfaces.Items.Add(friendlyName);
            }
        }

        private void cobInterfaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Stopped)
            {
                wifi_device = Interfaces[cobInterfaces.SelectedIndex];
            }
        }
    }
}
