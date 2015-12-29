using SmartHouse.Models.DeviceSettings;
using SmartHouse.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHouse.Models
{
    public class Lamp : Device, IDrawe, ISettings
    {
        #region Private Data

        private readonly byte maxBright = 100;
        private byte _bright;

        #endregion





        #region Constructors

        public Lamp() {}

        public Lamp(byte maxBright)
        {
            this.maxBright = maxBright;
        }

        #endregion





        #region Public Members

        // Entity
        public byte Bright 
        {
            get { return _bright; }
            set
            {
                if (value >= 0 && value <= maxBright)
                {
                    _bright = value;
                }
            }
        }

        // View
        public string BrightPercent(int bright)
        {
            double percent = maxBright != 0 ? (double)bright / (double)maxBright : 0;
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
            return "~/Views/Lamp/Index.cshtml";
        }

        // ISettings
        public IEnumerable<string> GetSettings()
        {
            List<string> settings = new List<string>();
            foreach (LampSettings seting in Enum.GetValues(typeof(LampSettings)))
            {
                settings.Add(seting.ToString());
            }
            return settings;
        }

        public string GetValue(string selectedSetting)
        {
            LampSettings setting = (LampSettings)Enum.Parse(typeof(LampSettings), selectedSetting, true);
            switch (setting)
            {
                case LampSettings.Bright:
                    return Bright.ToString();
                default:
                    return "";
            }
        }

        public void IncrValue(string selectedSetting)
        {
            LampSettings setting = (LampSettings)Enum.Parse(typeof(LampSettings), selectedSetting, true);
            switch (setting)
            {
                case LampSettings.Bright:
                    Bright += 20;
                    break;
                default:
                    break;
            }
        }

        public void DecrValue(string selectedSetting)
        {
            LampSettings setting = (LampSettings)Enum.Parse(typeof(LampSettings), selectedSetting, true);
            switch (setting)
            {
                case LampSettings.Bright:
                    Bright -= 20;
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
            _bright = 0;
        }

        #endregion

    }
}