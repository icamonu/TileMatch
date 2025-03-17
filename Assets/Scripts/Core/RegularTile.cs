using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Interfaces;
using ScriptableObjects;
using UnityEngine;
using Pooling;

namespace Core
{
    public class RegularTile: Tile, IMatchable, IMovable, IBlastable
    {
        [SerializeField] private TileMovement tileMovement;
        [SerializeField] private MovementSettings movementSettings;
        [SerializeField] private GameSettings gameSettings;
        public int TileType { get; private set; }
        public ISelectable Selectable { get; private set; }
        
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

        public void Move(Vector2Int targetPosition)
        {
            Vector3 targetPos = new Vector3(targetPosition.x, targetPosition.y,0f);
            tileMovement.Fall(targetPos, movementSettings.fallDuration);
            tileSpriteSelector.SetSortingOrder(targetPosition.y);
        }
        
        public void SetSelectable(Cell cell)
        {
            Selectable=cell;
        }

        public async void Blast()
        {
            tileMovement.BlastMovement(movementSettings.collapseDuration);
            await Task.Delay((int)(movementSettings.collapseDuration * 1000));
            ObjectPool<RegularTile>.Release(this);
        }

        protected override void SetSpriteConditions()
        {
            tileSpriteSelector.SetConditions(new List<int>
            {
                gameSettings.matchConditionA,
                gameSettings.matchConditionB,
                gameSettings.matchConditionC
            });
        }
    }
}