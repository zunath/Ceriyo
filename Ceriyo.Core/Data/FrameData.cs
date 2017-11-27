using System;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores animation frame data.
    /// </summary>
    public class FrameData : IDataDomainObject
    {
        /// <inheritdoc />
        public string GlobalID { get; set; }

        /// <inheritdoc />
        [SerializationIgnore]
        public string DirectoryName => null;

        /// <summary>
        /// The name of the frame.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Whether or not the frame is flipped horizontally.
        /// </summary>
        public bool FlipHorizontal { get; set; }

        /// <summary>
        /// Whether or not the frame is flipped vertically.
        /// </summary>
        public bool FlipVertical { get; set; }

        /// <summary>
        /// The length of the frame before the animation moves to the next frame.
        /// </summary>
        public float FrameLength { get; set; }

        /// <summary>
        /// The X position (in cells) of the frame on the sprite sheet.
        /// </summary>
        public int TextureCellX { get; set; }

        /// <summary>
        /// The Y position (in cells) of the frame on the sprite sheet.
        /// </summary>
        public int TextureCellY { get; set; }

        /// <summary>
        /// Constructs a new frame data object.
        /// </summary>
        public FrameData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
