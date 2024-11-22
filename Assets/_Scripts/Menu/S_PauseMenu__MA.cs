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
            pauseMenuIsActive = !pauseMenuIsActive;
            pauseMenu.SetActive(pauseMenuIsActive);
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
