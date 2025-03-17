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
        public bool Deadlock { get; private set; }=false;
        
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
        }

        public HashSet<Cell> GetMatches(Cell startCell)
        {
            matches.Clear();
            visitedTiles.Clear();
            queue.Clear();
            
            if(startCell.Tile is not IMatchable)
                return matches;
            
            queue.Enqueue(startCell);

            Cell currentCell;

            while (queue.Count > 0)
            {
                currentCell = queue.Dequeue();
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
        
        public bool CheckTheBoard()
        {
            determinedMatches.Clear();
            int matchCounter = 0;
            
            for(int i=0; i<boardData.Board.Length; i++)
            {
                if(boardData.Board[i].Tile==null)
                    continue;
                
                if(determinedMatches.Contains(boardData.Board[i]))
                    continue;
                
                HashSet<Cell> currentMatches = GetMatches(boardData.Board[i]);

                if(currentMatches.Count>1)
                    matchCounter++;
                
                foreach (Cell cell in currentMatches)
                {
                    if (cell.Tile is IMatchable matchableTile)
                        matchableTile.OnMatch(currentMatches.Count);
                    
                }
                determinedMatches.UnionWith(currentMatches);
            }

            Deadlock = matchCounter == 0;

            return Deadlock;
        }
    }
}