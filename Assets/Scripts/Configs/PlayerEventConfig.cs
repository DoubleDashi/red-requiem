﻿using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerEventConfig", menuName = "Configs/PlayerEventConfig")]
    public class PlayerEventConfig : ScriptableObject
    {
        public static Action<Guid> OnPlayerChargeComplete;
        public static Action<Guid> OnPlayerMove;
    }
}