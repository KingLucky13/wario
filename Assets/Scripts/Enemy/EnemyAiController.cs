using LearnGame.Enemy.States;
using UnityEngine;

namespace LearnGame.Enemy
{
    public class EnemyAiController : MonoBehaviour
    {
        [SerializeField]
        private float viewRadius = 20f;

        private EnemyTarget _target;
        private EnemyStateMachine _stateMachine;

        protected void Awake()
        {
            var player=FindObjectOfType<PlayerCharacter>();

            var enemyDirectionController=GetComponent<EnemyDirectionController>();

            var navMesher = new NavMesher(transform);

            _target = new EnemyTarget(transform,viewRadius,player);
            _stateMachine = new EnemyStateMachine(enemyDirectionController,navMesher,_target);
        }

        protected void Update()
        {
            _target.FindClosest();
            _stateMachine.Update();

        }
    }
}