using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSceneScripts : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ChangeSceneByName(string sceneName)
    {
      SceneManager.LoadScene(sceneName);   
    }

    // Update is called once per frame
    public void ChangeSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

}
