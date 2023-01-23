using UnityEngine;
using UnityEngine.Audio;


public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;


    public void SetVolume(float volume)
    {
        mixer.SetFloat("MasterVolume", volume);
    }
}
