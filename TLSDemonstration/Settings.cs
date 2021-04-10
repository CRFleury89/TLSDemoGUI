using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLSDemonstration
{
    public static class Settings
    {
        public static string LogFolder
        {
            get { return ConfigurationManager.AppSettings["LogFolder"]; }
        }
    }
}
