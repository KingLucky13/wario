using System;
using UnityEngine;

namespace LearnGame.Shooting
{
    public class ShootingController : MonoBehaviour
    {
        public bool HasTargert => _target != null;
        public Vector3 TargetPosition => _target.transform.position;
        private Weapon _weapon;
        private float _nextShotTimeSec;
        private GameObject _target;
        private Collider[] _colliders=new Collider[2];

        protected void Update()
        {
            _target = getTarget();
            _nextShotTimeSec -= Time.deltaTime;
            if (_nextShotTimeSec < 0)
            {
                if (HasTargert)
                    _weapon.Shoot(TargetPosition);
            
                _nextShotTimeSec = _weapon.ShootSpeedSec;
            }
        }

        private GameObject getTarget()
        {
            GameObject target = null;
            Vector3 weaponPosition = _weapon.transform.position;
            float attackRadius = _weapon.ShootRadius;
            int mask = LayerUtils.EnemyMask;

            var size = Physics.OverlapSphereNonAlloc(weaponPosition,attackRadius,_colliders,mask);
            if (size > 1)
            {
                for (int i = 0; i < size; i++)
                {
                    if (_colliders[i].gameObject != gameObject)
                    {
                        target = _colliders[i].gameObject;
                        break;
                    }
                }
            }
            return target; 
        }

        public void SetWeapon(Weapon weaponPrefab,Transform hand) 
        {
            if(_weapon != null)
            {
                Destroy(_weapon.gameObject);
            }
            _weapon=Instantiate(weaponPrefab,hand);
            _weapon.transform.localPosition = Vector3.zero;
            _weapon.transform.localRotation = Quaternion.identity;
        }
    }
}