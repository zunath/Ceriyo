using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Ceriyo.Data;
using Ceriyo.Data.Engine;
using Ceriyo.Data.Enumerations;
using Ceriyo.Data.EventArguments;
using Ceriyo.Data.GameObjects;
using Ceriyo.Data.ResourceObjects;
using Ceriyo.Data.ViewModels;
using Ceriyo.Library.Processing;
using Microsoft.Xna.Framework.Graphics;

namespace Ceriyo.Toolset.Components
{
    /// <summary>
    /// Interaction logic for AnimationEditorComponent.xaml
    /// </summary>
    public partial class AnimationEditorComponent
    {
        private AnimationEditorVM Model { get; set; }
        private GameResourceProcessor Processor { get; set; }
        private ResourcePackDataManager ResourcePackManager { get; set; }
        private WorkingDataManager WorkingManager { get; set; }

        public event EventHandler<EditorItemChangedEventArgs> OnAnimationsListChanged;

        public AnimationEditorComponent()
        {
            InitializeComponent();
            Model = new AnimationEditorVM();
            Processor = new GameResourceProcessor();
            ResourcePackManager = new ResourcePackDataManager();
            WorkingManager = new WorkingDataManager();
            DataContext = Model;
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

            foreach (SpriteAnimation animation in Model.Animations)
            {
                GameResource resource = Model.Graphics.SingleOrDefault(x => x.FileName == animation.Graphic.FileName);
                if (resource != null)
                {
                    animation.Graphic = resource;
                }
            }

            if (Model.Animations.Count > 0)
            {
                lbAnimations.SelectedItem = Model.Animations[0];
            }

        }

        public void Save(object sender, EventArgs e)
        {
            FileOperationResultTypeEnum result = WorkingManager.ReplaceAllGameObjectFiles(Model.Animations.Cast<IGameObject>().ToList(), WorkingPaths.AnimationsDirectory);

            if (result != FileOperationResultTypeEnum.Success)
            {
                MessageBox.Show("Unable to save animations.", "Animation save failed.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void NewAnimation(object sender, RoutedEventArgs e)
        {
            SpriteAnimation animation = new SpriteAnimation();
            string resref = Processor.GenerateUniqueResref(Model.Animations.Cast<IGameObject>().ToList(), animation.CategoryName);

            animation.Name = resref;
            animation.Tag = resref;
            animation.Resref = resref;
            animation.Graphic = Model.Graphics[0];
            animation.Frames.Add(new SpriteAnimationFrame("Frame 1"));

            Model.Animations.Add(animation);
            int index = lbAnimations.Items.IndexOf(animation);
            lbAnimations.SelectedItem = lbAnimations.Items[index];

            if (OnAnimationsListChanged != null)
            {
                OnAnimationsListChanged(this, new EditorItemChangedEventArgs(animation, animation.Resref, true));
            }
        }

        private void DeleteAnimation(object sender, RoutedEventArgs e)
        {
            if (Model.SelectedAnimation == null) return;
            if (MessageBox.Show("Are you sure you want to delete this animation?", "Delete animation?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string resref = Model.SelectedAnimation.Resref;
                Model.Animations.Remove(Model.SelectedAnimation);
                Model.SelectedAnimation = null;
                Model.IsAnimationSelected = false;
                Model.IsFrameSelected = false;

                RefreshPreview();
                RefreshSelectedFrame();

                if (OnAnimationsListChanged != null)
                {
                    OnAnimationsListChanged(this, new EditorItemChangedEventArgs(Model.SelectedAnimation, resref, false));
                }
            }
        }

        private void NewFrame(object sender, RoutedEventArgs e)
        {
            int frameCount = Model.SelectedAnimation.Frames.Count + 1;
            SpriteAnimationFrame frame = new SpriteAnimationFrame("Frame " + frameCount);
            Model.SelectedAnimation.Frames.Add(frame);
        }

        private void DeleteFrame(object sender, RoutedEventArgs e)
        {
            if (Model.SelectedFrame != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this frame?", "Delete frame?", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    Model.SelectedAnimation.Frames.Remove(Model.SelectedFrame);
                    Model.SelectedFrame = null;
                    Model.IsFrameSelected = false;

                    RefreshPreview();
                    RefreshSelectedFrame();
                }
            }
        }

        private void AnimationSelected(object sender, SelectionChangedEventArgs e)
        {
            SpriteAnimation animation = lbAnimations.SelectedItem as SpriteAnimation;
            Model.SelectedAnimation = animation;
            Model.IsAnimationSelected = animation != null;

            if (animation != null)
            {
                if (animation.Frames.Count > 0)
                {
                    Model.SelectedFrame = animation.Frames[0];
                }
            }

            RefreshPreview();
            RefreshSelectedFrame();
        }

        private void FrameSelected(object sender, SelectionChangedEventArgs e)
        {
            SpriteAnimationFrame frame = lbFrames.SelectedItem as SpriteAnimationFrame;
            Model.SelectedFrame = frame;
            Model.IsFrameSelected = frame != null;
            rectSelectedCell.Visibility = frame == null ? Visibility.Hidden : Visibility.Visible;

            RefreshSelectedFrame();
            RefreshPreview();
        }

        private void GraphicSelected(object sender, SelectionChangedEventArgs e)
        {
            GameResource resource = lbGraphics.SelectedItem as GameResource;

            if (resource != null)
            {
                if (resource.ResourceType == ResourceTypeEnum.None)
                {
                    imgGraphic.Source = null;
                }
                else
                {
                    BitmapImage image = Processor.ToBitmapImage(resource);
                    Model.SelectedAnimation.Graphic = resource;
                    imgGraphic.Source = image;
                }

                if (Model.SelectedFrame != null)
                {
                    Model.SelectedFrame.TextureCellX = 0;
                    Model.SelectedFrame.TextureCellY = 0;
                }
            }

            RefreshPreview();
            RefreshSelectedFrame();
        }

        private void EditorCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Model.SelectedFrame != null)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point position = e.GetPosition(cnvEditor);

                    int cellX = (int)position.X / EngineConstants.AnimationFrameWidth;
                    int cellY = (int)position.Y / EngineConstants.AnimationFrameHeight;

                    if ((cellX * EngineConstants.AnimationFrameWidth + EngineConstants.AnimationFrameWidth) <= imgGraphic.Width &&
                        (cellY * EngineConstants.AnimationFrameHeight + EngineConstants.AnimationFrameHeight) <= imgGraphic.Height)
                    {
                        Model.SelectedFrame.TextureCellX = cellX;
                        Model.SelectedFrame.TextureCellY = cellY;

                        RefreshSelectedFrame();
                        RefreshPreview();
                    }
                    
                }
            }
        }

