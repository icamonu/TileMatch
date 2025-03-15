using System.Collections.Generic;
using Core.Data;

namespace Core
{
    public class ColumnSorter
    {
        private BoardData boardData;
        
        public ColumnSorter(BoardData boardData)
        {
            this.boardData = boardData;
        }

        public List<Cell> SortColumns()
        {
            List<Cell> emptyCells = new List<Cell>(boardData.Board.Length);
            
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
                    }
                    else
                    {
                        boardData.Board[y].SetTile(columnTiles[i]);
                        ((RegularTile)(boardData.Board[y].Tile)).Selectable = boardData.Board[y];
                    }
                }
            }

            return emptyCells;
        }
    }
}