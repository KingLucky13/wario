using UnityEditor;
using UnityEngine;

namespace LearnGame.Pickup
{
    public class PickUpSpawner : MonoBehaviour
    {
        [SerializeField]
        private PickUpItem _pickUpPrefab;
        [SerializeField]
        private float _range = 2f;
        [SerializeField]
        private int _maxCount = 2;
        [SerializeField]
        private float _minSpawnIntervalSec = 10f;
        [SerializeField]
        private float _maxSpawnIntervalSec = 10f;

        private float _spawnIntervalSec;
        private float _curentSpawnTimerSec;
        private float _curentCount;

        protected void Start()
        {
         _spawnIntervalSec=Random.Range(_minSpawnIntervalSec, _maxSpawnIntervalSec);
        }
        protected void Update()
        {
            if(_curentCount< _maxCount)
            {
                _curentSpawnTimerSec += Time.deltaTime;
                if (_curentSpawnTimerSec > _spawnIntervalSec)
                {
                    _spawnIntervalSec = Random.Range(_minSpawnIntervalSec, _maxSpawnIntervalSec);
                    _curentSpawnTimerSec = 0;
                    _curentCount++;

                    var randomPointInRange = Random.insideUnitCircle * _range;
                    var randomPosition = new Vector3(randomPointInRange.x, 1, randomPointInRange.y) + transform.position;

                    var item = Instantiate(_pickUpPrefab, randomPosition, Quaternion.identity,transform);
                    item.OnPickedUp += OnItemPickedUp;
                }
            }
        }

        private void OnItemPickedUp(PickUpItem pickedUpItem)
        {
            _curentCount--;
            pickedUpItem.OnPickedUp -= OnItemPickedUp;
        }

        protected void OnDrawGizmos()
        {
            var cashedColor = Handles.color;
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, _range);
            Handles.color = cashedColor;
        }
    }
}