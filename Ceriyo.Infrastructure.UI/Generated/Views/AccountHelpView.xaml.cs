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
    public partial class AccountHelpView : UIRoot {
        
        private DockPanel e_0;
        
        private Grid e_1;
        
        private Image e_2;
        
        private TextBlock e_3;
        
        private ComboBox e_4;
        
        private DockPanel e_5;
        
        private Grid e_6;
        
        private TextBlock e_7;
        
        private TextBox e_8;
        
        private TextBlock e_9;
        
        private TextBox e_10;
        
        private Button e_11;
        
        private Button e_12;
        
        public AccountHelpView() : 
                base() {
            this.Initialize();
        }
        
        public AccountHelpView(int width, int height) : 
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
            row_e_1_2.Height = new GridLength(40F, GridUnitType.Pixel);
            this.e_1.RowDefinitions.Add(row_e_1_2);
            RowDefinition row_e_1_3 = new RowDefinition();
            row_e_1_3.Height = new GridLength(1F, GridUnitType.Star);
            this.e_1.RowDefinitions.Add(row_e_1_3);
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
            this.e_3.Text = "How can we help?";
            Grid.SetColumn(this.e_3, 1);
            Grid.SetRow(this.e_3, 1);
            Grid.SetColumnSpan(this.e_3, 2);
            // e_4 element
            this.e_4 = new ComboBox();
            this.e_1.Children.Add(this.e_4);
            this.e_4.Name = "e_4";
            this.e_4.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_4.TabIndex = 0;
            Grid.SetColumn(this.e_4, 0);
            Grid.SetRow(this.e_4, 2);
            Grid.SetColumnSpan(this.e_4, 4);
            Binding binding_e_4_IsEnabled = new Binding("IsEnabled");
            this.e_4.SetBinding(ComboBox.IsEnabledProperty, binding_e_4_IsEnabled);
            Binding binding_e_4_ItemsSource = new Binding("HelpOptions");
            this.e_4.SetBinding(ComboBox.ItemsSourceProperty, binding_e_4_ItemsSource);
            Binding binding_e_4_SelectedIndex = new Binding("SelectedHelpOptionIndex");
            this.e_4.SetBinding(ComboBox.SelectedIndexProperty, binding_e_4_SelectedIndex);
            // e_5 element
            this.e_5 = new DockPanel();
            this.e_1.Children.Add(this.e_5);
            this.e_5.Name = "e_5";
            Grid.SetColumn(this.e_5, 0);
            Grid.SetRow(this.e_5, 3);
            Grid.SetColumnSpan(this.e_5, 4);
            // e_6 element
            this.e_6 = new Grid();
            this.e_5.Children.Add(this.e_6);
            this.e_6.Name = "e_6";
            RowDefinition row_e_6_0 = new RowDefinition();
            row_e_6_0.Height = new GridLength(50F, GridUnitType.Pixel);
            this.e_6.RowDefinitions.Add(row_e_6_0);
            RowDefinition row_e_6_1 = new RowDefinition();
            row_e_6_1.Height = new GridLength(50F, GridUnitType.Pixel);
            this.e_6.RowDefinitions.Add(row_e_6_1);
            RowDefinition row_e_6_2 = new RowDefinition();
            row_e_6_2.Height = new GridLength(50F, GridUnitType.Pixel);
            this.e_6.RowDefinitions.Add(row_e_6_2);
            RowDefinition row_e_6_3 = new RowDefinition();
            row_e_6_3.Height = new GridLength(50F, GridUnitType.Pixel);
            this.e_6.RowDefinitions.Add(row_e_6_3);
            ColumnDefinition col_e_6_0 = new ColumnDefinition();
            col_e_6_0.Width = new GridLength(1F, GridUnitType.Star);
            this.e_6.ColumnDefinitions.Add(col_e_6_0);
            ColumnDefinition col_e_6_1 = new ColumnDefinition();
            col_e_6_1.Width = new GridLength(1F, GridUnitType.Star);
            this.e_6.ColumnDefinitions.Add(col_e_6_1);
            ColumnDefinition col_e_6_2 = new ColumnDefinition();
            col_e_6_2.Width = new GridLength(1F, GridUnitType.Star);
            this.e_6.ColumnDefinitions.Add(col_e_6_2);
            ColumnDefinition col_e_6_3 = new ColumnDefinition();
            col_e_6_3.Width = new GridLength(1F, GridUnitType.Star);
            this.e_6.ColumnDefinitions.Add(col_e_6_3);
            // e_7 element
            this.e_7 = new TextBlock();
            this.e_6.Children.Add(this.e_7);
            this.e_7.Name = "e_7";
            this.e_7.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_7.Text = "Username:";
            Grid.SetColumn(this.e_7, 0);
            Grid.SetRow(this.e_7, 0);
            // e_8 element
            this.e_8 = new TextBox();
            this.e_6.Children.Add(this.e_8);
            this.e_8.Name = "e_8";
            this.e_8.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_8.TabIndex = 1;
            Grid.SetColumn(this.e_8, 1);
            Grid.SetRow(this.e_8, 0);
            Grid.SetColumnSpan(this.e_8, 3);
            Binding binding_e_8_IsEnabled = new Binding("IsEnabled");
            this.e_8.SetBinding(TextBox.IsEnabledProperty, binding_e_8_IsEnabled);
            Binding binding_e_8_Text = new Binding("Username");
            this.e_8.SetBinding(TextBox.TextProperty, binding_e_8_Text);
            // e_9 element
            this.e_9 = new TextBlock();
            this.e_6.Children.Add(this.e_9);
            this.e_9.Name = "e_9";
            this.e_9.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_9.Text = "Email:";
            Grid.SetColumn(this.e_9, 0);
            Grid.SetRow(this.e_9, 1);
            // e_10 element
            this.e_10 = new TextBox();
            this.e_6.Children.Add(this.e_10);
            this.e_10.Name = "e_10";
            this.e_10.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_10.TabIndex = 2;
            Grid.SetColumn(this.e_10, 1);
            Grid.SetRow(this.e_10, 1);
            Grid.SetColumnSpan(this.e_10, 3);
            Binding binding_e_10_IsEnabled = new Binding("IsEnabled");
            this.e_10.SetBinding(TextBox.IsEnabledProperty, binding_e_10_IsEnabled);
            Binding binding_e_10_Text = new Binding("Email");
            this.e_10.SetBinding(TextBox.TextProperty, binding_e_10_Text);
            // e_11 element
            this.e_11 = new Button();
            this.e_6.Children.Add(this.e_11);
            this.e_11.Name = "e_11";
            this.e_11.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_11.TabIndex = 3;
            Grid.SetColumn(this.e_11, 0);
            Grid.SetRow(this.e_11, 2);
            Grid.SetColumnSpan(this.e_11, 2);
            Binding binding_e_11_IsEnabled = new Binding("IsEnabled");
            this.e_11.SetBinding(Button.IsEnabledProperty, binding_e_11_IsEnabled);
            Binding binding_e_11_Content = new Binding("ConfirmButtonText");
            this.e_11.SetBinding(Button.ContentProperty, binding_e_11_Content);
            Binding binding_e_11_Command = new Binding("ConfirmCommand");
            this.e_11.SetBinding(Button.CommandProperty, binding_e_11_Command);
            // e_12 element
            this.e_12 = new Button();
            this.e_6.Children.Add(this.e_12);
            this.e_12.Name = "e_12";
            this.e_12.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_12.TabIndex = 4;
            this.e_12.Content = "Back";
            Grid.SetColumn(this.e_12, 2);
            Grid.SetRow(this.e_12, 2);
            Grid.SetColumnSpan(this.e_12, 2);
            Binding binding_e_12_IsEnabled = new Binding("IsEnabled");
            this.e_12.SetBinding(Button.IsEnabledProperty, binding_e_12_IsEnabled);
            Binding binding_e_12_Command = new Binding("BackCommand");
            this.e_12.SetBinding(Button.CommandProperty, binding_e_12_Command);
            ImageManager.Instance.AddImage("Images/CeriyoLogo");
        }
        
        private static void InitializeElementResources(UIElement elem) {
            elem.Resources.MergedDictionaries.Add(Dictionary.Instance);
        }
    }
}
