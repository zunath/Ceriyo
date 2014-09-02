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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ceriyo.Data;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;
using Ceriyo.Toolset.Windows;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for ConversationSelectionControl.xaml
    /// </summary>
    public partial class DialogSelectionControl : UserControl
    {
        protected DialogSelectionVM Model { get; set; }
        private EditDialogWindow EditPropertiesWindow { get; set; }
        private WorkingDataManager WorkingManager { get; set; }

        public DialogSelectionControl()
        {
            InitializeComponent();
            WorkingManager = new WorkingDataManager();
            InitializeModel();
            this.DataContext = Model;
            EditPropertiesWindow = new EditDialogWindow();

        }

        private void InitializeModel()
        {
            this.Model = new DialogSelectionVM();
            Model.Dialogs = WorkingManager.GetAllGameObjects<Dialog>(ModulePaths.DialogsDirectory) as BindingList<Dialog>;
        }

        private void Create(object sender, RoutedEventArgs e)
        {
            Dialog dialog = new Dialog();
            EditPropertiesWindow.Open(dialog, false);
        }

        private void Save(object sender, GameObjectEventArgs e)
        {
            Dialog dialog = Model.Dialogs.SingleOrDefault(x => x.Resref == e.GameObject.Resref);

            if (dialog == null)
            {
                Model.Dialogs.Add(e.GameObject as Dialog);
            }
            else
            {
                int index = Model.Dialogs.IndexOf(dialog);
                Model.Dialogs[index] = e.GameObject as Dialog;
            }
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            Dialog dialog = lbDialogs.SelectedItem as Dialog;

            if (lbDialogs.SelectedItem != null)
            {
                EditPropertiesWindow.Open(dialog, true);
            }
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            Dialog dialog = lbDialogs.SelectedItem as Dialog;

            if (dialog != null)
            {
                try
                {
                    if (MessageBox.Show("Are you sure you want to delete the dialog " + dialog.Name + " ?", "Delete Dialog?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        FileOperationResultTypeEnum result = WorkingManager.DeleteGameObjectFile(dialog);

                        if (result == FileOperationResultTypeEnum.Success)
                        {
                            Model.Dialogs.Remove(dialog);
                        }
                        else if (result == FileOperationResultTypeEnum.FileDoesNotExist)
                        {
                            MessageBox.Show("Unable to delete dialog. File does not exist.", "Unable to delete dialog", MessageBoxButton.OK);
                        }
                        else if (result == FileOperationResultTypeEnum.Failure)
                        {
                            MessageBox.Show("Unable to delete dialog. Deletion failed.", "Unable to delete dialog", MessageBoxButton.OK);
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
            if (lbDialogs.SelectedItem != null)
            {
                btnOpen.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }

        private void ContextMenuNew(object sender, RoutedEventArgs e)
        {
            if (lbDialogs.SelectedItem != null)
            {
                btnCreate.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }

        private void ContextMenuOpen(object sender, RoutedEventArgs e)
        {
            if (lbDialogs.SelectedItem != null)
            {
                btnOpen.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }

        private void ContextMenuDelete(object sender, RoutedEventArgs e)
        {
            if (lbDialogs.SelectedItem != null)
            {
                btnDelete.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }

    }
}
