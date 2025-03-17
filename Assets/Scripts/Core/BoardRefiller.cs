using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class BoardRefiller
    {
        private RandomLevelLoader randomLevelLoader;

        public BoardRefiller(RandomLevelLoader randomLevelLoader)
        {
            this.randomLevelLoader = randomLevelLoader;
        }

        public void Refill(List<Cell> emptyCells)
        {
            for (int i = 0; i < emptyCells.Count; i++)
            {
                Tile randomTile = randomLevelLoader.GetRandomRegularTile();
                ((RegularTile)randomTile).SetSelectable(emptyCells[i]);
                randomTile.transform.position = new Vector3(emptyCells[i].GridPosition.x, 20f, 0f);
                emptyCells[i].SetTile(randomTile);
            }
        }
    }
}