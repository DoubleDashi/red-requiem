using UnityEngine;

namespace Entities.Enemy.States
{
    public class EnemyMove : EnemyState
    {
        // Temp values
        private readonly Vector2 _randomPositionMinMaxX = new(-4, 3);
        private readonly Vector2 _randomPositionMinMaxY = new(-4, 3);
        
        private Vector2 _randomPosition;
        private Vector2 _velocity;
        
        public EnemyMove(EnemyController controller) : base(controller)
        {
        }

        public override void Enter()
        {
            _randomPosition = new Vector2(
                Random.Range(_randomPositionMinMaxX.x, _randomPositionMinMaxX.y),
                Random.Range(_randomPositionMinMaxY.x, _randomPositionMinMaxY.y)
            );
        }
        
        public override void Update()
        {
            ForceDecelerate();
        }
        
        protected override void SetTransitions()
        {
            AddTransition(EnemyStateType.Idle, () => Controller.body.linearVelocity == Vector2.zero);
        }

        private void Move()
        {
            Controller.transform.position = Vector2.SmoothDamp(
                Controller.transform.position,
                _randomPosition,
                ref _velocity,
                0.3f,
                3.5f
            );
        }

        private void ForceDecelerate()
        {
            Vector2 velocity = Controller.body.linearVelocity;
            
            velocity.x = Mathf.Lerp(velocity.x, 0f, Controller.stats.decelerationSpeed * Time.deltaTime);
            velocity.y = Mathf.Lerp(velocity.y, 0f, Controller.stats.decelerationSpeed * Time.deltaTime);
            
            Controller.body.linearVelocity = velocity;
            
            if (velocity.magnitude < 0.1f)
            {
                Controller.body.linearVelocity = Vector2.zero;
            }
        }
    }
}