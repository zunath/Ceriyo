using System;
using System.ComponentModel.Design;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Infrastructure.WPF.MonoGameWpfInterop
{
    /// <summary>
    /// The <see cref="ContentManager"/> needs a <see cref="IGraphicsDeviceService"/> to be in the <see cref="IServiceContainer"/>. This class fulfills this purpose.
    /// </summary>
    public class WpfGraphicsDeviceService : IGraphicsDeviceService
    {
        /// <summary>
        /// Create a new instance of the dummy. The constructor will automatically add the instance itself to the <see cref="D3D11Host.Services"/> container of <see cref="host"/>.
        /// </summary>
        /// <param name="host"></param>
        public WpfGraphicsDeviceService(D3D11Host host)
        {
            if (host == null)
                throw new ArgumentNullException(nameof(host));

            if (host.Services.GetService(typeof(IGraphicsDeviceService)) != null)
                throw new NotSupportedException("A graphics device service is already registered.");

            GraphicsDevice = host.GraphicsDevice;
            host.Services.AddService(typeof(IGraphicsDeviceService), this);
        }

        
        [Obsolete("Dummy implementation will never call DeviceCreated")]
        public event EventHandler<EventArgs> DeviceCreated;

        [Obsolete("Dummy implementation will never call DeviceDisposing")]
        public event EventHandler<EventArgs> DeviceDisposing;

        [Obsolete("Dummy implementation will never call DeviceReset")]
        public event EventHandler<EventArgs> DeviceReset;

        [Obsolete("Dummy implementation will never call DeviceResetting")]
        public event EventHandler<EventArgs> DeviceResetting;
        

        public GraphicsDevice GraphicsDevice { get; }
        
    }
}