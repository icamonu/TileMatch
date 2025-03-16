using ScriptableObjects;
using UnityEngine;

namespace Core
{
    public abstract class Tile: MonoBehaviour
    {
        [SerializeField] protected TileSpriteSelector tileSpriteSelector;
        protected abstract void SetSpriteConditions();
        protected TileSO tileSO;

        protected void OnEnable()
        {
            SetSpriteConditions();
        }
    }
}