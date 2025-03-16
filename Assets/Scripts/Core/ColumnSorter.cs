using System.Collections.Generic;
using Core.Data;

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
            
            for (int x = 0; x < boardData.Columns; x++)
            {
                List<Tile> columnTiles = new List<Tile>(boardData.Columns);
                
                for (int i = 0; i < boardData.Rows; i++)
                {
                    int y=x+i*boardData.Columns;
                    
                    if(boardData.Board[y].Tile==null)
                        continue;
                    
                    columnTiles.Add(boardData.Board[y].Tile);
                }
                
                for (int i = 0; i < boardData.Rows; i++)
                {
                    int y=x+i*boardData.Columns;

                    if (i >= columnTiles.Count)
                    {
                        boardData.Board[y].SetTile(null);
                        emptyCells.Add(boardData.Board[y]);
                        modifiedCells.Add(boardData.Board[y]);
                    }
                    else
                    {
                        if(boardData.Board[y / boardData.Columns].Tile!=columnTiles[i])
                            modifiedCells.Add(boardData.Board[y]);
                        
                        boardData.Board[y].SetTile(columnTiles[i]);
                        ((RegularTile)(boardData.Board[y].Tile)).Selectable = boardData.Board[y];
                    }
                }
            }
        }
    }
}