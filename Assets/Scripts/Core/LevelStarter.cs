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
        
        public BoardData BoardData { get; private set; }
        
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            BoardData = new BoardData(gameSettings.rows, gameSettings.columns);
            BoardData.SetBoard();
            
            RandomLevelLoader randomLevelLoader = new RandomLevelLoader(BoardData, tilePrefab, obstacleTilePrefab,
                gameSettings);
            randomLevelLoader.PopulateTheBoard();
            
            MatchChecker matchChecker = new MatchChecker(BoardData);
            matchChecker.CheckTheBoard();
            
            BoardShuffler boardShuffler = new BoardShuffler(BoardData);

            if (matchChecker.Deadlock)
                boardShuffler.Shuffle();
            
            ColumnSorter columnSorter = new ColumnSorter(BoardData);
            BoardRefiller boardRefiller = new BoardRefiller(randomLevelLoader);
            ProcessController processController = new ProcessController(BoardData, matchChecker, 
                columnSorter, movementSettings, boardShuffler, boardRefiller);
        }
    }
}