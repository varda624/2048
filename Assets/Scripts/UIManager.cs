using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject GameOverPanel;
    public GameObject TextsParent;
    public TMP_Text HighestDiceText;
    public TMP_Text RecordScoreText;
    public TMP_Text CurrentScoreText;
    public TMP_Text GameOverRecordText;
    public TMP_Text GameOverTimeText;
    public Button RestartButton;
    public Button QuitMenuButton;
    public Button ExitButton;

    private float _levelTime;


    private void Awake()
    {
        RestartButton.onClick.AddListener(RestartGame);
        QuitMenuButton.onClick.AddListener(QuitMenu);
        ExitButton.onClick.AddListener(ExitGame);

        RestrictedZone.OnGameOver.AddListener(ActivateGameOverPanel);
    }

    public void Update()
    {
        HighestDiceText.text = $"Highest Dice: {GameManager.HighestNumberOnDice}";
        RecordScoreText.text = $"Record: {GameManager.RecordGameScore}";
        CurrentScoreText.text = $"Current Score: {GameManager.CurrentGameScore}";
        _levelTime = Time.timeSinceLevelLoad;
    }

    private void ActivateGameOverPanel()
    {
        GameOverPanel.SetActive(true);
        DeactivateTexts();
        GameOverRecordText.text = $"Record: {GameManager.CurrentGameScore:D6}";
        float min = _levelTime / 60;
        float sec = _levelTime % 60;
        GameOverTimeText.text = $"Time:  {(int)min}m : {(int)sec}s";
    }

    private void OnDestroy()
    {
        GameManager.OnGameOver.RemoveListener(ActivateGameOverPanel);
    }

    private void DeactivateTexts()
    {
        TextsParent.SetActive(false);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);      
    }

    private void QuitMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    private void ExitGame()
    {
        Application.Quit();
    }

}
