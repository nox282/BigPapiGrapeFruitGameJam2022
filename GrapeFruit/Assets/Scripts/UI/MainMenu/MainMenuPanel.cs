using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanel : UIPanel
{
    [SerializeField] private string PlaySceneName;

    [Header("Buttons")]
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button SettingsButton;
    [SerializeField] private Button HowToPlayButton;
    [SerializeField] private Button ExitButton;

    [Header("UIPanels")]
    [SerializeField] private UIPanel SettingsPanel;
    [SerializeField] private UIPanel HowToPlayPanel;


    private void OnEnable()
    {
        PlayButton.onClick.AddListener(OnPlayButtonClicked);
        SettingsButton.onClick.AddListener(OnSettingsButtonClicked);
        HowToPlayButton.onClick.AddListener(OnHowToPlayButtonClicked);
        ExitButton.onClick.AddListener(OnExitButtonClicked);
    }

    private void OnDisable()
    {
        PlayButton.onClick.RemoveListener(OnPlayButtonClicked);
        SettingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        HowToPlayButton.onClick.RemoveListener(OnHowToPlayButtonClicked);
        ExitButton.onClick.RemoveListener(OnExitButtonClicked);
    }

    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(PlaySceneName, LoadSceneMode.Single);
    }

    private void OnSettingsButtonClicked()
    {
        SettingsPanel.Open();
    }

    private void OnHowToPlayButtonClicked()
    {
        HowToPlayPanel.Open();
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
