using Core.Data;
using ScriptableObjects;
using UnityEngine;

namespace Core
{
    public class TilePool
    {
        private BoardData boardData;
        private GameObject tilePrefab;
        private GameSettings gameSettings;
        
        public TilePool(BoardData boardData, GameObject tilePrefab, GameSettings gameSettings)
        {
            this.boardData = boardData;
            this.tilePrefab = tilePrefab;
            this.gameSettings = gameSettings;
        }

        public void PopulateTheBoard()
        {
            for (int i = 0; i < boardData.Board.Length; i++)
            {
                Vector3 position = new Vector3(boardData.Board[i].GridPosition.x, boardData.Board[i].GridPosition.y, 0);
                RegularTile tile = Object.Instantiate(tilePrefab, position, Quaternion.identity).GetComponent<RegularTile>();
                int randomTileType = Random.Range(0, gameSettings.regularTiles.Count);
                tile.SetTileSO(gameSettings.regularTiles[randomTileType], randomTileType);
                boardData.Board[i].SetTile(tile);
                ((RegularTile)boardData.Board[i].Tile).Selectable = boardData.Board[i];
            }
        }

        public Tile GetRandomTile()
        {
            int randomTileType = Random.Range(0, gameSettings.regularTiles.Count);
            RegularTile tile = Object.Instantiate(tilePrefab).GetComponent<RegularTile>();
            tile.SetTileSO(gameSettings.regularTiles[randomTileType], randomTileType);
            return tile;
        }
    }
}