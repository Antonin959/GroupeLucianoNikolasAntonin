using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuscript : MonoBehaviour
{
    public void StartButton(int sceneindex)
    {
        SceneManager.LoadScene(sceneindex);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
