using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroCase.Common.Helper
{
    public class EncodeDecode
    {
        public static string Encode(string str)
        {
            byte[] toEncodeAsBytes = UnicodeEncoding.Unicode.GetBytes(str);
            return Convert.ToBase64String(toEncodeAsBytes);
        }

        public static string Decode(string str)
        {
            byte[] encodedDataAsBytes = Convert.FromBase64String(str);
            return UnicodeEncoding.Unicode.GetString(encodedDataAsBytes);
        }
    }
}
