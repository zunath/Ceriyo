using Ceriyo.Data;
using Ceriyo.Data.ResourceObjects;
using Ceriyo.Entities.GUI;
using FlatRedBall.IO;
using Squid;

namespace Ceriyo.Library.SquidGUI
{
    public class SquidLayoutManager
    {
        private SquidDesktop _desktop;

        public SquidLayoutManager()
        {
            _desktop = new SquidDesktop();
        }

        public SquidDesktop LayoutToDesktop(string layoutFileName)
        {
            BuildLayout(layoutFileName);

            return _desktop;
        }

        private void BuildLayout(string layoutFileName)
        {
            string path = EnginePaths.GUIDirectory + "Layouts/" + layoutFileName + EnginePaths.DataExtension;
            UILayout layout = FileManager.XmlDeserialize<UILayout>(path);

            foreach (UIComponent component in layout.Components)
            {
                Control control = BuildControl(component, _desktop);

                foreach (UIComponent child in component.Children)
                {
                    BuildControl(child, control);
                }
            }
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
                default:
                    // If not a primative control, assume it's another layout and attempt to load it.
                    try
                    {
                        BuildLayout(component.ComponentType.ToLower());
                    }
                    catch
                    {
                        result = null;
                    }
                    break;
            }

            if (result != null)
            {
                result.Parent = parent;

                result.Size = new Point(component.SizeX, component.SizeY);
                result.Position = new Point(component.PositionX, component.PositionY);
                result.Anchor = component.Anchor;
                result.Cursor = component.CursorType;
                result.Name = component.Name;
                result.Enabled = component.Enabled;
                result.Visible = component.Visible;
            }
            

            return result;
        }

        private Window BuildWindow(UIComponent component)
        {
            UIWindow window = new UIWindow();
            window.Titlebar.Text = component.Title;
            window.Resizable = component.Resizeable;

            return window;
        }

        private Label BuildLabel(UIComponent component)
        {
            Label label = new Label();
            label.Text = component.Text;

            return label;
        }

        private TextBox BuildTextBox(UIComponent component)
        {
            TextBox box = new TextBox();
            box.Text = component.Text;
            box.Style = "textbox";

            return box;
        }

        private Button BuildButton(UIComponent component)
        {
            Button button = new Button();
            button.Text = component.Text;
            button.Style = "button";
            
            return button;
        }

        private CheckBox BuildCheckBox(UIComponent component)
        {
            CheckBox box = new CheckBox();
            box.Text = component.Text;
            box.Button.Style = "checkBox";
            
            return box;
        }
    }
}
