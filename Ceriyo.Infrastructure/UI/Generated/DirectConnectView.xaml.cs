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
    public partial class DirectConnectView : UIRoot {
        
        private Grid e_0;
        
        private Image e_1;
        
        private TextBlock e_2;
        
        private TextBox e_3;
        
        private TextBlock e_4;
        
        private NumericTextBox e_5;
        
        private TextBlock e_6;
        
        private PasswordBox e_7;
        
        private Button e_8;
        
        private Button e_9;
        
        public DirectConnectView() : 
                base() {
            this.Initialize();
        }
        
        public DirectConnectView(int width, int height) : 
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
            InitializeElementResources(this);
            // e_0 element
            this.e_0 = new Grid();
            this.Content = this.e_0;
            this.e_0.Name = "e_0";
            RowDefinition row_e_0_0 = new RowDefinition();
            row_e_0_0.Height = new GridLength(100F, GridUnitType.Pixel);
            this.e_0.RowDefinitions.Add(row_e_0_0);
            RowDefinition row_e_0_1 = new RowDefinition();
            row_e_0_1.Height = new GridLength(30F, GridUnitType.Pixel);
            this.e_0.RowDefinitions.Add(row_e_0_1);
            RowDefinition row_e_0_2 = new RowDefinition();
            row_e_0_2.Height = new GridLength(30F, GridUnitType.Pixel);
            this.e_0.RowDefinitions.Add(row_e_0_2);
            RowDefinition row_e_0_3 = new RowDefinition();
            row_e_0_3.Height = new GridLength(40F, GridUnitType.Pixel);
            this.e_0.RowDefinitions.Add(row_e_0_3);
            ColumnDefinition col_e_0_0 = new ColumnDefinition();
            col_e_0_0.Width = new GridLength(1F, GridUnitType.Star);
            this.e_0.ColumnDefinitions.Add(col_e_0_0);
            ColumnDefinition col_e_0_1 = new ColumnDefinition();
            col_e_0_1.Width = new GridLength(1F, GridUnitType.Star);
            this.e_0.ColumnDefinitions.Add(col_e_0_1);
            ColumnDefinition col_e_0_2 = new ColumnDefinition();
            col_e_0_2.Width = new GridLength(1F, GridUnitType.Star);
            this.e_0.ColumnDefinitions.Add(col_e_0_2);
            ColumnDefinition col_e_0_3 = new ColumnDefinition();
            col_e_0_3.Width = new GridLength(1F, GridUnitType.Star);
            this.e_0.ColumnDefinitions.Add(col_e_0_3);
            ColumnDefinition col_e_0_4 = new ColumnDefinition();
            col_e_0_4.Width = new GridLength(1F, GridUnitType.Star);
            this.e_0.ColumnDefinitions.Add(col_e_0_4);
            // e_1 element
            this.e_1 = new Image();
            this.e_0.Children.Add(this.e_1);
            this.e_1.Name = "e_1";
            BitmapImage e_1_bm = new BitmapImage();
            e_1_bm.TextureAsset = "UI/Images/DirectConnect";
            this.e_1.Source = e_1_bm;
            Grid.SetColumn(this.e_1, 1);
            Grid.SetRow(this.e_1, 0);
            Grid.SetColumnSpan(this.e_1, 3);
            // e_2 element
            this.e_2 = new TextBlock();
            this.e_0.Children.Add(this.e_2);
            this.e_2.Name = "e_2";
            this.e_2.Margin = new Thickness(12F, 4F, 4F, 4F);
            this.e_2.Text = "IP Address: ";
            Grid.SetColumn(this.e_2, 0);
            Grid.SetRow(this.e_2, 1);
            // e_3 element
            this.e_3 = new TextBox();
            this.e_0.Children.Add(this.e_3);
            this.e_3.Name = "e_3";
            Grid.SetColumn(this.e_3, 1);
            Grid.SetRow(this.e_3, 1);
            Grid.SetColumnSpan(this.e_3, 2);
            Binding binding_e_3_Text = new Binding("IPAddress");
            this.e_3.SetBinding(TextBox.TextProperty, binding_e_3_Text);
            // e_4 element
            this.e_4 = new TextBlock();
            this.e_0.Children.Add(this.e_4);
            this.e_4.Name = "e_4";
            this.e_4.Margin = new Thickness(12F, 4F, 4F, 4F);
            this.e_4.Text = "Port: ";
            Grid.SetColumn(this.e_4, 3);
            Grid.SetRow(this.e_4, 1);
            // e_5 element
            this.e_5 = new NumericTextBox();
            this.e_0.Children.Add(this.e_5);
            this.e_5.Name = "e_5";
            Grid.SetColumn(this.e_5, 4);
            Grid.SetRow(this.e_5, 1);
            Binding binding_e_5_Value = new Binding("Port");
            this.e_5.SetBinding(NumericTextBox.ValueProperty, binding_e_5_Value);
            // e_6 element
            this.e_6 = new TextBlock();
            this.e_0.Children.Add(this.e_6);
            this.e_6.Name = "e_6";
            this.e_6.Margin = new Thickness(12F, 4F, 4F, 4F);
            this.e_6.Text = "Password: ";
            Grid.SetColumn(this.e_6, 0);
            Grid.SetRow(this.e_6, 2);
            // e_7 element
            this.e_7 = new PasswordBox();
            this.e_0.Children.Add(this.e_7);
            this.e_7.Name = "e_7";
            Grid.SetColumn(this.e_7, 1);
            Grid.SetRow(this.e_7, 2);
            Grid.SetColumnSpan(this.e_7, 2);
            Binding binding_e_7_Text = new Binding("Password");
            this.e_7.SetBinding(PasswordBox.TextProperty, binding_e_7_Text);
            // e_8 element
            this.e_8 = new Button();
            this.e_0.Children.Add(this.e_8);
            this.e_8.Name = "e_8";
            this.e_8.Margin = new Thickness(1F, 4F, 0F, 4F);
            this.e_8.Content = "Back";
            Grid.SetColumn(this.e_8, 3);
            Grid.SetRow(this.e_8, 3);
            Grid.SetColumnSpan(this.e_8, 2);
            Binding binding_e_8_Command = new Binding("BackCommand");
            this.e_8.SetBinding(Button.CommandProperty, binding_e_8_Command);
            // e_9 element
            this.e_9 = new Button();
            this.e_0.Children.Add(this.e_9);
            this.e_9.Name = "e_9";
            this.e_9.Margin = new Thickness(0F, 4F, 1F, 4F);
            this.e_9.Content = "Connect";
            Grid.SetColumn(this.e_9, 1);
            Grid.SetRow(this.e_9, 3);
            Grid.SetColumnSpan(this.e_9, 2);
            Binding binding_e_9_Command = new Binding("ConnectCommand");
            this.e_9.SetBinding(Button.CommandProperty, binding_e_9_Command);
            ImageManager.Instance.AddImage("UI/Images/DirectConnect");
        }
        
        private static void InitializeElementResources(UIElement elem) {
            elem.Resources.MergedDictionaries.Add(Dictionary.Instance);
        }
    }
}
