using UnityEngine;
namespace LearnGame.Movement
{
    [RequireComponent(typeof(CharacterController))]

    public class CharacterMovementController : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 1f;
        [SerializeField]
        private float _rotationSpeed = 10f;
        private static readonly float SqrtEps = Mathf.Epsilon *Mathf.Epsilon;
        public Vector3 MovementDirection { get; set; }
        public Vector3 LookDirection { get; set; }
        private CharacterController _characterController;

        private float _accelerationPower = 1f;
        private float _accelerationTime=0f;

        protected void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        protected void Update()
        {
            Translate();
            if(_rotationSpeed > 0f && LookDirection != Vector3.zero)
            {
                Rotate();
            }
        }

        private void Translate()
        {
            Vector3 delta = MovementDirection * _speed * Time.deltaTime;
            if (_accelerationTime > 0f)
            {
                delta = delta * _accelerationPower;
                _accelerationTime -= Time.deltaTime;
            }
            _characterController.Move(delta);
        }

        private void Rotate()
        {
            var _currentRotation = transform.rotation*Vector3.forward;
            float sqrtMagnitude = (_currentRotation - LookDirection).sqrMagnitude;
            if (sqrtMagnitude > SqrtEps)
            {
                Quaternion newRotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(LookDirection,Vector3.up),_rotationSpeed*Time.deltaTime);
                transform.rotation= newRotation;
            }
        }

        public void setSpeedBoost(float power,float time)
        {
            _accelerationPower = power;
            _accelerationTime = time;
        }
    }
}
