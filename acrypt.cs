using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AnalCrypt
{

    public class ACrypt
    {
        string Header = "ACR";
        double RandomDecimal()
        {
            Random r = new Random();

            return r.NextDouble();
        }
        Int64 RemoveDecimals(string any)
        {
            Console.WriteLine(any);
            Int64 sex = Int64.Parse(any.Split('.')[0]);
            return sex;
        }
        string reverse(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        Int64[] GetFactors(string key)
        {
            Int64 c1 = (Int64)key[0] * 2;
            Int64 c2 = (Int64)key[key.Length - 1] * 3;
            return new Int64[] { c1, c2 };
        }
        Int64 GetMath(string key)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(key);
            Int64 rel = 0;
            Int64[] factors = GetFactors(key);
            foreach (byte b in bytes)
            {
                rel += (Int64)b * bytes.Length * factors[0] * factors[1];
            }
            return rel;
        }
        public string Encrypt(string text, string key)
        {
            string[] result = { };
            foreach (char i in text)
            {
                List<string> list = new List<string>(result);
                Int64 EncryptionMath = (Int64)i * GetMath(key);
                list.Add(EncryptionMath.ToString() + RandomDecimal().ToString().Substring(1));
                result = list.ToArray();
            }
            string ToReturn = this.Header + "/" + string.Join("/", result);
            return ToReturn;
        }
        public string Decrypt(string text, string key)
        {
            if (text.StartsWith(this.Header))
            {
                Int64 EncryptionMath = GetMath(key);
                string DecryptionResult = "";
                string[] NewArray = text.Split('/');
                foreach (string i in NewArray)
                {
                    if (i != this.Header)
                    {
                       try
                       {
                            Int64 e = this.RemoveDecimals(i);
                            DecryptionResult += (char)(e / EncryptionMath);
                       }
                       catch { }
                    } 
                }
                return string.Join("", DecryptionResult);
            }
            return "INVALID_CIPHERTEXT";
        }
    }
}