using System;
using System.Encoding;
using System.Collections.Generic;
 


namespace ACrypt {
    public static class Extensions
    {
        public static T[] push<T>(this T[] array, T item)
        {
            List<T> list = new List<T>(array);
            list.Add(item);
    
            return list.ToArray();
        }
    }
    public class ACrypt {
        string reverse(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
        int Sum(key){
            byte[] bytes = Encoding.ASCII.GetBytes(key);
            int rel = 0;
            foreach(byte b in bytes) {
                rel += (int)b;
            }
            return rel;
        }
        string EncodeA(text,key) {
            string[] result = {};
            foreach(char i in text) {
                result.push((int)i)/Sum(key))
            }
            string newstr = string.Join("/",result);
            return reverse(newstr);
        }
        string DecodeA(text,key) {
            string result = "";
            string[] e = reverse(text).Split('/');
            foreach(string i in e) {
              result += (char)int.Parse(i/Sum(key));
            }
            return result;
        }
        public string Encrypt(text, key){
            var tmp = EncodeA(text,key)
            tmp = "[ACRYPT] "+reverse(tmp)
            return tmp ?? "";
        }
        public string Decrypt(text, key){
            if (text.StartsWith("[ACRYPT]")) {
                var tmp = reverse(text.Replace("[ACRYPT] ",""));
                return DecodeA(tmp,key);
            }
            return "";
        }
    }
}