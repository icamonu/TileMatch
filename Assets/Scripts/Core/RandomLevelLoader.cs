using Core.Data;
using Core.Interfaces;
using ScriptableObjects;
using UnityEngine;
using Pooling;

namespace Core
{
    public class RandomLevelLoader
    {
        private BoardData boardData;
        private RegularTile tilePrefab;
        private ObstacleTile obstacleTilePrefab;
        private GameSettings gameSettings;
        
        public RandomLevelLoader(BoardData boardData, RegularTile tilePrefab, ObstacleTile obstacleTilePrefab, GameSettings gameSettings)
        {
            this.boardData = boardData;
            this.tilePrefab = tilePrefab;
            this.obstacleTilePrefab = obstacleTilePrefab;
            this.gameSettings = gameSettings;
        }

        public void PopulateTheBoard()
        {
            int maxObstacleCount = (int)(boardData.Board.Length * gameSettings.obstacleTileRate);
            int obstacleCounter = 0;
            
            while (obstacleCounter<maxObstacleCount)
            {
                int randomIndex = Random.Range(0, boardData.Board.Length);
                
                if (boardData.Board[randomIndex].Tile != null)
                    continue;
                
                Vector3 position = new Vector3(boardData.Board[randomIndex].GridPosition.x, 
                    boardData.Board[randomIndex].GridPosition.y, 0);
                ObstacleTile obstacleTile = ObjectPool<ObstacleTile>.Get(obstacleTilePrefab, position);
                boardData.Board[randomIndex].SetTile(obstacleTile);
                obstacleCounter++;
            }
            
            for (int i = 0; i < boardData.Board.Length; i++)
            {
                if (boardData.Board[i].Tile != null)
                    continue;
                
                Vector3 position = new Vector3(boardData.Board[i].GridPosition.x, boardData.Board[i].GridPosition.y, 0);
                RegularTile tile = ObjectPool<RegularTile>.Get(tilePrefab, position, Quaternion.identity);
                int randomTileType = Random.Range(0, gameSettings.regularTiles.Count);
                tile.SetTileSO(gameSettings.regularTiles[randomTileType], randomTileType);
                boardData.Board[i].SetTile(tile);
                
                if (tile is IMovable movableTile)
                    movableTile.SetSelectable(boardData.Board[i]);
            }
        }

        public Tile GetRandomRegularTile()
        {
            int randomTileType = Random.Range(0, gameSettings.regularTiles.Count);
            RegularTile tile = ObjectPool<RegularTile>.Get(tilePrefab);
            tile.SetTileSO(gameSettings.regularTiles[randomTileType], randomTileType);
            return tile;
        }
    }
}