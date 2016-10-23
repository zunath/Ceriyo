﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Infrastructure.WPF.MonoGameWpfInterop
{
    /// <summary>
    /// Host a Direct3D 11 scene.
    /// </summary>
    public abstract class D3D11Host : Image, IDisposable
    {
        private static readonly object GraphicsDeviceLock = new object();

        // Render timing:
        private readonly Stopwatch _timer;

        // The Direct3D 11 device (shared by all D3D11Host elements):
        private static GraphicsDevice _graphicsDevice;
        private static bool? _isInDesignMode;
        private static int _referenceCount;

        private D3D11Image _d3D11Image;
        private bool _disposed;
        private TimeSpan _lastRenderingTime;
        private bool _loaded;

        // Image source:
        private RenderTarget2D _renderTarget;
        private bool _resetBackBuffer;
        private TimeSpan _timeSinceStart = TimeSpan.Zero;
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="D3D11Host"/> class.
        /// </summary>
        protected D3D11Host(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
            // defaulting to fill as that's what's needed in most cases
            Stretch = Stretch.Fill;

            _timer = new Stopwatch();
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }
        
        /// <summary>
        /// Gets a value indicating whether the controls runs in the context of a designer (e.g.
        /// Visual Studio Designer or Expression Blend).
        /// </summary>
        /// <value>
        /// <see langword="true" /> if controls run in design mode; otherwise, 
        /// <see langword="false" />.
        /// </value>
        public static bool IsInDesignMode
        {
            get
            {
                if (!_isInDesignMode.HasValue)
                    _isInDesignMode = (bool)DependencyPropertyDescriptor.FromProperty(DesignerProperties.IsInDesignModeProperty, typeof(FrameworkElement)).Metadata.DefaultValue;

                return _isInDesignMode.Value;
            }
        }

        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        /// <value>The graphics device.</value>
        public GraphicsDevice GraphicsDevice => _graphicsDevice;

        /// <summary>
        /// Default services collection.
        /// </summary>
        public GameServiceContainer Services { get; } = new GameServiceContainer();
        

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;

            Dispose(true);
        }

        protected abstract void Dispose(bool disposing);

        protected virtual void Initialize()
        {
        }

        /// <summary>
        /// Raises the <see cref="FrameworkElement.SizeChanged" /> event, using the specified 
        /// information as part of the eventual event data.
        /// </summary>
        /// <param name="sizeInfo">Details of the old and new size involved in the change.</param>
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            _resetBackBuffer = true;
            base.OnRenderSizeChanged(sizeInfo);
        }

        protected virtual void Render(GameTime time)
        {
        }

        private static void InitializeGraphicsDevice()
        {
            lock (GraphicsDeviceLock)
            {
                _referenceCount++;
                if (_referenceCount == 1)
                {
                    // Create Direct3D 11 device.
                    var presentationParameters = new PresentationParameters
                    {
                        // Do not associate graphics device with window.
                        DeviceWindowHandle = IntPtr.Zero,
                    };
                    _graphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.HiDef, presentationParameters);
                }
            }
        }

        private static void UninitializeGraphicsDevice()
        {
            lock (GraphicsDeviceLock)
            {
                _referenceCount--;
                if (_referenceCount == 0)
                {
                    _graphicsDevice.Dispose();
                    _graphicsDevice = null;
                }
            }
        }

        private void CreateBackBuffer()
        {
            _d3D11Image.SetBackBuffer(null);
            if (_renderTarget != null)
            {
                _renderTarget.Dispose();
                _renderTarget = null;
            }

            int width = Math.Max((int)ActualWidth, 1);
            int height = Math.Max((int)ActualHeight, 1);
            _renderTarget = new RenderTarget2D(_graphicsDevice, width, height, false, SurfaceFormat.Bgr32, DepthFormat.Depth24Stencil8, 0, RenderTargetUsage.DiscardContents, true);
            _d3D11Image.SetBackBuffer(_renderTarget);
        }

        private void InitializeImageSource()
        {
            _d3D11Image = new D3D11Image();
            _d3D11Image.IsFrontBufferAvailableChanged += OnIsFrontBufferAvailableChanged;
            CreateBackBuffer();
            Source = _d3D11Image;
        }

        private void OnIsFrontBufferAvailableChanged(object sender, DependencyPropertyChangedEventArgs eventArgs)
        {
            if (_d3D11Image.IsFrontBufferAvailable)
            {
                StartRendering();
                _resetBackBuffer = true;
            }
            else
            {
                StopRendering();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs eventArgs)
        {
            if (IsInDesignMode || _loaded)
                return;

            _loaded = true;
            //InitializeGraphicsDevice();
            InitializeImageSource();
            Initialize();
            StartRendering();
        }

        private void OnRendering(object sender, EventArgs eventArgs)
        {
            if (!_timer.IsRunning)
                return;

            // Recreate back buffer if necessary.
            if (_resetBackBuffer)
                CreateBackBuffer();

            // CompositionTarget.Rendering event may be raised multiple times per frame
            // (e.g. during window resizing).
            var renderingEventArgs = (RenderingEventArgs)eventArgs;
            if (_lastRenderingTime != renderingEventArgs.RenderingTime || _resetBackBuffer)
            {
                _lastRenderingTime = renderingEventArgs.RenderingTime;

                GraphicsDevice.SetRenderTarget(_renderTarget);
                var diff = _timer.Elapsed - _timeSinceStart;
                _timeSinceStart = _timer.Elapsed;
                Render(new GameTime(_timer.Elapsed, diff));
                GraphicsDevice.Flush();
            }

            _d3D11Image.Invalidate(); // Always invalidate D3DImage to reduce flickering
                                      // during window resizing.

            _resetBackBuffer = false;
        }

        private void OnUnloaded(object sender, RoutedEventArgs eventArgs)
        {
            if (IsInDesignMode)
                return;

            StopRendering();
            Dispose();
            UnitializeImageSource();
            UninitializeGraphicsDevice();
        }

        private void StartRendering()
        {
            if (_timer.IsRunning)
                return;

            CompositionTarget.Rendering += OnRendering;
            _timer.Start();
        }

        private void StopRendering()
        {
            if (!_timer.IsRunning)
                return;

            CompositionTarget.Rendering -= OnRendering;
            _timer.Stop();
        }

        private void UnitializeImageSource()
        {
            _d3D11Image.IsFrontBufferAvailableChanged -= OnIsFrontBufferAvailableChanged;
            Source = null;

            if (_d3D11Image != null)
            {
                _d3D11Image.Dispose();
                _d3D11Image = null;
            }
            if (_renderTarget != null)
            {
                _renderTarget.Dispose();
                _renderTarget = null;
            }
        }
        
    }
}