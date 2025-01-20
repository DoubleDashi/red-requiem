﻿using System.Collections;
using UnityEngine;

namespace Entities.Enemy.States
{
    public class EnemyIdle : EnemyState
    {
        private bool IsWaiting { get; set; }
        
        public EnemyIdle(EnemyController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            Controller.StartCoroutine(WaitRoutine());
        }
        
        public override void Exit()
        {
            IsWaiting = true;
        }

        protected override void SetTransitions()
        {
            AddTransition(EnemyStateType.Hurt, () => Controller.isHurt);
            AddTransition(EnemyStateType.Move, () => IsWaiting == false);
        }
        
        private IEnumerator WaitRoutine()
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            IsWaiting = false;
        }
    }
}