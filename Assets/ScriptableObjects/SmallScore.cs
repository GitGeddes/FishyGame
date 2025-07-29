using UnityEngine;

[CreateAssetMenu(fileName = "SmallScore", menuName = "Scriptable Objects/SmallScore")]
public class SmallScore : ScoreType
{
    SmallScore() {
        this.scoreAmount = 1;
        this.boneText = "\U0001F9B4";
    }
}
