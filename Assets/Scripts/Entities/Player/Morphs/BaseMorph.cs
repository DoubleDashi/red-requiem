namespace Entities.Player.Morphs
{
    public abstract class BaseMorph
    {
        public bool HasCharge;

        protected readonly PlayerController Controller;
        protected readonly MorphDTO MorphDTO;

        protected BaseMorph(PlayerController controller, MorphDTO morphDTO)
        {
            Controller = controller;
            MorphDTO = morphDTO;
        }

        public virtual void Attack()
        {

        }

        public abstract bool IsFinished();
    }
}