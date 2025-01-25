namespace Entities.Player.Factories
{
    public enum PlayerStateType
    {
        // Primary states
        Idle,
        Move,
        Hurt,
        Morph,
        Attack,
        Death,
        
        // Morph states
        ShardAttack,
        SwordAttack,
        CannonCharge,
        CannonAttack,
    }
}