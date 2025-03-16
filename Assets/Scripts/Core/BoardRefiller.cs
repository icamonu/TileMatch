using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class BoardRefiller
    {
        private TilePool tilePool;

        public BoardRefiller(TilePool tilePool)
        {
            this.tilePool = tilePool;
        }

        public void Refill(List<Cell> emptyCells)
        {
            for (int i = 0; i < emptyCells.Count; i++)
            {
                Tile randomTile = tilePool.GetRandomTile();
                ((RegularTile)randomTile).Selectable = emptyCells[i];
                randomTile.transform.position = new Vector3(emptyCells[i].GridPosition.x, 20f, 0f);
                emptyCells[i].SetTile(randomTile);
            }
        }
    }
}