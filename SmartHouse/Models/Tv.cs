using SmartHouse.Models.DevicePropStates;
using SmartHouse.Models.DeviceSettings;
using SmartHouse.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHouse.Models
{
    public class Tv : Device, IDrawe, ISettings
    {
         #region Private Data

        private readonly byte maxVolume = 10;
        private int _volume;

        #endregion





        #region Constructors

        public Tv() {}

        public Tv(byte maxVolume)
        {
            this.maxVolume = maxVolume;
        }

        #endregion





        #region Public Members

        // Entity
        public Channels Channel { get; set; }

        public int Volume 
        {
            get { return _volume; }
            set
            {
                if (value >= 0 && value <= maxVolume)
                {
                    _volume = value;
                }
            }
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
            return "~/Views/Tv/Index.cshtml";
        }

        // ISettings
        public IEnumerable<string> GetSettings()
        {
            List<string> settings = new List<string>();
            foreach (TvSettings seting in Enum.GetValues(typeof(TvSettings)))
            {
                settings.Add(seting.ToString());
            }
            return settings;
        }

        public string GetValue(string selectedSetting)
        {
            TvSettings setting = (TvSettings)Enum.Parse(typeof(TvSettings), selectedSetting, true);
            switch (setting)
            {
                case TvSettings.Channel:
                    return Convert.ToByte(Channel).ToString();
                case TvSettings.Volume:
                    return Volume.ToString();
                default:
                    return "";
            }
        }

        public void IncrValue(string selectedSetting)
        {
            TvSettings setting = (TvSettings)Enum.Parse(typeof(TvSettings), selectedSetting, true);
            switch (setting)
            {
                case TvSettings.Volume:
                    Volume++;
                    break;
                case TvSettings.Channel:
                    ChannelUp();
                    break;
                default:
                    break;
            }
        }

        public void DecrValue(string selectedSetting)
        {
            TvSettings setting = (TvSettings)Enum.Parse(typeof(TvSettings), selectedSetting, true);
            switch (setting)
            {
                case TvSettings.Volume:
                    Volume--;
                    break;
                case TvSettings.Channel:
                    ChannelDown();
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

        public void ChannelUp()
        {
            if ((int)Channel < Enum.GetValues(typeof(Channels)).Length - 1)
            {
                Channel++;
            }
        }

        public void ChannelDown()
        {
            if ((int)Channel > 0)
            {
                Channel--;
            }
        }

        private void ResetSettings()
        {
            Channel = 0;
            _volume = 0;
        }

        #endregion

    }
}