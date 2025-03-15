using Core.Interfaces;
using ScriptableObjects;
using UnityEngine;

namespace Core
{
    public abstract class Tile: MonoBehaviour
    {
        [SerializeField] protected TileSpriteSelector tileSpriteSelector;
        
        protected TileSO tileSO;
        public ISelectable Cell { get; set; }
    }
}