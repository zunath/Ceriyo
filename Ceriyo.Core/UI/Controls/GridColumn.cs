using System;
using Squid;

namespace Ceriyo.Core.UI.Controls
{
    public class GridColumn
    {
        private int _width = 30;
        private string _text = "Column";
        private GridColumnHeader _header;

        internal event EventHandler Click;

        public int Index { get; internal set; }
        public object Tag;

        public GridColumn()
        {
            Container = new GridFrame();
            Header = new GridColumnHeader();
            Header.Style = "button";
            Header.MouseClick += Header_MouseClick;
            Container.Header = Header;
            Header.Column = this;
        }

        void Header_MouseClick(Control sender, MouseEventArgs args)
        {
            if (Click != null) Click(this, null);
        }

        internal GridFrame Container;

        internal GridColumnHeader Header
        {
            get { return _header; }
            set
            {
                _header = value;

                if (_header != null)
                    _header.Text = Text;
            }
        }

        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (_width == value)
                    return;

                _width = value;

                if (Header != null)
                    Header.Size = new Point(_width, Header.Size.y);

                if (Container != null)
                    Container.Size = new Point(_width, Container.Size.y);
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;

                if (Header != null)
                    Header.Text = value;
            }
        }
    }

}
