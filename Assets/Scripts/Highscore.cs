using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Highscore : Saveable
{
    // Public members will be serialized
    public List<HighscoreEntry> table;

    public Highscore()
    {
        if (SaveManager.SaveExists(SaveType.Highscore))
            FromJsonString(SaveManager.Load(SaveType.Highscore));
        else table = new List<HighscoreEntry>();
    }

    // Do custom transformation, validation and serialization of Score here
    public void Save(string nickName, int score)
    {
        if (nickName.Length < 1) nickName = "unknown player";
        table.Add(new HighscoreEntry {nickName = nickName, score = score});
        table = table.OrderByDescending(scoreEntry => scoreEntry.score).ToList();
        if (table.Count > 10) table.RemoveRange(10, table.Count - 10);
        SaveManager.Save(ToJsonString(), SaveType.Highscore);
    }
}

[System.Serializable]
public class HighscoreEntry
{
    public string nickName;
    public int score;
}