using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField] private GameObject levelSelect;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject loadingScreen;

    void Start()
    {
        loadingScreen.SetActive(false);
    }

    public void PressPlay()
    {
        levelSelect.SetActive(true);
        playButton.SetActive(false);
        quitButton.SetActive(false);
    }

    public void GoToLevel1()
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene("Level1");
    }
    
    public void BackButton()
    {
        levelSelect.SetActive(false);
        playButton.SetActive(true);
        quitButton.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
