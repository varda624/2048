using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Button StartGameButton;
    public Button OpenSettingsPanelButton;
    public Button CloseSettingPanelButton;
    public Button QuitGameButton;
    public CanvasGroup LoadingScreen;
    public CanvasGroup SettingsPanel;

    private Vector3 _targetScale = Vector3.one * 0.9f;
    private Vector3 _originalScale = Vector3.one;
    private float _duration = 0.1f;

    private void Awake()
    {
        StartGameButton.onClick.AddListener(StartGame);
        OpenSettingsPanelButton.onClick.AddListener(OpenSettingsPanel);
        CloseSettingPanelButton.onClick.AddListener(CloseSettingsPanel);
        QuitGameButton.onClick.AddListener(QuitGame);

    }

    private void StartGame()
    {
        StartGameButton.transform.DOScale(_targetScale, _duration).OnComplete(() =>
        {
            StartGameButton.transform.DOScale(_originalScale, _duration).OnComplete(() =>
            {
                LoadingScreen.gameObject.SetActive(true);
                LoadingScreen.DOFade(1, 2f).OnComplete(() =>
                {
                    SceneManager.LoadScene("SampleScene");
                    StartGameButton.transform.DOKill();
                    LoadingScreen.DOKill();
                });
            });
        });
    }

    private void OpenSettingsPanel()
    {
        OpenSettingsPanelButton.transform.DOScale(_targetScale, _duration).OnComplete(() =>
        {
            OpenSettingsPanelButton.transform.DOScale(_originalScale, _duration).OnComplete(() =>
            {
                SettingsPanel.alpha = 0;
                SettingsPanel.gameObject.SetActive(true);
                SettingsPanel.DOFade(1, 0.5f);
                
            });
        });
    }

    private void CloseSettingsPanel()
    {
        CloseSettingPanelButton.transform.DOScale(_targetScale, _duration).OnComplete(() =>
        {
            CloseSettingPanelButton.transform.DOScale(_originalScale, _duration).OnComplete(() =>
            {
                SettingsPanel.DOFade(0, 0.5f).OnComplete(() => SettingsPanel.gameObject.SetActive(false));
                
            });
        });
    }

    private void QuitGame()
    {
        QuitGameButton.transform.DOScale(_targetScale, _duration).OnComplete(() =>
        {
            QuitGameButton.transform.DOScale(_originalScale, _duration).OnComplete(() =>
            {
                LoadingScreen.gameObject.SetActive(true);
                LoadingScreen.DOFade(1, 1f).OnComplete(() =>
                {
                    Application.Quit();
                    QuitGameButton.transform.DOKill();
                    LoadingScreen.DOKill();
                });
            });
        });
    }
}
