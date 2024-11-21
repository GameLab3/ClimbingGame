using UnityEngine;

public class S_PauseMenu__MA : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;


    void ContinuePlaying()
    {
        pauseMenu.SetActive(false);
    }


}
