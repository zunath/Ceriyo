using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Windows.Threading;

namespace Ceriyo.Core.Extensions
{
    public class ObservableCollectionEx<T>
        : IList<T>, IList
        , INotifyCollectionChanged
        , INotifyPropertyChanged
    {
        #region Fields

        private readonly IList<T> _collection = new List<T>();
        private readonly Dispatcher _dispatcher;
        private readonly ReaderWriterLock _sync = new ReaderWriterLock();
        private readonly List<T> _internalList = new List<T>();

        [NonSerialized]
        private object _syncRoot;

        #endregion

        #region Constructors

        public ObservableCollectionEx()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        public ObservableCollectionEx(IEnumerable<T> createFrom)
            : this()
        {
            Source = createFrom;
        }

        #endregion

        #region Properties

        #region Filter

        private Func<T, bool> _filter;

        public Func<T, bool> Filter
        {
            get { return _filter; }
            set
            {
                //ignore if values are equal
                if (value == _filter) return;

                _filter = value;

                ApplyFilter();

                RaisePropertyChanged(() => Filter);
            }
        }

        #endregion

        #region Source

        private IEnumerable<T> _source;

        public IEnumerable<T> Source
        {
            get
            {
                return _source;
            }
            set
            {
                //ignore if values are equal
                if (value == _source) return;

                if (_source is INotifyCollectionChanged)
                    (_source as INotifyCollectionChanged).CollectionChanged -= Source_CollectionChanged;

                _source = value;

                InitFrom(_source);

                if (_source is INotifyCollectionChanged)
                    (_source as INotifyCollectionChanged).CollectionChanged += Source_CollectionChanged;

                RaisePropertyChanged(() => Source);
            }
        }

        #endregion

        #endregion

        #region Methods

        public void Add(T item)
        {
            if (Thread.CurrentThread == _dispatcher.Thread)
                DoAdd(item);
            else
                _dispatcher.BeginInvoke((Action)(() => DoAdd(item)));
        }

        private int DoAdd(T item)
        {
            _sync.AcquireWriterLock(Timeout.Infinite);

            var index = DoAddInternal(item, true);

            _sync.ReleaseWriterLock();

            return index;
        }

        private int DoAddInternal(T item, bool attachMonitorChanges)
        {
            // Attach to PropertyChanged event for monitoring future properties' changes
            if (attachMonitorChanges)
                AttachPropertyChanged(item);

            // Check if it should be here
            if (!ShouldBeHere(item))
                return -1;

            // Add item to collection
            var index = _collection.Count;
            _collection.Add(item);

            // Notify about collection's changes
            RaisePropertyChanged(() => Count);
            RaisePropertyChanged("Item[]");
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));

