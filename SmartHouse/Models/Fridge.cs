using SmartHouse.Models.DeviceSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SmartHouse.Models.Interfaces;

namespace SmartHouse.Models
{
    public class Fridge : Device, IDrawe, ISettings
    {
        #region Private Data

        private readonly int minTemp1 = -20;
        private readonly int maxTemp2 = 15;
        private int _temp1;
        private int _temp2;

        #endregion





        #region Constructors

        public Fridge() {}

        public Fridge(int minTemp1, int maxTemp2)
        {
            this.minTemp1 = minTemp1;
            this.maxTemp2 = maxTemp2;
        }

        #endregion





        #region Public Members

        // Entity
        public int Temp1 
        {
            get { return _temp1; }
            set
            {
                if (value <= 0 && value >= minTemp1)
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

        // View
        public string TempScale(int temp)
        {
            return temp > 0 ? temp.ToString() + "px" : temp.ToString().Replace("-", "") + "px";
        }

        public string TempScale(int temp, byte multiplier)
        {
            temp *= multiplier;
            return temp > 0 ? temp.ToString() + "px" : temp.ToString().Replace("-", "") + "px";
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
            return "~/Views/Fridge/Index.cshtml";
        }

        // ISettings
        public IEnumerable<string> GetSettings()
        {
            List<string> settings = new List<string>();
            foreach (FridgeSettings setting in Enum.GetValues(typeof(FridgeSettings)))
            {
                settings.Add(setting.ToString());
            }
            return settings;
        }

        public string GetValue(string selectedSetting)
        {
            FridgeSettings setting = (FridgeSettings)Enum.Parse(typeof(FridgeSettings), selectedSetting, true);
            switch (setting)
            {
                case FridgeSettings.Temp1:
                    return Temp1.ToString();
                case FridgeSettings.Temp2:
                    return Temp2.ToString();
                default:
                    return "";
            }
        }

        public void IncrValue(string selectedSetting)
        {
            FridgeSettings setting = (FridgeSettings)Enum.Parse(typeof(FridgeSettings), selectedSetting, true);
            switch (setting)
            {
                case FridgeSettings.Temp1:
                    Temp1++;
                    break;
                case FridgeSettings.Temp2:
                    Temp2++;
                    break;
                default:
                    break;
            }
        }

        public void DecrValue(string selectedSetting)
        {
            FridgeSettings setting = (FridgeSettings)Enum.Parse(typeof(FridgeSettings), selectedSetting, true);
            switch (setting)
            {
                case FridgeSettings.Temp1:
                    Temp1--;
                    break;
                case FridgeSettings.Temp2:
                    Temp2--;
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
        }

        #endregion


    }
}