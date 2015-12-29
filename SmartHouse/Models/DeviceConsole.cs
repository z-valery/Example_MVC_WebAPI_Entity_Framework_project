using SmartHouse.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHouse.Models
{
    public class DeviceConsole
    {
        ISettings device;
        private string selectedSetting;
        private string value;
        private List<string> settings;

        public DeviceConsole(ISettings device)
        {
            this.device = device;
            settings = new List<string>();
            device.DeviceOffHandler += Off;
            device.DeviceOnHandler += On;
            if(!device.IsOn)
            {
                Off(null, EventArgs.Empty);
            }
            else
            {
                On(null, EventArgs.Empty);
            }
        }

        public List<string> Settings
        { get { return settings; } }

        public string SelectedSetting
        { get { return selectedSetting; } }

        public string Value
        { get { return value; } }

        public void PrevSetting()
        {
            if(settings.IndexOf(selectedSetting) > 0)
            {
                selectedSetting = settings[settings.IndexOf(selectedSetting) - 1];
                RefreshValue();
            }
        }

        public void NextSetting()
        {
            if(settings.IndexOf(selectedSetting) < settings.Count - 1)
            {
                selectedSetting = settings[settings.IndexOf(selectedSetting) + 1];
                RefreshValue();
            }
        }

        public void DecrValue()
        {
            device.DecrValue(selectedSetting);
            RefreshValue();
        }

        public void IncrValue()
        {
            device.IncrValue(selectedSetting);
            RefreshValue();
        }

        private void RefreshValue()
        {
            value = device.GetValue(selectedSetting);
        }

        private void Off(object sender, EventArgs e)
        {
            settings.Clear();
            value = "";
        }

        private void On(object sender, EventArgs e)
        {
            settings = device.GetSettings().ToList();
            selectedSetting = settings[0];
            RefreshValue();
        }

    }
}