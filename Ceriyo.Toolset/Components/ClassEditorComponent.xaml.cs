﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Ceriyo.Data;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;
using Ceriyo.Library.Processing;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for ClassEditorComponent.xaml
    /// </summary>
    public partial class ClassEditorComponent
    {
        private ClassEditorVM Model { get; set; }
        private GameResourceProcessor Processor { get; set; }
        private WorkingDataManager WorkingManager { get; set; }

        public event EventHandler<EditorItemChangedEventArgs> OnClassesListChanged;

        public ClassEditorComponent()
        {
            InitializeComponent();
            Model = new ClassEditorVM();
            Processor = new GameResourceProcessor();
            WorkingManager = new WorkingDataManager();
            DataContext = Model;
        }


        private void ClassSelected(object sender, SelectionChangedEventArgs e)
        {
            CharacterClass charClass = lbClasses.SelectedItem as CharacterClass;
            Model.SelectedClass = charClass;
            Model.IsClassSelected = charClass != null;

        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (Model.SelectedClass != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this class?", "Delete class?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    string resref = Model.SelectedClass.Resref;
                    Model.Classes.Remove(Model.SelectedClass);
                    Model.SelectedClass = null;
                    Model.IsClassSelected = false;

                    if (OnClassesListChanged != null)
                    {
                        OnClassesListChanged(this, new EditorItemChangedEventArgs(Model.SelectedClass, resref, false));
                    }
                }
            }

        }

        private void New(object sender, RoutedEventArgs e)
        {
            CharacterClass charClass = new CharacterClass();
            string resref = Processor.GenerateUniqueResref(Model.Classes.Cast<IGameObject>().ToList(), charClass.CategoryName);

            charClass.Name = resref;
            charClass.Tag = resref;
            charClass.Resref = resref;

            Model.Classes.Add(charClass);
            int index = Model.Classes.IndexOf(charClass);
            Model.SelectedClass = Model.Classes[index];

            if (OnClassesListChanged != null)
            {
                OnClassesListChanged(this, new EditorItemChangedEventArgs(charClass, resref, true));
            }
        }

        public void Save(object sender, EventArgs e)
        {
            FileOperationResultTypeEnum result = WorkingManager.ReplaceAllGameObjectFiles(Model.Classes.Cast<IGameObject>().ToList(), WorkingPaths.CharacterClassesDirectory);

            if (result != FileOperationResultTypeEnum.Success)
            {
                MessageBox.Show("Unable to save classes.", "Saving classes failed.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Open(object sender, EventArgs e)
        {
            Model.Classes = WorkingManager.GetAllGameObjects<CharacterClass>(ModulePaths.CharacterClassesDirectory);

            if (Model.Classes.Count > 0)
            {
                Model.SelectedClass = Model.Classes[0];
            }
        }
    }
}
