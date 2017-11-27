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
    public partial class RegisterView : UIRoot {
        
        private DockPanel e_0;
        
        private Grid e_1;
        
        private Image e_2;
        
        private TextBlock e_3;
        
        private TextBox e_4;
        
        private TextBlock e_5;
        
        private TextBox e_6;
        
        private TextBlock e_7;
        
        private PasswordBox e_8;
        
        private TextBlock e_9;
        
        private PasswordBox e_10;
        
        private Button e_11;
        
        private Button e_12;
        
        private Border e_13;
        
        private TextBlock e_14;
        
        public RegisterView() : 
                base() {
            this.Initialize();
        }
        
        public RegisterView(int width, int height) : 
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
            RowDefinition row_e_1_5 = new RowDefinition();
            row_e_1_5.Height = new GridLength(50F, GridUnitType.Pixel);
            this.e_1.RowDefinitions.Add(row_e_1_5);
            RowDefinition row_e_1_6 = new RowDefinition();
            row_e_1_6.Height = new GridLength(50F, GridUnitType.Pixel);
            this.e_1.RowDefinitions.Add(row_e_1_6);
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
            this.e_3.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_3.Text = "Username: ";
            Grid.SetColumn(this.e_3, 0);
            Grid.SetRow(this.e_3, 1);
            // e_4 element
            this.e_4 = new TextBox();
            this.e_1.Children.Add(this.e_4);
            this.e_4.Name = "e_4";
            this.e_4.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_4.TabIndex = 0;
            this.e_4.MaxLength = 256;
            Grid.SetColumn(this.e_4, 1);
            Grid.SetRow(this.e_4, 1);
            Grid.SetColumnSpan(this.e_4, 3);
            Binding binding_e_4_IsEnabled = new Binding("IsEnabled");
            this.e_4.SetBinding(TextBox.IsEnabledProperty, binding_e_4_IsEnabled);
            Binding binding_e_4_Text = new Binding("Username");
            this.e_4.SetBinding(TextBox.TextProperty, binding_e_4_Text);
            // e_5 element
            this.e_5 = new TextBlock();
            this.e_1.Children.Add(this.e_5);
            this.e_5.Name = "e_5";
            this.e_5.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_5.Text = "Email:";
            Grid.SetColumn(this.e_5, 0);
            Grid.SetRow(this.e_5, 2);
            // e_6 element
            this.e_6 = new TextBox();
            this.e_1.Children.Add(this.e_6);
            this.e_6.Name = "e_6";
            this.e_6.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_6.TabIndex = 1;
            this.e_6.MaxLength = 256;
            Grid.SetColumn(this.e_6, 1);
            Grid.SetRow(this.e_6, 2);
            Grid.SetColumnSpan(this.e_6, 3);
            Binding binding_e_6_IsEnabled = new Binding("IsEnabled");
            this.e_6.SetBinding(TextBox.IsEnabledProperty, binding_e_6_IsEnabled);
            Binding binding_e_6_Text = new Binding("Email");
            this.e_6.SetBinding(TextBox.TextProperty, binding_e_6_Text);
            // e_7 element
            this.e_7 = new TextBlock();
            this.e_1.Children.Add(this.e_7);
            this.e_7.Name = "e_7";
            this.e_7.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_7.Text = "Password";
            Grid.SetColumn(this.e_7, 0);
            Grid.SetRow(this.e_7, 3);
            // e_8 element
            this.e_8 = new PasswordBox();
            this.e_1.Children.Add(this.e_8);
            this.e_8.Name = "e_8";
            this.e_8.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_8.TabIndex = 2;
            this.e_8.MaxLength = 100;
            Grid.SetColumn(this.e_8, 1);
            Grid.SetRow(this.e_8, 3);
            Grid.SetColumnSpan(this.e_8, 3);
            Binding binding_e_8_IsEnabled = new Binding("IsEnabled");
            this.e_8.SetBinding(PasswordBox.IsEnabledProperty, binding_e_8_IsEnabled);
            Binding binding_e_8_Text = new Binding("Password");
            this.e_8.SetBinding(PasswordBox.TextProperty, binding_e_8_Text);
            // e_9 element
            this.e_9 = new TextBlock();
            this.e_1.Children.Add(this.e_9);
            this.e_9.Name = "e_9";
            this.e_9.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_9.Text = "Confirm Password:";
            Grid.SetColumn(this.e_9, 0);
            Grid.SetRow(this.e_9, 4);
            // e_10 element
            this.e_10 = new PasswordBox();
            this.e_1.Children.Add(this.e_10);
            this.e_10.Name = "e_10";
            this.e_10.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_10.TabIndex = 3;
            this.e_10.MaxLength = 100;
            Grid.SetColumn(this.e_10, 1);
            Grid.SetRow(this.e_10, 4);
            Grid.SetColumnSpan(this.e_10, 3);
            Binding binding_e_10_IsEnabled = new Binding("IsEnabled");
            this.e_10.SetBinding(PasswordBox.IsEnabledProperty, binding_e_10_IsEnabled);
            Binding binding_e_10_Text = new Binding("ConfirmPassword");
            this.e_10.SetBinding(PasswordBox.TextProperty, binding_e_10_Text);
            // e_11 element
            this.e_11 = new Button();
            this.e_1.Children.Add(this.e_11);
            this.e_11.Name = "e_11";
            this.e_11.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_11.TabIndex = 4;
            this.e_11.Content = "Register";
            Grid.SetColumn(this.e_11, 2);
            Grid.SetRow(this.e_11, 5);
            Binding binding_e_11_IsEnabled = new Binding("IsEnabled");
            this.e_11.SetBinding(Button.IsEnabledProperty, binding_e_11_IsEnabled);
            Binding binding_e_11_Command = new Binding("RegisterCommand");
            this.e_11.SetBinding(Button.CommandProperty, binding_e_11_Command);
            // e_12 element
            this.e_12 = new Button();
            this.e_1.Children.Add(this.e_12);
            this.e_12.Name = "e_12";
            this.e_12.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_12.TabIndex = 5;
            this.e_12.Content = "Cancel";
            Grid.SetColumn(this.e_12, 3);
            Grid.SetRow(this.e_12, 5);
            Binding binding_e_12_IsEnabled = new Binding("IsEnabled");
            this.e_12.SetBinding(Button.IsEnabledProperty, binding_e_12_IsEnabled);
            Binding binding_e_12_Command = new Binding("CancelCommand");
            this.e_12.SetBinding(Button.CommandProperty, binding_e_12_Command);
            // e_13 element
            this.e_13 = new Border();
            this.e_1.Children.Add(this.e_13);
            this.e_13.Name = "e_13";
            this.e_13.Height = 50F;
            this.e_13.HorizontalAlignment = HorizontalAlignment.Center;
            this.e_13.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetColumn(this.e_13, 0);
            Grid.SetRow(this.e_13, 5);
            Grid.SetColumnSpan(this.e_13, 2);
            // e_14 element
            this.e_14 = new TextBlock();
            this.e_13.Child = this.e_14;
            this.e_14.Name = "e_14";
            this.e_14.Margin = new Thickness(4F, 4F, 4F, 4F);
            Binding binding_e_14_Foreground = new Binding("InfoTextBrush");
            this.e_14.SetBinding(TextBlock.ForegroundProperty, binding_e_14_Foreground);
            Binding binding_e_14_Text = new Binding("InfoText");
            this.e_14.SetBinding(TextBlock.TextProperty, binding_e_14_Text);
            ImageManager.Instance.AddImage("Images/CeriyoLogo");
        }
        
        private static void InitializeElementResources(UIElement elem) {
            elem.Resources.MergedDictionaries.Add(Dictionary.Instance);
        }
    }
}
