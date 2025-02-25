using System;
using UnityEngine;

namespace LearnGame.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private PlayerCharacter _player;
        [SerializeField]
        private Vector3 _cameraOffset = Vector3.zero;
        [SerializeField]
        private Vector3 _rotationOffset = Vector3.zero;

        protected void Awake()
        {
            if (_player == null)
            {
                throw new NullReferenceException($"Camera can't follow null player={nameof(_player)}");
            }
        }

        protected void LateUpdate()
        {
            if (_player != null)
            {
                Vector3 targetRotation = _rotationOffset - _cameraOffset;
                transform.position = _player.transform.position + _cameraOffset;
                transform.rotation = Quaternion.LookRotation(targetRotation, Vector3.up);
            }
        }
    }
}