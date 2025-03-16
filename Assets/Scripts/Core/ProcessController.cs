using System.Collections.Generic;
using System.Threading.Tasks;
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
        private BoardRefiller boardRefiller;
        private float fallDuration;

        public ProcessController(BoardData boardData, MatchChecker matchChecker, 
            ColumnSorter columnSorter, TilePool tilePool, MovementSettings movementSettings)
        {
            this.boardData = boardData;
            this.matchChecker = matchChecker;
            this.columnSorter = columnSorter;
            this.tilePool = tilePool;
            this.fallDuration = movementSettings.fallDuration;

            foreach (var cell in this.boardData.Board)
            {
                cell.OnTileSelected += OnTileSelected;
            }

            boardRefiller = new BoardRefiller(tilePool);
        }
        
        ~ProcessController()
        {
            foreach (var cell in boardData.Board)
            {
                cell.OnTileSelected -= OnTileSelected;
            }
        }
        
        public async void OnTileSelected(Cell cell)
        {
            HashSet<Cell> blastMatches = matchChecker.GetMatches(cell);
            if (blastMatches.Count<2)
                return;
            
            boardData.BlastTiles(blastMatches);
            
            HashSet<Cell> damagedBlastTiles = boardData.GetDamagedBlastTiles(blastMatches);
            boardData.BlastTiles(damagedBlastTiles);
            
            columnSorter.SortColumns();
            
            List<Cell> emptyCells = columnSorter.GetEmptyCells();
            boardRefiller.Refill(emptyCells);
            
            List<Cell> modifiedCells = columnSorter.GetModifiedCells();
            foreach (var modifiedCell in modifiedCells)
            {
                Vector3 cellPosition = new Vector3(modifiedCell.GridPosition.x,
                    modifiedCell.GridPosition.y, 0);
                ((RegularTile)modifiedCell.Tile).Move(cellPosition);
            }
            
            await Task.Delay((int)(fallDuration * 1000));
            matchChecker.CheckTheBoard();
        }
    }
}