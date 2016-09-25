using System;
using Ceriyo.Core.Constants;
using Ceriyo.Core.Scripting.Client.Contracts;
using Squid;

namespace Ceriyo.Core.Scripting.Client
{
    public class StyleMethods: IStyleMethods
    {
        public Skin CreateSkin()
        {
            return new Skin();
        }

        public ControlStyle CreateControlStyle()
        {
            return new ControlStyle();
        }
        public ControlStyle CreateControlStyle(ControlStyle style)
        {
            return new ControlStyle(style);
        }

        public void SetSkin(Skin skin)
        {
            GuiHost.SetSkin(skin);
        }

        public Margin CreateMargin(int all)
        {
            return new Margin(all);
        }

        public Margin CreateMargin(int left, int top, int right, int bottom)
        {
            return new Margin(left, top, right, bottom);
        }

        public int CreateColor(int red, int green, int blue, int alpha)
        {
            float redFloat = red/255.0f;
            float greenFloat = green/255.0f;
            float blueFloat = blue/255.0f;
            float alphaFloat = alpha/255.0f;

            return ColorInt.RGBA(redFloat, greenFloat, blueFloat, alphaFloat);
        }

        public void SetCursor(
            Skin skin,
            CursorType cursorType, 
            string textureFile, 
            int width, 
            int height, 
            int hotSpotX, 
            int hotSpotY)
        {
            string squidCursor;
            switch (cursorType)
            {
                case CursorType.Default:
                    squidCursor = Cursors.Default;
                    break;
                case CursorType.Link:
                    squidCursor = Cursors.Link;
                    break;
                case CursorType.Move:
                    squidCursor = Cursors.Move;
                    break;
                case CursorType.Select:
                    squidCursor = Cursors.Select;
                    break;
                case CursorType.SizeNS:
                    squidCursor = Cursors.SizeNS;
                    break;
                case CursorType.SizeWE:
                    squidCursor = Cursors.SizeWE;
                    break;
                case CursorType.HSplit:
                    squidCursor = Cursors.HSplit;
                    break;
                case CursorType.VSplit:
                    squidCursor = Cursors.VSplit;
                    break;
                case CursorType.SizeNESW:
                    squidCursor = Cursors.SizeNESW;
                    break;
                case CursorType.SizeNWSE:
                    squidCursor = Cursors.SizeNWSE;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cursorType), cursorType, null);
            }

            skin.Cursors.Add(squidCursor,
                new Cursor
                {
                    Texture = textureFile,
                    Size = new Point(width, height),
                    HotSpot = new Point(hotSpotX, hotSpotY)
                });
        }
    }
}
