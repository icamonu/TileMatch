using UnityEngine;

namespace Core
{
    public class Select: MonoBehaviour
    {
        [SerializeField] private Tile tile;
        private void OnMouseDown()
        {
            tile.Cell.TileSelected();
        }
    }
}