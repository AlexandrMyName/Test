using System;
using System.Text;

namespace TadWhat.Encoding
{
    public interface ICrypto
    {
        string Token { get; set; }
        string Secret { get; set; }
    }

    public class CryptoDadata : ICrypto
    {
        string token;
        string secret;


        public  string Token { 
            get => token;
            set => token =   value;
        }
        public string Secret
        {
            get => secret;
            set => secret = value;
        }
        //public static string Encrypt(string str)
        //{
        //    var result = string.Empty;
        //    foreach (char c in str)
        //    {
        //        result += (char)(c ^ 43);
        //    }
        //   // var strBytes = Encoding.UTF8.GetBytes(result);

        //    return result;

        //}
        //public static string Decrypt(string str)
        //{
        //    var result = string.Empty;
        //    foreach (char c in str)
        //    {
        //        result += (char)(c ^ 43);
        //    }
        //   // var strBytes = Convert.FromBase64String(str);

        //    return result;

        //}
    }
}
