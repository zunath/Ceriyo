using System;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores dialog data
    /// </summary>
    public class DialogData : IDataDomainObject
    {
        /// <inheritdoc />
        public string GlobalID { get; set; }

        /// <inheritdoc />
        [SerializationIgnore]
        public string DirectoryName => "Dialog";

        /// <summary>
        /// The name of the dialog.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The tag of the dialog.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// The resref of the dialog.
        /// </summary>
        public string Resref { get; set; }

        /// <summary>
        /// Constructs a new dialog data object.
        /// </summary>
        public DialogData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
