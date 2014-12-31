using System.Windows;
using Ceriyo.Data;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ViewModels;

namespace Ceriyo.Toolset.Windows
{
    /// <summary>
    /// Interaction logic for ModulePropertiesWindow.xaml
    /// </summary>
    public partial class ModulePropertiesWindow
    {
        private ModulePropertiesVM Model { get; set; }

        public ModulePropertiesWindow()
        {
            InitializeComponent();
            Model = new ModulePropertiesVM();
            DataContext = Model;
            SetLimits();
        }

        public void Open()
        {
            GameModule gameModule = WorkingDataManager.GetGameModule();
            Model.Comments = gameModule.Comments;
            Model.Description = gameModule.Description;
            Model.LocalVariables = gameModule.LocalVariables;
            Model.Name = gameModule.Name;
            Model.Resref = gameModule.Resref;
            Model.Scripts = WorkingDataManager.GetAllScriptNames();
            Model.Tag = gameModule.Tag;
            Model.Levels = gameModule.Levels.Levels;

            Show();
        }

        private void SetLimits()
        {
            numMaxLevel.Minimum = 1;
            numMaxLevel.Maximum = EngineConstants.MaxLevel;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            GameModule module = new GameModule(Model.Name, Model.Tag, Model.Resref, Model.Description, Model.Comments)
            {
                Levels = new LevelChart(Model.Levels),
                LocalVariables = Model.LocalVariables
            };

            module.Scripts.Add(ScriptEventTypeEnum.OnAreaHeartbeat, Model.OnHeartbeatScript);
            module.Scripts.Add(ScriptEventTypeEnum.OnModuleLoad, Model.OnModuleLoadScript);
            module.Scripts.Add(ScriptEventTypeEnum.OnPlayerDeath, Model.OnPlayerDeathScript);
            module.Scripts.Add(ScriptEventTypeEnum.OnPlayerDying, Model.OnPlayerDyingScript);
            module.Scripts.Add(ScriptEventTypeEnum.OnModulePlayerEnter, Model.OnPlayerEnterScript);
            module.Scripts.Add(ScriptEventTypeEnum.OnModulePlayerLeaving, Model.OnPlayerLeavingScript);
            module.Scripts.Add(ScriptEventTypeEnum.OnModulePlayerLeft, Model.OnPlayerLeftScript);
            module.Scripts.Add(ScriptEventTypeEnum.OnPlayerRespawn, Model.OnPlayerRespawnScript);

            FileOperationResultTypeEnum result = WorkingDataManager.SaveModuleSettings(module);

            if (result == FileOperationResultTypeEnum.Success)
            {
                Hide();
            }
            else
            {
                MessageBox.Show("Failed to save module properties.", "Failed to save", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

    }
}
