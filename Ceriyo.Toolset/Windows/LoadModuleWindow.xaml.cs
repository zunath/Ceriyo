using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Ceriyo.Data;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for LoadModuleWindow.xaml
    /// </summary>
    public partial class LoadModuleWindow : Window
    {
        protected LoadModuleVM Model { get; set; }
        public event EventHandler<GameModuleEventArgs> OnOpenModule;

        public LoadModuleWindow()
        {
            InitializeComponent();
            Model = new LoadModuleVM();
            BindDataContexts();
            Model.GameModules = new BindingList<GameModule>(ModuleDataManager.GetModules());
        }



        private void BindDataContexts()
        {
            lbModuleList.DataContext = Model;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            GameModule moduleToLoad = lbModuleList.SelectedItem as GameModule;
            if (moduleToLoad != null)
            {
                FileOperationResultTypeEnum result = ModuleDataManager.LoadModule(moduleToLoad.Resref);

                if (result == FileOperationResultTypeEnum.FileExists)
                {
                    if (MessageBox.Show("WARNING: A module's temporary files are located on disk. If you continue, you will lose them. Are you sure you want to continue?", "Temporary Files Found", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        result = ModuleDataManager.LoadModule(moduleToLoad.Resref, true);
                    }
                    else
                    {
                        return;
                    }
                }

                if (result == FileOperationResultTypeEnum.FileDoesNotExist)
                {
                    MessageBox.Show("Module could not be found.", "Warning", MessageBoxButton.OK);
                }
                else if (result == FileOperationResultTypeEnum.Failure)
                {
                    MessageBox.Show("Failed to load module.", "Error", MessageBoxButton.OK);
                }
                else if (result == FileOperationResultTypeEnum.Success)
                {
                    this.Close();

                    if (OnOpenModule != null)
                    {
                        OnOpenModule(this, new GameModuleEventArgs(moduleToLoad));
                    }
                }
            }
        }

        private void lbModuleList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbModuleList.SelectedItem != null)
            {
                btnOpen.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}
