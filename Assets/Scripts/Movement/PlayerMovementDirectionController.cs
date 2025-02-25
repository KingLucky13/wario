using UnityEngine;

namespace LearnGame.Movement
{
    public class PlayerMovementDirectionController : MonoBehaviour,IMovementDirectionSource
    {
        private UnityEngine.Camera _camera;
        public Vector3 MovementDirection { get; set; }
        [SerializeField]
        private float _accelerationPower=1.5f;

        protected void Awake()
        {
            _camera = UnityEngine.Camera.main;
        }

        protected void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var spacebar = Input.GetKey(KeyCode.Space);
            var direction = new Vector3(horizontal, 0, vertical);
            direction = _camera.transform.rotation *direction;
            direction.y = 0;
            direction= direction.normalized;
            if (spacebar)
            {
               direction *= _accelerationPower;
            }
            MovementDirection= direction;

        }
    }
}