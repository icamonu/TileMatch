using UnityEngine;

namespace Core
{
    public class ObstacleTile: Tile, IBlastable, IDamageable
    {
        [SerializeField] private int health = 2;
        
        public void Blast()
        {
            Destroy(gameObject);
        }

        public void GetDamage()
        {
            health--;
            tileSpriteSelector.SetSprite(health);
            if (health<=0)
                Blast();
        }
    }
}