using SmartHouse.Models.DevicePropStates;
using SmartHouse.Models.DeviceSettings;
using SmartHouse.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHouse.Models
{
    public class Microwave : Device, IDrawe, ISettings
    {
        #region Private Data

        private readonly int maxPower = 750;
        private int _power;

        #endregion





        #region Constructors

        public Microwave() {}

        public Microwave(int maxPower)
        {
            this.maxPower = maxPower;
        }

        #endregion





        #region Public Members

        // Entity
        public GrillStates Grill { get; set; }
        public int Power 
        {
            get { return _power; }
            set
            {
                if (value >= 0 && value <= maxPower)
                {
                    _power = value;
                }
            }
        }
        
        // View
        public string PowerPercent(int power)
        {
            double percent = maxPower != 0 ? (double)power / (double)maxPower : 0;
            return percent.ToString().Replace(",", ".");
        }

        public string GrillPercent(GrillStates state)
        {
            switch (state)
            {
                case GrillStates.Off:
                    return "0";
                case GrillStates.Min:
                    return "0.2";
                case GrillStates.Mid:
                    return "0.5";
                case GrillStates.Max:
                    return "1";
                default:
                    return "0";
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
            return "~/Views/Microwave/Index.cshtml";
        }

        // ISettings
        public IEnumerable<string> GetSettings()
        {
            List<string> settings = new List<string>();
            foreach (MicrowaveSettings seting in Enum.GetValues(typeof(MicrowaveSettings)))
            {
                settings.Add(seting.ToString());
            }
            return settings;
        }

        public string GetValue(string selectedSetting)
        {
            MicrowaveSettings setting = (MicrowaveSettings)Enum.Parse(typeof(MicrowaveSettings), selectedSetting, true);
            switch (setting)
            {
                case MicrowaveSettings.Power:
                    return Power.ToString();
                case MicrowaveSettings.Grill:
                    return Grill.ToString();
                default:
                    return "";
            }
        }

        public void IncrValue(string selectedSetting)
        {
            MicrowaveSettings setting = (MicrowaveSettings)Enum.Parse(typeof(MicrowaveSettings), selectedSetting, true);
            switch (setting)
            {
                case MicrowaveSettings.Power:
                    Power += 150;
                    break;
                case MicrowaveSettings.Grill:
                    GrillUp();
                    break;
                default:
                    break;
            }
        }

        public void DecrValue(string selectedSetting)
        {
            MicrowaveSettings setting = (MicrowaveSettings)Enum.Parse(typeof(MicrowaveSettings), selectedSetting, true);
            switch (setting)
            {
                case MicrowaveSettings.Power:
                    Power -= 150;
                    break;
                case MicrowaveSettings.Grill:
                    GrillDown();
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

        public void GrillUp()
        {
            if ((int)Grill < Enum.GetValues(typeof(GrillStates)).Length - 1)
            {
                Grill++;
            }
        }

        public void GrillDown()
        {
            if ((int)Grill > 0)
            {
                Grill--;
            }
        }

        private void ResetSettings()
        {
            Grill = GrillStates.Off;
            _power = 0;
        }

        #endregion


    }
}