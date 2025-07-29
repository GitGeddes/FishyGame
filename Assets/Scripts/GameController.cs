using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private UIDocument m_document;
    [SerializeField]
    private VisualTreeAsset m_treeAsset;
    private PlayViewPresenter playViewPresenter;
    private GameScoreController m_scoreController;
    private void Awake()
    {
        playViewPresenter = m_document.GetComponent<PlayViewPresenter>();

        VisualElement visualElement = m_document.rootVisualElement;
        m_scoreController = new GameScoreController(visualElement);
    }

    public void SetGameScoreData(int score, int fishEaten)
    {
        GameScore gameScore = ScriptableObject.CreateInstance<GameScore>();
        gameScore.score += score;
        gameScore.fishEatenCount += fishEaten;
        m_scoreController.SetGameScoreData(gameScore);
    }

    public void AddScore(int score)
    {
        m_scoreController.AddScore(score);
    }

    public void AddFishEaten(int fishEaten)
    {
        m_scoreController.AddFishEaten(fishEaten);
    }

    public void Win()
    {
        playViewPresenter.Win();
    }

    public void Lose()
    {
        playViewPresenter.Lose();
    }
}
