using System.Threading.Tasks;
using Core.Interfaces;
using ScriptableObjects;
using UnityEngine;

namespace Core
{
    public class RegularTile: Tile, IMatchable, IMovable, IBlastable
    {

        [SerializeField] private TileMovement tileMovement;
        [SerializeField] private float fallDuration = 0.5f;
        [SerializeField] private float collapseDuration = 0.5f;
        public int TileType { get; private set; }
        public ISelectable Selectable { get; set; }
        
        public void SetTileSO(TileSO tileSO, int tileType)
        {
            this.tileSO = tileSO;
            TileType = tileType;
            tileSpriteSelector.Init(tileSO);
            OnMatch(1);
        }
        
        public void OnMatch(int matchCount)
        {
            tileSpriteSelector.SetSprite(matchCount);
        }

        public void Move(Vector3 targetPosition)
        {
            tileMovement.Fall(targetPosition, fallDuration);
        }

        public void Blast()
        {
            // tileMovement.BlastMovement(transform.position + Vector3.up, collapseDuration);
            // await Task.Delay((int)(collapseDuration * 1000));
            Destroy(gameObject);
        }
    }
}