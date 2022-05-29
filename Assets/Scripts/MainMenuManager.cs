using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private RectTransform _currenPanel;
    private RectTransform _previousPanel;

    [SerializeField] private RectTransform _mainMenuPanel;
    [SerializeField] private RectTransform _beginPanel;
    [SerializeField] private RectTransform _aboutPanel;

    public void Awake()
    {
        _currenPanel = _mainMenuPanel;
    }

    private void SetNewCurrentPanel(RectTransform panel)
    {
        _previousPanel = _currenPanel;
        _currenPanel = panel;

        _previousPanel.gameObject.SetActive(false);
        _currenPanel.gameObject.SetActive(true);
    }
    public void BeginButtonClick()
    {
        SceneManager.LoadScene(1);
        //SetNewCurrentPanel(_beginPanel);
    }

    public void AboutButtonClick()
    {
        SetNewCurrentPanel(_aboutPanel);
    }

    public void ExitButtonClick()
    {
        Application.Quit(0);
    }

    public void BackButtonClick()
    {
        SetNewCurrentPanel(_previousPanel);
    }
}
