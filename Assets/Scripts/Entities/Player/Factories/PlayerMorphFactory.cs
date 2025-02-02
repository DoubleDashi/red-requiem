﻿using System.Collections.Generic;
using Configs;
using UnityEngine;

namespace Entities.Player.Factories
{
    public class PlayerMorphFactory
    {
        private readonly List<MorphConfig> _morphConfigs;
        
        public PlayerMorphFactory(List<MorphConfig> morphConfigs)
        {
            _morphConfigs = morphConfigs;
        }

        public MorphConfig FindByType(MorphType type)
        {
            Debug.Log("Finding of type: " + type);
            return _morphConfigs.Find(morphConfig => morphConfig.type == type);
        }
    }
}