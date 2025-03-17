using System.Collections.Generic;
using Core.Data;

namespace Core
{
    public class BoardShuffler
    {
        int maxPossibleTileInCluster = 10;
        private BoardData boardData;
        private Dictionary<int, LinkedList<Cell>> tileTypes= new Dictionary<int, LinkedList<Cell>>();
        private HashSet<Cell> swappedCells = new HashSet<Cell>();
        
        public BoardShuffler(BoardData boardData)
        {
            this.boardData = boardData;
        }

        public void Shuffle()
        {
            swappedCells.Clear();
            GetAllTileTypes();
            foreach (int tileType in tileTypes.Keys)
            {
                if(tileTypes[tileType].Count<2)
                    continue;
                
                BuildCluster(tileType);
            }
        }

        private void BuildCluster(int tileType)
        {
            int blockClusterCounter = 0;
            
            Cell currentCell = tileTypes[tileType].First.Value;
            swappedCells.Add(currentCell);
            tileTypes[tileType].RemoveFirst();

            int neighborIndex = 0;
            
            while (blockClusterCounter<maxPossibleTileInCluster)
            {
                if(tileTypes[tileType].Count == 0) break;

                Cell swapCell = tileTypes[tileType].First.Value;
                tileTypes[tileType].RemoveFirst();
                do
                {
                    neighborIndex++;
                } while (currentCell.Neighbours[neighborIndex%currentCell.Neighbours.Count].Tile is ObstacleTile);
                
                if(swappedCells.Contains(currentCell.Neighbours[neighborIndex%currentCell.Neighbours.Count]))
                    continue;
                
                Cell randomNeighbor = currentCell.Neighbours[neighborIndex%currentCell.Neighbours.Count];
                SwapTiles(randomNeighbor, swapCell);
                currentCell = currentCell.Neighbours[neighborIndex%currentCell.Neighbours.Count];
                neighborIndex++;
                blockClusterCounter++;
            }
        }

        private void GetAllTileTypes()
        {
            tileTypes.Clear();
            for (int i = 0; i < boardData.Board.Length; i++)
            {
                if (boardData.Board[i].Tile is ObstacleTile)
                    continue;
                
                int tileType = ((RegularTile)boardData.Board[i].Tile).TileType;
                if (!tileTypes.ContainsKey(tileType))
                {
                    tileTypes.Add(tileType, new LinkedList<Cell>());
                }
                
                bool addFirst = UnityEngine.Random.Range(0, 2) == 0;

                if (addFirst)
                    tileTypes[tileType].AddFirst(boardData.Board[i]);
                else
                    tileTypes[tileType].AddLast(boardData.Board[i]);
            }
        }
        
        private void SwapTiles(Cell cell1, Cell cell2)
        {
            if(cell1.Tile is ObstacleTile || cell2.Tile is ObstacleTile)
                return;

            if (swappedCells.Contains(cell1) || swappedCells.Contains(cell2))
            {
                swappedCells.Add(cell1);
                swappedCells.Add(cell2);
                return;
            }
                 
            
            Tile tile1 = cell1.Tile;
            Tile tile2 = cell2.Tile;
            
            if(((RegularTile)tile1).TileType == ((RegularTile)tile2).TileType)
                return;
            
            cell1.SetTile(tile2);
            cell2.SetTile(tile1);
            ((RegularTile)(cell1.Tile)).SetSelectable(cell1);
            ((RegularTile)(cell2.Tile)).SetSelectable(cell2);
            
            ((RegularTile)(cell1.Tile)).Move(cell1.GridPosition);
            ((RegularTile)(cell2.Tile)).Move(cell2.GridPosition);
            
            swappedCells.Add(cell1);
            swappedCells.Add(cell2);
        }
    }
}