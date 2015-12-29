using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHouse.Models
{
    public abstract class Device
    {
        public int Id { get; set; }
        public bool IsOn { get; set; }

        public abstract string TypeDevice();
        public abstract void Off();
        public abstract void On();
    }
}