using System;
using System.Collections.Generic;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores animation data
    /// </summary>
    public class AnimationData : IDataDomainObject
    {
        /// <inheritdoc />
        public string GlobalID { get; set; }
        
        /// <inheritdoc />
        [SerializationIgnore]
        public string DirectoryName => "Animation";

        /// <summary>
        /// The name of the animation.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The tag of the animation.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// The resref of the animation.
        /// </summary>
        public string Resref { get; set; }

        /// <summary>
        /// The description of the animation
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The comment of the animation.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// The frames used by the animation.
        /// </summary>
        public IEnumerable<FrameData> Frames { get; set; }

        /// <summary>
        /// Constructs a new animation data object
        /// </summary>
        public AnimationData()
        {
            GlobalID = Guid.NewGuid().ToString();
            Frames = new List<FrameData>();
        }
    }
}
