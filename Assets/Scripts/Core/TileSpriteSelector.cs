using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Core
{
    public class TileSpriteSelector: MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private TileSO tileSO;
        [SerializeField] private List<int> conditions;
        
        public void Init(TileSO tileSO)
        {
            this.tileSO = tileSO;
        }

        public void SetSprite(int conditionValue)
        {
            for (int i = conditions.Count - 1; i >= 0; i--)
            {
                if (conditionValue>conditions[i])
                {
                    spriteRenderer.sprite = tileSO.tileSprites[i+1];
                    return;
                }
            }
            
            spriteRenderer.sprite = tileSO.tileSprites[0];
        }
    }
}