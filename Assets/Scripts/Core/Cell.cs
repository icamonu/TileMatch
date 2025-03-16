using System;
using System.Collections.Generic;
using Core.Interfaces;
using UnityEngine;

namespace Core
{
    public class Cell: ISelectable
    {
        public Vector2Int GridPosition { get; private set; }
        public Tile Tile { get; private set; }
        public List<Cell> Neighbours { get; private set; }=new List<Cell>(4);
        public Cell TopCell { get; private set; }
        public Cell BottomCell { get; private set; }
        
        public event Action<Cell> OnTileSelected; 
        public Cell(Vector2Int gridPosition)
        {
            GridPosition = gridPosition;
        }
        
        public void SetTile(Tile tile)
        {
            Tile = tile;
        }
        
        public void BlastTile()
        {
            if (Tile is IBlastable blastableTile)
            {
                blastableTile.Blast();
            }
            Tile = null;
        }
        
        public void SetNeighbours(List<Cell> neighbours, Cell topCell, Cell bottomCell)
        {
            TopCell = topCell;
            BottomCell = bottomCell;
            Neighbours = neighbours;
        }

        public void TileSelected()
        {
            OnTileSelected?.Invoke(this);
        }
    }
}