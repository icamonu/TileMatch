using System.Collections.Generic;
using Core.Data;
using Core.Interfaces;

namespace Core
{
    public class ColumnSorter
    {
        private BoardData boardData;
        private List<Cell> emptyCells;
        private List<Cell> modifiedCells;
        
        public ColumnSorter(BoardData boardData)
        {
            this.boardData = boardData;
            emptyCells = new List<Cell>(boardData.Board.Length);
            modifiedCells = new List<Cell>(boardData.Board.Length);
        }
        
        public List<Cell> GetModifiedCells()
        {
            return modifiedCells;
        }
        
        public List<Cell> GetEmptyCells()
        {
            return emptyCells;
        }

        public void SortColumns()
        {
            emptyCells.Clear();
            modifiedCells.Clear();
            
            for (int i = 0; i < boardData.Columns; i++)
            {
                Sort(boardData.Board[i]);
            }
            
            for(int i=boardData.Board.Length-1; i>=boardData.Board.Length-boardData.Columns; i--)
            {
                FindFillableCells(boardData.Board[i]);
            }
        }
        
        private void Sort(Cell cell)
        {
            Cell currentCell = cell;

            while (currentCell.TopCell!=null)
            {
                if (currentCell.Tile is ObstacleTile)
                {
                    currentCell = currentCell.TopCell;
                    continue;
                }

                if (currentCell.Tile==null)
                {
                    Cell nextCell = currentCell.TopCell;

                    while (nextCell.Tile==null && nextCell.TopCell!=null)
                    {
                        nextCell = nextCell.TopCell;
                        continue;
                    }

                    if (nextCell.Tile is IMovable)
                    {
                        currentCell.SetTile(nextCell.Tile);
                        ((RegularTile)(currentCell.Tile)).SetSelectable(currentCell);
                        modifiedCells.Add(currentCell);
                        nextCell.SetTile(null);
                        currentCell = currentCell.TopCell;
                        continue;
                    }

                    if (nextCell.Tile is not IMovable)
                    {
                        currentCell = nextCell;
                        continue;
                    }
                }
                
                currentCell = currentCell.TopCell;
            }
        }

        private void FindFillableCells(Cell cell)
        {
            Cell currentCell = cell;

            while (currentCell!=null)
            {
                if(currentCell.Tile!=null)
                    break;
                
                emptyCells.Add(currentCell);
                modifiedCells.Add(currentCell);
                currentCell = currentCell.BottomCell;
            }
        }
    }
}