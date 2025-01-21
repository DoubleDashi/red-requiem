using Configs.Morphs;
using Entities.Player.Controllers;

namespace Entities.Player.Morphs.Morphs
{
    public abstract class BaseMorph
    {
        protected readonly MorphConfig Config;
        protected readonly PlayerController Controller;
        
        protected BaseMorph(PlayerController controller, MorphConfig config)
        {
            Config = config;
            Controller = controller;
        }

        public virtual void Enter() { }
        public virtual void Exit() { }
        public virtual void Update() { }

        public abstract bool IsComplete();
    }
}