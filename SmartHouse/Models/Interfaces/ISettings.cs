using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHouse.Models.Interfaces
{
    public interface ISettings
    {
        IEnumerable<string> GetSettings();
        string GetValue(string selectedSetting);
        void IncrValue(string selectedSetting);
        void DecrValue(string selectedSetting);
        bool IsOn { get; set; }
        event EventHandler DeviceOffHandler;
        event EventHandler DeviceOnHandler;
    }
}
