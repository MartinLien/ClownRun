using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Transform _visual = null;
    [SerializeField] private AnimationCurve _jumpArc = null;
    [SerializeField] private FloatVariable _jumpDuration = null;
    [SerializeField] private FloatVariable _landingParticleTiming = null;

    [Header("Particles")]
    [SerializeField] private ParticleSystem _jumpingParticle = null;
    [SerializeField] private ParticleSystem _landingParticle = null;

    [Header("Animations")]
    [SerializeField] private AnimationCurve _runningCurve = null;
    [SerializeField] private FloatVariable _runCycleTimer = null;

    private bool _running = true;
    private bool _jumping = false;

    private float _timer = 0;

    private Vector3 _originPosition = Vector3.zero;

    private void Start()
    {
        _originPosition = _visual.position;
    }

    private void Update()
    {
        if (_running)
            RunningBehaviour();

        if (Input.GetKeyDown(KeyCode.Space) && !_jumping)
        {
            _timer = 0;
            _running = false;
            _jumping = true;
            StartCoroutine(Jump());
        }
    }

    private void RunningBehaviour()
    {
        _visual.position = _originPosition + Vector3.up * _runningCurve.Evaluate(_timer / _runCycleTimer.Variable);

        _timer += Time.deltaTime;
        if (_timer >= _runCycleTimer.Variable)
            _timer = 0;
    }

    private IEnumerator Jump()
    {
        _jumpingParticle.Play();

        bool landingPlayed = false;
        float timer = 0;
        while (timer < _jumpDuration.Variable)
        {
            _visual.position = _originPosition + (Vector3.up * _jumpArc.Evaluate(timer / _jumpDuration.Variable));

            if (timer >= Mathf.Lerp(0, _jumpDuration.Variable, _landingParticleTiming.Variable) && !landingPlayed)
            {
                landingPlayed = true;
                _landingParticle.Play();
            }

            timer += Time.deltaTime;
            yield return null;
        }

        _running = true;
        _jumping = false;
    }
}
