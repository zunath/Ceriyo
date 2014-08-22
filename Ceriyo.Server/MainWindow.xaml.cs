using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ceriyo.Data;
using Ceriyo.Data.Settings;
using Ceriyo.Data.ViewModels;
using FlatRedBall;
using FlatRedBall.IO;

namespace Ceriyo.Server
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServerVM Model { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Model = new ServerVM();
            SetDataContexts();
        }

        private void SetDataContexts()
        {
            txtAnnouncement.DataContext = Model;
            txtBlacklistUsername.DataContext = Model;
            txtDescription.DataContext = Model;
            txtGMPassword.DataContext = Model;
            txtModuleFileName.DataContext = Model;
            txtPlayerPassword.DataContext = Model;
            txtServerMessage.DataContext = Model;
            txtServerName.DataContext = Model;
            txtServerStatus.DataContext = Model;
            numMaxLevel.DataContext = Model;
            numMaxPlayers.DataContext = Model;
            numPort.DataContext = Model;
            ddlPVPType.DataContext = Model;
            lbBlacklist.DataContext = Model;
            lbGameType.DataContext = Model;
            lblIPAddress.DataContext = Model;
            lbLog.DataContext = Model;
            lbPlayers.DataContext = Model;
        }

        private void SaveSettings(object sender, RoutedEventArgs e)
        {
            string path = EnginePaths.SettingsDirectory + "ServerSettings" + EnginePaths.DataExtension;

            FileManager.XmlSerialize(Model.ServerSettings, path);
        }

        private void LoadSettings()
        {
            string path = EnginePaths.SettingsDirectory + "ServerSettings" + EnginePaths.DataExtension;

            if (FileManager.FileExists(path))
            {
                Model.ServerSettings = FileManager.XmlDeserialize<ServerSettings>(path);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettings();
        }
    }
}
