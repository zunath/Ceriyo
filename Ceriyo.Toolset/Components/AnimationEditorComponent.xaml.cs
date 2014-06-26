using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ResourceObjects;
using Ceriyo.Data.ViewModels;
using Ceriyo.Library.Processing;
using FlatRedBall.Graphics.Animation;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for AnimationEditorComponent.xaml
    /// </summary>
    public partial class AnimationEditorComponent : UserControl
    {
        private AnimationEditorVM Model { get; set; }
        private GameResourceProcessor Processor { get; set; }
        private ResourcePackDataManager ResourcePackManager { get; set; }
        private WorkingDataManager WorkingManager { get; set; }

        public AnimationEditorComponent()
        {
            InitializeComponent();
            Model = new AnimationEditorVM();
            Processor = new GameResourceProcessor();
            ResourcePackManager = new ResourcePackDataManager();
            WorkingManager = new WorkingDataManager();
            SetDataContexts();
        }

        private void SetDataContexts()
        {
            txtName.DataContext = Model;
            txtResref.DataContext = Model;
            txtTag.DataContext = Model;
            lbAnimations.DataContext = Model;
            lbFrames.DataContext = Model;

        }

        public void Open(object sender, EventArgs e)
        {
            Model.Graphics = ResourcePackManager.GetGameResources(ResourceTypeEnum.Graphic);
            GameResource graphic = new GameResource("", "(No Graphic)", ResourceTypeEnum.None);
            Model.Graphics.Insert(0, graphic);

            Model.Animations = WorkingManager.GetAllGameObjects<SpriteAnimation>(ModulePaths.AnimationsDirectory);
            Model.IsAnimationSelected = false;
            Model.IsFrameSelected = false;
            Model.SelectedAnimation = new SpriteAnimation();
            Model.SelectedFrame = new SpriteAnimationFrame();
        }

        public void Save(object sender, EventArgs e)
        {
            foreach (SpriteAnimation animation in Model.Animations)
            {
                FileOperationResultTypeEnum result = WorkingManager.SaveGameObjectFile(animation);

                if (result != FileOperationResultTypeEnum.Success)
                {
                    MessageBox.Show("Unable to save animation: '" + animation.Name + "'", "Saving animation failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void NewAnimation(object sender, RoutedEventArgs e)
        {
            SpriteAnimation animation = new SpriteAnimation();
            string resref = Processor.GenerateUniqueResref(Model.Animations.Cast<IGameObject>().ToList(), animation.CategoryName);

            animation.Name = resref;
            animation.Tag = resref;
            animation.Resref = resref;

            Model.Animations.Add(animation);
        }

        private void DeleteAnimation(object sender, RoutedEventArgs e)
        {

        }

        private void NewFrame(object sender, RoutedEventArgs e)
        {
            int frameCount = Model.SelectedAnimation.Frames.Count + 1;
            SpriteAnimationFrame frame = new SpriteAnimationFrame("Frame " + frameCount);
            Model.SelectedAnimation.Frames.Add(frame);
            

        }

        private void DeleteFrame(object sender, RoutedEventArgs e)
        {

        }

        private void AnimationSelected(object sender, SelectionChangedEventArgs e)
        {
            SpriteAnimation animation = lbAnimations.SelectedItem as SpriteAnimation;
            Model.SelectedAnimation = animation;
            Model.IsAnimationSelected = animation == null ? false : true;
        }

        private void FrameSelected(object sender, SelectionChangedEventArgs e)
        {
            SpriteAnimationFrame frame = lbFrames.SelectedItem as SpriteAnimationFrame;
            Model.SelectedFrame = frame;
            Model.IsFrameSelected = frame == null ? false : true;
        }
    }
}
