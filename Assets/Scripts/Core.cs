using UnityEngine;
using UnityEngine.SceneManagement;

public class Core
{
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public static void Exit()
    {
        Application.Quit();
    }

    public static void LoadCurrentSceneAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
