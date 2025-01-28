using System;
using UnityEngine;

namespace Animations
{
    public class SimpleAnimationStateBehaviour : StateMachineBehaviour
    {
        public static event Action<int> OnAnimationCompleted;
        public static event Action<int> OnAnimationTriggerActivated;
        
        [SerializeField] [Range(0f, 1f)] private float triggerEventAt;
        
        private bool triggeredEvent { get; set; }
        private bool completed { get; set; }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            triggeredEvent = false;
            completed = false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float currentNormalizedTime = stateInfo.normalizedTime % 1f;
            float wiggleRoom = 0.05f;
            
            if (currentNormalizedTime >= triggerEventAt && triggeredEvent == false)
            {
                triggeredEvent = true;
                OnAnimationTriggerActivated?.Invoke(stateInfo.shortNameHash);
            }
            
            if (currentNormalizedTime >= 1f - wiggleRoom && completed == false)
            {
                completed = true;
                OnAnimationCompleted?.Invoke(stateInfo.shortNameHash);
            }
        }
    
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            triggeredEvent = false;
            completed = false;
        }
    }
}