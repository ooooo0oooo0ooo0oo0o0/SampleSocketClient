#define ASYNC_CLIENT

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

using SampleSocketClient.Utility;

namespace SampleSocketClient.Network
{
    // 電文受信時ハンドラの型宣言
    public delegate void ReceiveEventHandler(string message);


    public class AsyncSocketClient : ISocketClient
    {
        private Socket socket = null;

        /// <summary>
        /// 電文受信時イベントハンドラ
        /// </summary>
        public event ReceiveEventHandler OnReceive;


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public AsyncSocketClient(ReceiveEventHandler onReceive)
        {
            OnReceive += onReceive;
        }

        /// <summary>
        /// サーバへの接続処理
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        public void Connect(string address, int port)
        {
            if (socket == null)
            {
                try
                {
                    var serverEndPoint = new IPEndPoint(IPAddress.Parse(address), port);
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
#if ASYNC_CLIENT
                    // 非同期接続
                    socket.BeginConnect(serverEndPoint, new AsyncCallback(ConnectCallback), socket);
#else
                    // 同期接続(接続成功 or 例外発生するまでblockingされる)
                    socket.Connect(serverEndPoint);

                    // 接続成功時は、受信時callbackの登録処理を行う。
                    var state = new StateObject()
                    {
                        ClientSocket = socket
                    };
                    socket.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
#endif

                }
                catch (Exception)
                {
                    // 生成したsocketのリソースを開放しておく
                    socket?.Close();
                    socket = null;

                    // 再Throw
                    throw;
                }
            }
        }

        /// <summary>
        /// 切断処理
        /// </summary>
        public void Disconnect()
        {
            if (socket != null)
            {
                if (socket.Connected)
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                socket = null;
            }
        }

        /// <summary>
        /// サーバへの送信処理
        /// </summary>
        /// <param name="message"></param>
        public void Send(string message)
        {
            if (socket == null)
            {
                // TODO : 再接続する等の対応が必要となる
                OnReceive("Socketが破棄されています");// とりあえずのやっつけ処理
                return;
            }

            try
            {
                var byteString = CommonUtility.ConvertToSjisByteString(message);
                socket.BeginSend(byteString, 0, byteString.Length, 0, new AsyncCallback(SendCallback), socket);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Socket接続状態を確認する
        /// </summary>
        /// <returns>true : 接続中 / false : 接続されていない</returns>
        public bool IsConnected()
        {
            return (socket != null) && socket.Connected ;
        }

        /// <summary>
        /// 接続コールバック処理
        /// </summary>
        /// <param name="asyncResult"></param>
        private void ConnectCallback(IAsyncResult asyncResult)
        {
            try
            {
                // 非同期接続要求を終了
                var client = (Socket)asyncResult.AsyncState;
                client.EndConnect(asyncResult);

                // 続いて受信時コールバック登録を実施
                var state = new StateObject()
                {
                    ClientSocket = client
                };
                client.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception ex)
            {
                //throw; // 再throwしてもcatch出来るモジュールが無い
                OnReceive($"Failed to connect... : {ex.Message}");// とりあえずのやっつけ処理
                Disconnect();
            }
        }

        /// <summary>
        /// 受信時コールバック処理
        /// </summary>
        /// <param name="asyncResult"></param>
        private void ReceiveCallback(IAsyncResult asyncResult)
        {
            try
            {
                var state = (StateObject)asyncResult.AsyncState;
                var client = state.ClientSocket;

                // データを受信
                int byteRead = client.EndReceive(asyncResult);
                var message = Encoding.GetEncoding("Shift_JIS").GetString(state.Buffer, 0, byteRead);

                if (byteRead >= StateObject.BufferSize)
                {
                    // 受信分はbuilderに格納しておき、再度受信コールバック登録(MTU超過によるTCP分割送信時処理)
                    state.messageBuilder.Append(message);
                }
                else
                {
                    if (state.messageBuilder.Length > 1)
                    {
                        // 格納済み文字列と連結した上で、ハンドラに渡す
                        state.messageBuilder.Append(message);
                        OnReceive(state.messageBuilder.ToString());
                    }
                    else
                    {
                        OnReceive(message);
                    }
                }
                // 次なる受信時Callbackを登録
                client.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception ex)
            {
                if (ex is ObjectDisposedException)
                {
                    // Socket切断時は、ObjectDisposedExceptionが発行される
                    return;
                }
                if (ex is SocketException)
                {
                    // サーバ側が切断した場合はここに来る
                    Disconnect();
                    return;
                }
                // 上記以外は再throw
                throw;
            }
        }

        /// <summary>
        /// 送信コールバック処理
        /// </summary>
        /// <param name="asyncResult"></param>
        private static void SendCallback(IAsyncResult asyncResult)
        {
            try
            {
                var client = (Socket)asyncResult.AsyncState;
                int bytesSent = client.EndSend(asyncResult);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 非同期通信状態Object
        /// </summary>
        public class StateObject
        {
            /// <summary>
            /// 受信Socket
            /// </summary>
            public Socket ClientSocket { get; set; }

            /// <summary>
            /// 受信Bufferのサイズ
            /// </summary>
            public const int BufferSize = 4096;

            /// <summary>
            /// 受信Buffer
            /// </summary>
            public byte[] Buffer { get; } = new byte[BufferSize];

            /// <summary>
            /// 受信データ構築用builder
            /// </summary>
            public StringBuilder messageBuilder = new ();
        }

    }
}
