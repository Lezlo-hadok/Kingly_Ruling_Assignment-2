using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class AudioScripts : MonoBehaviour
{
   public AudioMixer audioMixer;
    string _mixerChannelToEdit;

    public void GetCurrentMixer(string name)
    {
        _mixerChannelToEdit = name;
    }
    public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat(_mixerChannelToEdit, volume);
    }
}
