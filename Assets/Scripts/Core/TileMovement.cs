using UnityEngine;
using DG.Tweening;

namespace Core
{
    public class TileMovement: MonoBehaviour
    {
        public void Fall(Vector3 targetPosition, float duration)
        {
            transform.DOJump(targetPosition,1 ,1 ,duration);
        }
        
        public void BlastMovement(Vector3 targetPosition, float duration)
        {
            transform.DOMove(targetPosition, duration)
                .SetEase(Ease.OutElastic);
        }
    }
}