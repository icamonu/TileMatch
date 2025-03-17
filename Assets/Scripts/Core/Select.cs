using UnityEngine;

namespace Core
{
    public class Select: MonoBehaviour
    {
        [SerializeField] private Tile tile;
        private void OnMouseDown()
        {
            if(tile is RegularTile regularTile)
                regularTile.Selectable.TileSelected();
        }
    }
}