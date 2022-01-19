using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using SampleSocketClient.Network;
using SampleSocketClient.Utility;

namespace SampleSocketClient
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Socket Clientクラス
        /// </summary>
        private AsyncSocketClient socketClient;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// FormのLoad時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // SJISを利用するためのおまじない
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            socketClient = new (msg =>
            {
                Invoke((MethodInvoker)delegate
                {
                    MessageBox.AppendText(msg + "\n");
                });
            });

            AddressBox.Text = "10.1.1.99";
            PortBox.Text = "30000";

            DisconnectButton.Enabled = false;
            SendButton.Enabled = false;
        }

        /// <summary>
        /// IPアドレス編集時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddressBox_TextChanged(object sender, EventArgs e)
        {
            AddressBox.BackColor =
                CommonUtility.CheckIpv4Pattern(AddressBox.Text) ? Color.White : Color.Yellow;
        }

        private void PortBox_TextChanged(object sender, EventArgs e)
        {
            PortBox.BackColor =
                CommonUtility.CheckPortPattern(PortBox.Text) ? Color.White : Color.Yellow;
        }

        /// <summary>
        /// 接続ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (AddressBox.BackColor.Equals(Color.Yellow) || PortBox.BackColor.Equals(Color.Yellow))
            {
                MessageBox.AppendText("IP Address or Port No is invalid.\n");
                return;
            }

            try
            {
                socketClient.Connect(AddressBox.Text.ToString(), int.Parse(PortBox.Text.ToString()));

                ConnectButton.Enabled = false;
                DisconnectButton.Enabled = true;
                SendButton.Enabled = true;

                SocketConnectionCheckTimer.Enabled = true;

                MessageBox.AppendText($"Connected !\n");
            }
            catch (Exception ex)
            {
                MessageBox.AppendText($"Failed to connect... : {ex.Message}\n");
            }
        }

        /// <summary>
        /// 切断ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                socketClient.Disconnect();

                ConnectButton.Enabled = true;
                DisconnectButton.Enabled = false;
                SendButton.Enabled = false;

                SocketConnectionCheckTimer.Enabled = false;

                MessageBox.AppendText($"Disconnected !\n");
            }
            catch (Exception ex)
            {
                MessageBox.AppendText($"Failed to disconnect... : {ex.Message}\n");
            }
        }

        /// <summary>
        /// 送信ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendButton_Click(object sender, EventArgs e)
        {
            try
            {
                // 入力したデータを送信
                string message = DataBox.Text.ToString();
                socketClient.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.AppendText($"Failed to send... : {ex.Message}\n");
            }
        }

        /// <summary>
        /// Socket接続確認Timer処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SocketConnectionCheckTimer_Tick(object sender, EventArgs e)
        {
            // Server起因のSocket断検知を実施する術が無さそうなので、Timerに頼ってみる。
            if (socketClient != null)
            {
                if (!socketClient.IsConnected())
                {
                    MessageBox.AppendText($"Socket's been disconnected... Try to re-connect.\n");
                    try
                    {
                        socketClient.Connect(AddressBox.Text.ToString(), int.Parse(PortBox.Text.ToString()));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.AppendText($"Re-connection failed... : {ex.Message}\n");
                    }
                }
            }
        }
    }
}
