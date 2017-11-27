namespace Ceriyo.Core.Constants
{
    /// <summary>
    /// Script events
    /// </summary>
    public enum ScriptEvent
    {
        /// <summary>
        /// Unknown script event
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Fires when a player enters an area.
        /// </summary>
        OnAreaEnter = 1,

        /// <summary>
        /// Fires when a player leaves an area.
        /// </summary>
        OnAreaExit = 2,

        /// <summary>
        /// Fires once every six seconds.
        /// </summary>
        OnHeartbeat = 3,

        /// <summary>
        /// Fires when a player enters the module.
        /// </summary>
        OnModulePlayerEnter = 4,

        /// <summary>
        /// Fires when a player leaves the module.
        /// </summary>
        OnModulePlayerLeaving = 5,

        /// <summary>
        /// Fires when a player has already left the module.
        /// </summary>
        OnModulePlayerLeft = 6,

        /// <summary>
        /// Fires when the module has loaded.
        /// </summary>
        OnModuleLoad = 7,

        /// <summary>
        /// Fires when a player is below 0 hit points but has not reached death yet.
        /// </summary>
        OnPlayerDying = 8,

        /// <summary>
        /// Fires when a player has died.
        /// </summary>
        OnPlayerDeath = 9,

        /// <summary>
        /// Fires when a player chooses to respawn.
        /// </summary>
        OnPlayerRespawn = 10,

        /// <summary>
        /// Fires when a player acquires an item.
        /// </summary>
        OnItemAcquired = 11,

        /// <summary>
        /// Fires when a player loses an item.
        /// </summary>
        OnItemUnacquired = 12,

        /// <summary>
        /// Fires when a player activates an item.
        /// </summary>
        OnItemActivated = 13,

        /// <summary>
        /// Fires when a player equips an item.
        /// </summary>
        OnItemEquipped = 14,

        /// <summary>
        /// Fires when a player unequips an item.
        /// </summary>
        OnItemUnequipped = 15,

        /// <summary>
        /// Fires when a placeable closes.
        /// </summary>
        OnPlaceableClose = 16,

        /// <summary>
        /// Fires when a placeable is damaged.
        /// </summary>
        OnPlaceableDamaged = 17,

        /// <summary>
        /// Fires when a placeable is destroyed.
        /// </summary>
        OnPlaceableDeath = 18,

        /// <summary>
        /// Fires when the inventory of a placeable changes.
        /// </summary>
        OnPlaceableDisturbed = 20,

        /// <summary>
        /// Fires when a placeable is locked.
        /// </summary>
        OnPlaceableLocked = 21,

        /// <summary>
        /// Fires when a placeable is attacked.
        /// </summary>
        OnPlaceableAttacked = 22,

        /// <summary>
        /// Fires when a placeable is opened.
        /// </summary>
        OnPlaceableOpen = 23,

        /// <summary>
        /// Fires when a placeable is unlocked.
        /// </summary>
        OnPlaceableUnlocked = 24,

        /// <summary>
        /// Fires when a placeable is used.
        /// </summary>
        OnPlaceableUsed = 25,

        /// <summary>
        /// Fires when a conversation is started with a creature 
        /// </summary>
        OnCreatureConversation = 26,

        /// <summary>
        /// Fires when a creature is damaged.
        /// </summary>
        OnCreatureDamaged = 27,

        /// <summary>
        /// Fires when a creature dies.
        /// </summary>
        OnCreatureDeath = 28,

        /// <summary>
        /// Fires when a creature's inventory is disturbed.
        /// </summary>
        OnCreatureDisturbed = 29,

        /// <summary>
        /// Fires when a creature is attacked.
        /// </summary>
        OnCreatureAttacked = 31,

        /// <summary>
        /// Fires when a creature is spawned.
        /// </summary>
        OnCreatureSpawned = 32,

        /// <summary>
        /// Fires when an ability is activated.
        /// </summary>
        OnAbilityActivated = 33,

        /// <summary>
        /// Fires when a player levels up.
        /// </summary>
        OnModulePlayerLevelUp = 34
    }
}
