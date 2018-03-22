using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot.Addons
{
    public static class Pool
    {
        public static Dictionary<string, int> pool;
        public static List<string> voters;
        public static string Options
        {
            get
            {
                var o = "";
                foreach (var item in pool.Keys)
                {
                    o += item + " ";
                }
                return o;
            }
        }
        public static string Results
        {
            get
            {
                var o = "";
                foreach (var item in pool.Keys)
                {
                    o += item + ": " + pool[item] + " ";
                }
                return o;
            }
        }
    }
}
