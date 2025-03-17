using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Data;
using Core.Interfaces;

namespace Core
{
    public class ProcessController
    {
        private BoardData boardData;
        private MatchChecker matchChecker;
        private ColumnSorter columnSorter;
        private BoardRefiller boardRefiller;
        private BoardShuffler boardShuffler;
        private float fallDuration;

        public ProcessController(BoardData boardData, MatchChecker matchChecker, 
            ColumnSorter columnSorter, MovementSettings movementSettings,
            BoardShuffler boardShuffler, BoardRefiller boardRefiller)
        {
            this.boardData = boardData;
            this.matchChecker = matchChecker;
            this.columnSorter = columnSorter;
            this.boardShuffler = boardShuffler;
            this.boardRefiller = boardRefiller;
            this.fallDuration = movementSettings.fallDuration;
            
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
                if(modifiedCell.Tile is IMovable movableTile)
                    movableTile.Move(modifiedCell.GridPosition);
            }
            
            await Task.Delay((int)(fallDuration * 800));
            bool isDeadLock = matchChecker.CheckTheBoard();

            if (isDeadLock)
            {
                boardShuffler.Shuffle();
                matchChecker.CheckTheBoard();
            }
        }
    }
}