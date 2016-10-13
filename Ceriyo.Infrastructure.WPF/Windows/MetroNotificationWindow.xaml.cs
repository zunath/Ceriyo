using System.Windows;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Infrastructure.WPF.Windows
{
    /// <summary>
    /// Interaction logic for MetroNotificationWindow.xaml
    /// </summary>
    public partial class MetroNotificationWindow
    {
        public MetroNotificationWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Sets or gets the <see cref="IConfirmation"/> shown by this window./>
        /// </summary>
        public INotification Notification
        {
            get
            {
                return DataContext as IConfirmation;
            }
            set
            {
                DataContext = value;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
