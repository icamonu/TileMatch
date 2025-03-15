using System;
using Core.Data;
using ScriptableObjects;
using UnityEngine;

namespace Core
{
    public class LevelStarter : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private GameObject tilePrefab;
        
        private void Awake()
        {
            Init();
        }
        
        private void Init()
        {
            BoardData boardData = new BoardData(gameSettings.rows, gameSettings.columns);
            boardData.SetBoard();
            
            TilePool tilePool = new TilePool(boardData, tilePrefab, gameSettings);
            tilePool.PopulateTheBoard();
            
            MatchChecker matchChecker = new MatchChecker(boardData);
            matchChecker.CheckTheBoard();
        }
    }
}