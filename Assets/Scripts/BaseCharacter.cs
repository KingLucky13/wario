
using LearnGame.Movement;
using LearnGame.Pickup;
using LearnGame.Shooting;
using System;
using UnityEngine;

namespace LearnGame
{
    [RequireComponent(typeof(CharacterMovementController),typeof(ShootingController))]
    public abstract class BaseCharacter : MonoBehaviour
    {

        public event Action<BaseCharacter> Dead;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private Weapon _baseWeapon;

        [SerializeField]
        private Transform _hand;

        [SerializeField]
        private float _hp = 2f;

        private IMovementDirectionSource _movementDirectionSource;
        private CharacterMovementController _characterMovementController;
        private ShootingController _shootingController;
        protected void Awake()
        {
            _movementDirectionSource=GetComponent<IMovementDirectionSource>();

            _characterMovementController = GetComponent<CharacterMovementController>();
            _shootingController = GetComponent<ShootingController>();
        }

        protected void Start()
        {
            SetWeapon(_baseWeapon);
        }

        protected void Update()
        {
            Vector3 direction = _movementDirectionSource.MovementDirection;
            Vector3 lookDirection = direction;
            if (_shootingController.HasTargert)
            {
                lookDirection = (_shootingController.TargetPosition - transform.position).normalized;
            }
            _characterMovementController.MovementDirection = direction;
            _characterMovementController.LookDirection = lookDirection;

            _animator.SetBool("isMoving",direction !=  Vector3.zero);
            _animator.SetBool("isShooting",_shootingController.HasTargert);

            if (_hp <= 0)
            {
                Dead?.Invoke(this);
                Destroy(gameObject);
            }
        }

        protected void OnTriggerEnter(Collider other)
        {
            if(LayerUtils.IsBullet(other.gameObject))
            {
                Bullet bullet=other.gameObject.GetComponent<Bullet>();
                _hp -=bullet.Damage;
                Destroy(other.gameObject);
            }
            else if(LayerUtils.IsItem(other.gameObject))
            {
                var item = other.gameObject.GetComponent<PickUpItem>();
                item.PickUp(this);
                Destroy(other.gameObject);
            }
        }

        public void SetWeapon(Weapon weapon)
        {
            _shootingController.SetWeapon(weapon, _hand);
        }
        
        public void SetSpeedBoost(float power,float time)
        {
            _characterMovementController.setSpeedBoost(power,time);
        }

    }
}
