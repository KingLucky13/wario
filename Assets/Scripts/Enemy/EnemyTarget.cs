using UnityEngine;

namespace LearnGame.Enemy
{
    public class EnemyTarget
    {
        public GameObject Closest {  get; private set; }

        private readonly float _viewRadius;
        private readonly Transform _agentTransform;
        private readonly PlayerCharacter _player;

        private readonly Collider[] _colliders=new Collider[10];
        public EnemyTarget(Transform agent,float viewRadius, PlayerCharacter player)
        {
            _agentTransform = agent;
            _viewRadius = viewRadius;
            _player = player;
        }

        public float DistanceToClosestFromAgent()
        {
            if(Closest!=null)
            {
                DistanceFromAgentTo(Closest);
            }
            return 0;
        }

        private float DistanceFromAgentTo(GameObject go)=>(_agentTransform.position- go.transform.position).magnitude;

        private int FindAllTargets(int layerMask)
        {
            var size = Physics.OverlapSphereNonAlloc(_agentTransform.position, _viewRadius, _colliders, layerMask);
            return size;
        }
        public void FindClosest()
        {
            float minDistance = float.MaxValue;

            var count = FindAllTargets(LayerUtils.ItemMask | LayerUtils.EnemyMask);

            for(int i = 0; i < count; i++)
            {
                var go = _colliders[i].gameObject;
                if (go == _agentTransform.gameObject) continue;
                var distnace = DistanceFromAgentTo(go);
                if (distnace < minDistance) { 
                    minDistance = distnace;
                    Closest = go;
                }
            }

            if(_player!= null && DistanceFromAgentTo(_player.gameObject) < minDistance) {
                Closest=_player.gameObject;
            }
        }
    }
}
