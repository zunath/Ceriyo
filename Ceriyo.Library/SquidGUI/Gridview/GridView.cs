using Squid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Ceriyo.Library.SquidGUI.Gridview
{
    public class GridViewItemCollection : ActiveList<GridViewItem> { }
    public class GridColumnCollection : ActiveList<GridColumn> { }

    public class GridViewEventArgs : EventArgs
    {
        public GridColumn Column;
    }

    [Toolbox]
    public class GridView : Control
    {
        private Panel Panel;
        private Frame headerContainer;

        public Frame Header { get; private set; }

        public event SelectedItemChangedEventHandler SelectedItemChanged;
        public event EventHandler<GridViewEventArgs> ColumnClicked;

        public GridViewItemCollection Items { get; set; }
        public GridColumnCollection Columns { get; set; }
        public GridViewItem SelectedItem { get; set; }

        public bool Multiselect { get; set; }

        public GridView()
        {
            Size = new Point(100, 100);

            Columns = new GridColumnCollection();
            Columns.ItemAdded += Columns_OnAdd;
            Columns.ItemRemoved += Columns_OnRemove;
            Columns.BeforeItemRemoved += Columns_BeforeItemRemoved;
            Columns.BeforeItemAdded += Columns_BeforeItemAdded;
            Columns.BeforeItemsCleared += Columns_BeforeItemsCleared;

            Items = new GridViewItemCollection();
            Items.ItemAdded += Items_OnAdd;
            Items.ItemRemoved += Items_OnRemove;
            Items.BeforeItemsCleared += Items_BeforeItemsCleared;
            Items.ItemsSorted += Items_ItemsSorted;

            Header = new Frame();
            Header.Size = new Point(20, 32);
            Header.Dock = DockStyle.Top;
            Elements.Add(Header);

            headerContainer = new Frame();
            headerContainer.Size = new Point(20, 32);
            headerContainer.AutoSize = Squid.AutoSize.Horizontal;
            Header.Controls.Add(headerContainer);

            Panel = new Panel();
            Panel.Dock = DockStyle.Fill;
            Panel.Content.AutoSize = Squid.AutoSize.HorizontalVertical;

            Panel.VScroll.ButtonUp.Visible = false;
            Panel.VScroll.ButtonDown.Visible = false;
            Panel.VScroll.Size = new Point(13, 12);
            Panel.VScroll.Slider.Style = "vscrollTrack";
            Panel.VScroll.Slider.Button.Style = "vscrollButton";
            Panel.VScroll.Dock = DockStyle.Right;
            Panel.VScroll.Margin = new Margin(4, 0, 0, 0);

            Panel.HScroll.ButtonUp.Visible = false;
            Panel.HScroll.ButtonDown.Visible = false;
            Panel.HScroll.Size = new Point(13, 12);
            Panel.HScroll.Slider.Style = "vscrollTrack";
            Panel.HScroll.Slider.Button.Style = "vscrollButton";
            Panel.HScroll.Margin = new Margin(0, 4, 0, 0);
            Panel.Content.Update += Content_OnControlUpdate;
            Elements.Add(Panel);
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            // we want to perform mouse scrolling if:
            // the mouse is scrolling and there is any control hovered
            if (GuiHost.MouseScroll != 0 && Desktop.HotControl != null)
            {
                // ok, lets check if the mouse is anywhere near us
                if (Hit(GuiHost.MousePosition.x, GuiHost.MousePosition.y))
                {
                    // now lets check if its really this window or anything in it
                    if (Desktop.HotControl == this || Desktop.HotControl.IsChildOf(this))
                        Panel.VScroll.Scroll(GuiHost.MouseScroll);
                }
            }
        }

        void Content_OnControlUpdate(Control sender)
        {
            headerContainer.Position = new Point(Panel.Content.Position.x, 0);
            headerContainer.PerformLayout();
        }

        void Columns_BeforeItemsCleared(object sender, EventArgs e)
        {
            foreach (GridViewItem item in Items)
                RemoveItem(item);

            foreach (GridColumn col in Columns)
            {
                headerContainer.Controls.Remove(col.Header);
                Panel.Content.Controls.Remove(col.Container);

                col.Container = null;
                col.Header = null;
            }
        }

        void Columns_BeforeItemAdded(object sender, ListEventArgs<GridColumn> e)
        {
            foreach (GridViewItem item in Items)
                RemoveItem(item);
        }

        void Columns_BeforeItemRemoved(object sender, ListEventArgs<GridColumn> e)
        {
            foreach (GridViewItem item in Items)
                RemoveItem(item);
        }

        void Items_BeforeItemsCleared(object sender, EventArgs e)
        {
            foreach (GridViewItem item in Items)
                RemoveItem(item);
        }

        void Items_ItemsSorted(object sender, EventArgs e)
        {
            foreach (GridViewItem item in Items)
                RemoveItem(item);

            foreach (GridViewItem item in Items)
                AddItem(item);
        }

        void Columns_OnRemove(object sender, ListEventArgs<GridColumn> e)
        {
            headerContainer.Controls.Remove(e.Item.Header);
            Panel.Content.Controls.Remove(e.Item.Container);

            e.Item.Container = null;
            e.Item.Header = null;

            foreach (GridViewItem item in Items)
                AddItem(item);
        }

        void Columns_OnAdd(object sender, ListEventArgs<GridColumn> e)
        {
            e.Item.Click += new EventHandler(Column_Click);
            headerContainer.Controls.Add(e.Item.Header);
            Panel.Content.Controls.Add(e.Item.Container);

            e.Item.Index = Columns.IndexOf(e.Item);

            foreach (GridViewItem item in Items)
                AddItem(item);
        }

        void Column_Click(object sender, EventArgs e)
        {
            if (ColumnClicked != null)
                ColumnClicked(this, new GridViewEventArgs { Column = sender as GridColumn });
        }

        private void AddItem(GridViewItem item)
        {
            item.Tags.Clear();

            int i = 0;
            foreach (GridColumn column in Columns)
            {
                if (i == 0)
                {
                    column.Container.Controls.Add(item);
                }
                else
                {
                    int index = i - 1;
                    if (item.SubItems.Count > index)
                    {
                        column.Container.Controls.Add(item.SubItems[index]);
                        item.Tags.Add(item.SubItems[index]);
                    }
                    else
                    {
                        Control spacer = CreateSpace(item.Size.y);

                        column.Container.Controls.Add(spacer);
                        item.Tags.Add(spacer);
                    }
                }

                i++;
            }
        }

        private void RemoveItem(GridViewItem item)
        {
            int i = 0;
            foreach (GridColumn column in Columns)
            {
                if (i == 0)
                {
                    column.Container.Controls.Remove(item);
                }
                else
                {
                    int index = i - 1;
                    if (item.Tags.Count > index)
                    {
                        column.Container.Controls.Remove(item.Tags[index]);
                    }
                }

                i++;
            }

            item.Tags.Clear();
        }

        private Control CreateSpace(int height)
        {
            Control spacer = new Control();
            spacer.Size = new Point(height, height);
            spacer.Dock = DockStyle.Top;
            return spacer;
        }

        void Items_OnRemove(object sender, ListEventArgs<GridViewItem> e)
        {
            RemoveItem(e.Item);
        }

        void Items_OnAdd(object sender, ListEventArgs<GridViewItem> e)
        {
            AddItem(e.Item);
        }

        void item_OnMouseClick(Control sender, MouseEventArgs args)
        {
            if (args.Button > 0) return;

            GridViewItem item = sender as GridViewItem;

            if (Multiselect)
            {
                item.Selected = !item.Selected;
            }
            else
            {
                if (SelectedItem != null) SelectedItem.Selected = false;
                SelectedItem = item;
                if (SelectedItem != null) SelectedItem.Selected = true;

                // if (SelectedItemChanged != null)
                //     SelectedItemChanged(this, SelectedItem);
            }
        }
    }

    internal sealed class GridFrame : Frame
    {
        internal GridColumnHeader Header;
        private Button ResizeHandle;
        private Point ClickedPos;
        private Point OldSize;

        public GridFrame()
        {
            Scissor = true;
            AutoSize = AutoSize.Vertical;
            Dock = DockStyle.Left;

            ResizeHandle = new Button();
            ResizeHandle.Size = new Point(4, 2);
            ResizeHandle.Dock = DockStyle.Right;
            ResizeHandle.MouseDown += ResizeHandle_OnMouseDown;
            ResizeHandle.MousePress += ResizeHandle_OnMousePress;
            ResizeHandle.Cursor = Cursors.VSplit;
            ResizeHandle.Style = "button";
            Controls.Add(ResizeHandle);
        }

        void ResizeHandle_OnMouseDown(Control sender, MouseEventArgs args)
        {
            if (args.Button > 0) return;

            ClickedPos = GuiHost.MousePosition;
            OldSize = Size;
        }

        void ResizeHandle_OnMousePress(Control sender, MouseEventArgs args)
        {
            if (args.Button > 0) return;

            Point p = GuiHost.MousePosition - ClickedPos;
            ResizeTo(OldSize + p, AnchorStyles.Right);
            Header.Size = new Point(Size.x, Header.Size.y);
        }
    }

    internal sealed class GridColumnHeader : Frame
    {
        internal GridColumn Column;

        private Button Button;
        private Button ResizeHandle;
        private Point ClickedPos;
        private Point OldSize;

        public string Text { get { return Button.Text; } set { Button.Text = value; } }

        public GridColumnHeader()
        {
            Scissor = true;
            Dock = DockStyle.Left;

            ResizeHandle = new Button();
            ResizeHandle.Size = new Point(4, 4);
            ResizeHandle.Dock = DockStyle.Right;
            ResizeHandle.MouseDown += ResizeHandle_OnMouseDown;
            ResizeHandle.MousePress += ResizeHandle_OnMousePress;
            ResizeHandle.Cursor = Cursors.VSplit;
            //ResizeHandle.Style = "button";
            Elements.Add(ResizeHandle);

            Button = new Button();
            Button.Dock = DockStyle.Fill;
            Button.Style = "button";
            Elements.Add(Button);
        }

        void ResizeHandle_OnMouseDown(Control sender, MouseEventArgs args)
        {
            if (args.Button > 0) return;

            ClickedPos = GuiHost.MousePosition;
            OldSize = Size;
        }

        void ResizeHandle_OnMousePress(Control sender, MouseEventArgs args)
        {
            if (args.Button > 0) return;

            Point p = GuiHost.MousePosition - ClickedPos;
            ResizeTo(OldSize + p, AnchorStyles.Right);
            Column.Width = Size.x;
        }
    }

}