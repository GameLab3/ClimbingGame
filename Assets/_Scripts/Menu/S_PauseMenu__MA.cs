using UnityEngine;
using UnityEngine.SceneManagement;

public class S_PauseMenu__MA : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    bool pauseMenuIsActive;


    public void ContinuePlaying()
    {
        pauseMenu.SetActive(false);
        pauseMenuIsActive = false;
    }

    void OnEscape()
    {
            pauseMenu.SetActive(pauseMenuIsActive);
            pauseMenuIsActive = !pauseMenuIsActive;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
