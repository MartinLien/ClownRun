using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public enum Lane { Left, Center, Right }
    [SerializeField] private Lane _currentLane = Lane.Center;
    [SerializeField] private List<Transform> _lanePositions = new List<Transform>();

    [SerializeField] private Transform _visual = null;
    [SerializeField] private AnimationCurve _jumpArc = null;
    [SerializeField] private FloatVariable _jumpDuration = null;
    [SerializeField] private FloatVariable _landingParticleTiming = null;

    [Header("Stun")]
    [SerializeField] private FloatVariable _stunnedDuration = null;

    [Header("Particles")]
    [SerializeField] private ParticleSystem _jumpingParticle = null;
    [SerializeField] private ParticleSystem _landingParticle = null;

    [Header("Animations")]
    [SerializeField] private AnimationCurve _runningCurve = null;
    [SerializeField] private FloatVariable _runCycleTimer = null;

    private Lane _prevLane = Lane.Center;
    private bool _running = true;
    private bool _jumping = false;

    private bool _stunned = false;

    private float _timer = 0;
    private float _stunTimer = 0;

    private Vector3 _originPosition = Vector3.zero;

    private void Start()
    {
        _originPosition = _visual.localPosition;

        FindObjectOfType<ObstacleSpawner>().OnObstacleTriggered += Stunned;
    }

    private void Update()
    {
        if (_running)
            RunningBehaviour();

        if (_stunned)
        {
            StunnedBehaviour();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_jumping)
        {
            _timer = 0;
            _running = false;
            _jumping = true;
            StartCoroutine(Jump());
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _currentLane = (Lane)Mathf.Clamp((int)_currentLane - 1, 0, 2);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _currentLane = (Lane)Mathf.Clamp((int)_currentLane + 1, 0, 2);
        }

        if (_currentLane != _prevLane)
        {
            ChangeLane(_currentLane);
            _prevLane = _currentLane;
        }
    }

    private void Stunned()
    {
        _stunTimer = 0;
        _stunned = true;
    }

    private void StunnedBehaviour()
    {
        _stunTimer += Time.deltaTime;
        if (_stunTimer >= _stunnedDuration.Variable)
            _stunned = false;
    }

    private void RunningBehaviour()
    {
        _visual.localPosition = _originPosition + Vector3.up * _runningCurve.Evaluate(_timer / _runCycleTimer.Variable);

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
            _visual.localPosition = _originPosition + (Vector3.up * _jumpArc.Evaluate(timer / _jumpDuration.Variable));

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

    private void ChangeLane(Lane newLane)
    {
        transform.position = _lanePositions[(int)newLane].position;

        //switch (newLane)
        //{
        //    case Lane.Left:

        //        break;
        //    case Lane.Center:

        //        break;
        //    case Lane.Right:

        //        break;
        //}
    }
}
