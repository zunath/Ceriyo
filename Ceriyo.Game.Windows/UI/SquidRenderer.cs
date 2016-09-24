using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Squid;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Ceriyo.Game.Windows.UI
{
    public class SquidRenderer : ISquidRenderer
    {
        [DllImport("user32.dll")]
        private static extern int GetKeyboardLayout(int dwLayout);
        [DllImport("user32.dll")]
        private static extern int GetKeyboardState(ref byte pbKeyState);
        [DllImport("user32.dll", EntryPoint = "MapVirtualKeyEx")]
        private static extern int MapVirtualKeyExA(int uCode, int uMapType, int dwhkl);
        [DllImport("user32.dll")]
        private static extern int ToAsciiEx(int uVirtKey, int uScanCode, ref byte lpKeyState, ref short lpChar, int uFlags, int dwhkl);

        private static int _keyboardLayout;
        private readonly byte[] _keyStates;

        private readonly Dictionary<int, SpriteFont> _fonts = new Dictionary<int, SpriteFont>();
        private readonly Dictionary<string, int> _fontLookup = new Dictionary<string, int>();

        private readonly Dictionary<int, Texture2D> _textures = new Dictionary<int, Texture2D>();
        private readonly Dictionary<string, int> _textureLookup = new Dictionary<string, int>();

        private readonly Dictionary<string, Font> _fontTypes = new Dictionary<string, Font>();

        private readonly Microsoft.Xna.Framework.Game _game;
        private readonly SpriteBatch _batch;

        private int _fontIndex;
        private int _textureIndex;
        private readonly Texture2D _blankTexture;


        private readonly RasterizerState _rasterizer;
        private readonly SamplerState _sampler;

        public SquidRenderer(Microsoft.Xna.Framework.Game game)
        {
            _game = game;
            _batch = new SpriteBatch(game.GraphicsDevice);

            _blankTexture = new Texture2D(_game.GraphicsDevice, 1, 1);
            _blankTexture.SetData(new[] { new Color(255, 255, 255, 255) });
            
            _fontTypes.Add(Font.Default, new Font
            {
                Name = "Spritefonts/Arial10",
                Family = "Arial",
                Size = 8,
                Bold = true,
                International = true
            });

            _keyboardLayout = GetKeyboardLayout(0);
            _keyStates = new byte[0x100];

            _rasterizer = new RasterizerState {ScissorTestEnable = true};

            _sampler = new SamplerState {Filter = TextureFilter.Anisotropic};
        }

        public static int VirtualKeyToScancode(int key)
        {
            return MapVirtualKeyExA(key, 0, _keyboardLayout);
        }

        public bool TranslateKey(int code, ref char character)
        {
            short lpChar = 0;
            if (GetKeyboardState(ref _keyStates[0]) == 0)
                return false;

            int result = ToAsciiEx(MapVirtualKeyExA(code, 1, _keyboardLayout), code, ref _keyStates[0], ref lpChar, 0, _keyboardLayout);
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
            if (_textureLookup.ContainsKey(name))
                return _textureLookup[name];

            Texture2D texture = _game.Content.Load<Texture2D>(Path.ChangeExtension(name, null));
            _textureIndex++;

            _textureLookup.Add(name, _textureIndex);
            _textures.Add(_textureIndex, texture);

            return _textureIndex;
        }

        public int GetFont(string name)
        {
            if (_fontLookup.ContainsKey(name))
                return _fontLookup[name];

            if (!_fontTypes.ContainsKey(name))
                return -1;

            Font type = _fontTypes[name];

            SpriteFont font = _game.Content.Load<SpriteFont>(type.Name);
            _fontIndex++;

            _fontLookup.Add(name, _fontIndex);
            _fonts.Add(_fontIndex, font);

            return _fontIndex;
        }

        public Squid.Point GetTextSize(string text, int font)
        {
            if (string.IsNullOrEmpty(text))
                return new Squid.Point();

            SpriteFont f = _fonts[font];
            Vector2 size = f.MeasureString(text);
            return new Squid.Point((int)size.X, (int)size.Y);
        }

        public Squid.Point GetTextureSize(int texture)
        {
            Texture2D tex = _textures[texture];
            return new Squid.Point(tex.Width, tex.Height);
        }

        public void DrawBox(int x, int y, int w, int h, int color)
        {
            Rectangle destination = new Rectangle(x, y, w, h);
            _batch.Draw(_blankTexture, destination, destination, ColorFromtInt32(color));
        }

        public void DrawText(string text, int x, int y, int font, int color)
        {
            if (!_fonts.ContainsKey(font))
                return;

            SpriteFont f = _fonts[font];
            _batch.DrawString(f, text, new Vector2(x, y), ColorFromtInt32(color));
        }

        public void DrawTexture(int texture, int x, int y, int w, int h, Squid.Rectangle rect, int color)
        {
            if (!_textures.ContainsKey(texture))
                return;

            Texture2D tex = _textures[texture];

            Rectangle destination = new Rectangle(x, y, w, h);
            Rectangle source = new Rectangle
            {
                X = rect.Left,
                Y = rect.Top,
                Width = rect.Width,
                Height = rect.Height
            };


            _batch.Draw(tex, destination, source, ColorFromtInt32(color));
        }

        public void Scissor(int x, int y, int w, int h)
        {
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            _game.GraphicsDevice.ScissorRectangle = new Rectangle(x, y, w, h);
        }

        public void StartBatch()
        {
            _batch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, _sampler, null, _rasterizer);
        }

        public void EndBatch(bool final)
        {
            _batch.End();
        }

        public void Dispose() { }
    }
}
