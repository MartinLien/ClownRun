using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeedController : MonoBehaviour
{
    [Header("Speeds")]
    [SerializeField] private FloatVariable _normal;
    [SerializeField] private FloatVariable _fast;
    [SerializeField] private FloatVariable _faster;

    [Header("Anim")]
    [SerializeField] private Animator _anim = null;

    private GameplayAudioState _state = GameplayAudioState.Normal;

    public void SetState(GameplayAudioState state)
    {
        if (_state == state)
            return;

        _state = state;
        switch (_state)
        {
            case GameplayAudioState.Normal:
                UpdateAnimSpeed(_normal.Variable);
                break;
            case GameplayAudioState.Fast:
                UpdateAnimSpeed(_fast.Variable);
                break;
            case GameplayAudioState.Faster:
                UpdateAnimSpeed(_faster.Variable);
                break;
        }
    }

    private void UpdateAnimSpeed(float speed)
    {
        _anim.SetFloat("speedMultiplier", speed);
    }
}
