using Entities.Player.Components;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "MorphConfig", menuName = "Configs/MorphConfig", order = 0)]
    public class MorphConfig : ScriptableObject
    {
        public MorphType type;
        public GameObject prefab;
        public Collider2D collider;
        public float knockbackForce;
        public float collisionRadius;
    }
}