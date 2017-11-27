using System;
using System.Collections.Generic;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores item data
    /// </summary>
    public class ItemData : IDataDomainObject
    {
        /// <inheritdoc />
        public string GlobalID { get; set; }

        /// <inheritdoc />
        [SerializationIgnore]
        public string DirectoryName => "Item";

        /// <summary>
        /// The name of the item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The tag of the item.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// The resref of the item.
        /// </summary>
        public string Resref { get; set; }

        /// <summary>
        /// The resref of the item type.
        /// </summary>
        public string ItemTypeResref { get; set; }

        /// <summary>
        /// Whether or not the item is undroppable.
        /// </summary>
        public bool IsUndroppable { get; set; }

        /// <summary>
        /// Whether or not the item has been stolen.
        /// </summary>
        public bool IsStolen { get; set; }

        /// <summary>
        /// Whether or not the item is part of a plot.
        /// </summary>
        public bool IsPlot { get; set; }

        /// <summary>
        /// The description of the item.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The comment of the item.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// The script fired when the item is activated.
        /// </summary>
        public string OnActivated { get; set; }

        /// <summary>
        /// The script fired when the item is acquired.
        /// </summary>
        public string OnAcquired { get; set; }

        /// <summary>
        /// The script fired when the item is unacquired.
        /// </summary>
        public string OnUnacquired { get; set; }

        /// <summary>
        /// The script fired when the item is equipped.
        /// </summary>
        public string OnEquipped { get; set; }

        /// <summary>
        /// The script fired when the item is unequipped.
        /// </summary>
        public string OnUnequipped { get; set; }

        /// <summary>
        /// The local variables stored on the item.
        /// </summary>
        public LocalVariableData LocalVariables { get; set; }

        /// <summary>
        /// The class requirements needed to equip the item.
        /// </summary>
        public List<ClassRequirementData> ClassRequirements { get; set; }

        /// <summary>
        /// The item properties attached to this item.
        /// </summary>
        public List<string> ItemPropertyResrefs { get; set; }

        /// <summary>
        /// Constructs a new item data object.
        /// </summary>
        public ItemData()
        {
            GlobalID = Guid.NewGuid().ToString();
            LocalVariables = new LocalVariableData();
            ClassRequirements = new List<ClassRequirementData>();
            ItemPropertyResrefs = new List<string>();
        }
    }
}
