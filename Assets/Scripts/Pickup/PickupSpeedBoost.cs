using UnityEngine;

namespace LearnGame.Pickup
{
    public class PickupSpeedBoost : PickUpItem
    {
        [SerializeField]
        private float _power;
        [SerializeField]
        private float _time;
        public override void PickUp(BaseCharacter character)
        {
            base.PickUp(character);
            character.SetSpeedBoost(_power,_time);
        }
    }
}