using UnityEngine;
using UnityEngine.SceneManagement;

public class S_MainMenuManager_IS : MonoBehaviour
{
    [SerializeField] private string gameScene;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(gameScene);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
