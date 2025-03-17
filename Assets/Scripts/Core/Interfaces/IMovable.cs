using UnityEngine;

namespace Core.Interfaces
{
    public interface IMovable
    {
        void Move(Vector2Int targetPosition);
        void SetSelectable(Cell cell);
    }
}