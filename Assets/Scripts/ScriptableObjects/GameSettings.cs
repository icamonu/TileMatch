using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 0)]
    public class GameSettings: ScriptableObject
    {
        [Header("Board Dimensions")]
        public int rows;
        public int columns;
        
        [Header("Regular Tiles(Scriptable Objects)")]
        public List<TileSO> regularTiles;
        
        [Header("Obstacle Tiles(Scriptable Objects)")]
        public List<TileSO> obstacleTiles;

        [Header("Match Conditions")] 
        public int matchConditionA;
        public int matchConditionB;
        public int matchConditionC;
    }
}