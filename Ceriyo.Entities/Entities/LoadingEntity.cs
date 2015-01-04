 using FlatRedBall.Graphics;

namespace Ceriyo.Entities.Entities
{
    public class LoadingEntity : GraphicEntity
    {
        private readonly Text _loadingText;

        public LoadingEntity()
            : base("LoadingEntity")
        {
            _loadingText = TextManager.AddText("Loading...");
            _loadingText.VerticalAlignment = VerticalAlignment.Center;
            _loadingText.HorizontalAlignment = HorizontalAlignment.Center;
        }

        protected override void CustomInitialize()
        {
        }

        protected override void CustomActivity()
        {
        }

        protected override void CustomDestroy()
        {
            TextManager.RemoveText(_loadingText);
        }

    }
}
