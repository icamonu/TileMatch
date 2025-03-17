using Core.Data;
using ScriptableObjects;
using UnityEngine;

namespace Core
{
    public class LevelStarter : MonoBehaviour
    {
        [SerializeField] private GameSettings gameSettings;
        [SerializeField] private MovementSettings movementSettings;
        [SerializeField] private RegularTile tilePrefab;
        [SerializeField] private ObstacleTile obstacleTilePrefab;
        
        private void Awake()
        {
            Application.targetFrameRate = 120;
            Init();
        }

        private BoardData boardData;
        private void Init()
        {
            boardData = new BoardData(gameSettings.rows, gameSettings.columns);
            boardData.SetBoard();
            
            RandomLevelLoader randomLevelLoader = new RandomLevelLoader(boardData, tilePrefab, obstacleTilePrefab,
                gameSettings);
            randomLevelLoader.PopulateTheBoard();
            
            MatchChecker matchChecker = new MatchChecker(boardData);
            matchChecker.CheckTheBoard();
            
            ColumnSorter columnSorter = new ColumnSorter(boardData);
            ProcessController processController = new ProcessController(boardData, matchChecker, 
                columnSorter, randomLevelLoader, movementSettings);
            
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                new BoardShuffler(boardData).Shuffle();
        }
    }
}