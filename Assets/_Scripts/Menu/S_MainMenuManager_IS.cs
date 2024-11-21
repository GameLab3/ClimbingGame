using Eflatun.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_MainMenuManager_IS : MonoBehaviour
{
    [SerializeField] private SceneReference gameScene;
    
    public void PlayGame()
    {
        SceneManager.LoadScene(gameScene.BuildIndex);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
