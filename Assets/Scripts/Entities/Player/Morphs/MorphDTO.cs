using System;
using UnityEngine;

namespace Entities.Player.Morphs
{
    [Serializable]
    public class MorphDTO
    {
        public GameObject morphPrefab;
        public Transform player;
    }
}