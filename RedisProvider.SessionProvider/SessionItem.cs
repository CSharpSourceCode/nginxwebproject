using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisProvider.SessionProvider
{
    #region Session Item Model
    [Serializable]
    public class SessionItem
    {
        public DateTime CreatedAt { get; set; }
        public DateTime LockDate { get; set; }
        public int LockID { get; set; }
        public int Timeout { get; set; }
        public bool Locked { get; set; }
        public string SessionItems { get; set; }
        public int Flags { get; set; }
        /// <summary>
        /// 请求的端口号
        /// </summary>
        public int Port { get; set; }
        #endregion Properties

    }

}