            return index;
        }

        public int Add(object item)
        {
            if (Thread.CurrentThread == _dispatcher.Thread)
                return DoAdd((T)item);
            else
            {
                var op = _dispatcher.BeginInvoke(new Func<T, int>(DoAdd), item);
                if (op == null || op.Result == null)
                    return -1;
                return (int)op.Result;
            }
        }

        public bool Contains(object value)
        {
            return Contains((T)value);
        }

        public void Clear()
        {
            if (Thread.CurrentThread == _dispatcher.Thread)
                DoClear();
            else
                _dispatcher.BeginInvoke((Action)(DoClear));
        }

        public int IndexOf(object value)
        {
            return IndexOf((T)value);
        }

        public void Insert(int index, object value)
        {
            Insert(index, (T)value);
        }

        public void Remove(object value)
        {
            Remove((T)value);
        }

        private void DoClear()
        {
            _sync.AcquireWriterLock(Timeout.Infinite);

            DoClearInternal();

            _sync.ReleaseWriterLock();
        }

        private void DoClearInternal()
        {
            // Detach from PropertyChanged events
            foreach (var item in _internalList.ToArray())
                DetachPropertyChanged(item);

            // Clear collection
            _collection.Clear();

            // Notify about collection's changes
            RaisePropertyChanged(() => Count);
            RaisePropertyChanged("Item[]");
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public bool Contains(T item)
        {
            _sync.AcquireReaderLock(Timeout.Infinite);
            var result = ContainsInternal(item);
            _sync.ReleaseReaderLock();
            return result;
        }

        private bool ContainsInternal(T item)
        {
            return _collection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _sync.AcquireWriterLock(Timeout.Infinite);
            _collection.CopyTo(array, arrayIndex);
            _sync.ReleaseWriterLock();
        }

        public void CopyTo(Array array, int index)
        {
            _sync.AcquireWriterLock(Timeout.Infinite);
            for (var i = 0; i < _collection.Count; i++)
            {
                array.SetValue(_collection[i], index + i);
            }
            _sync.ReleaseWriterLock();
        }

        public int Count
        {
            get
            {
                _sync.AcquireReaderLock(Timeout.Infinite);
                var result = _collection.Count;
                _sync.ReleaseReaderLock();
                return result;
            }
        }

        public object SyncRoot
        {
            get
            {
                if (_syncRoot == null)
                {
                    var c = _collection as ICollection;
                    if (c != null)
                        _syncRoot = c.SyncRoot;
                    else
                        Interlocked.CompareExchange<object>(ref _syncRoot, new object(), null);
                }
                return _syncRoot;
            }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public bool Remove(T item)
        {
            if (Thread.CurrentThread == _dispatcher.Thread)
                return DoRemove(item);
            else
            {
                var op = _dispatcher.BeginInvoke(new Func<T, bool>(DoRemove), item);
                if (op == null || op.Result == null)
                    return false;
                return (bool)op.Result;
            }
        }

        private bool DoRemove(T item)
        {
            _sync.AcquireWriterLock(Timeout.Infinite);

            var result = DoRemoveInternal(item, true);

            _sync.ReleaseWriterLock();

            return result;
        }

        private bool DoRemoveInternal(T item, bool detachMonitorChanges)
        {
            // Check if item is still at collection
            var index = _collection.IndexOf(item);
            if (index == -1)
                return false;

            // Detach from PropertyChanged event
            if (detachMonitorChanges)
                DetachPropertyChanged(item);

            // Remove item from collection
            var result = _collection.Remove(item);

            // Notify about collection's changes
            if (result)
            {
                RaisePropertyChanged(() => Count);
                RaisePropertyChanged("Item[]");
                RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item,
                                                                            index));
            }

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _collection.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            _sync.AcquireReaderLock(Timeout.Infinite);
            var result = _collection.IndexOf(item);
            _sync.ReleaseReaderLock();
            return result;
        }

        public void Insert(int index, T item)
        {
            if (Thread.CurrentThread == _dispatcher.Thread)
                DoInsert(index, item);
            else
                _dispatcher.BeginInvoke((Action)(() => DoInsert(index, item)));
        }

        private void DoInsert(int index, T item)
        {
            _sync.AcquireWriterLock(Timeout.Infinite);

            // Attach to PropertyChanged event for monitoring future properties' changes
            AttachPropertyChanged(item);

            // Check if it should be here
            if (!ShouldBeHere(item))
                return;

            // Insert item in collection
            _collection.Insert(index, item);

            // Notify about collection's changes
            RaisePropertyChanged(() => Count);
            RaisePropertyChanged("Item[]");
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));

            _sync.ReleaseWriterLock();
        }

        public void RemoveAt(int index)
        {
            if (Thread.CurrentThread == _dispatcher.Thread)
                DoRemoveAt(index);
            else
                _dispatcher.BeginInvoke((Action)(() => DoRemoveAt(index)));
        }

        object IList.this[int index]
        {
            get { return this[index]; }
            set { this[index] = (T)value; }
        }

        private void DoRemoveAt(int index)
        {
            _sync.AcquireWriterLock(Timeout.Infinite);

            if (_collection.Count == 0 || _collection.Count <= index)
            {
                _sync.ReleaseWriterLock();
                return;
            }

            var item = _collection[index];

            // Detach from PropertyChanged event
            DetachPropertyChanged(item);

            // Remove item from collection
            _collection.RemoveAt(index);

            // Notify about collection's changes
            RaisePropertyChanged(() => Count);
            RaisePropertyChanged("Item[]");
            RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));

            _sync.ReleaseWriterLock();

        }

        public T this[int index]
        {
            get
            {
                _sync.AcquireReaderLock(Timeout.Infinite);
                var result = _collection[index];
                _sync.ReleaseReaderLock();
                return result;
            }
            set
            {
                _sync.AcquireWriterLock(Timeout.Infinite);

                if (_collection.Count == 0 || _collection.Count <= index)
                {
                    _sync.ReleaseWriterLock();
                    return;
                }

                var oldItem = _collection[index];

                DetachPropertyChanged(oldItem);
                AttachPropertyChanged(value);

                if (ShouldBeHere(value))
                {
                    _collection[index] = value;

                    // Notify about collection's changes
                    RaisePropertyChanged("Item[]");
                    RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, oldItem, index));
                }
                else
                {
                    //
                    // Remove current item from collection
                    //

                    // Detach from PropertyChanged event
                    DetachPropertyChanged(oldItem);

                    // Remove item from collection
                    _collection.RemoveAt(index);

                    // Notify about collection's changes
                    RaisePropertyChanged(() => Count);
                    RaisePropertyChanged("Item[]");
                    RaiseCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldItem, index));
                }

                _sync.ReleaseWriterLock();
            }
        }

        #region Internal methods

        private void AttachPropertyChanged(T item)
        {
            _internalList.Add(item);
            if (item is INotifyPropertyChanged)
            {
                (item as INotifyPropertyChanged).PropertyChanged += Item_PropertyChanged;
            }
        }

        private void DetachPropertyChanged(T item)
        {
            _internalList.Remove(item);
            if (item is INotifyPropertyChanged)
            {
                (item as INotifyPropertyChanged).PropertyChanged -= Item_PropertyChanged;
            }
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _sync.AcquireWriterLock(Timeout.Infinite);

            var item = (T)sender;

            var containsAfter = ApplyFilter(item);

            if (containsAfter)
                if (ItemPropertyChanged != null)
                    ItemPropertyChanged(sender, e);

            _sync.ReleaseWriterLock();
        }

        private bool ApplyFilter(T item)
        {
            var contains = ContainsInternal(item);
            var containsAfter = contains;

            if (ShouldBeHere(item))
            {
                if (!contains)
                {
                    DoAddInternal(item, false);
                    containsAfter = true;
                }
            }
            else
            {
                if (contains)
                {
                    DoRemoveInternal(item, false);
                    containsAfter = false;
                }
            }
            return containsAfter;
        }

        private bool ShouldBeHere(T item)
        {
            return (Filter == null) || Filter(item);
        }

        private void RaisePropertyChanged<TSource>(Expression<Func<TSource>> propertyExpression)
        {
            var propertyName = ((MemberExpression)propertyExpression.Body).Member.Name;
            RaisePropertyChanged(propertyName);
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void InitFrom(IEnumerable<T> source)
        {
            if (source == null)
            {
                Clear();
                return;
            }

            _sync.AcquireWriterLock(Timeout.Infinite);

            foreach (var item in source)
            {
                DoAddInternal(item, true);
            }

            _sync.ReleaseWriterLock();
        }

        private void RaiseCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, e);
        }

        public void ApplyFilter()
        {
            _sync.AcquireWriterLock(Timeout.Infinite);

            foreach (var item in _internalList)
            {
                ApplyFilter(item);
            }

            _sync.ReleaseWriterLock();
        }

        #endregion

        #endregion

        #region Events

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public event PropertyChangedEventHandler ItemPropertyChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Event handlers

        void Source_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _sync.AcquireWriterLock(Timeout.Infinite);

            if (e.OldItems != null)
            {
                foreach (var oldItem in e.OldItems.OfType<T>())
                {
                    DoRemoveInternal(oldItem, true);
                }
            }

            if (e.NewItems != null)
            {
                foreach (var newItem in e.NewItems.OfType<T>())
                {
                    DoAddInternal(newItem, true);
                }
            }

            _sync.ReleaseWriterLock();
        }

        #endregion
    }
}
