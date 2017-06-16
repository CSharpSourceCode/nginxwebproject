using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RedisProvider.SessionProvider;
using ServiceStack.Redis;
namespace nginxwebproject
{
    public partial class content : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginSession.CheckIsLoginSessionExist();
            if (!IsPostBack)
            {
                OutServerContent();
            }
        }

        private void OutServerContent()
        {
            Response.Write("请求开始时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>");
            Response.Write("服务器名称：" + Server.MachineName + "<br/>"); //服务器名称  
            Response.Write("服务器IP地址：" + Request.ServerVariables["LOCAL_ADDR"] + "<br/>"); //服务器IP地址  
            Response.Write("HTTP访问端口：" + Request.ServerVariables["SERVER_PORT"]);//HTTP访问端口"
            Response.Write(".NET解释引擎版本：" + ".NET CLR" + Environment.Version.Major + "." + Environment.Version.Minor + "quot;." + Environment.Version.Build + "." + Environment.Version.Revision + "<br/>"); //.NET解释引擎版本  
            Response.Write("服务器操作系统版本：" + Environment.OSVersion.ToString() + "<br/>");//服务器操作系统版本  
            Response.Write("服务器IIS版本：" + Request.ServerVariables["SERVER_SOFTWARE"] + "<br/>");//服务器IIS版本  
            Response.Write("服务器域名：" + Request.ServerVariables["SERVER_NAME"] + "<br/>");//服务器域名  
            Response.Write("虚拟目录的绝对路径：" + Request.ServerVariables["APPL_RHYSICAL_PATH"] + "<br/>");//虚拟目录的绝对路径  
            Response.Write("执行文件的绝对路径：" + Request.ServerVariables["PATH_TRANSLATED"] + "<br/>");//执行文件的绝对路径  
            Response.Write("虚拟目录Session总数：" + Session.Contents.Count.ToString() + "<br/>"); //虚拟目录Session总数  
            Response.Write("虚拟目录Application总数：" + Application.Contents.Count.ToString() + "<br/>");//虚拟目录Application总数  
            Response.Write("域名主机：" + Request.ServerVariables["HTTP_HOST"] + "<br/>");//域名主机  
            Response.Write("服务器区域语言：" + Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"] + "<br/>");//服务器区域语言  
            Response.Write("用户信息：" + Request.ServerVariables["HTTP_USER_AGENT"] + "<br/>");
            Response.Write("CPU个数：" + Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS") + "<br/>");//CPU个数  
            Response.Write("CPU类型：" + Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER") + "<br/>");//CPU类型  
            Response.Write("请求来源地址：" + Request.Headers["X-Real-IP"] + "<br/>");
            using (RedisClient client = CustomReaderWebConfig.GetCustomerReaderWebConfig.RedisSessionClient)
            {
                Response.Write("getRedisSet:" + client.GetValue("name") + "<br/>");
            }
            SessionItem loginSession = LoginSession.UserLogin;
            if (loginSession != null)
            {
                Response.Write("分配到地址的端口号为:" + loginSession.Port.ToString() + "<br/>");
                Response.Write("SessionItem:" + loginSession.SessionItems + ";<br/>createTime:" + loginSession.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss") + "</br>");
                if (loginSession.Locked)
                {
                    Response.Write("lockID:" + loginSession.LockID + ";</br>" + loginSession.LockDate.ToString("yyyy-MM-dd HH:mm:ss") + "</br>");
                }
                Response.Write("flags:" + loginSession.Flags + ";<br/>timeout" + loginSession.Timeout + "</br>");
                Response.Write("SessionId:" + LoginSession.SessionID + "</br>");
            }
            else
            {
                Response.Write("不知道何种原因从Session里面取到的LoginSession 为null");
            }
        }
    }
}