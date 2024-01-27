using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip _normal;
    [SerializeField] private AudioClip _fast;
    [SerializeField] private AudioClip _faster;

    private GameplayAudioState _state = GameplayAudioState.Normal;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _normal;
        _audioSource.Play();
    }

    public void SetState(GameplayAudioState state)
    {
        if (_state == state)
            return;
        _state = state;
        float timePercent = _audioSource.time / _audioSource.clip.length;
        switch (_state)
        {
            case GameplayAudioState.Normal:
                _audioSource.clip = _normal;
                break;
            case GameplayAudioState.Fast:
                _audioSource.clip = _fast;
                break;
            case GameplayAudioState.Faster:
                _audioSource.clip = _faster;
                break;
        }
        _audioSource.time = timePercent * _audioSource.clip.length;
        _audioSource.Play();
    }
}

public enum GameplayAudioState
{
    Normal,
    Fast,
    Faster
}
