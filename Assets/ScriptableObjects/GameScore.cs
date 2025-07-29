using UnityEngine;

[CreateAssetMenu(fileName = "GameScore", menuName = "Scriptable Objects/GameScore")]
public class GameScore : ScriptableObject
{
    public int score = 0;
    public int fishEatenCount = 0;
    public string text = "";
}
