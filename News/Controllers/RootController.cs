using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace News.Controllers
{
    public static class RootController 
    {
        public static string ToUrl(string text)
        {
            return GenerateDirectory(text);
        }


        public static string GenerateDirectory(string Directory)
        {
            string strTitle = Directory.ToString();
            strTitle = strTitle.Trim();
            strTitle = strTitle.Trim('-');
            strTitle = strTitle.ToLower();
            char[] chars = @"$%#@!*?;:~`+=()[]{}|\'<>,/^&"".".ToCharArray();
            strTitle = strTitle.Replace(".", "-");
            strTitle = strTitle.Replace("ş", "s");
            strTitle = strTitle.Replace("ı", "i");
            strTitle = strTitle.Replace("ğ", "g");
            strTitle = strTitle.Replace("ü", "u");
            strTitle = strTitle.Replace("ç", "c");
            strTitle = strTitle.Replace("ö", "o");

            for (int i = 0; i < chars.Length; i++)
            {
                string strChar = chars.GetValue(i).ToString();
                if (strTitle.Contains(strChar))
                {
                    strTitle = strTitle.Replace(strChar, string.Empty);
                }
            }
            strTitle = strTitle.Replace(" ", "-");
            strTitle = strTitle.Replace("--", "-");
            strTitle = strTitle.Replace("---", "-");
            strTitle = strTitle.Replace("----", "-");
            strTitle = strTitle.Replace("-----", "-");
            strTitle = strTitle.Replace("----", "-");
            strTitle = strTitle.Replace("---", "-");
            strTitle = strTitle.Replace("--", "-");
            strTitle = strTitle.Trim();
            strTitle = strTitle.Trim('-');
            strTitle = strTitle.ToLower();
            return strTitle;
        }
    }
}