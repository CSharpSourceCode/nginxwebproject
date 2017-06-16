using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web;
using RedisProvider.SessionProvider;

namespace nginxwebproject
{
    public class LoginSession
    {
        public static readonly string SESSIONKEY = "UserLogin";
        public static void CheckIsLoginSessionExist()
        {
            if (HttpContext.Current.Session == null || HttpContext.Current.Session[SESSIONKEY] == null)
            {
                HttpContext.Current.Response.Redirect("~/default.aspx", true);
            }
        }
        /// <summary>
        /// 设置session
        /// </summary>
        public static SessionItem UserLogin
        {
            get
            {
                return HttpContext.Current.Session[SESSIONKEY] as SessionItem;
            }
            set
            {
                HttpContext.Current.Session[SESSIONKEY] = value;
            }
        }
        /// <summary>
        /// 获取SessionID
        /// </summary>
        public static string SessionID
        {
            get
            {
                if (HttpContext.Current.Session[SESSIONKEY] == null)
                    return "还没有创建Session";
                else
                    return HttpContext.Current.Session.SessionID;
            }
        }
        public static string GetMD5Str(string str)
        {
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            byte[] byValue = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] byMd5 = MD5.ComputeHash(byValue);
            MD5.Clear();
            return Convert.ToBase64String(byMd5);
        }
    }
}