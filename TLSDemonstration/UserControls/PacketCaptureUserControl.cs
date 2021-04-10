using System;
using System.ComponentModel;
using System.Windows.Forms;
using TLSDemonstration.ViewModel;
using System.Net.Sockets;
using System.Net;
using TLSDemonstration.PacketData;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TLSDemonstration.UserControls
{
    public partial class PacketCaptureUserControl : UserControl
    {
        IPAddress myIp;
        IPAddress webIp;
        Socket _socket;
        byte[] _in = new byte[4] { 1, 0, 0, 0 };
        byte[] _out = new byte[4];
        byte[] _buffer = new byte[8192];

        private bool Stopped = false;

        private BackgroundWorker Worker;
        private string process;
        Regex regex = new Regex(@"([0-9]{1,3}\.){3}[0-9]{1,3}");

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
                    lblUserIP.Text = myIp.ToString();
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
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (Stopped)
            {
                Stopped = false;
                lvMessages.Items.Clear();
            }
            process = "start";
            Worker.RunWorkerAsync();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Stopped = true;
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
            StartCapturing();
        }

        private void StartCapturing()
        {
            string hostName = Dns.GetHostName();
            myIp = Dns.GetHostAddresses(hostName)[1];
            Worker.ReportProgress(1);

            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
                _socket.Bind(new IPEndPoint(myIp, 0));
                _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);
                _socket.IOControl(IOControlCode.ReceiveAll, _in, _out);

                ProcessReceived();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }
        }

        private void ProcessReceived()
        {
            while (!Stopped)
            {
                int _bytes = _socket.Receive(_buffer, 0, _buffer.Length, SocketFlags.None);

                if (_bytes > 0 && _buffer.Length > 0)
                {
                    ConvertData(_buffer, _bytes);
                }
                Array.Clear(_buffer, 0, _buffer.Length);
            }
        }

        private void ConvertData(byte[] bfr, int rcvd)
        {
            PacketIP packet = new PacketIP(bfr, rcvd);
            ListViewItem item = new ListViewItem();

            if (packet.Protocol == "TCP" 
                && (packet.SourceAddress.ToString() ==  webIp.ToString() || packet.DestinationAddress.ToString() == webIp.ToString()))
            {
                PacketTcp tcp = new PacketTcp(packet.Data, packet.MessageLength);
                item.Text = DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString();
                item.SubItems.Add(packet.SourceAddress.ToString());
                item.SubItems.Add(tcp.SourcePort);
                item.SubItems.Add(packet.DestinationAddress.ToString());
                item.SubItems.Add(tcp.DestinationPort);
                item.SubItems.Add(packet.Protocol);
                item.SubItems.Add(packet.TotalLength);

                Worker.ReportProgress(1, item);
            }
        }
    }
}
