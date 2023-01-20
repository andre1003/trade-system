using UnityEngine;
using UnityEngine.Audio;


public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;


    public void SetVolume(float sliderValue)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }
}
