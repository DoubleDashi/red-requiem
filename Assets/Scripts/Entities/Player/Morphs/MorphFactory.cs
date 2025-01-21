using System.Collections.Generic;
using Configs.Morphs;
using Entities.Player.Controllers;
using Entities.Player.Morphs.Morphs;

namespace Entities.Player.Morphs
{
    public class MorphFactory
    {
        private readonly Dictionary<MorphType, BaseMorph> _morphs = new();

        public MorphFactory(PlayerController controller, MorphConfig[] morphConfigs)
        {
            _morphs.Add(MorphType.Spear, new SpearMorph(controller, FindConfigByType(MorphType.Spear, morphConfigs)));
            _morphs.Add(MorphType.Shard, new ShardMorph(controller, FindConfigByType(MorphType.Shard, morphConfigs)));
        }
        
        public BaseMorph GetMorph(MorphType morphType)
        {
            return _morphs[morphType];
        }

        private MorphConfig FindConfigByType(MorphType type, MorphConfig[] morphConfigs)
        {
            foreach (var morphConfig in morphConfigs)
            {
                if (morphConfig.type == type)
                {
                    return morphConfig;
                }
            }

            return null;
        }
    }
}