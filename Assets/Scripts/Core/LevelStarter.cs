using Core.Data;
using ScriptableObjects;
using UnityEngine;

namespace Core
{
    public class LevelStarter : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private MovementSettings movementSettings;
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private GameObject obtacleTilePrefab;
        
        private void Awake()
        {
            Application.targetFrameRate = 120;
            Init();
        }
        
        private void Init()
        {
            BoardData boardData = new BoardData(gameSettings.rows, gameSettings.columns);
            boardData.SetBoard();
            
            TilePool tilePool = new TilePool(boardData, tilePrefab, obtacleTilePrefab,
                gameSettings);
            tilePool.PopulateTheBoard();
            
            MatchChecker matchChecker = new MatchChecker(boardData);
            matchChecker.CheckTheBoard();
            
            ColumnSorter columnSorter = new ColumnSorter(boardData);
            ProcessController processController = new ProcessController(boardData, matchChecker, 
                columnSorter, tilePool, movementSettings);
        }
    }
}