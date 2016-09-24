using System.Collections.Generic;
using System.ComponentModel;
using Squid;

namespace Ceriyo.Game.Windows.UI.Controls
{
    public class GridViewItem : GridViewSubItem
    {
        internal List<Control> Tags = new List<Control>();

        public object Value { get; set; }

        public ActiveList<GridViewSubItem> SubItems { get; set; }

        public GridViewItem()
        {
            SubItems = new ActiveList<GridViewSubItem>();
            Size = new Point(100, 20);
            Dock = DockStyle.Top;
        }

        protected override void OnLateUpdate()
        {
            if (this.Equals(Desktop.HotControl))
            {
                foreach (GridViewSubItem item in SubItems)
                    item.State = State;
            }
            else
            {
                foreach (GridViewSubItem item in SubItems)
                {
                    if (item.Equals(Desktop.HotControl))
                    {
                        State = item.State;

                        foreach (GridViewSubItem i in SubItems)
                        {
                            if (!i.Equals(item))
                                i.State = item.State;
                        }
                        break;
                    }
                }
            }
        }
    }

    public class GridViewSubItem : Label
    {
        public event VoidEvent OnSelectedChanged;

        private bool _selected;

        [DefaultValue(false)]
        public new bool Selected
        {
            get { return _selected; }
            set
            {
                if (value == _selected) return;

                _selected = value;

                if (OnSelectedChanged != null)
                    OnSelectedChanged(this);
            }
        }

        public GridViewSubItem()
        {
            Size = new Point(100, 20);
            Dock = DockStyle.Top;
            //Style = "item";
        }
    }
}
