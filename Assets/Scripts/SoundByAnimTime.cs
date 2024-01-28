using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundByAnimTime : MonoBehaviour
{
    [SerializeField] private AudioSource _source = null;
    [SerializeField] private AudioClip _clip = null;

    [SerializeField] private float _timing = 0.5f;

    [SerializeField] private FloatVariable _speedDefault = null;
    [SerializeField] private FloatVariable _speedFast = null;
    [SerializeField] private FloatVariable _speedFastest = null;

    private float _speed = 1f;
    private float _timer = 0;

    private void Start()
    {
        _speed = _speedDefault.Variable;
        _timer = _timing;
    }

    public void ChangeSpeed(GameplayAudioState state)
    {
        switch (state)
        {
            case GameplayAudioState.Normal:
                _speed = _speedDefault.Variable;
                break;
            case GameplayAudioState.Fast:
                _speed = _speedFast.Variable;
                break;
            case GameplayAudioState.Faster:
                _speed = _speedFastest.Variable;
                break;
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime * _speed;
        if (_timer >= _timing)
        {
            _source.PlayOneShot(_clip);
            _timer = 0;
        }
    }
}
