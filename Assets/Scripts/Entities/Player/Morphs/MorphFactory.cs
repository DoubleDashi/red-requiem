using System.Collections.Generic;
using Entities.Player.Morphs.Strategies;

namespace Entities.Player.Morphs
{
    public class MorphFactory
    {
        private readonly Dictionary<MorphType, BaseMorph> _morphs;
        
        public MorphFactory(PlayerController controller, MorphDTO morphDTO)
        {
            _morphs = new Dictionary<MorphType, BaseMorph>
            {
                { MorphType.Spear, new SpearMorph(controller, morphDTO) },
                { MorphType.Shards, new ShardMorph(controller, morphDTO) },
            };
        }

        public BaseMorph GetMorph(MorphType type)
        {
            return _morphs[type];
        }
    }
}