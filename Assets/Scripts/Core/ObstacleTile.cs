using System.Collections.Generic;

namespace Core
{
    public class ObstacleTile: Tile, IBlastable, IDamageable
    {
        public int Health { get; private set; } = 2;
        
        public void Blast()
        {
            Destroy(gameObject);
        }

        public void GetDamage()
        {
            Health--;
            tileSpriteSelector.SetSprite(Health);
        }

        protected override void SetSpriteConditions()
        {
            tileSpriteSelector.SetConditions(new List<int>(1));
            tileSpriteSelector.SetSortingOrder((int)transform.position.y);
        }
    }
}