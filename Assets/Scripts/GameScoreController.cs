using UnityEngine;
using UnityEngine.UIElements;

public class GameScoreController
{
    VisualElement bonesImagesList;
    VisualElement topRow;
    VisualElement midRow;
    VisualElement bottomRow;
    int maxTopRow = 5;
    int maxMidRow = 10;
    int maxSmallRow = 15;
    int smallScore = 0;
    int midScore = 0;
    int bigScore = 0;

    VisualTreeAsset bigFishAsset = Resources.Load<VisualTreeAsset>("FishBone");
    VisualTreeAsset midFishAsset = Resources.Load<VisualTreeAsset>("MidFishBone");
    VisualTreeAsset smallFishAsset = Resources.Load<VisualTreeAsset>("SmallFishBone");
    VisualElement bigFish;
    VisualElement midFish;
    VisualElement smallFish;

    Label scoreLabel;
    public GameScore m_gameScore;
    public int m_fishEaten = 0;
    const string FISH_EATEN_STRING = "Fish Eaten: ";

    public GameScoreController(VisualElement visualElement)
    {
        SetVisualElement(visualElement);
    }

    public void SetVisualElement(VisualElement visualElement)
    {
        scoreLabel = visualElement.Q<Label>("ScoreText");
        scoreLabel.text = FISH_EATEN_STRING + m_fishEaten.ToString();
        m_gameScore = ScriptableObject.CreateInstance<GameScore>();

        bonesImagesList = visualElement.Q<VisualElement>("Images");
        topRow = visualElement.Q<VisualElement>("TopRow");
        midRow = visualElement.Q<VisualElement>("MidRow");
        bottomRow = visualElement.Q<VisualElement>("BottomRow");
    }

    public void SetGameScoreData(GameScore gameScore)
    {
        m_gameScore = gameScore;
        scoreLabel.text = m_gameScore.score.ToString();
    }

    public void AddScore(int score)
    {
        m_gameScore.score += score;
        AddFishScore(score);
    }

    public void AddFishEaten(int fishEaten)
    {
        m_gameScore.fishEatenCount += fishEaten;
        m_fishEaten = fishEaten;
        scoreLabel.text = FISH_EATEN_STRING + (m_gameScore.fishEatenCount).ToString();
    }

    public void AddFishScore(int fishScore)
    {
        int smallScore = fishScore % maxSmallRow;
        int midScore = (fishScore / maxSmallRow) % maxMidRow;
        int bigScore = (fishScore / maxSmallRow / maxMidRow) % maxTopRow;

        Debug.Log("small score: " + smallScore + " mid score: " + midScore + " big score: " + bigScore);

        AddSmallFish(smallScore);
        AddMidFish(midScore);
        AddBigFish(bigScore);
    }

    public void AddSmallFish(int smallFishScore)
    {
        if (smallFishScore > 0)
        {
            smallScore += smallFishScore;
            if (smallScore > maxSmallRow)
            {
                int overflow = smallScore / maxSmallRow;
                smallScore = smallScore % maxSmallRow;
                bottomRow.Clear();
                AddMidFish(overflow);
            }
            for (int i = 0; i < smallFishScore; i++)
            {
                smallFish = smallFishAsset.Instantiate();
                bottomRow.Add(smallFish);
            }
        }
    }

    public void AddMidFish(int midFishScore)
    {
        if (midFishScore > 0)
        {
            midScore += midFishScore;
            if (midScore > maxMidRow)
            {
                int overflow = midScore / maxMidRow;
                midScore = midScore % maxMidRow;
                midRow.Clear();
                AddBigFish(overflow);
            }
            for (int i = 0; i < midFishScore; i++)
            {
                midFish = midFishAsset.Instantiate();
                midRow.Add(midFish);
            }
        }
    }

    public void AddBigFish(int bigFishScore)
    {
        if (bigFishScore > 0)
        {
            for (int i = 0; i < bigFishScore; i++)
            {
                bigFish = bigFishAsset.Instantiate();
                topRow.Add(bigFish);
            }
        }
    }
}
