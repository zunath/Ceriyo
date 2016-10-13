using System.Windows;
using Prism.Interactivity.InteractionRequest;

namespace Ceriyo.Infrastructure.WPF.Windows
{
    /// <summary>
    /// Interaction logic for MetroConfirmationWindow.xaml
    /// </summary>
    public partial class MetroConfirmationWindow
    {
        public MetroConfirmationWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Sets or gets the <see cref="IConfirmation"/> shown by this window./>
        /// </summary>
        public IConfirmation Confirmation
        {
            get
            {
                return DataContext as IConfirmation;
            }
            set
            {
                this.DataContext = value;
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Confirmation.Confirmed = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Confirmation.Confirmed = false;
            this.Close();
        }
    }
}
