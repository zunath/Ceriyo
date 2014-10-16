using System.ComponentModel;
using System.Windows;
using Ceriyo.Data;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for EditDialogWindow.xaml
    /// </summary>
    public partial class EditDialogWindow
    {
        private EditDialogVM Model { get; set; }
        private bool IsEditing { get; set; }
        private WorkingDataManager WorkingManager { get; set; }

        public EditDialogWindow()
        {
            InitializeComponent();
            Model = new EditDialogVM();
            WorkingManager = new WorkingDataManager();
            DataContext = Model;
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
            IsEditing = isEditing;
            Show();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Hide();
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
                    Close();
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
            Hide();
        }

    }
}
