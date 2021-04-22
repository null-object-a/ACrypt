using System;
using System.Collections.Generic;
using System.Text;

namespace ACrypt {
    
    public class ACrypt {
        string reverse(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        int Sum(string key){
            byte[] bytes = Encoding.ASCII.GetBytes(key);
            int rel = 0;
            foreach(byte b in bytes) {
                rel += (int)b * bytes.Length;
            }
            return rel;
        }
        string EncodeA(string text,string key) {
            int[] result = {};
            foreach(char i in text) {
                List<int> list = new List<int>(result);
                list.Add((int)i * Sum(key));
                result = list.ToArray();
            }
            string newstr = string.Join("/",result);
            return reverse(newstr);
        }
        string DecodeA(string text,string key) {
            string result = "";
            string[] e = reverse(text).Split('/');
            foreach(string i in e) {
              result += (char)int.Parse(i) / Sum(key);
            }
            return result;
        }
        public string Encrypt(string text, string key){
            var tmp = EncodeA(text, key);
            tmp = "[ACRYPT] " + reverse(tmp);
            return tmp ?? "";
        }
        public string Decrypt(string text, string key){
            if (text.StartsWith("[ACRYPT]")) {
                var tmp = reverse(text.Replace("[ACRYPT] ",""));
                return DecodeA(tmp,key);
            }
            return "";
        }
    }
}
