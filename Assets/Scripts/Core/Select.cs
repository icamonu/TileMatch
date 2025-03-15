using UnityEngine;

namespace Core
{
    public class Select: MonoBehaviour
    {
        [SerializeField] private Tile tile;
        private void OnMouseDown()
        {
            ((RegularTile)tile).Selectable.TileSelected();
        }
    }
}