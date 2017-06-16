using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedisProvider.SessionProvider;
using ServiceStack.Redis;
using CommonFunDAL;
using System.Security.Cryptography;
using DBHelpers;
namespace nginxwebproject
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (UrlParamHelper.QueryString("type"))
                {
                    case "0":
                        LoginSystem();
                        break;
                    default:
                        break;
                }
            }
        }

        void LoginSystem()
        {
            var userName = UrlParamHelper.QueryString("userName");
            var userPwd = UrlParamHelper.QueryString("userPwd");
            if (userName.Length == 0)
            {
                OutPutJsonContentToPage("false", "用户名不能为空");
            }
            if (userPwd.Length == 0)
            {
                OutPutJsonContentToPage("false", "密码不能为空");
            }
            userPwd=LoginSession.GetMD5Str(userPwd);
            using (RedisClient client = CustomReaderWebConfig.GetCustomerReaderWebConfig.RedisSessionClient)
            {             
                var pwd = client.GetValue(userName);
                if (pwd == null)
                {
                    if (pwd != userPwd)
                    {
                        OutPutJsonContentToPage("false", "用户名或者密码错误");
                    }
                }
                else
                {
 
                }
            }

            SessionItem si = new SessionItem();
            si.CreatedAt = DateTime.Now;
            si.SessionItems = userName;
            si.Port = Request.Url.Port;
            LoginSession.UserLogin = si;
            Response.Redirect("content.aspx", true);
        }
        void OutPutJsonContentToPage(string status, string message)
        {
            var data = new { status = status, message = message };
            string json = JsonHelper.ObjectToJson(data);
            Response.ContentType = "text/json";
            Response.Write(json);
            Response.End();
        }
    }
}