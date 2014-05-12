using System.ComponentModel;
using System.IO;
using System.Windows;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.ResourceObjects;
using Ceriyo.Data.ViewModels;
using Microsoft.Win32;
using System.Linq;
using Ceriyo.Data;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for ResourcePackEditorWindow.xaml
    /// </summary>
    public partial class ResourcePackEditorWindow : Window
    {
        private ResourceEditorVM Model { get; set; }
        private OpenFileDialog AddResourceFile { get; set; }
        private OpenFileDialog OpenFile { get; set; }
        private SaveFileDialog SaveFile { get; set; }

        public ResourcePackEditorWindow()
        {
            InitializeComponent();
            Initialize();
            SetDataContexts();
        }

        private void Initialize()
        {
            Model = new ResourceEditorVM();
            AddResourceFile = new OpenFileDialog();
            OpenFile = new OpenFileDialog();
            SaveFile = new SaveFileDialog();

            AddResourceFile.Multiselect = true;

            AddResourceFile.Filter = EnginePaths.ResourceFileFilter;
            SaveFile.Filter = EnginePaths.ResourcePackFileFilter;
            OpenFile.Filter = EnginePaths.ResourcePackFileFilter;

        }

        private void SetDataContexts()
        {
            dgResources.DataContext = Model;
        }

        public void Open()
        {
            this.Show();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            Model.Resources.Clear();
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            bool isOpening = (bool)OpenFile.ShowDialog();

            if (isOpening)
            {
                Model.Resources.Clear();
                Model.Resources = ResourcePackDataManager.OpenResourcePack(OpenFile.FileName);
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            bool isSaving = (bool)SaveFile.ShowDialog();

            if (isSaving)
            {
                FileOperationResultTypeEnum result = ResourcePackDataManager.SaveResourcePack(Model.Resources, SaveFile.FileName);

                if (result != FileOperationResultTypeEnum.Success)
                {
                    MessageBox.Show("Unable to save resource package.", "Error saving!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

        }

        private void AddResource(object sender, RoutedEventArgs e)
        {
            bool isAdding = (bool)AddResourceFile.ShowDialog();

            if (isAdding)
            {
                string[] filePaths = AddResourceFile.FileNames;

                foreach (string path in filePaths)
                {
                    ResourceEditorItem item = new ResourceEditorItem();
                    item.FileName = Path.GetFileNameWithoutExtension(path);
                    item.Extension = Path.GetExtension(path);
                    item.SizeBytes = new FileInfo(path).Length;
                    item.Contents = File.ReadAllBytes(path);

                    ResourceEditorItem existingItem = Model.Resources.FirstOrDefault(x => x.FileName == item.FileName && x.Extension == item.Extension);
                    if (existingItem == null)
                    {
                        Model.Resources.Add(item);
                    }
                    else
                    {
                        Model.Resources.Remove(existingItem);
                        Model.Resources.Add(item);
                    }

                }
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Model.Resources.Clear();
            this.Hide();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Model.Resources.Clear();
            this.Hide();
        }
    }
}
