using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static float MusicVolume;
    public Slider MusicSlider;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = MusicVolume;
        MusicSlider.value = MusicVolume;
        MusicSlider.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float value)
    {
        MusicVolume = value;
        _audioSource.volume = MusicVolume;
        MusicSlider.value = MusicVolume;
    }
}
