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
        OnAreaHeartbeat = 3,
        OnModulePlayerEnter = 4,
        OnModulePlayerLeaving = 5,
        OnModulePlayerLeft = 6,
        OnModuleLoad = 7,
        OnPlayerDying = 8,
        OnPlayerDeath = 9,
        OnPlayerRespawn = 10,
        OnItemAcquired = 11,
        OnItemUnacquired = 12,
        OnItemActivated = 13,
        OnItemEquipped = 14,
        OnItemUnequipped = 15,
        OnPlaceableClose = 16,
        OnPlaceableDamaged = 17,
        OnPlaceableDeath = 18,
        OnPlaceableHeartbeat = 19,
        OnPlaceableDisturbed = 20,
        OnPlaceableLocked = 21,
        OnPlaceableAttacked = 22,
        OnPlaceableOpen = 23,
        OnPlaceableUnlock = 24,
        OnPlaceableUsed = 25
    }
}
