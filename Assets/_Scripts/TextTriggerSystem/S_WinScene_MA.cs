using UnityEngine;
using UnityEngine.SceneManagement;

public class S_WinScene_MA : MonoBehaviour
{
    [SerializeField] string SceneReference;
    public void InstantiateWin()
    {
        SceneManager.LoadScene("GoodEndScene");
    }
}
