using Configs.Morphs;
using Entities.Player.Morphs;
using Entities.Player.Morphs.Morphs;
using UnityEngine;

namespace Entities.Player.Controllers
{
    public class PlayerAttackController : MonoBehaviour
    {
        [SerializeField] private MorphConfig[] morphConfigs;
        
        public BaseMorph currentMorph { get; private set; }

        private MorphFactory _morphFactory;

        private void Awake()
        {
            _morphFactory = new MorphFactory(GetComponent<PlayerController>(), morphConfigs);
            
            currentMorph = _morphFactory.GetMorph(MorphType.Spear);
        }
        
        public void ChangeMorph(MorphType morphType)
        {
            currentMorph.Exit();
            currentMorph = _morphFactory.GetMorph(morphType);
            currentMorph.Enter();
        }
    }
}