using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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
    /// Interaction logic for EditDialogWindow.xaml
    /// </summary>
    public partial class EditDialogWindow : Window
    {
        private EditDialogVM Model { get; set; }
        public event EventHandler<GameObjectEventArgs> OnSaveSuccess;
        private bool IsEditing { get; set; }
        private WorkingDataManager WorkingManager { get; set; }

        public EditDialogWindow()
        {
            InitializeComponent();
            Model = new EditDialogVM();
            WorkingManager = new WorkingDataManager();
            this.DataContext = Model;
            SetLimits();
        }

        private void PopulateModel(Dialog dialog)
        {
            Model.Comments = dialog.Comments;
            Model.Description = dialog.Description;
            Model.LocalVariables = dialog.LocalVariables;
            Model.Name = dialog.Name;
            Model.Resref = dialog.Resref;
            Model.Scripts = WorkingManager.GetAllScriptNames();
            Model.Tag = dialog.Tag;
        }

        private void SetLimits()
        {
        }

        public void Open(Dialog dialog, bool isEditing)
        {
            PopulateModel(dialog);
            this.IsEditing = isEditing;
            //txtResref.IsEnabled = !IsEditing;
            this.Show();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            Dialog dialog = new Dialog(Model.Name, Model.Tag, Model.Resref, Model.Description, Model.Comments);

            if (WorkingManager.DoesGameObjectExist(dialog) && !IsEditing)
            {
                MessageBox.Show("A dialog with that resref already exists. Please select a different resref.", "Resref in use", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                dialog.LocalVariables = Model.LocalVariables;
                
                FileOperationResultTypeEnum result = WorkingManager.SaveGameObjectFile(dialog);

                if (result == FileOperationResultTypeEnum.Success)
                {
                    if (OnSaveSuccess != null)
                    {
                        OnSaveSuccess(this, new GameObjectEventArgs(dialog));
                    }

                    this.Close();
                }
                else if (result == FileOperationResultTypeEnum.Failure)
                {
                    MessageBox.Show("Could not save dialog.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

    }
}
