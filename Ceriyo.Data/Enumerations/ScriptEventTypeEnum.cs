using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ceriyo.Data.Enumerations
{
    public enum ScriptEventTypeEnum
    {
        Unknown = 0,
        OnAreaEnter = 1,
        OnAreaExit = 2,
        OnHeartbeat = 3,
        OnPlayerEnter = 4,
        OnPlayerLeaving = 5,
        OnPlayerLeft = 6,
        OnModuleLoad = 7,
        OnPlayerDying = 8,
        OnPlayerDeath = 9,
        OnPlayerRespawn = 10,
        OnItemAcquired = 11,
        OnItemUnacquired = 12,
        OnItemActivated = 13,
        OnItemEquipped = 14,
        OnItemUnequipped = 15
    }
}
