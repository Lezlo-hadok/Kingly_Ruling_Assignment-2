using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class ExitGameScripts : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    

    public void ExitToDesktop()
    {
        Application.Quit();
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        
    }
}
