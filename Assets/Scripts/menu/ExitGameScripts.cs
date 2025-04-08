using UnityEngine;

public class ExitGameScripts : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void ExitToDesktop()
    {
#if UNITY_EDITOR
UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
