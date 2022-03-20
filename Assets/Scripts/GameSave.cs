[System.Serializable]
public class GameSave : Saveable
{
    public string nickName;
    public string sceneName;
    public int score;
    public bool valid;

    public GameSave()
    {
        if (SaveManager.SaveExists(SaveType.Game))
            FromJsonString(SaveManager.Load(SaveType.Game));
        else valid = false;
    }

    // Do custom transformation, validation and serialization of Game save here
    public void Save(string nickName, string sceneName, int score)
    {
        this.nickName = nickName;
        this.sceneName = sceneName;
        this.score = score;
        valid = true;
        SaveManager.Save(ToJsonString(), SaveType.Game);
    }

    public void Invalidate()
    {
        valid = false;
        SaveManager.Save(ToJsonString(), SaveType.Game);
    }
}