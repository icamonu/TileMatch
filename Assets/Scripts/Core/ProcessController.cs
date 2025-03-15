using System.Collections.Generic;
using Core.Data;
using UnityEngine;

namespace Core
{
    public class ProcessController
    {
        private BoardData boardData;
        private MatchChecker matchChecker;
        private ColumnSorter columnSorter;
        private TilePool tilePool;
        
        public ProcessController(BoardData boardData, MatchChecker matchChecker, 
            ColumnSorter columnSorter, TilePool tilePool)
        {
            this.boardData = boardData;
            this.matchChecker = matchChecker;
            this.columnSorter = columnSorter;
            this.tilePool = tilePool;

            foreach (var cell in this.boardData.Board)
            {
                cell.OnTileSelected += OnTileSelected;
            }
        }
        
        ~ProcessController()
        {
            foreach (var cell in boardData.Board)
            {
                cell.OnTileSelected -= OnTileSelected;
            }
        }
        
        public void OnTileSelected(Cell cell)
        {
            Debug.Log("Cell position: " + cell.GridPosition);
            Debug.Log("Cell tile type: " + ((RegularTile)cell.Tile).TileType);

            for (int i = 0; i < cell.Neighbours.Count; i++)
            {
                Debug.Log("Neighbour position: " + cell.Neighbours[i].GridPosition);
            }
            
            HashSet<Cell> blastMatches = matchChecker.GetMatches(cell);
            if (blastMatches.Count<2)
                return;
            
            boardData.BlastTiles(blastMatches);
            List<Cell> emptyCells = columnSorter.SortColumns();
            for (int i = 0; i < emptyCells.Count; i++)
            {
                Tile randomTile = tilePool.GetRandomTile();
                ((RegularTile)randomTile).Selectable = emptyCells[i];
                randomTile.transform.position = new Vector3(emptyCells[i].GridPosition.x, 20f, 0f);
                emptyCells[i].SetTile(randomTile);
            }
            matchChecker.CheckTheBoard();
        }
    }
}