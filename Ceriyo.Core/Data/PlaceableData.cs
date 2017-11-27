using System;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores placeable data.
    /// </summary>
    public class PlaceableData : IDataDomainObject
    {
        /// <inheritdoc />
        public string GlobalID { get; set; }

        /// <inheritdoc />
        [SerializationIgnore]
        public string DirectoryName => "Placeable";

        /// <summary>
        /// The name of the placeable.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The tag of the placeable.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// The resref of the placeable.
        /// </summary>
        public string Resref { get; set; }

        /// <summary>
        /// The description of the placeable.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The comment of the placeable.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Whether or not the placeable is static. 
        /// Static placeables do not fire scripts and cannot be picked up by scripting.
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// Whether or not the placeable is part of a plot and cannot be damaged.
        /// </summary>
        public bool IsPlot { get; set; }

        /// <summary>
        /// Whether or not the placeable can be used.
        /// </summary>
        public bool IsUseable { get; set; }

        /// <summary>
        /// Whether or not the placeable is locked.
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// Whether or not the placeable requires a key to unlock.
        /// </summary>
        public bool IsKeyRequired { get; set; }

        /// <summary>
        /// Whether or not the key should be removed when the placeable is unlocked.
        /// </summary>
        public bool AutoRemoveKey { get; set; }

        /// <summary>
        /// The tag of the key item used to unlock the placeable.
        /// </summary>
        public string KeyTag { get; set; }

        /// <summary>
        /// The script resref fired when the placeable is closed.
        /// </summary>
        public string OnClosed { get; set; }

        /// <summary>
        /// The script resref fired when the placeable is opened.
        /// </summary>
        public string OnOpened { get; set; }

        /// <summary>
        /// The script resref fired when the placeable is damaged.
        /// </summary>
        public string OnDamaged { get; set; }

        /// <summary>
        /// The script resref fired when the placeable is destroyed.
        /// </summary>
        public string OnDeath { get; set; }

        /// <summary>
        /// The script resref fired every heartbeat.
        /// </summary>
        public string OnHeartbeat { get; set; }

        /// <summary>
        /// The script resref fired when the inventory of the placeable is disturbed.
        /// </summary>
        public string OnDisturbed { get; set; }

        /// <summary>
        /// The script resref fired when the placeable is locked.
        /// </summary>
        public string OnLocked { get; set; }

        /// <summary>
        /// The script resref fired when the placeable is unlocked.
        /// </summary>
        public string OnUnlocked { get; set; }

        /// <summary>
        /// The script resref fired when the placeable is attacked.
        /// </summary>
        public string OnAttacked { get; set; }

        /// <summary>
        /// The script resref fired when the placeable is used.
        /// </summary>
        public string OnUsed { get; set; }

        /// <summary>
        /// The local variable data stored on the placeable.
        /// </summary>
        public LocalVariableData LocalVariables { get; set; }

        /// <summary>
        /// Constructs a new placeable data object.
        /// </summary>
        public PlaceableData()
        {
            GlobalID = Guid.NewGuid().ToString();
            LocalVariables = new LocalVariableData();
        }
    }
}
