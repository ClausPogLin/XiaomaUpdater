using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XiaomaUpdater.Models;

namespace XiaomaUpdater.Users
{
    public class Users
    {
        public struct User
        {
            public string name;
            public long studentid;
            public string token;
            public string req_sign;
            public string bark_id;
            public bool is_expired;
            public List<SportUpdateItem> vid_urls;
        };

        public struct SportUpdateItem
        {
            public long planid;
            public string url;
        };
    }
}
