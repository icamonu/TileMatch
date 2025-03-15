using System.Collections.Generic;
using Core.Data;

namespace Core
{
    public class MatchChecker
    {
        private BoardData boardData;
        private Queue<Cell> queue;
        private HashSet<Cell> matches;
        private HashSet<Cell> visitedTiles;
        private HashSet<Cell> determinedMatches;
        
        public MatchChecker(BoardData boardData)
        {
            this.boardData = boardData;
            Init();
        }

        private void Init()
        {
            matches = new HashSet<Cell>(boardData.Board.Length);
            visitedTiles = new HashSet<Cell>(boardData.Board.Length);
            queue = new Queue<Cell>(boardData.Board.Length);
            determinedMatches = new HashSet<Cell>(boardData.Board.Length);
            foreach (Cell cell in boardData.Board)
            {
                cell.OnTileSelected += OnTileSelected;
            }
        }

        ~MatchChecker()
        {
            foreach (Cell cell in boardData.Board)
            {
                cell.OnTileSelected -= OnTileSelected;
            }
        }

        private void OnTileSelected(Cell cell)
        {
            HashSet<Cell> blastMatches = GetMatches(cell);
            if (blastMatches.Count<2)
                return;
            
            boardData.BlastTiles(blastMatches);
        }

        public HashSet<Cell> GetMatches(Cell startCell)
        {
            matches.Clear();
            visitedTiles.Clear();
            queue.Clear();
            
            queue.Enqueue(startCell);

            while (queue.Count > 0)
            {
                Cell currentCell = queue.Dequeue();
                matches.Add(currentCell);
                visitedTiles.Add(currentCell);
                
                List<Cell> neighbours = currentCell.Neighbours;
                foreach (Cell neighbour in neighbours)
                {
                    if(neighbour.Tile==null)
                        continue;
                    
                    if (visitedTiles.Contains(neighbour))
                        continue;

                    if (neighbour.Tile is not RegularTile tile)
                        continue;
                    
                    if (tile.TileType == ((RegularTile)currentCell.Tile).TileType)
                    {
                        matches.Add(neighbour);
                        queue.Enqueue(neighbour);
                    }
                }
            }

            return matches;
        }
        
        public void CheckTheBoard()
        {
            determinedMatches.Clear();
            
            for(int i=0; i<boardData.Board.Length; i++)
            {
                if(boardData.Board[i].Tile==null)
                    continue;
                
                if(determinedMatches.Contains(boardData.Board[i]))
                    continue;
                
                HashSet<Cell> currentMatches = GetMatches(boardData.Board[i]);
                
                foreach (Cell cell in currentMatches)
                {
                    ((IMatchable)(cell.Tile)).OnMatch(currentMatches.Count);
                }
                determinedMatches.UnionWith(currentMatches);
            }
        }
    }
}