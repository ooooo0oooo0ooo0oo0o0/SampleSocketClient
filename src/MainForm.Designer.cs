
namespace SampleSocketClient
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AddressLabel = new System.Windows.Forms.Label();
            this.AddressBox = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.PortBox = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.MessageBox = new System.Windows.Forms.RichTextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.SocketConnectionCheckTimer = new System.Windows.Forms.Timer(this.components);
            this.ConnectionBox = new System.Windows.Forms.GroupBox();
            this.DataBox = new System.Windows.Forms.TextBox();
            this.ConnectionBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // AddressLabel
            // 
            this.AddressLabel.AutoSize = true;
            this.AddressLabel.Location = new System.Drawing.Point(0, 44);
            this.AddressLabel.Name = "AddressLabel";
            this.AddressLabel.Size = new System.Drawing.Size(131, 25);
            this.AddressLabel.TabIndex = 0;
            this.AddressLabel.Text = "Server Address";
            // 
            // AddressBox
            // 
            this.AddressBox.Location = new System.Drawing.Point(137, 41);
            this.AddressBox.Name = "AddressBox";
            this.AddressBox.Size = new System.Drawing.Size(187, 31);
            this.AddressBox.TabIndex = 1;
            this.AddressBox.TextChanged += new System.EventHandler(this.AddressBox_TextChanged);
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(32, 81);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(99, 25);
            this.PortLabel.TabIndex = 2;
            this.PortLabel.Text = "Server Port";
            // 
            // PortBox
            // 
            this.PortBox.Location = new System.Drawing.Point(137, 78);
            this.PortBox.Name = "PortBox";
            this.PortBox.Size = new System.Drawing.Size(187, 31);
            this.PortBox.TabIndex = 3;
            this.PortBox.TextChanged += new System.EventHandler(this.PortBox_TextChanged);
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(364, 36);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(177, 75);
            this.ConnectButton.TabIndex = 4;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Location = new System.Drawing.Point(563, 36);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(177, 75);
            this.DisconnectButton.TabIndex = 5;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // MessageBox
            // 
            this.MessageBox.Location = new System.Drawing.Point(12, 177);
            this.MessageBox.Name = "MessageBox";
            this.MessageBox.ReadOnly = true;
            this.MessageBox.Size = new System.Drawing.Size(763, 261);
            this.MessageBox.TabIndex = 6;
            this.MessageBox.Text = "";
            // 
            // SendButton
            // 
            this.SendButton.Location = new System.Drawing.Point(652, 140);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(123, 31);
            this.SendButton.TabIndex = 7;
            this.SendButton.Text = "Send Data";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // SocketConnectionCheckTimer
            // 
            this.SocketConnectionCheckTimer.Interval = 2000;
            this.SocketConnectionCheckTimer.Tick += new System.EventHandler(this.SocketConnectionCheckTimer_Tick);
            // 
            // ConnectionBox
            // 
            this.ConnectionBox.Controls.Add(this.DisconnectButton);
            this.ConnectionBox.Controls.Add(this.ConnectButton);
            this.ConnectionBox.Controls.Add(this.AddressBox);
            this.ConnectionBox.Controls.Add(this.AddressLabel);
            this.ConnectionBox.Controls.Add(this.PortBox);
            this.ConnectionBox.Controls.Add(this.PortLabel);
            this.ConnectionBox.Location = new System.Drawing.Point(12, 12);
            this.ConnectionBox.Name = "ConnectionBox";
            this.ConnectionBox.Size = new System.Drawing.Size(763, 117);
            this.ConnectionBox.TabIndex = 8;
            this.ConnectionBox.TabStop = false;
            this.ConnectionBox.Text = "Connection";
            // 
            // DataBox
            // 
            this.DataBox.Location = new System.Drawing.Point(12, 140);
            this.DataBox.Name = "DataBox";
            this.DataBox.Size = new System.Drawing.Size(634, 31);
            this.DataBox.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.DataBox);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.MessageBox);
            this.Controls.Add(this.ConnectionBox);
            this.Name = "MainForm";
            this.Text = "SampleSocketClient";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ConnectionBox.ResumeLayout(false);
            this.ConnectionBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AddressLabel;
        private System.Windows.Forms.TextBox AddressBox;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.TextBox PortBox;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.RichTextBox MessageBox;
        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.Timer SocketConnectionCheckTimer;
        private System.Windows.Forms.GroupBox ConnectionBox;
        private System.Windows.Forms.TextBox DataBox;
    }
}

