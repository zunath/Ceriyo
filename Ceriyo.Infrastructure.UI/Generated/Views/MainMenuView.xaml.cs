// -----------------------------------------------------------
//  
//  This file was generated, please do not modify.
//  
// -----------------------------------------------------------
namespace EmptyKeys.UserInterface.Generated {
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.ObjectModel;
    using EmptyKeys.UserInterface;
    using EmptyKeys.UserInterface.Charts;
    using EmptyKeys.UserInterface.Data;
    using EmptyKeys.UserInterface.Controls;
    using EmptyKeys.UserInterface.Controls.Primitives;
    using EmptyKeys.UserInterface.Input;
    using EmptyKeys.UserInterface.Interactions.Core;
    using EmptyKeys.UserInterface.Interactivity;
    using EmptyKeys.UserInterface.Media;
    using EmptyKeys.UserInterface.Media.Effects;
    using EmptyKeys.UserInterface.Media.Animation;
    using EmptyKeys.UserInterface.Media.Imaging;
    using EmptyKeys.UserInterface.Shapes;
    using EmptyKeys.UserInterface.Renderers;
    using EmptyKeys.UserInterface.Themes;
    
    
    [GeneratedCodeAttribute("Empty Keys UI Generator", "3.0.0.0")]
    public partial class MainMenuView : UIRoot {
        
        private DockPanel e_0;
        
        private Grid e_1;
        
        private Image e_2;
        
        private TextBlock e_3;
        
        private Button e_4;
        
        private Button e_5;
        
        private Button e_6;
        
        private Button e_7;
        
        private Button e_8;
        
        private Button e_9;
        
        public MainMenuView() : 
                base() {
            this.Initialize();
        }
        
        public MainMenuView(int width, int height) : 
                base(width, height) {
            this.Initialize();
        }
        
        private void Initialize() {
            Style style = RootStyle.CreateRootStyle();
            style.TargetType = this.GetType();
            this.Style = style;
            this.InitializeComponent();
        }
        
