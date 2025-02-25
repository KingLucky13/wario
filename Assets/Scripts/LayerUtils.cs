using UnityEngine;

namespace LearnGame
{
    public static class LayerUtils
    {
        public const string BulletLayerName = "Bullet";
        public const string EnemyLayerName = "Enemy";
        public const string PlayerLayerName = "Player";
        public const string ItemLayerName = "Item";

        public static readonly int BulletLayer=LayerMask.NameToLayer(BulletLayerName);
        public static readonly int ItemLayer=LayerMask.NameToLayer(ItemLayerName);

        public static readonly int EnemyMask =LayerMask.GetMask(EnemyLayerName,PlayerLayerName);
        public static readonly int ItemMask =LayerMask.GetMask(ItemLayerName);

        public static bool IsBullet(GameObject other) =>other.layer == BulletLayer;
        public static bool IsItem(GameObject other) =>other.layer == ItemLayer;
    }
}
