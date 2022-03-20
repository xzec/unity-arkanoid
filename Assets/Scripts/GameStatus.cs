using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameStatus : MonoBehaviour
{
    [Range(0.1f, 10f)] [SerializeField] private float gameSpeed = 1f;
    [SerializeField] private int pointsPerBlockDestroyed = 2;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI nickNameText;
    [SerializeField] private bool isAutoPlayEnabled;
    private int _currentScore;
    private string _nickName = "";
    public bool isWin = true;

    public string NickName
    {
        get => _nickName;
        set
        {
            _nickName = value;
            nickNameText.text = value;
        }
    }

    public int CurrentScore
    {
        get => _currentScore;
        private set
        {
            _currentScore = value;
            scoreText.text = $"Score {_currentScore.ToString()}";
        }
    }

    private void Awake()
    {
        var gameStatusCount = FindObjectsOfType<GameStatus>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        else DontDestroyOnLoad(gameObject);
    }

    private void Update() => Time.timeScale = gameSpeed;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        LoadGameIfAvailable();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        var isLevel = scene.name.StartsWith("Level");
        var isGameEnd = scene.name.StartsWith("GameEnd");

        ShowHud(isLevel);
        if (isLevel) SaveProgress();
        if (isGameEnd) HandleGameEnd();
    }

    private void SaveProgress()
    {
        var activeSceneName = SceneManager.GetActiveScene().name;
        new GameSave().Save(NickName, activeSceneName, _currentScore);
    }

    private void HandleGameEnd()
    {
        new Highscore().Save(NickName, _currentScore);
        new GameSave().Invalidate();
    }

    private void LoadGameIfAvailable()
    {
        var gameSave = new GameSave();
        if (!gameSave.valid) return;
        CurrentScore = gameSave.score;
        NickName = gameSave.nickName;
        SceneManager.LoadScene(gameSave.sceneName);
    }

    private void ShowHud(bool show)
    {
        if (!nickNameText || !scoreText) return;
        nickNameText.gameObject.SetActive(show);
        scoreText.gameObject.SetActive(show);
    }

    public void AddToScore()
    {
        _currentScore += pointsPerBlockDestroyed;
        scoreText.text = $"Score {_currentScore.ToString()}";
    }

    public void ResetGame() => Destroy(gameObject);

    public bool IsAutoPlayEnabled() => isAutoPlayEnabled;

    public void Lose() => isWin = false;
}