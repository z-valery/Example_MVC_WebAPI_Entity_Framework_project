using SmartHouse.Models;
using SmartHouse.Models.DeviceSettings;
using SmartHouse.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartHouse.Controllers
{
    public class ApiConsoleController : ApiController
    {
        [Route("api/ApiConsole/Tv/GetSettings")]
        public List<string> GetSettings()
        {
            List<string> settings = new List<string>();
            foreach (TvSettings setting in Enum.GetValues(typeof(TvSettings)))
            {
                settings.Add(setting.ToString());
            }
            return settings;
        }

        [Route("api/ApiConsole/Tv/GetValue/{setting}")]
        public string PostGetValue(string setting, Tv tv)
        {
            string str = (tv as ISettings).GetValue(setting);
            return str;
        }

        [Route("api/ApiConsole/Tv/PutIsOn/{isOn}")]
        public Tv PutIsOn(bool isOn, Tv tv)
        {
            if(isOn)
            {
                tv.On();
            }
            else
            {
                tv.Off();
            }
            return tv;
        }

        [Route("api/ApiConsole/Tv/PutIncrValue/{setting}")]
        public Tv PutIncrValue(string setting, Tv tv)
        {
            (tv as ISettings).IncrValue(setting);
            return tv;
        }

        [Route("api/ApiConsole/Tv/PutDecrValue/{setting}")]
        public Tv PutDecrValue(string setting, Tv tv)
        {
            (tv as ISettings).DecrValue(setting);
            return tv;
        }


    }
}
