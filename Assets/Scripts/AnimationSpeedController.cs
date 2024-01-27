using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeedController : MonoBehaviour
{
    [SerializeField] private Animator _anim = null;
    [SerializeField] private FloatVariable _speed = null;

    private float _prevSpeed = 0f;

    private void Update()
    {
        if (_speed.Variable != _prevSpeed)
        {
            _prevSpeed = _speed.Variable;
            UpdateAnimSpeed();
        }
    }

    private void UpdateAnimSpeed()
    {
        _anim.speed = _speed.Variable;
    }
}
