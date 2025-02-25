using LearnGame.Shooting;
using UnityEngine;

namespace LearnGame.Shooting
{
    public class Weapon : MonoBehaviour
    {
        [field:SerializeField]
        public Bullet BulletPrefab {  get; private set; }
        [field:SerializeField]
        public float ShootRadius { get; private set; } = 5f;
        [field:SerializeField]
        public float ShootSpeedSec { get; private set; } = 1f;
        [SerializeField]
        private float _bulletSpeed = 1f;
        [SerializeField]
        private float _bulletMaxDistance = 10f;
        [SerializeField]
        private Transform _bulletSpawnPosition;
        [SerializeField]
        private float _damage = 1f;

        [SerializeField]
        private ParticleSystem _shootParticle;

        [SerializeField]
        private AudioSource _shootAudio;

        public void Shoot(Vector3 targetPoint) 
        {
            Bullet bullet = Instantiate(BulletPrefab, _bulletSpawnPosition.position, Quaternion.identity);
            _shootParticle.Play();
            _shootAudio.Play();

            Vector3 target=targetPoint-_bulletSpawnPosition.position;
            target.y = 0;
            target.Normalize();
            bullet.Initialize(target, _bulletSpeed, _bulletMaxDistance,_damage);
        }
    }
}