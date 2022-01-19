namespace SampleSocketClient.Network
{
    /// <summary>
    /// Socket Client Interfaceクラス
    /// </summary>
    public interface ISocketClient
    {
        /// <summary>
        /// サーバへの接続処理
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        public void Connect(string address, int port);

        /// <summary>
        /// 切断処理
        /// </summary>
        public void Disconnect();

        /// <summary>
        /// サーバへの送信処理
        /// </summary>
        /// <param name="message"></param>
        public void Send(string message);

        /// <summary>
        /// Socket接続状態を確認する
        /// </summary>
        /// <returns>true : 接続中 / false : 接続されていない</returns>
        public bool IsConnected();
    }
}
