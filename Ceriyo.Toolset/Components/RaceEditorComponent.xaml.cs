﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Ceriyo.Data;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;
using Ceriyo.Library.Processing;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for SkillEditorComponent.xaml
    /// </summary>
    public partial class RaceEditorComponent
    {
        private RaceEditorVM Model { get; set; }
        private GameResourceProcessor Processor { get; set; }
        private WorkingDataManager WorkingManager { get; set; }

        public RaceEditorComponent()
        {
            InitializeComponent();
            Model = new RaceEditorVM();
            Processor = new GameResourceProcessor();
            WorkingManager = new WorkingDataManager();
            DataContext = Model;
        }

        private void RaceSelected(object sender, SelectionChangedEventArgs e)
        {
            Race race = lbRaces.SelectedItem as Race;
            Model.SelectedRace = race;
            Model.IsRaceSelected = race != null;

        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (Model.SelectedRace != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this race?", "Delete race?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Model.Races.Remove(Model.SelectedRace);
                    Model.SelectedRace = null;
                    Model.IsRaceSelected = false;
                }
            }
        }

        private void New(object sender, RoutedEventArgs e)
        {
            Race race = new Race();
            string resref = Processor.GenerateUniqueResref(Model.Races.Cast<IGameObject>().ToList(), race.CategoryName);

            race.Name = resref;
            race.Tag = resref;
            race.Resref = resref;

            Model.Races.Add(race);
            int index = Model.Races.IndexOf(race);
            Model.SelectedRace = Model.Races[index];
        }

        public void Save(object sender, EventArgs e)
        {
            FileOperationResultTypeEnum result = WorkingManager.ReplaceAllGameObjectFiles(Model.Races.Cast<IGameObject>().ToList(), WorkingPaths.RacesDirectory);

            if (result != FileOperationResultTypeEnum.Success)
            {
                MessageBox.Show("Unable to save races.", "Saving races failed.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Open(object sender, EventArgs e)
        {
            Model.Races = WorkingManager.GetAllGameObjects<Race>(ModulePaths.RacesDirectory);


            if (Model.Races.Count > 0)
            {
                Model.SelectedRace = Model.Races[0];
            }
        }
    }
}