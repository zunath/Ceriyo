using System;
using Ceriyo.Core.Scripting.Client.Contracts;
using Ceriyo.Core.Services.Contracts;
using Ceriyo.Core.UI.Controls;
using Squid;

namespace Ceriyo.Core.Scripting.Client
{
    public class ControlMethods : IControlMethods
    {

        public void AddToControl(Control parent, Control controlToAdd)
        {
            controlToAdd.Parent = parent;
        }

        public Window CreateWindow(
            int width,
            int height,
            int x,
            int y,
            string header = null,
            bool isResizeable = true
        )
        {
            try
            {
                if (header == null)
                {
                    return new Window
                    {
                        Size = new Point(width, height),
                        Position = new Point(x, y),
                        Resizable = isResizeable
                    };
                }
                else
                {
                    return new TitledWindow
                    {
                        Size = new Point(width, height),
                        Position = new Point(x, y),
                        Titlebar = {Text = header},
                        Resizable = isResizeable
                    };
                }
            }
            catch
            {
                return null;
            }
        }

        public Button CreateButton()
        {
            return new Button();
        }

        public CheckBox CreateCheckBox()
        {
            return new CheckBox();
        }

        public DropDownList CreateComboBox()
        {
            return new DropDownList();
        }
        
        public FlowLayoutFrame CreateFlowLayoutFrame()
        {
            return new FlowLayoutFrame();
        }

        public Dialog CreateDialog()
        {
            throw new NotImplementedException();
        }

        public Frame CreateFrame()
        {
            return new Frame();
        }

        public ImageControl CreateImageControl()
        {
            return new ImageControl();
        }

        public Label CreateLabel()
        {
            return new Label();
        }

        public ListBox CreateListBox()
        {
            return new ListBox();
        }

        public ListView CreateListView()
        {
            return new ListView();
        }

        public Panel CreatePanel()
        {
            return new Panel();
        }

        public RadioButton CreateRadioButton()
        {
            return new RadioButton();
        }

        public Resizer CreateResizer()
        {
            return new Resizer();
        }

        public ScrollBar CreateScrollBar()
        {
            return new ScrollBar();
        }

        public Slider CreateSlider()
        {
            return new Slider();
        }

        public SplitContainer CreateSplitContainer()
        {
            return new SplitContainer();
        }

        public TabControl CreateTabControl()
        {
            return new TabControl();
        }

        public TextArea CreateTextArea()
        {
            return new TextArea();
        }

        public TextBox CreateTextBox()
        {
            return new TextBox();
        }

        public TreeView CreateTreeView()
        {
            return new TreeView();
        }
    }
}
