using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome_WPF.Factory
{
    class SettingsFactory
    {
        private static SettingsFactory _instance;
        private string _username;

        private SettingsFactory()
        {
        }

        public static SettingsFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SettingsFactory();

                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
            }
        }

    }
 }
