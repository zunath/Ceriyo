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
    public partial class MainMenu : UIRoot {
        
        private DockPanel e_0;
        
        private Grid e_1;
        
        private Image e_2;
        
        private Button e_3;
        
        private Button e_4;
        
        private Button e_5;
        
        private Button e_6;
        
        public MainMenu() : 
                base() {
            this.Initialize();
        }
        
        public MainMenu(int width, int height) : 
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
            this.Background = new SolidColorBrush(new ColorW(169, 169, 169, 255));
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
            row_e_1_0.Height = new GridLength(150F, GridUnitType.Pixel);
            this.e_1.RowDefinitions.Add(row_e_1_0);
            RowDefinition row_e_1_1 = new RowDefinition();
            row_e_1_1.Height = new GridLength(50F, GridUnitType.Pixel);
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
            Grid.SetColumn(this.e_2, 0);
            Grid.SetRow(this.e_2, 0);
            Grid.SetColumnSpan(this.e_2, 4);
            // e_3 element
            this.e_3 = new Button();
            this.e_1.Children.Add(this.e_3);
            this.e_3.Name = "e_3";
            this.e_3.Margin = new Thickness(4F, 4F, 4F, 4F);
            Grid.SetColumn(this.e_3, 1);
            Grid.SetRow(this.e_3, 1);
            Grid.SetColumnSpan(this.e_3, 2);
            Binding binding_e_3_Content = new Binding("JoinServerText");
            this.e_3.SetBinding(Button.ContentProperty, binding_e_3_Content);
            Binding binding_e_3_Command = new Binding("JoinServerCommand");
            this.e_3.SetBinding(Button.CommandProperty, binding_e_3_Command);
            // e_4 element
            this.e_4 = new Button();
            this.e_1.Children.Add(this.e_4);
            this.e_4.Name = "e_4";
            this.e_4.Margin = new Thickness(4F, 4F, 4F, 4F);
            Grid.SetColumn(this.e_4, 1);
            Grid.SetRow(this.e_4, 2);
            Grid.SetColumnSpan(this.e_4, 2);
            Binding binding_e_4_Content = new Binding("DirectConnectText");
            this.e_4.SetBinding(Button.ContentProperty, binding_e_4_Content);
            Binding binding_e_4_Command = new Binding("DirectConnectCommand");
            this.e_4.SetBinding(Button.CommandProperty, binding_e_4_Command);
            // e_5 element
            this.e_5 = new Button();
            this.e_1.Children.Add(this.e_5);
            this.e_5.Name = "e_5";
            this.e_5.Margin = new Thickness(4F, 4F, 4F, 4F);
            Grid.SetColumn(this.e_5, 1);
            Grid.SetRow(this.e_5, 3);
            Grid.SetColumnSpan(this.e_5, 2);
            Binding binding_e_5_Content = new Binding("SettingsText");
            this.e_5.SetBinding(Button.ContentProperty, binding_e_5_Content);
            Binding binding_e_5_Command = new Binding("SettingsCommand");
            this.e_5.SetBinding(Button.CommandProperty, binding_e_5_Command);
            // e_6 element
            this.e_6 = new Button();
            this.e_1.Children.Add(this.e_6);
            this.e_6.Name = "e_6";
            this.e_6.Margin = new Thickness(4F, 4F, 4F, 4F);
            Grid.SetColumn(this.e_6, 1);
            Grid.SetRow(this.e_6, 4);
            Grid.SetColumnSpan(this.e_6, 2);
            Binding binding_e_6_Content = new Binding("ExitApplicationText");
            this.e_6.SetBinding(Button.ContentProperty, binding_e_6_Content);
            Binding binding_e_6_Command = new Binding("ExitButtonCommand");
            this.e_6.SetBinding(Button.CommandProperty, binding_e_6_Command);
            ImageManager.Instance.AddImage("Images/CeriyoLogo");
        }
        
        private static void InitializeElementResources(UIElement elem) {
            elem.Resources.MergedDictionaries.Add(Dictionary.Instance);
        }
    }
}
