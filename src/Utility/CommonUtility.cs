using System.Text.RegularExpressions;
using System.Text;

namespace SampleSocketClient.Utility
{
    /// <summary>
    /// 共通Utilityクラス
    /// </summary>
    public static class CommonUtility
    {
        /// <summary>
        /// IPアドレスパターン検証
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static bool CheckIpv4Pattern(string address)
        {
            return Regex.IsMatch(address, @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b");
        }

        /// <summary>
        /// Port番号パターン検証
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static bool CheckPortPattern(string port)
        {
            if (int.TryParse(port, out int portNum))
            {
                return (0 < portNum) && (portNum <= 65535);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 文字列をSJISのbyte列に変換する
        /// </summary>
        /// <param name="telegram"></param>
        /// <returns></returns>
        public static byte[] ConvertToSjisByteString(string baseString)
        {
            return Encoding.GetEncoding("shift_jis").GetBytes(baseString);
        }

    }
}