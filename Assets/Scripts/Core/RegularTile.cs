using System.Threading.Tasks;
using Core.Interfaces;
using ScriptableObjects;
using UnityEngine;

namespace Core
{
    public class RegularTile: Tile, IMatchable, IMovable, IBlastable
    {

        [SerializeField] private TileMovement tileMovement;
        [SerializeField] private MovementSettings movementSettings;
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
            tileMovement.Fall(targetPosition, movementSettings.fallDuration);
        }

        public void Blast()
        {
            // tileMovement.BlastMovement(transform.position + Vector3.up, movementSettings.collapseDuration);
            // await Task.Delay((int)(collapseDuration * 1000));
            Destroy(gameObject);
        }
    }
}