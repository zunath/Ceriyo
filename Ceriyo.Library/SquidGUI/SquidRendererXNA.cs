using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;
using FlatRedBall;

namespace Ceriyo.Library.SquidGUI
{
    public class SquidRendererXNA : Squid.ISquidRenderer
    {
        [DllImport("user32.dll")]
        private static extern int GetKeyboardLayout(int dwLayout);
        [DllImport("user32.dll")]
        private static extern int GetKeyboardState(ref byte pbKeyState);
        [DllImport("user32.dll", EntryPoint = "MapVirtualKeyEx")]
        private static extern int MapVirtualKeyExA(int uCode, int uMapType, int dwhkl);
        [DllImport("user32.dll")]
        private static extern int ToAsciiEx(int uVirtKey, int uScanCode, ref byte lpKeyState, ref short lpChar, int uFlags, int dwhkl);

        private static int KeyboardLayout;
        private readonly byte[] KeyStates;

        private readonly Dictionary<int, SpriteFont> Fonts = new Dictionary<int, SpriteFont>();
        private readonly Dictionary<string, int> FontLookup = new Dictionary<string, int>();

        private readonly Dictionary<int, Texture2D> Textures = new Dictionary<int, Texture2D>();
        private readonly Dictionary<string, int> TextureLookup = new Dictionary<string, int>();

        private readonly Dictionary<string, Squid.Font> FontTypes = new Dictionary<string, Squid.Font>();

        private readonly SpriteBatch Batch;

        private int FontIndex;
        private int TextureIndex;
        private readonly Texture2D BlankTexture;


        private readonly RasterizerState Rasterizer;
        private readonly SamplerState Sampler;

        public SquidRendererXNA()
        {
            Batch = new SpriteBatch(FlatRedBallServices.Game.GraphicsDevice);

            BlankTexture = new Texture2D(FlatRedBallServices.Game.GraphicsDevice, 1, 1);
            BlankTexture.SetData(new[] { new Color(255, 255, 255, 255) });

            FontTypes.Add(Squid.Font.Default, new Squid.Font
            {
                Name = "Arial10", 
                Family = "Arial", 
                Size = 8, 
                Bold = true, 
                International = true
            });

            KeyboardLayout = GetKeyboardLayout(0);
            KeyStates = new byte[0x100];

            Rasterizer = new RasterizerState
            {
                ScissorTestEnable = true
            };

            Sampler = new SamplerState
            {
                Filter = TextureFilter.Anisotropic
            };
        }

        public static int VirtualKeyToScancode(int key)
        {
            return MapVirtualKeyExA(key, 0, KeyboardLayout);
        }

        public bool TranslateKey(int code, ref char character)
        {
            short lpChar = 0;
            if (GetKeyboardState(ref KeyStates[0]) == 0)
                return false;

            int result = ToAsciiEx(MapVirtualKeyExA(code, 1, KeyboardLayout), code, ref KeyStates[0], ref lpChar, 0, KeyboardLayout);
            if (result == 1)
            {
                character = (char)((ushort)lpChar);
                return true;
            }

            return false;
        }

        private Color ColorFromtInt32(int color)
        {
            Byte[] bytes = BitConverter.GetBytes(color);

            return new Color(bytes[2], bytes[1], bytes[0], bytes[3]);
        }

        public int GetTexture(string name)
        {
            if (TextureLookup.ContainsKey(name))
                return TextureLookup[name];

            Texture2D texture = FlatRedBallServices.Load<Texture2D>("Content/GUI/" + Path.ChangeExtension(name, null));
            TextureIndex++;

            TextureLookup.Add(name, TextureIndex);
            Textures.Add(TextureIndex, texture);

            return TextureIndex;
        }

        public int GetFont(string name)
        {
            if (FontLookup.ContainsKey(name))
                return FontLookup[name];

            if (!FontTypes.ContainsKey(name))
                return -1;

            SpriteFont font = FlatRedBallServices.Load<SpriteFont>("Content/Fonts/Arial10");

            FontIndex++;

            FontLookup.Add(name, FontIndex);
            Fonts.Add(FontIndex, font);

            return FontIndex;
        }

        public Squid.Point GetTextSize(string text, int font)
        {
            if (string.IsNullOrEmpty(text))
                return new Squid.Point();

            SpriteFont f = Fonts[font];
            Vector2 size = f.MeasureString(text);
            return new Squid.Point((int)size.X, (int)size.Y);
        }

        public Squid.Point GetTextureSize(int texture)
        {
            Texture2D tex = Textures[texture];
            return new Squid.Point(tex.Width, tex.Height);
        }

        public void DrawBox(int x, int y, int w, int h, int color)
        {
            Rectangle destination = new Rectangle(x, y, w, h);
            Batch.Draw(BlankTexture, destination, destination, ColorFromtInt32(color));
        }

        public void DrawText(string text, int x, int y, int font, int color)
        {
            if (!Fonts.ContainsKey(font)) 
                return;

            SpriteFont f = Fonts[font];
            Batch.DrawString(f, text, new Vector2(x, y), ColorFromtInt32(color));
        }

        public void DrawTexture(int texture, int x, int y, int w, int h, Squid.Rectangle rect, int color)
        {
            if (!Textures.ContainsKey(texture))
                return;

            Texture2D tex = Textures[texture];

            Rectangle destination = new Rectangle(x, y, w, h);
            Rectangle source = new Rectangle
            {
                X = rect.Left, 
                Y = rect.Top, 
                Width = rect.Width, 
                Height = rect.Height
            };

            Batch.Draw(tex, destination, source, ColorFromtInt32(color));
        }

        public void Scissor(int x, int y, int w, int h)
        {
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            FlatRedBallServices.Game.GraphicsDevice.ScissorRectangle = new Rectangle(x, y, w, h);
        }

        public void StartBatch()
        {
            Batch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, Sampler, null, Rasterizer); 
        }

        public void EndBatch(bool final)
        {
            Batch.End();
        }

        public void Dispose() { }
    }
}
