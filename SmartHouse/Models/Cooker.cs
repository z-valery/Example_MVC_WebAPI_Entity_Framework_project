using SmartHouse.Models.DeviceSettings;
using SmartHouse.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHouse.Models
{
    public class Cooker : Device, IDrawe, ISettings
    {
        #region Private Data

        private readonly int maxTemp1 = 400;
        private readonly int maxTemp2 = 200;
        private readonly int maxTemp3 = 200;
        private readonly int maxTemp4 = 100;
        private int _temp1;
        private int _temp2;
        private int _temp3;
        private int _temp4;

        #endregion





        #region Constructors

        public Cooker() {}

        public Cooker(int maxTemp1, int maxTemp2, int maxTemp3, int maxTemp4)
        {
            this.maxTemp1 = maxTemp1;
            this.maxTemp2 = maxTemp2;
            this.maxTemp3 = maxTemp3;
            this.maxTemp4 = maxTemp4;
        }

        #endregion





        #region Public Members

        // Entity
        public int Temp1 
        {
            get { return _temp1; }
            set
            {
                if (value >= 0 && value <= maxTemp1)
                {
                    _temp1 = value;
                }
            }
        }

        public int Temp2
        {
            get { return _temp2; }
            set
            {
                if (value >= 0 && value <= maxTemp2)
                {
                    _temp2 = value;
                }
            }
        }

        public int Temp3
        {
            get { return _temp3; }
            set
            {
                if (value >= 0 && value <= maxTemp3)
                {
                    _temp3 = value;
                }
            }
        }

        public int Temp4 
        {
            get { return _temp4; }
            set
            {
                if (value >= 0 && value <= maxTemp4)
                {
                    _temp4 = value;
                }
            }
        }

        // View
        public int MaxTemp1
        { get { return maxTemp1; } }

        public int MaxTemp2
        { get { return maxTemp2; } }

        public int MaxTemp3
        { get { return maxTemp3; } }

        public int MaxTemp4
        { get { return maxTemp4; } }

        public string TempPercent(int temp, int maxTemp)
        {
            double percent = maxTemp != 0 ? (double)temp / (double)maxTemp : 0;
            return percent.ToString().Replace(",", ".");
        }

        // Device
        public override string TypeDevice()
        {
            return this.GetType().ToString().Substring(this.GetType().ToString().LastIndexOf(".") + 1);
        }

        public override void On()
        {
            IsOn = true;
            if (DeviceOnHandler != null)
            {
                DeviceOnHandler(this, EventArgs.Empty);
            }
        }

        public override void Off()
        {
            IsOn = false;
            ResetSettings();
            if (DeviceOffHandler != null)
            {
                DeviceOffHandler(this, EventArgs.Empty);
            }
        }

        // IDrawe
        public string Drawe()
        {
            return "~/Views/Cooker/Index.cshtml";
        }

        // ISettings
        public IEnumerable<string> GetSettings()
        {
            List<string> settings = new List<string>();
            foreach (CookerSettings seting in Enum.GetValues(typeof(CookerSettings)))
            {
                settings.Add(seting.ToString());
            }
            return settings;
        }

        public string GetValue(string selectedSetting)
        {
            CookerSettings setting = (CookerSettings)Enum.Parse(typeof(CookerSettings), selectedSetting, true);
            switch (setting)
            {
                case CookerSettings.Temp1:
                    return Temp1.ToString();
                case CookerSettings.Temp2:
                    return Temp2.ToString();
                case CookerSettings.Temp3:
                    return Temp3.ToString();
                case CookerSettings.Temp4:
                    return Temp4.ToString();
                default:
                    return "";
            }
        }

        public void IncrValue(string selectedSetting)
        {
            CookerSettings setting = (CookerSettings)Enum.Parse(typeof(CookerSettings), selectedSetting, true);
            switch (setting)
            {
                case CookerSettings.Temp1:
                    Temp1 += 25;
                    break;
                case CookerSettings.Temp2:
                    Temp2 += 25;
                    break;
                case CookerSettings.Temp3:
                    Temp3 += 25;
                    break;
                case CookerSettings.Temp4:
                    Temp4 += 25;
                    break;
                default:
                    break;
            }
        }

        public void DecrValue(string selectedSetting)
        {
            CookerSettings setting = (CookerSettings)Enum.Parse(typeof(CookerSettings), selectedSetting, true);
            switch (setting)
            {
                case CookerSettings.Temp1:
                    Temp1 -= 25;
                    break;
                case CookerSettings.Temp2:
                    Temp2 -= 25;
                    break;
                case CookerSettings.Temp3:
                    Temp3 -= 25;
                    break;
                case CookerSettings.Temp4:
                    Temp4 -= 25;
                    break;
                default:
                    break;
            }
        }

        // Event ISettings
        public event EventHandler DeviceOffHandler;
        public event EventHandler DeviceOnHandler;

        #endregion





        #region Private Members

        private void ResetSettings()
        {
            _temp1 = 0;
            _temp2 = 0;
            _temp3 = 0;
            _temp4 = 0;
        }

        #endregion


    }
}