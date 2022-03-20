using System.Collections.Generic;
using UnityEngine;
using System.IO;

public enum SaveType
{
    Game,
    Highscore
}

public static class SaveManager
{
    private static readonly string SavesDir = Path.Combine(Application.persistentDataPath, "saves");

    private static readonly Dictionary<SaveType, string> SaveFilePath
        = new Dictionary<SaveType, string>
        {
            {SaveType.Game, Path.Combine(SavesDir, "game.json")},
            {SaveType.Highscore, Path.Combine(SavesDir, "highscore.json")}
        };

    private static void EnsureDirExists()
    {
        if (!Directory.Exists(SavesDir)) Directory.CreateDirectory(SavesDir);
    }

    // Handle technical aspects of Save here - extensions, sanitation, file originality check, etc...
    public static void Save(string saveString, SaveType saveType)
    {
        EnsureDirExists();
        File.WriteAllText(SaveFilePath[saveType], saveString);
    }

    // Handle technical aspects of Load here
    public static string Load(SaveType saveType)
    {
        EnsureDirExists();
        return File.ReadAllText(SaveFilePath[saveType]);
    }

    public static bool SaveExists(SaveType saveType) => File.Exists(SaveFilePath[saveType]);
}