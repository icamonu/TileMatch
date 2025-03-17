using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Data
{
    public class BoardData
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        
        public Cell[] Board { get; private set; }

        public event Action OnBoardCreated; 
        
        public BoardData(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
        }

        public void SetBoard()
        {
            Board = new Cell[Rows * Columns];
            
            for (int i = 0; i < Columns*Rows; i++)
            {
                Board[i] = new Cell(new Vector2Int(i % Columns, i / Columns));
            }
            
            FindNeighbours();
            
            OnBoardCreated?.Invoke();
        }

        public void BlastTiles(HashSet<Cell> blastTiles)
        {
            foreach (Cell cell in blastTiles)
            {
                cell.BlastTile();
            }
        }
        
        public HashSet<Cell> GetDamagedBlastTiles(HashSet<Cell> blastMatches)
        {
            HashSet<Tile> getDamagedTiles = new HashSet<Tile>();
            HashSet<Cell> damagedBlastTiles = new HashSet<Cell>();
            foreach (var blastMatch in blastMatches)
            {
                List<Cell> neighbours = blastMatch.Neighbours;
                foreach (var neighbour in neighbours)
                {
                    if (neighbour.Tile is not ObstacleTile tile)
                        continue;

                    if (getDamagedTiles.Contains(tile))
                        break;
                    
                    tile.GetDamage();
                    getDamagedTiles.Add(tile);
                    
                    if (tile.Health == 0)
                    {
                        damagedBlastTiles.Add(neighbour);
                    }
                }
            }
            
            return damagedBlastTiles;
        }
        
        private void FindNeighbours()
        {
            for (int i = 0; i < Board.Length; i++)
            {
                List<Cell> neighbours = new List<Cell>(4);
                Cell topCell=null;
                Cell bottomCell=null;
                
                if (i % Columns != 0)
                {
                    neighbours.Add(Board[i - 1]);
                }

                if (i % Columns != Columns - 1)
                {
                    neighbours.Add(Board[i + 1]);
                }

                if (i / Columns != 0)
                {
                    neighbours.Add(Board[i - Columns]);
                    bottomCell = Board[i - Columns];
                }

                if (i / Columns != Rows - 1)
                {
                    neighbours.Add(Board[i + Columns]);
                    topCell = Board[i + Columns];
                }

                Board[i].SetNeighbours(neighbours, topCell, bottomCell);
            }
        }
    }
}