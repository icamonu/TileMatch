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
        private readonly BoardRefiller boardRefiller;

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

            boardRefiller = new BoardRefiller(tilePool);
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
            HashSet<Cell> blastMatches = matchChecker.GetMatches(cell);
            if (blastMatches.Count<2)
                return;
            
            boardData.BlastTiles(blastMatches);
            List<Cell> emptyCells = columnSorter.SortColumns();
            boardRefiller.Refill(emptyCells);
            matchChecker.CheckTheBoard();
        }
    }
}