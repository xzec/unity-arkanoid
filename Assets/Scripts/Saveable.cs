using UnityEngine;

public abstract class Saveable
{
    // Deserialize
    protected void FromJsonString(string jsonString) => JsonUtility.FromJsonOverwrite(jsonString, this);

    // Serialize
    protected string ToJsonString() => JsonUtility.ToJson(this);
}