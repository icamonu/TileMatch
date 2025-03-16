using UnityEngine;
using DG.Tweening;

namespace Core
{
    public class TileMovement: MonoBehaviour
    {
        public void Fall(Vector3 targetPosition, float duration)
        {
            transform.DOMove(targetPosition, duration).SetEase(Ease.OutCubic);
        }
        
        public void BlastMovement(float duration)
        {
            transform.DOScale(0, duration)
                .SetEase(Ease.OutCubic);
        }
    }
}