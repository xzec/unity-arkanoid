using TMPro;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameEndTitleText;
    [SerializeField] private TextMeshProUGUI gameEndScoreText;

    private void Start()
    {
        var gameStatus = FindObjectOfType<GameStatus>();
        gameEndTitleText.text = gameStatus.isWin ? "Congrats, you win!" : "Game over";
        gameEndScoreText.text = $"Total score: {gameStatus.CurrentScore}";
    }
}