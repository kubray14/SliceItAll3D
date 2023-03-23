using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_VOLUME = "SoundVolume";
    public static SoundManager Instance;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip cutSound;
    [SerializeField] private AudioClip handleHitSound;
    [SerializeField] private AudioClip stuckSound;
    private AudioSource audioSource;
    private float volume;


    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_VOLUME, 1f); // Ayarlanmadýysa default olarak 1f.
    }

    private void Start()
    {
        InputManager.Instance.OnClick += InputManager_OnClick;
    }

    private void InputManager_OnClick()
    {
        PlayJumpSound();
    }

    private void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip, volume);
    }

    public void PlayJumpSound()
    {
        PlaySound(jumpSound);
    }

    public void PlayCutSound()
    {
        PlaySound(cutSound);
    }

    public void PlayHandleHitSound()
    {
        PlaySound(handleHitSound);
    }

    public void PlayStuckSound()
    {
        PlaySound(stuckSound);
    }

    public void ChangeSoundVolume()
    {
        volume += 0.1f;
        if (volume > 1)
        {
            volume = 0;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume * 10;
    }
}
