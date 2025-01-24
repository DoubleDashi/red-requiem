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
        
        // Morph states
        SpearCharge,
        SpearAttack,
        ShardAttack,
        SwordAttack,
        ScytheAttack,
        CannonCharge,
        CannonAttack,
    }
}