        private void RefreshSelectedFrame()
        {
            if (Model.SelectedFrame != null)
            {
                Canvas.SetLeft(rectSelectedCell, Model.SelectedFrame.TextureCellX * EngineConstants.AnimationFrameWidth);
                Canvas.SetTop(rectSelectedCell, Model.SelectedFrame.TextureCellY * EngineConstants.AnimationFrameHeight);
            }
        }

        private void RefreshPreview()
        {
            if (Model.SelectedAnimation != null && 
                Model.SelectedFrame != null && 
                Model.SelectedAnimation.Graphic != null && 
                Model.SelectedAnimation.Graphic.ResourceType == ResourceTypeEnum.Graphic)
            {
                Texture2D texture = Processor.GetSubTexture(Model.SelectedAnimation.Graphic,
                    Model.SelectedFrame.TextureCellX * EngineConstants.AnimationFrameWidth,
                    Model.SelectedFrame.TextureCellY * EngineConstants.AnimationFrameHeight,
                    EngineConstants.AnimationFrameWidth,
                    EngineConstants.AnimationFrameHeight);

                imgPreview.Source = Processor.ToBitmapImage(texture);
                imgPreview.RenderTransformOrigin = new Point(0.5, 0.5);

                float xTransform = Model.SelectedFrame.FlipHorizontal ? -1.0f : 1.0f;
                float yTransform = Model.SelectedFrame.FlipVertical ? -1.0f : 1.0f;

                ScaleTransform transform = new ScaleTransform(xTransform, yTransform);
                imgPreview.RenderTransform = transform;
            }
            else
            {
                imgPreview.Source = null;
            }
        }

        private void chkFlipHorizontal_Checked(object sender, RoutedEventArgs e)
        {
            RefreshPreview();
        }

        private void chkFlipVertical_Checked(object sender, RoutedEventArgs e)
        {
            RefreshPreview();
        }

        private void MoveFrameUp(object sender, RoutedEventArgs e)
        {
            SpriteAnimationFrame frame = lbFrames.SelectedItem as SpriteAnimationFrame;

            if (frame != null)
            {
                int oldIndex = Model.SelectedAnimation.Frames.IndexOf(frame);
                int newIndex = oldIndex - 1;

                if (oldIndex > 0)
                {
                    Model.SelectedAnimation.Frames.RemoveAt(oldIndex);
                    Model.SelectedAnimation.Frames.Insert(newIndex, frame);

                    lbFrames.SelectedIndex = newIndex;
                }
            }
        }

        private void MoveFrameDown(object sender, RoutedEventArgs e)
        {
            SpriteAnimationFrame frame = lbFrames.SelectedItem as SpriteAnimationFrame;

            if (frame != null)
            {
                int oldIndex = Model.SelectedAnimation.Frames.IndexOf(frame);
                int newIndex = oldIndex + 1;

                if (newIndex < Model.SelectedAnimation.Frames.Count)
                {
                    Model.SelectedAnimation.Frames.RemoveAt(oldIndex);
                    Model.SelectedAnimation.Frames.Insert(newIndex, frame);

                    lbFrames.SelectedIndex = newIndex;
                }
            }
        }


    }
}