        private void InitializeComponent() {
            Binding binding__OwnedWindowsContent = new Binding("Windows");
            this.SetBinding(UIRoot.OwnedWindowsContentProperty, binding__OwnedWindowsContent);
            InitializeElementResources(this);
            // e_0 element
            this.e_0 = new DockPanel();
            this.Content = this.e_0;
            this.e_0.Name = "e_0";
            // e_1 element
            this.e_1 = new Grid();
            this.e_0.Children.Add(this.e_1);
            this.e_1.Name = "e_1";
            RowDefinition row_e_1_0 = new RowDefinition();
            row_e_1_0.Height = new GridLength(100F, GridUnitType.Pixel);
            this.e_1.RowDefinitions.Add(row_e_1_0);
            RowDefinition row_e_1_1 = new RowDefinition();
            row_e_1_1.Height = new GridLength(20F, GridUnitType.Pixel);
            this.e_1.RowDefinitions.Add(row_e_1_1);
            RowDefinition row_e_1_2 = new RowDefinition();
            row_e_1_2.Height = new GridLength(50F, GridUnitType.Pixel);
            this.e_1.RowDefinitions.Add(row_e_1_2);
            RowDefinition row_e_1_3 = new RowDefinition();
            row_e_1_3.Height = new GridLength(50F, GridUnitType.Pixel);
            this.e_1.RowDefinitions.Add(row_e_1_3);
            RowDefinition row_e_1_4 = new RowDefinition();
            row_e_1_4.Height = new GridLength(50F, GridUnitType.Pixel);
            this.e_1.RowDefinitions.Add(row_e_1_4);
            RowDefinition row_e_1_5 = new RowDefinition();
            row_e_1_5.Height = new GridLength(50F, GridUnitType.Pixel);
            this.e_1.RowDefinitions.Add(row_e_1_5);
            RowDefinition row_e_1_6 = new RowDefinition();
            row_e_1_6.Height = new GridLength(50F, GridUnitType.Pixel);
            this.e_1.RowDefinitions.Add(row_e_1_6);
            RowDefinition row_e_1_7 = new RowDefinition();
            row_e_1_7.Height = new GridLength(50F, GridUnitType.Pixel);
            this.e_1.RowDefinitions.Add(row_e_1_7);
            RowDefinition row_e_1_8 = new RowDefinition();
            row_e_1_8.Height = new GridLength(50F, GridUnitType.Pixel);
            this.e_1.RowDefinitions.Add(row_e_1_8);
            ColumnDefinition col_e_1_0 = new ColumnDefinition();
            col_e_1_0.Width = new GridLength(1F, GridUnitType.Star);
            this.e_1.ColumnDefinitions.Add(col_e_1_0);
            ColumnDefinition col_e_1_1 = new ColumnDefinition();
            col_e_1_1.Width = new GridLength(1F, GridUnitType.Star);
            this.e_1.ColumnDefinitions.Add(col_e_1_1);
            ColumnDefinition col_e_1_2 = new ColumnDefinition();
            col_e_1_2.Width = new GridLength(1F, GridUnitType.Star);
            this.e_1.ColumnDefinitions.Add(col_e_1_2);
            ColumnDefinition col_e_1_3 = new ColumnDefinition();
            col_e_1_3.Width = new GridLength(1F, GridUnitType.Star);
            this.e_1.ColumnDefinitions.Add(col_e_1_3);
            // e_2 element
            this.e_2 = new Image();
            this.e_1.Children.Add(this.e_2);
            this.e_2.Name = "e_2";
            BitmapImage e_2_bm = new BitmapImage();
            e_2_bm.TextureAsset = "Images/CeriyoLogo";
            this.e_2.Source = e_2_bm;
            Grid.SetColumn(this.e_2, 1);
            Grid.SetRow(this.e_2, 0);
            Grid.SetColumnSpan(this.e_2, 2);
            // e_3 element
            this.e_3 = new TextBlock();
            this.e_1.Children.Add(this.e_3);
            this.e_3.Name = "e_3";
            this.e_3.HorizontalAlignment = HorizontalAlignment.Center;
            Grid.SetColumn(this.e_3, 1);
            Grid.SetRow(this.e_3, 1);
            Grid.SetColumnSpan(this.e_3, 2);
            Binding binding_e_3_Text = new Binding("WelcomeText");
            this.e_3.SetBinding(TextBlock.TextProperty, binding_e_3_Text);
            // e_4 element
            this.e_4 = new Button();
            this.e_1.Children.Add(this.e_4);
            this.e_4.Name = "e_4";
            this.e_4.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_4.Content = "Join Server";
            Grid.SetColumn(this.e_4, 1);
            Grid.SetRow(this.e_4, 2);
            Grid.SetColumnSpan(this.e_4, 2);
            Binding binding_e_4_Command = new Binding("JoinServerCommand");
            this.e_4.SetBinding(Button.CommandProperty, binding_e_4_Command);
            // e_5 element
            this.e_5 = new Button();
            this.e_1.Children.Add(this.e_5);
            this.e_5.Name = "e_5";
            this.e_5.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_5.Content = "Direct Connect";
            Grid.SetColumn(this.e_5, 1);
            Grid.SetRow(this.e_5, 3);
            Grid.SetColumnSpan(this.e_5, 2);
            Binding binding_e_5_Command = new Binding("DirectConnectCommand");
            this.e_5.SetBinding(Button.CommandProperty, binding_e_5_Command);
            // e_6 element
            this.e_6 = new Button();
            this.e_1.Children.Add(this.e_6);
            this.e_6.Name = "e_6";
            this.e_6.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_6.Content = "Game Settings";
            Grid.SetColumn(this.e_6, 1);
            Grid.SetRow(this.e_6, 4);
            Grid.SetColumnSpan(this.e_6, 2);
            Binding binding_e_6_Command = new Binding("GameSettingsCommand");
            this.e_6.SetBinding(Button.CommandProperty, binding_e_6_Command);
            // e_7 element
            this.e_7 = new Button();
            this.e_1.Children.Add(this.e_7);
            this.e_7.Name = "e_7";
            this.e_7.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_7.Content = "Manage Account";
            Grid.SetColumn(this.e_7, 1);
            Grid.SetRow(this.e_7, 5);
            Grid.SetColumnSpan(this.e_7, 2);
            Binding binding_e_7_Command = new Binding("ManageAccountButtonCommand");
            this.e_7.SetBinding(Button.CommandProperty, binding_e_7_Command);
            // e_8 element
            this.e_8 = new Button();
            this.e_1.Children.Add(this.e_8);
            this.e_8.Name = "e_8";
            this.e_8.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_8.Content = "Log Out";
            Grid.SetColumn(this.e_8, 1);
            Grid.SetRow(this.e_8, 6);
            Grid.SetColumnSpan(this.e_8, 2);
            Binding binding_e_8_Command = new Binding("LogOutButtonCommand");
            this.e_8.SetBinding(Button.CommandProperty, binding_e_8_Command);
            // e_9 element
            this.e_9 = new Button();
            this.e_1.Children.Add(this.e_9);
            this.e_9.Name = "e_9";
            this.e_9.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_9.Content = "Exit";
            Grid.SetColumn(this.e_9, 1);
            Grid.SetRow(this.e_9, 7);
            Grid.SetColumnSpan(this.e_9, 2);
            Binding binding_e_9_Command = new Binding("ExitButtonCommand");
            this.e_9.SetBinding(Button.CommandProperty, binding_e_9_Command);
            ImageManager.Instance.AddImage("Images/CeriyoLogo");
        }
        
        private static void InitializeElementResources(UIElement elem) {
            elem.Resources.MergedDictionaries.Add(Dictionary.Instance);
        }
    }
}
