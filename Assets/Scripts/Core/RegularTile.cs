using System.Collections.Generic;
using Core.Interfaces;
using ScriptableObjects;
using UnityEngine;

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
        
        public void SetSelectable(ISelectable selectable)
        {
            Selectable = selectable;
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