using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ceriyo.Data.EventArguments;
using Ceriyo.Toolset.Windows;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for MenuBarComponent.xaml
    /// </summary>
    public partial class MenuBarComponent : UserControl
    {
        public event EventHandler<GameModuleEventArgs> OnOpenModule;

        public MenuBarComponent()
        {
            InitializeComponent();
        }

        private void NewModule_Click(object sender, RoutedEventArgs e)
        {
            NewModuleWindow modWindow = new NewModuleWindow("New Module");
            modWindow.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            LoadModuleWindow loadWindow = new LoadModuleWindow();
            loadWindow.OnOpenModule += OpenModuleFinished;
            loadWindow.ShowDialog();
        }

        private void OpenModuleFinished(object sender, GameModuleEventArgs e)
        {
            if(OnOpenModule != null)
            {
                OnOpenModule(sender, e);
            }
        }

    }
}
