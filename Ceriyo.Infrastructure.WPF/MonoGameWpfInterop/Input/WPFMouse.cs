using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Xna.Framework.Input;

namespace Ceriyo.Infrastructure.WPF.MonoGameWpfInterop.Input
{
    /// <summary>
    /// Helper class that converts WPF mouse input to the XNA/MonoGame <see cref="_mouseState"/>.
    /// Required for any WPF hosted control.
    /// </summary>
    public class WpfMouse
    {
        #region Fields

        private readonly UIElement _focusElement;

        private MouseState _mouseState;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the keyboard helper.
        /// </summary>
        /// <param name="focusElement">The element that will be used as the focus point. Provide your implementation of <see cref="WpfGame"/> here.</param>
        public WpfMouse(UIElement focusElement)
        {
            if (focusElement == null)
                throw new ArgumentNullException(nameof(focusElement));

            _focusElement = focusElement;
            _focusElement.MouseWheel += HandleMouse;
            // movement
            _focusElement.MouseMove += HandleMouse;
            _focusElement.MouseEnter += HandleMouse;
            _focusElement.MouseLeave += HandleMouse;
            // clicks
            _focusElement.MouseLeftButtonDown += HandleMouse;
            _focusElement.MouseLeftButtonUp += HandleMouse;
            _focusElement.MouseRightButtonDown += HandleMouse;
            _focusElement.MouseRightButtonUp += HandleMouse;
        }

        #endregion

        #region Methods

        public MouseState GetState() => _mouseState;

        private void HandleMouse(object sender, MouseEventArgs e)
        {
            if (e.Handled)
                return;

            var pos = e.GetPosition(_focusElement);
            if (!_focusElement.IsMouseDirectlyOver || _focusElement.IsMouseCaptured)
            {
                // IsMouseDirectlyOver always returns true if the mouse is captured, so we need to do our own hit testing if the Mouse is captured to find out whether it is actually over the control or not
                if (_focusElement.IsMouseCaptured)
                {
                    // apparently all WPF IInputElements are always Visuals, so we can cast directly
                    var v = (Visual)_focusElement;
                    bool hit = false;
                    VisualTreeHelper.HitTest(v, filterTarget => HitTestFilterBehavior.Continue, target =>
                    {
                        if (target.VisualHit == _focusElement)
                        {
                            // our actual element was hit
                            hit = true;
                        }
                        return HitTestResultBehavior.Continue;
                    }, new PointHitTestParameters(pos));

                    if (hit)
                    {
                        // TODO: technically this doesn't account for any elements that are overlayed onto our control (e.g. textbox overlayed onto our control will never be able to receive focus this way)
                        // that is what IsMouseDirectlyOver is supposed to check, but it doesn't work correctly when mouse is captured..
                        // its possible that the mouse is over a control that is layed ontop of ours, thus we should consider our control to be not hit, but I have no idea how to check for that
                    }
                    else
                    {
                        // outside the hitbox

                        // when the mouse is leaving the control we need to register button releases
                        // when the user clicks in the control, holds the button and moves it outside the control and releases there it normally does not registered
                        // the control would thus think that the button is still pressed
                        // using capture allows us to receive this event, propagate it and then free the mouse

                        _mouseState = new MouseState(_mouseState.X, _mouseState.Y, _mouseState.ScrollWheelValue, (ButtonState)e.LeftButton, (ButtonState)e.MiddleButton, (ButtonState)e.RightButton, (ButtonState)e.XButton1, (ButtonState)e.XButton2);
                        // only release if LeftMouse is up
                        if (e.LeftButton == MouseButtonState.Released)
                        {
                            _focusElement.ReleaseMouseCapture();
                        }
                        e.Handled = true;
                        return;
                    }
                    // inside the control and captured -> still requires full update, so run code below and don't return right away
                }
                else
                {
                    // mouse is outside the control and not captured, so don't update the mouse state
                    return;
                }
            }

            // capture the mouse, this allows receiving of mouse event while the mouse is leaving the control: https://msdn.microsoft.com/en-us/library/ms591452(v=vs.110).aspx
            if (!_focusElement.IsMouseCaptured)
            {
                // however, only focus if we are the active window, otherwise the window will become active and pop into foreground just by hovering the mouse over the game panel
                if (WindowHelper.IsControlOnActiveWindow(_focusElement))
                {
                    // however, only focus if we are the active window, otherwise the window will become active while remaining in the background
                    //
                    _focusElement.CaptureMouse();
                }
                else
                {
                    // don't update mouse events if we are just hovering over different window
                    return;
                }
            }
            e.Handled = true;
            var m = _mouseState;
            var w = e as MouseWheelEventArgs;
            _mouseState = new MouseState((int)pos.X, (int)pos.Y, m.ScrollWheelValue + w?.Delta ?? 0, (ButtonState)e.LeftButton, (ButtonState)e.MiddleButton, (ButtonState)e.RightButton, (ButtonState)e.XButton1, (ButtonState)e.XButton2);
        }

        /// <summary>
        /// Sets the cursor to the specific coordinates within the attached game.
        /// This is required as the monogame Mouse.SetPosition function relies on the underlying Winforms implementation and will not work with WPF.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetCursor(int x, int y)
        {
            var p = _focusElement.PointToScreen(new Point(x, y));
            SetCursorPos((int)p.X, (int)p.Y);
        }

        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        #endregion
    }
}