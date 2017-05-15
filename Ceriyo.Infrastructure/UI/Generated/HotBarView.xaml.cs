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
    public partial class HotBarView : UserControl {
        
        private Grid e_0;
        
        private Button e_1;
        
        private Button e_2;
        
        private Button e_3;
        
        private Button e_4;
        
        private Button e_5;
        
        private Button e_6;
        
        private Button e_7;
        
        private Button e_8;
        
        private Button e_9;
        
        private Button e_10;
        
        public HotBarView() {
            Style style = UserControlStyle.CreateUserControlStyle();
            style.TargetType = this.GetType();
            this.Style = style;
            this.InitializeComponent();
        }
        
        private void InitializeComponent() {
            // e_0 element
            this.e_0 = new Grid();
            this.Content = this.e_0;
            this.e_0.Name = "e_0";
            ColumnDefinition col_e_0_0 = new ColumnDefinition();
            col_e_0_0.Width = new GridLength(30F, GridUnitType.Pixel);
            this.e_0.ColumnDefinitions.Add(col_e_0_0);
            ColumnDefinition col_e_0_1 = new ColumnDefinition();
            col_e_0_1.Width = new GridLength(30F, GridUnitType.Pixel);
            this.e_0.ColumnDefinitions.Add(col_e_0_1);
            ColumnDefinition col_e_0_2 = new ColumnDefinition();
            col_e_0_2.Width = new GridLength(30F, GridUnitType.Pixel);
            this.e_0.ColumnDefinitions.Add(col_e_0_2);
            ColumnDefinition col_e_0_3 = new ColumnDefinition();
            col_e_0_3.Width = new GridLength(30F, GridUnitType.Pixel);
            this.e_0.ColumnDefinitions.Add(col_e_0_3);
            ColumnDefinition col_e_0_4 = new ColumnDefinition();
            col_e_0_4.Width = new GridLength(30F, GridUnitType.Pixel);
            this.e_0.ColumnDefinitions.Add(col_e_0_4);
            ColumnDefinition col_e_0_5 = new ColumnDefinition();
            col_e_0_5.Width = new GridLength(30F, GridUnitType.Pixel);
            this.e_0.ColumnDefinitions.Add(col_e_0_5);
            ColumnDefinition col_e_0_6 = new ColumnDefinition();
            col_e_0_6.Width = new GridLength(30F, GridUnitType.Pixel);
            this.e_0.ColumnDefinitions.Add(col_e_0_6);
            ColumnDefinition col_e_0_7 = new ColumnDefinition();
            col_e_0_7.Width = new GridLength(30F, GridUnitType.Pixel);
            this.e_0.ColumnDefinitions.Add(col_e_0_7);
            ColumnDefinition col_e_0_8 = new ColumnDefinition();
            col_e_0_8.Width = new GridLength(30F, GridUnitType.Pixel);
            this.e_0.ColumnDefinitions.Add(col_e_0_8);
            ColumnDefinition col_e_0_9 = new ColumnDefinition();
            col_e_0_9.Width = new GridLength(30F, GridUnitType.Pixel);
            this.e_0.ColumnDefinitions.Add(col_e_0_9);
            // e_1 element
            this.e_1 = new Button();
            this.e_0.Children.Add(this.e_1);
            this.e_1.Name = "e_1";
            this.e_1.Content = "1";
            Grid.SetColumn(this.e_1, 0);
            Grid.SetRow(this.e_1, 0);
            // e_2 element
            this.e_2 = new Button();
            this.e_0.Children.Add(this.e_2);
            this.e_2.Name = "e_2";
            this.e_2.Content = "2";
            Grid.SetColumn(this.e_2, 1);
            Grid.SetRow(this.e_2, 0);
            // e_3 element
            this.e_3 = new Button();
            this.e_0.Children.Add(this.e_3);
            this.e_3.Name = "e_3";
            this.e_3.Content = "3";
            Grid.SetColumn(this.e_3, 2);
            Grid.SetRow(this.e_3, 0);
            // e_4 element
            this.e_4 = new Button();
            this.e_0.Children.Add(this.e_4);
            this.e_4.Name = "e_4";
            this.e_4.Content = "4";
            Grid.SetColumn(this.e_4, 3);
            Grid.SetRow(this.e_4, 0);
            // e_5 element
            this.e_5 = new Button();
            this.e_0.Children.Add(this.e_5);
            this.e_5.Name = "e_5";
            this.e_5.Content = "5";
            Grid.SetColumn(this.e_5, 4);
            Grid.SetRow(this.e_5, 0);
            // e_6 element
            this.e_6 = new Button();
            this.e_0.Children.Add(this.e_6);
            this.e_6.Name = "e_6";
            this.e_6.Content = "6";
            Grid.SetColumn(this.e_6, 5);
            Grid.SetRow(this.e_6, 0);
            // e_7 element
            this.e_7 = new Button();
            this.e_0.Children.Add(this.e_7);
            this.e_7.Name = "e_7";
            this.e_7.Content = "7";
            Grid.SetColumn(this.e_7, 6);
            Grid.SetRow(this.e_7, 0);
            // e_8 element
            this.e_8 = new Button();
            this.e_0.Children.Add(this.e_8);
            this.e_8.Name = "e_8";
            this.e_8.Content = "8";
            Grid.SetColumn(this.e_8, 7);
            Grid.SetRow(this.e_8, 0);
            // e_9 element
            this.e_9 = new Button();
            this.e_0.Children.Add(this.e_9);
            this.e_9.Name = "e_9";
            this.e_9.Content = "9";
            Grid.SetColumn(this.e_9, 8);
            Grid.SetRow(this.e_9, 0);
            // e_10 element
            this.e_10 = new Button();
            this.e_0.Children.Add(this.e_10);
            this.e_10.Name = "e_10";
            this.e_10.Content = "0";
            Grid.SetColumn(this.e_10, 9);
            Grid.SetRow(this.e_10, 0);
        }
    }
}
