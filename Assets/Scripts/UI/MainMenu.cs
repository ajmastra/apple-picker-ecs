using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class MainMenu : MonoBehaviour
{
    public void LoadEasyLevel()
    {
        // SceneLoader.Instance.LoadScene(SceneType.Easy);
        SceneManager.LoadScene("Easy");
        SceneManager.LoadScene("SharedData", LoadSceneMode.Additive);
    }

    public void LoadMediumLevel()
    {
        SceneManager.LoadScene("Medium");
        SceneManager.LoadScene("SharedData", LoadSceneMode.Additive);
    }

    public void LoadHardLevel()
    {
        SceneManager.LoadScene("Hard");
        SceneManager.LoadScene("SharedData", LoadSceneMode.Additive);
    }
}
