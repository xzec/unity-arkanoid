using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private const int MinNameLength = 1;
    private GameStatus _gameStatus;

    private void Start() => _gameStatus = FindObjectOfType<GameStatus>();

    public void LoadLevelOneScene()
    {
        if (_gameStatus.NickName.Length < MinNameLength)
        {
            var input = FindObjectOfType<InputManager>();
            if (input != null) input.Shake();
            return;
        }
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameStatus>().ResetGame();
    }

    public void QuitGame() => Application.Quit();
}