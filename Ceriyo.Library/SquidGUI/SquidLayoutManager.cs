using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ceriyo.Data;
using Ceriyo.Data.ResourceObjects;
using Ceriyo.Entities.GUI;
using FlatRedBall.IO;
using Squid;

namespace Ceriyo.Library.SquidGUI
{
    public class SquidLayoutManager
    {
        public SquidDesktop LayoutToDesktop(string layoutFileName)
        {
            string path = EnginePaths.GUIDirectory + "Layouts/" + layoutFileName + EnginePaths.DataExtension;

            SquidDesktop desk = new SquidDesktop();
            UILayout layout = FileManager.XmlDeserialize<UILayout>(path);

            foreach (UIComponent component in layout.Components)
            {
                Control control = BuildControl(component, desk);

                foreach (UIComponent child in component.Children)
                {
                    BuildControl(child, control);
                }
            }

            return desk;
        }

        private Control BuildControl(UIComponent component, Control parent)
        {
            Control result = null;

            switch (component.ComponentType.ToLower())
            {
                case "window":
                    result = BuildWindow(component);
                    break;
                case "label":
                    result = BuildLabel(component);
                    break;
                case "textbox":
                    result = BuildTextBox(component);
                    break;
                case "button":
                    result = BuildButton(component);
                    break;
                case "checkbox":
                    result = BuildCheckBox(component);
                    break;
            }

            if (result != null)
            {
                result.Parent = parent;
            }

            return result;
        }

        private Window BuildWindow(UIComponent component)
        {
            UIWindow window = new UIWindow();
            window.Size = new Point(component.SizeX, component.SizeY);
            window.Position = new Point(component.PositionX, component.PositionY);
            window.Titlebar.Text = component.Title;
            window.Resizable = component.Resizeable;
            window.Anchor = component.Anchor;
            window.Cursor = component.CursorType;

            return window;
        }

        private Label BuildLabel(UIComponent component)
        {
            Label label = new Label();
            label.Text = component.Text;
            label.Size = new Point(component.SizeX, component.SizeY);
            label.Position = new Point(component.PositionX, component.PositionY);
            label.Cursor = component.CursorType;
            label.Anchor = component.Anchor;

            return label;
        }

        private TextBox BuildTextBox(UIComponent component)
        {
            TextBox box = new TextBox();
            box.Text = component.Text;
            box.Size = new Point(component.SizeX, component.SizeY);
            box.Position = new Point(component.PositionX, component.PositionY);
            box.Style = "textbox";
            box.Anchor = component.Anchor;

            return box;
        }

        private Button BuildButton(UIComponent component)
        {
            Button button = new Button();
            button.Size = new Point(component.SizeX, component.SizeY);
            button.Position = new Point(component.PositionX, component.PositionY);
            button.Text = component.Text;
            button.Style = "button";
            button.Anchor = component.Anchor;
            button.Cursor = component.CursorType;

            return button;
        }

        private CheckBox BuildCheckBox(UIComponent component)
        {
            CheckBox box = new CheckBox();
            box.Size = new Point(component.SizeX, component.SizeY);
            box.Position = new Point(component.PositionX, component.PositionY);
            box.Text = component.Text;
            box.Button.Style = "checkBox";
            box.Button.Size = new Point(26, 26);
            box.Button.Cursor = Cursors.Link;

            return box;
        }
    }
}
