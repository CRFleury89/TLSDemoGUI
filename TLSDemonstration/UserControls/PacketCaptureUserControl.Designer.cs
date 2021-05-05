using System.Windows.Forms;

namespace TLSDemonstration.UserControls
{
    partial class PacketCaptureUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PacketCaptureUserControl));
            this.lvMessages = new System.Windows.Forms.ListView();
            this.MsgTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MsgSource = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MsgSrcPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MsgDestination = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MsgDestPort = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MsgProtocol = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MsgSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnStart = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.tbWebAddress = new System.Windows.Forms.TextBox();
            this.lblWebsite = new System.Windows.Forms.Label();
            this.lblWebIP = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cobInterfaces = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvMessages
            // 
            this.lvMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.MsgTime,
            this.MsgSource,
            this.MsgSrcPort,
            this.MsgDestination,
            this.MsgDestPort,
            this.MsgProtocol,
            this.MsgSize});
            this.lvMessages.FullRowSelect = true;
            this.lvMessages.GridLines = true;
            this.lvMessages.HideSelection = false;
            this.lvMessages.Location = new System.Drawing.Point(3, 139);
            this.lvMessages.Name = "lvMessages";
            this.lvMessages.Size = new System.Drawing.Size(552, 230);
            this.lvMessages.TabIndex = 0;
            this.lvMessages.UseCompatibleStateImageBehavior = false;
            this.lvMessages.View = System.Windows.Forms.View.Details;
            // 
            // MsgTime
            // 
            this.MsgTime.Text = "Time";
            this.MsgTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MsgTime.Width = 80;
            // 
            // MsgSource
            // 
            this.MsgSource.Text = "Source";
            this.MsgSource.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MsgSource.Width = 100;
            // 
            // MsgSrcPort
            // 
            this.MsgSrcPort.Text = "Src Port";
            this.MsgSrcPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MsgDestination
            // 
            this.MsgDestination.Text = "Destination";
            this.MsgDestination.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MsgDestination.Width = 100;
            // 
            // MsgDestPort
            // 
            this.MsgDestPort.Text = "Dest Port";
            this.MsgDestPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MsgProtocol
            // 
            this.MsgProtocol.Text = "Protocol";
            this.MsgProtocol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MsgSize
            // 
            this.MsgSize.Text = "Pkg Size";
            this.MsgSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnStart,
            this.btnStop});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(575, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnStart
            // 
            this.btnStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStart.Image = ((System.Drawing.Image)(resources.GetObject("btnStart.Image")));
            this.btnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(23, 22);
            this.btnStart.Text = "toolStripButton1";
            this.btnStart.ToolTipText = "Start";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(23, 22);
            this.btnStop.Text = "toolStripButton2";
            this.btnStop.ToolTipText = "Stop";
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // tbWebAddress
            // 
            this.tbWebAddress.Location = new System.Drawing.Point(7, 84);
            this.tbWebAddress.Name = "tbWebAddress";
            this.tbWebAddress.Size = new System.Drawing.Size(227, 20);
            this.tbWebAddress.TabIndex = 8;
            // 
            // lblWebsite
            // 
            this.lblWebsite.AutoSize = true;
            this.lblWebsite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWebsite.Location = new System.Drawing.Point(7, 67);
            this.lblWebsite.Name = "lblWebsite";
            this.lblWebsite.Size = new System.Drawing.Size(169, 13);
            this.lblWebsite.TabIndex = 9;
            this.lblWebsite.Text = "Website (e.g. Wikipedia.org)";
            // 
            // lblWebIP
            // 
            this.lblWebIP.AutoSize = true;
            this.lblWebIP.Location = new System.Drawing.Point(63, 114);
            this.lblWebIP.Name = "lblWebIP";
            this.lblWebIP.Size = new System.Drawing.Size(27, 13);
            this.lblWebIP.TabIndex = 11;
            this.lblWebIP.Text = "N/A";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Web IP:";
            // 
            // cobInterfaces
            // 
            this.cobInterfaces.FormattingEnabled = true;
            this.cobInterfaces.Location = new System.Drawing.Point(75, 39);
            this.cobInterfaces.Name = "cobInterfaces";
            this.cobInterfaces.Size = new System.Drawing.Size(159, 21);
            this.cobInterfaces.TabIndex = 12;
            this.cobInterfaces.SelectedIndexChanged += new System.EventHandler(this.cobInterfaces_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Interface:";
            // 
            // PacketCaptureUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cobInterfaces);
            this.Controls.Add(this.lblWebIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblWebsite);
            this.Controls.Add(this.tbWebAddress);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.lvMessages);
            this.Name = "PacketCaptureUserControl";
            this.Size = new System.Drawing.Size(575, 549);
            this.Load += new System.EventHandler(this.PacketCaptureUserControl_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvMessages;
        private System.Windows.Forms.ColumnHeader MsgTime;
        private System.Windows.Forms.ColumnHeader MsgSource;
        private System.Windows.Forms.ColumnHeader MsgSrcPort;
        private System.Windows.Forms.ColumnHeader MsgDestination;
        private System.Windows.Forms.ColumnHeader MsgDestPort;
        private System.Windows.Forms.ColumnHeader MsgProtocol;
        private System.Windows.Forms.ColumnHeader MsgSize;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnStart;
        private System.Windows.Forms.ToolStripButton btnStop;
        private TextBox tbWebAddress;
        private Label lblWebsite;
        private Label lblWebIP;
        private Label label2;
        private ComboBox cobInterfaces;
        private Label label1;
    }
}
