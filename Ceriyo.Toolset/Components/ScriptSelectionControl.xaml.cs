using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ceriyo.Data;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.ViewModels;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for ScriptSelectionControl.xaml
    /// </summary>
    public partial class ScriptSelectionControl : UserControl
    {
        protected ScriptSelectionVM Model { get; set; }
        
        public ScriptSelectionControl()
        {
            InitializeComponent();
            InitializeModel();
            SetDataContexts();
        }

        private void InitializeModel()
        {
            this.Model = new ScriptSelectionVM();
            Model.Scripts = WorkingDataManager.GetAllScriptNames();
        }

        private void SetDataContexts()
        {
            lbScripts.DataContext = Model;
        }

        private void Create(object sender, RoutedEventArgs e)
        {
        }

        private void Save(object sender, ScriptEventArgs e)
        {
            if (!Model.Scripts.Contains(e.Name))
            {
                Model.Scripts.Add(e.Name);
            }
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            string scriptName = lbScripts.SelectedItem as string;


        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            string scriptName = lbScripts.SelectedItem as string;

            if (scriptName != null)
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to delete the script " + scriptName + " ?", "Delete Script?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        FileOperationResultTypeEnum result = WorkingDataManager.DeleteScript(scriptName);

                        if (result == FileOperationResultTypeEnum.Success)
                        {
                            Model.Scripts.Remove(scriptName);
                        }
                        else if (result == FileOperationResultTypeEnum.FileDoesNotExist)
                        {
                            MessageBox.Show("Unable to delete script. File does not exist.", "Unable to delete script", MessageBoxButton.OK);
                        }
                        else if (result == FileOperationResultTypeEnum.Failure)
                        {
                            MessageBox.Show("Unable to delete script. Deletion failed.", "Unable to delete script", MessageBoxButton.OK);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void DoubleClickItem(object sender, RoutedEventArgs e)
        {
            if (lbScripts.SelectedItem != null)
            {
                btnOpen.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }

        private void ContextMenuNew(object sender, RoutedEventArgs e)
        {
            if (lbScripts.SelectedItem != null)
            {
                btnCreate.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }

        private void ContextMenuOpen(object sender, RoutedEventArgs e)
        {
            if (lbScripts.SelectedItem != null)
            {
                btnOpen.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }

        private void ContextMenuDelete(object sender, RoutedEventArgs e)
        {
            if (lbScripts.SelectedItem != null)
            {
                btnDelete.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }


    }
}
