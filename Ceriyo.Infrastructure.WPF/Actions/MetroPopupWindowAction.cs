using System;
using System.Windows;
using Ceriyo.Infrastructure.WPF.Windows;
using MahApps.Metro.Controls;
using Prism.Interactivity;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Infrastructure.WPF.Actions
{
    public class MetroPopupWindowAction: PopupWindowAction
    {
        public ResizeMode ResizeMode { get; set; }

        public MetroPopupWindowAction()
        {
            ResizeMode = ResizeMode.CanResizeWithGrip;
        }

        protected override void Invoke(object parameter)
        {
            var args = parameter as InteractionRequestedEventArgs;
            if (args == null) return;

            // If the WindowContent shouldn't be part of another visual tree.
            if (WindowContent?.Parent != null) return;

            Window wrapperWindow = GetWindow(args.Context);
            wrapperWindow.ResizeMode = ResizeMode;
            wrapperWindow.SizeToContent = SizeToContent.WidthAndHeight;

            // We invoke the callback when the interaction's window is closed.
            var callback = args.Callback;
            EventHandler handler = null;
            handler =
                (o, e) => {
                    wrapperWindow.Closed -= handler;
                    wrapperWindow.Content = null;
                    callback?.Invoke();
                };
            wrapperWindow.Closed += handler;

            if (CenterOverAssociatedObject && AssociatedObject != null)
            {
                // If we should center the popup over the parent window we subscribe to the SizeChanged event
                // so we can change its position after the dimensions are set.
                SizeChangedEventHandler sizeHandler = null;
                sizeHandler =
                    (o, e) => {
                        wrapperWindow.SizeChanged -= sizeHandler;

                        FrameworkElement view = AssociatedObject;
                        Point position = view.PointToScreen(new Point(0, 0));

                        wrapperWindow.Top = position.Y + ((view.ActualHeight - wrapperWindow.ActualHeight) / 2);
                        wrapperWindow.Left = position.X + ((view.ActualWidth - wrapperWindow.ActualWidth) / 2);
                    };
                wrapperWindow.SizeChanged += sizeHandler;
            }

            if (IsModal)
            {
                wrapperWindow.ShowDialog();
            }
            else
            {
                wrapperWindow.Show();
            }
        }

        protected override Window GetWindow(INotification notification)
        {
            MetroWindow wrapperWindow;

            if (this.WindowContent != null)
            {
                wrapperWindow = new MetroWindow();

                // If the WindowContent does not have its own DataContext, it will inherit this one.
                wrapperWindow.DataContext = notification;
                wrapperWindow.Title = notification.Title;

                this.PrepareContentForWindow(notification, wrapperWindow);
            }
            else
            {
                wrapperWindow = this.CreateDefaultWindow(notification);
            }

            return wrapperWindow;
        }

        protected new MetroWindow CreateDefaultWindow(INotification notification)
        {
            MetroWindow window = null;

            if (notification is IConfirmation)
            {
                window = new MetroConfirmationWindow() { Confirmation = (IConfirmation)notification };
            }
            else
            {
                window = new MetroNotificationWindow() { Notification = notification };
            }

            return window;
        }
    }
}
