using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Ceriyo.Data.Engine;
using Ceriyo.Data.ResourceObjects;
using Ceriyo.Data.ViewModels;
using Microsoft.Win32;
using System.Linq;
using FlatRedBall.IO;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for ResourcePackEditorWindow.xaml
    /// </summary>
    public partial class ResourcePackEditorWindow
    {
        private ResourceEditorVM Model { get; set; }
        private OpenFileDialog AddResourceFile { get; set; }
        private OpenFileDialog OpenFile { get; set; }
        private SaveFileDialog SaveFile { get; set; }

        public ResourcePackEditorWindow()
        {
            InitializeComponent();
            Initialize();
            DataContext = Model;
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

        public void Open()
        {
            Show();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            Model.Resources.Clear();
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            bool isOpening = Convert.ToBoolean(OpenFile.ShowDialog());
            if (!isOpening) return;

            Model.Resources.Clear();
            Model.Resources = new BindingList<ResourceEditorItem>(FileManager.XmlDeserialize<List<ResourceEditorItem>>(OpenFile.FileName)); ;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            bool isSaving = Convert.ToBoolean(SaveFile.ShowDialog());
            if (!isSaving) return;

            FileManager.XmlSerialize(Model.Resources.ToList(), SaveFile.FileName);
        }

        private void AddResource(object sender, RoutedEventArgs e)
        {
            bool isAdding = Convert.ToBoolean(AddResourceFile.ShowDialog());
            if (!isAdding) return;

            string[] filePaths = AddResourceFile.FileNames;

            foreach (string path in filePaths)
            {
                ResourceEditorItem item = new ResourceEditorItem
                {
                    FileName = Path.GetFileNameWithoutExtension(path),
                    Extension = Path.GetExtension(path),
                    SizeBytes = new FileInfo(path).Length,
                    Contents = Convert.ToBase64String(File.ReadAllBytes(path))
                };

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

        private void Exit(object sender, RoutedEventArgs e)
        {
            Model.Resources.Clear();
            Hide();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Model.Resources.Clear();
            Hide();
        }
    }
}
