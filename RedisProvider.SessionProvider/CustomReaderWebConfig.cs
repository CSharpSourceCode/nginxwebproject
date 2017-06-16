using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Xml;
using ServiceStack.Redis;

namespace RedisProvider.SessionProvider
{
    public class CustomReaderWebConfig
    {
        private static CustomReaderWebConfig customerReaderWebConfig = null;

        // 定义一个标识确保线程同步
        private static readonly object locker = new object();

        static ConfigXmlDocument doc = new ConfigXmlDocument();
        public static CustomReaderWebConfig GetCustomerReaderWebConfig
        {
            get
            {
                if (customerReaderWebConfig == null)
                {
                    lock (locker)
                    {
                        customerReaderWebConfig = new CustomReaderWebConfig();
                        doc.Load(HttpContext.Current.Server.MapPath("web.config"));
                    }
                }
                return customerReaderWebConfig;
            }
        }
        /// <summary>
        /// Sessio过期时间,默认是20分钟
        /// </summary>
        public int SessionTimeOut
        {
            get
            {
                int sessionTimeOut = 20;
                XmlNode node = doc.SelectSingleNode("configuration/system.web / sessionState");
                if (node == null || node.Attributes["timeout"] == null)
                    return sessionTimeOut;
                if (!int.TryParse(node.Attributes["timeout"].Value, out sessionTimeOut))
                {
                    sessionTimeOut = 20;
                }
                return sessionTimeOut;
            }
        }
        public bool CookieLess
        {
            get
            {
                XmlNode node = doc.SelectSingleNode("configuration/system.web / sessionState");
                if (node == null || node.Attributes["cookieless"] == null)
                    return false;
                return node.Attributes["cookieless"].Value.ToUpper().Equals("TRUE");
            }
        }
        /// <summary>
        /// Redis连接的名称
        /// </summary>
        public string CustomProvider
        {
            get
            {
                XmlNode node = doc.SelectSingleNode("configuration/system.web / sessionState");
                if (node == null || node.Attributes["customProvider"] == null)
                {
                    throw new ConfigurationErrorsException("customProvider node not found in sessionstate ");
                }
                return node.Attributes["customProvider"].Value;
            }
        }
        /// <summary>
        /// 是否向Redis服务器书写错误日志
        /// </summary>
        public bool WriteExceptionsToEventLog
        {
            get
            {
                XmlNode node = doc.SelectSingleNode("configuration/system.web/ sessionState/providers/add[@name='" + CustomProvider + "']");
                if (node == null || node.Attributes["writeExceptionsToEventLog"] == null)
                {
                    return false;
                }
                return node.Attributes["writeExceptionsToEventLog"].Value.ToUpper().Equals("TRUE");
            }
        }
        /// <summary>
        /// 获取Redis服务器地址
        /// </summary>
        public string RedisServer
        {
            get
            {
                XmlNode node = doc.SelectSingleNode("configuration/system.web/ sessionState/providers/add[@name='" + CustomProvider + "']");
                if (node == null || node.Attributes["server"] == null)
                {
                    return "localhost";
                }
                return node.Attributes["server"].Value;
            }
        }
        /// <summary>
        /// Redis端口号
        /// </summary>
        public int RedisPort
        {
            get
            {
                int port = 6379;
                XmlNode node = doc.SelectSingleNode("configuration/system.web/ sessionState/providers/add[@name='" + CustomProvider + "']");
                if (node == null || node.Attributes["port"] == null)
                {
                    return port;
                }
                if (!int.TryParse(node.Attributes["port"].Value, out port))
                {
                    port = 6379;
                }
                return port;
            }
        }
        /// <summary>
        /// Redis端口号
        /// </summary>
        public string RedisPassword
        {
            get
            {
                XmlNode node = doc.SelectSingleNode("configuration/system.web/ sessionState/providers/add[@name='" + CustomProvider + "']");
                if (node == null || node.Attributes["password"] == null)
                {
                    return "";
                }
                return node.Attributes["password"].Value;
            }
        }

        /* 4.0 以上版本需要
         public RedisClient RedisSessionClient
         {
             get
             {
                 if (!string.IsNullOrEmpty(redisCfg.RedisPassword))
                 {
                     return new RedisClient(redisCfg.RedisServer, redisCfg.RedisPort, redisCfg.RedisPassword);
                 }

                 return new RedisClient(redisCfg.RedisServer, redisCfg.RedisPort);
             }
         }*/
        /// <summary>
        /// 3.5以下版本的配置
        /// </summary>
        public RedisClient RedisSessionClient
        {
            get
            {
                if (!string.IsNullOrEmpty(RedisPassword))
                {
                    return new RedisClient(RedisServer, RedisPort) { Password = RedisPassword };
                }
                return new RedisClient(RedisServer, RedisPort);
            }
        }
        /// <summary>
        /// 数据库连接串
        /// </summary>
        public string DBConnectionString
        {
            get
            {
                return WebConfigurationManager.ConnectionStrings["dbserver1"].ConnectionString;
            }
        }
        private CustomReaderWebConfig()
        {

        }


    }
}
