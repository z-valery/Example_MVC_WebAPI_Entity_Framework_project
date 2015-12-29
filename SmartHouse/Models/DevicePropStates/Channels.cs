using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartHouse.Models.DevicePropStates
{
    public enum Channels : byte
    {
        // music
        HitList,
        McIndie,
        RapTV,
        ReggaeTV,
        RockHits,
        // sport
        SuperSport,
        FoxSports,
        STARSports,
        Eurosport,
        GolfChannel,
        // news
        Channel24,
        CT24,
        JoyNews,
        ABPNews,
        BBCNews
    }
}