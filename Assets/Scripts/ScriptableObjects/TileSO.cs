using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "TileSO", menuName = "ScriptableObjects/TileSO", order = 0)]
    public class TileSO: ScriptableObject
    {
        public List<Sprite> tileSprites;
    }
}