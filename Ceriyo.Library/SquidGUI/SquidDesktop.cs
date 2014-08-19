using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Squid;

namespace Ceriyo.Library.SquidGUI
{
    public class SquidDesktop : Desktop
    {
        private Skin _skin;

        public SquidDesktop()
        {
            _skin = new Skin();
            InitializeStyles();
            InitializeCursors();
        }
        private void InitializeStyles()
        {
            ControlStyle baseStyle = new ControlStyle();
            baseStyle.Tiling = TextureMode.Grid;
            baseStyle.Grid = new Margin(3);
            baseStyle.Texture = "button_hot.dds";
            baseStyle.Default.Texture = "button_default.dds";
            baseStyle.Pressed.Texture = "button_down.dds";
            baseStyle.SelectedPressed.Texture = "button_down.dds";
            baseStyle.Focused.Texture = "button_down.dds";
            baseStyle.SelectedFocused.Texture = "button_down.dds";
            baseStyle.Selected.Texture = "button_down.dds";
            baseStyle.SelectedHot.Texture = "button_down.dds";

            ControlStyle itemStyle = new ControlStyle(baseStyle);
            itemStyle.TextPadding = new Margin(8, 0, 8, 0);
            itemStyle.TextAlign = Alignment.MiddleLeft;

            ControlStyle buttonStyle = new ControlStyle(baseStyle);
            buttonStyle.TextPadding = new Margin(0);
            buttonStyle.TextAlign = Alignment.MiddleCenter;

            ControlStyle tooltipStyle = new ControlStyle(buttonStyle);
            tooltipStyle.TextPadding = new Margin(8);
            tooltipStyle.TextAlign = Alignment.TopLeft;

            ControlStyle inputStyle = new ControlStyle();
            inputStyle.Texture = "input_default.dds";
            inputStyle.Hot.Texture = "input_focused.dds";
            inputStyle.Focused.Texture = "input_focused.dds";
            inputStyle.TextPadding = new Margin(8);
            inputStyle.Tiling = TextureMode.Grid;
            inputStyle.Focused.Tint = ColorInt.RGBA(1, 0, 0, 1);
            inputStyle.Grid = new Margin(3);

            ControlStyle windowStyle = new ControlStyle();
            windowStyle.Tiling = TextureMode.Grid;
            windowStyle.Grid = new Margin(9);
            windowStyle.Texture = "window.dds";

            ControlStyle frameStyle = new ControlStyle();
            frameStyle.Tiling = TextureMode.Grid;
            frameStyle.Grid = new Margin(4);
            frameStyle.Texture = "frame.dds";
            frameStyle.TextPadding = new Margin(8);

            ControlStyle vscrollTrackStyle = new ControlStyle();
            vscrollTrackStyle.Tiling = TextureMode.Grid;
            vscrollTrackStyle.Grid = new Margin(3);
            vscrollTrackStyle.Texture = "vscroll_track.dds";

            ControlStyle vscrollButtonStyle = new ControlStyle();
            vscrollButtonStyle.Tiling = TextureMode.Grid;
            vscrollButtonStyle.Grid = new Margin(3);
            vscrollButtonStyle.Texture = "vscroll_button.dds";
            vscrollButtonStyle.Hot.Texture = "vscroll_button_hot.dds";
            vscrollButtonStyle.Pressed.Texture = "vscroll_button_down.dds";

            ControlStyle vscrollUp = new ControlStyle();
            vscrollUp.Default.Texture = "vscrollUp_default.dds";
            vscrollUp.Hot.Texture = "vscrollUp_hot.dds";
            vscrollUp.Pressed.Texture = "vscrollUp_down.dds";
            vscrollUp.Focused.Texture = "vscrollUp_hot.dds";

            ControlStyle hscrollTrackStyle = new ControlStyle();
            hscrollTrackStyle.Tiling = TextureMode.Grid;
            hscrollTrackStyle.Grid = new Margin(3);
            hscrollTrackStyle.Texture = "hscroll_track.dds";

            ControlStyle hscrollButtonStyle = new ControlStyle();
            hscrollButtonStyle.Tiling = TextureMode.Grid;
            hscrollButtonStyle.Grid = new Margin(3);
            hscrollButtonStyle.Texture = "hscroll_button.dds";
            hscrollButtonStyle.Hot.Texture = "hscroll_button_hot.dds";
            hscrollButtonStyle.Pressed.Texture = "hscroll_button_down.dds";

            ControlStyle hscrollUp = new ControlStyle();
            hscrollUp.Default.Texture = "hscrollUp_default.dds";
            hscrollUp.Hot.Texture = "hscrollUp_hot.dds";
            hscrollUp.Pressed.Texture = "hscrollUp_down.dds";
            hscrollUp.Focused.Texture = "hscrollUp_hot.dds";

            ControlStyle checkButtonStyle = new ControlStyle();
            checkButtonStyle.Default.Texture = "checkbox_default.dds";
            checkButtonStyle.Hot.Texture = "checkbox_hot.dds";
            checkButtonStyle.Pressed.Texture = "checkbox_down.dds";
            checkButtonStyle.Checked.Texture = "checkbox_checked.dds";
            checkButtonStyle.CheckedFocused.Texture = "checkbox_checked_hot.dds";
            checkButtonStyle.CheckedHot.Texture = "checkbox_checked_hot.dds";
            checkButtonStyle.CheckedPressed.Texture = "checkbox_down.dds";

            ControlStyle comboLabelStyle = new ControlStyle();
            comboLabelStyle.TextPadding = new Margin(10, 0, 0, 0);
            comboLabelStyle.Default.Texture = "combo_default.dds";
            comboLabelStyle.Hot.Texture = "combo_hot.dds";
            comboLabelStyle.Pressed.Texture = "combo_down.dds";
            comboLabelStyle.Focused.Texture = "combo_hot.dds";
            comboLabelStyle.Tiling = TextureMode.Grid;
            comboLabelStyle.Grid = new Margin(3, 0, 0, 0);

            ControlStyle comboButtonStyle = new ControlStyle();
            comboButtonStyle.Default.Texture = "combo_button_default.dds";
            comboButtonStyle.Hot.Texture = "combo_button_hot.dds";
            comboButtonStyle.Pressed.Texture = "combo_button_down.dds";
            comboButtonStyle.Focused.Texture = "combo_button_hot.dds";

            ControlStyle multilineStyle = new ControlStyle();
            multilineStyle.TextAlign = Alignment.TopLeft;
            multilineStyle.TextPadding = new Margin(8);

            ControlStyle labelStyle = new ControlStyle();
            labelStyle.TextPadding = new Margin(8, 0, 8, 0);
            labelStyle.TextAlign = Alignment.MiddleLeft;
            labelStyle.TextColor = ColorInt.RGBA(.8f, .8f, .8f, 1);
            labelStyle.BackColor = ColorInt.RGBA(1, 1, 1, .125f);
            labelStyle.Default.BackColor = 0;

            _skin.Styles.Add("item", itemStyle);
            _skin.Styles.Add("textbox", inputStyle);
            _skin.Styles.Add("button", buttonStyle);
            _skin.Styles.Add("window", windowStyle);
            _skin.Styles.Add("frame", frameStyle);
            _skin.Styles.Add("checkBox", checkButtonStyle);
            _skin.Styles.Add("comboLabel", comboLabelStyle);
            _skin.Styles.Add("comboButton", comboButtonStyle);
            _skin.Styles.Add("vscrollTrack", vscrollTrackStyle);
            _skin.Styles.Add("vscrollButton", vscrollButtonStyle);
            _skin.Styles.Add("vscrollUp", vscrollUp);
            _skin.Styles.Add("hscrollTrack", hscrollTrackStyle);
            _skin.Styles.Add("hscrollButton", hscrollButtonStyle);
            _skin.Styles.Add("hscrollUp", hscrollUp);
            _skin.Styles.Add("multiline", multilineStyle);
            _skin.Styles.Add("tooltip", tooltipStyle);
            _skin.Styles.Add("label", labelStyle);

            GuiHost.SetSkin(_skin);

        }

