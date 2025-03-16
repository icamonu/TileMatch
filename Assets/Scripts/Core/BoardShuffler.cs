using System.Collections.Generic;
using Core.Data;

namespace Core
{
    public class BoardShuffler
    {
        private BoardData boardData;
        private Dictionary<int, List<Cell>> tileTypes= new Dictionary<int, List<Cell>>();
        private HashSet<Cell> swappedCells = new HashSet<Cell>();
        private Dictionary<int, Cell> randomCells= new Dictionary<int, Cell>();
        
        public BoardShuffler(BoardData boardData)
        {
            this.boardData = boardData;
        }
        
        public void Shuffle()
        {
            int maxPossibleTileInCluster = 10;
            swappedCells.Clear();
            
            // for (int i = 0; i < boardData.Board.Length; i++)
            // {
            //     randomCells.Add(i, boardData.Board[i]);
            // }
            
            foreach (var cell in boardData.Board)
            {
                if (cell.Tile is ObstacleTile)
                    continue;
                
                int tileType = ((RegularTile)cell.Tile).TileType;
                if (!tileTypes.ContainsKey(tileType))
                {
                    tileTypes.Add(tileType, new List<Cell>());
                }
                
                tileTypes[tileType].Add(cell);
            }

            for (int i = 0; i < 5; i++)
            {
                int blockClusterCounter = 0;
                
                Cell randomCell;
                do
                {
                    randomCell= boardData.Board[UnityEngine.Random.Range(0, boardData.Board.Length)];
                }while (randomCell.Tile is ObstacleTile);
                
                int tileType = ((RegularTile)randomCell.Tile).TileType;

                while (blockClusterCounter<maxPossibleTileInCluster)
                {
                    Cell swapCell=tileTypes[tileType][UnityEngine.Random.Range(0, tileTypes[tileType].Count)];
                    Cell randomNeighbor = randomCell.Neighbours[UnityEngine.Random.Range(0, randomCell.Neighbours.Count)];
                
                    SwapTiles(randomNeighbor, swapCell);
                    blockClusterCounter++;
                }
            }
        }
        
        private void SwapTiles(Cell cell1, Cell cell2)
        {
            if(cell1.Tile is ObstacleTile || cell2.Tile is ObstacleTile)
                return;
            
            if (swappedCells.Contains(cell1) || swappedCells.Contains(cell2))
                return;
            
            Tile tile1 = cell1.Tile;
            Tile tile2 = cell2.Tile;
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