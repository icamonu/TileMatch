using System.Threading.Tasks;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShuffleButton: MonoBehaviour
    {
        [SerializeField] private Button shuffleButton;
        [SerializeField] private LevelStarter levelStarter;

        private BoardShuffler boardShuffler;
        MatchChecker matchChecker;
        private void Start()
        {
            boardShuffler = new BoardShuffler(levelStarter.BoardData);
            matchChecker = new MatchChecker(levelStarter.BoardData);
            shuffleButton.onClick.AddListener(Shuffle);
        }
        
        private void OnDestroy()
        {
            shuffleButton.onClick.RemoveAllListeners();
        }

        async void Shuffle()
        {
            boardShuffler.Shuffle();
            await Task.Delay(500);
            matchChecker.CheckTheBoard();
        }
    }
}