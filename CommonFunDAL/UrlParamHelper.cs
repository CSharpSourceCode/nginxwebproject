using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CommonFunDAL
{
    public class UrlParamHelper
    {
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string QueryString(string key)
        {
            if (key == null || key.Length == 0)
                return "";
            var val = HttpContext.Current.Request.Params[key];
            val = val ?? "";
            return val;
        }
    }
}
