using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DienThoaiDiDong
{
    public static class XString
    {
        public static String ToBase64(this String s)
        {
            if (s != null)
            {
                var bytes = Encoding.UTF8.GetBytes(s);
                return Convert.ToBase64String(bytes);
            }
            return s;
        }
        public static String FromBase64(this String s)
        {
            if (s != null)
            {
                var bytes = Convert.FromBase64String(s);
                return Encoding.UTF8.GetString(bytes);
            }
            return s;
        }

        public static string ToMD5(this string s)
        {
            var bytes = Encoding.UTF8.GetBytes(s);
            var hash = MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        public static string Str_Slug(string s)
        {
            String[][] symbols = {
                                 new String[] { "[áàảãạăắằẳẵặâấầẩẫậ]", "a" },
                                 new String[] { "[đ]", "d" },
                                 new String[] { "[éèẻẽẹêếềểễệ]", "e" },
                                 new String[] { "[íìỉĩị]", "i" },
                                 new String[] { "[óòỏõọôốồổỗộơớờởỡợ]", "o" },
                                 new String[] { "[úùủũụưứừửữự]", "u" },
                                 new String[] { "[ýỳỷỹỵ]", "y" },
                                 new String[] { "[\\s'\"/';,]", "-" },
                                 new String[] { "[:]", "_" }
                             };
            s = s.ToLower();
            foreach (var ss in symbols)
            {
                s = Regex.Replace(s, ss[0], ss[1]);
            }
            return s;
        }
        public static string Str_Limit(string str, int? length)
        {
            int lengt = (length ?? 20);

            if (str.Length > lengt)
            {
                str = str.Substring(0, lengt) + "...";
            }

            return str;

        }
    }
}
