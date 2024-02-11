using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace XiaomaUpdater
{
    public class SignBuilder
    {
        private const string HARD_I = "e67dc5bd45ff495c";
        private const string HARD_C = "A10BFA449474A1CE06BA3CF70E8BC92C65733277";
        /// <summary>
        /// 给定一个string请求体，返回他的签名。
        /// </summary>
        /// <param name="JSON"></param>
        /// <returns></returns>
        public string GetSign(string json)
        {
            string sign = "" + HARD_I + HARD_C + json;
            sign = sign.Replace(" ", "").Replace("!", "").Replace("~", "").Replace(@"(", "").Replace(@")", "").Replace(@"'", "");
            sign = MD5Encrypter.MD5Encrypt32(MD5Encrypter.EncodeURIComponent(sign ,Encoding.UTF8,true)).ToUpper();
            return sign + "encodeutf8";
        }
    }

    public class MD5Encrypter
    {
        public static string MD5Encrypt32(string source)
        {
            string rule = "";
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(source));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得

            for (int i = 0; i < s.Length; i++)
            {
                rule = rule + s[i].ToString("x2"); // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
            }
            return rule;
        }

        public static string EncodeURIComponent(string strSrc, System.Text.Encoding encoding, bool bToUpper)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder(); for (int i = 0; i < strSrc.Length; i++)
            {
                string t = strSrc[i].ToString(); string k = HttpUtility.UrlEncode(t, encoding); if (t == k)
                {
                    stringBuilder.Append(t);
                }
                else
                {
                    if (bToUpper)
                        stringBuilder.Append(k.ToUpper());
                    else stringBuilder.Append(k);
                }
            }
            if (bToUpper) return stringBuilder.ToString().Replace("+", "%20"); else return stringBuilder.ToString();
        }
    }
}
