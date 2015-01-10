using System.ComponentModel;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Toolset;

namespace Ceriyo.Data.ViewModels
{
    public class HotBarVM: BaseVM
    {
        private bool _isAreaLoaded;
        private bool _isModuleLoaded;
        private BindingList<ToolsetLayer> _layers ;
        private ToolsetLayer _selectedLayer;

        public bool IsAreaLoaded
        {
            get { return _isAreaLoaded; }
            set
            {
                _isAreaLoaded = value;
                OnPropertyChanged("IsAreaLoaded");
            }
        }

        public bool IsModuleLoaded
        {
            get { return _isModuleLoaded; }
            set
            {
                _isModuleLoaded = value;
                OnPropertyChanged("IsModuleLoaded");
            }
        }

        public BindingList<ToolsetLayer> Layers
        {
            get { return _layers; }
            set
            {
                _layers = value;
                OnPropertyChanged("Layers");
            }
        }

        public ToolsetLayer SelectedLayer
        {
            get { return _selectedLayer; }
            set
            {
                _selectedLayer = value;
                OnPropertyChanged("SelectedLayer");
            }
        }

        public HotBarVM()
        {
            IsAreaLoaded = false;
            IsModuleLoaded = false;
            Layers = new BindingList<ToolsetLayer>();
            
            for (int x = 1; x <= EngineConstants.AreaMaxLayers; x++)
            {
                Layers.Add(new ToolsetLayer(x));
            }

            if (Layers.Count > 0)
            {
                SelectedLayer = Layers[0];
            }

        }
    }
}
