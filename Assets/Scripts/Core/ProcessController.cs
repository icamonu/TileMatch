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
        private RandomLevelLoader _randomLevelLoader;
        private BoardRefiller boardRefiller;
        private float fallDuration;
        private BoardShuffler boardShuffler;

        public ProcessController(BoardData boardData, MatchChecker matchChecker, 
            ColumnSorter columnSorter, RandomLevelLoader randomLevelLoader, MovementSettings movementSettings)
        {
            this.boardData = boardData;
            this.matchChecker = matchChecker;
            this.columnSorter = columnSorter;
            this._randomLevelLoader = randomLevelLoader;
            this.fallDuration = movementSettings.fallDuration;

            foreach (var cell in this.boardData.Board)
            {
                cell.OnTileSelected += OnTileSelected;
            }

            boardRefiller = new BoardRefiller(randomLevelLoader);
            boardShuffler= new BoardShuffler(boardData);
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
            
            await Task.Delay((int)(fallDuration * 800));
            bool isDeadLock = matchChecker.CheckTheBoard();

            if (isDeadLock)
            {
                Debug.Log("Deadlock");
                boardShuffler.Shuffle();
            }
                
        }
    }
}