        private void InitializeCursors()
        {
            Point cursorSize = new Point(32, 32);
            Point halfSize = cursorSize / 2;

            _skin.Cursors.Add(Cursors.Default, new Cursor { Texture = "cursors\\Arrow.png", Size = cursorSize, HotSpot = Point.Zero });
            _skin.Cursors.Add(Cursors.Link, new Cursor { Texture = "cursors\\Link.png", Size = cursorSize, HotSpot = Point.Zero });
            _skin.Cursors.Add(Cursors.Move, new Cursor { Texture = "cursors\\Move.png", Size = cursorSize, HotSpot = halfSize });
            _skin.Cursors.Add(Cursors.Select, new Cursor { Texture = "cursors\\Select.png", Size = cursorSize, HotSpot = halfSize });
            _skin.Cursors.Add(Cursors.SizeNS, new Cursor { Texture = "cursors\\SizeNS.png", Size = cursorSize, HotSpot = halfSize });
            _skin.Cursors.Add(Cursors.SizeWE, new Cursor { Texture = "cursors\\SizeWE.png", Size = cursorSize, HotSpot = halfSize });
            _skin.Cursors.Add(Cursors.HSplit, new Cursor { Texture = "cursors\\SizeNS.png", Size = cursorSize, HotSpot = halfSize });
            _skin.Cursors.Add(Cursors.VSplit, new Cursor { Texture = "cursors\\SizeWE.png", Size = cursorSize, HotSpot = halfSize });
            _skin.Cursors.Add(Cursors.SizeNESW, new Cursor { Texture = "cursors\\SizeNESW.png", Size = cursorSize, HotSpot = halfSize });
            _skin.Cursors.Add(Cursors.SizeNWSE, new Cursor { Texture = "cursors\\SizeNWSE.png", Size = cursorSize, HotSpot = halfSize });

        }
    }
}
