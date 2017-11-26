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
    public partial class LoginView : UIRoot {
        
        private DockPanel e_0;
        
        private Grid e_1;
        
        private Image e_2;
        
        private TextBlock e_3;
        
        private TextBox e_4;
        
        private TextBlock e_5;
        
        private PasswordBox e_6;
        
        private Button e_7;
        
        private Button e_8;
        
        private Button e_9;
        
        public LoginView() : 
                base() {
            this.Initialize();
        }
        
        public LoginView(int width, int height) : 
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
            e_2_bm.TextureAsset = "UI/Images/CeriyoLogo";
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
            Grid.SetColumn(this.e_4, 1);
            Grid.SetRow(this.e_4, 1);
            Grid.SetColumnSpan(this.e_4, 3);
            Binding binding_e_4_Text = new Binding("Username");
            this.e_4.SetBinding(TextBox.TextProperty, binding_e_4_Text);
            // e_5 element
            this.e_5 = new TextBlock();
            this.e_1.Children.Add(this.e_5);
            this.e_5.Name = "e_5";
            this.e_5.Text = "Password";
            Grid.SetColumn(this.e_5, 0);
            Grid.SetRow(this.e_5, 2);
            // e_6 element
            this.e_6 = new PasswordBox();
            this.e_1.Children.Add(this.e_6);
            this.e_6.Name = "e_6";
            this.e_6.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_6.TabIndex = 1;
            Grid.SetColumn(this.e_6, 1);
            Grid.SetRow(this.e_6, 2);
            Grid.SetColumnSpan(this.e_6, 3);
            Binding binding_e_6_Text = new Binding("Password");
            this.e_6.SetBinding(PasswordBox.TextProperty, binding_e_6_Text);
            // e_7 element
            this.e_7 = new Button();
            this.e_1.Children.Add(this.e_7);
            this.e_7.Name = "e_7";
            this.e_7.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_7.TabIndex = 2;
            this.e_7.Content = "Log In";
            Grid.SetColumn(this.e_7, 1);
            Grid.SetRow(this.e_7, 4);
            Binding binding_e_7_Command = new Binding("LoginCommand");
            this.e_7.SetBinding(Button.CommandProperty, binding_e_7_Command);
            // e_8 element
            this.e_8 = new Button();
            this.e_1.Children.Add(this.e_8);
            this.e_8.Name = "e_8";
            this.e_8.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_8.TabIndex = 3;
            this.e_8.Content = "Create Account";
            Grid.SetColumn(this.e_8, 2);
            Grid.SetRow(this.e_8, 4);
            Binding binding_e_8_Command = new Binding("CreateAccountCommand");
            this.e_8.SetBinding(Button.CommandProperty, binding_e_8_Command);
            // e_9 element
            this.e_9 = new Button();
            this.e_1.Children.Add(this.e_9);
            this.e_9.Name = "e_9";
            this.e_9.Margin = new Thickness(4F, 4F, 4F, 4F);
            this.e_9.TabIndex = 4;
            this.e_9.Content = "Exit";
            Grid.SetColumn(this.e_9, 3);
            Grid.SetRow(this.e_9, 4);
            Binding binding_e_9_Command = new Binding("ExitCommand");
            this.e_9.SetBinding(Button.CommandProperty, binding_e_9_Command);
            ImageManager.Instance.AddImage("UI/Images/CeriyoLogo");
        }
        
        private static void InitializeElementResources(UIElement elem) {
            elem.Resources.MergedDictionaries.Add(Dictionary.Instance);
        }
    }
}
