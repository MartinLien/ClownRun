using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class KillZoneController : MonoBehaviour
{
    private Coroutine _resetMovingRoutine = null;

    private GameplayAudioController _gameplayAudioController = null;
    private AnimationSpeedController _animationSpeedController = null;

    private ObstacleSpawner _obstacleSpawner = null;
    private GroundScrolling _groundScrolling = null;

    private Vector3 _originPosition = Vector3.zero;
    private Vector3 _currentOriginPosition = Vector3.zero;

    private Vector3 CurrentOriginPosition
    {
        get => _currentOriginPosition;
        set
        {
            _currentOriginPosition = value;
            float distance = 35 + value.z;
            if (distance >= 35)
            {
                _obstacleSpawner.ShouldSpawn = false;
                _groundScrolling.ShouldSpawn = false;
                ShouldEat = false;
                //Utils.ForceCrash(ForcedCrashCategory.FatalError);
            }
            if (distance > _fasterSongDistance)
            {
                _gameplayAudioController.SetState(GameplayAudioState.Faster);
                _animationSpeedController.SetState(GameplayAudioState.Faster);
            }
            else if (distance > _fastSongDistance)
            {
                _gameplayAudioController.SetState(GameplayAudioState.Fast);
                _animationSpeedController.SetState(GameplayAudioState.Fast);
            }
            else
            {
                _gameplayAudioController.SetState(GameplayAudioState.Normal);
                _animationSpeedController.SetState(GameplayAudioState.Normal);
            }
        }
    }

    public Vector3 CurrentPosition { get; private set; }
    public bool ShouldEat = true;

    [SerializeField] private Vector3 _moveLength = new(0, 0, 5);

    private bool _laughterMoving = false;
    private bool _resetMoving = false;

    private float _lastMove = 0;

    [SerializeField] private float _moveDelay = 5f;
    [SerializeField] private float _moveSpeed = 1f;

    [SerializeField] private float _fastSongDistance = 10f;
    [SerializeField] private float _fasterSongDistance = 20f;

    void Awake()
    {
        _gameplayAudioController = FindObjectOfType<GameplayAudioController>();
        _animationSpeedController = FindObjectOfType<AnimationSpeedController>();
        _obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
        _groundScrolling = FindObjectOfType<GroundScrolling>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _originPosition = transform.position;
        CurrentPosition = _originPosition;
        CurrentOriginPosition = CurrentPosition;
        _obstacleSpawner.OnObstacleTriggered += TriggerMove;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ShouldEat)
            return;
        if (Input.GetKeyDown(KeyCode.J) && !_laughterMoving)
        {
            TriggerMove();
        }

        if (_lastMove >= _moveDelay)
        {
            _resetMovingRoutine = StartCoroutine(ResetStep(CurrentOriginPosition - _moveLength, _moveSpeed));
        }

        if (!_laughterMoving && !_resetMoving && _originPosition.z < CurrentOriginPosition.z)
        {
            _lastMove += Time.deltaTime;
        }
    }

    void TriggerMove()
    {
        if (_resetMovingRoutine != null)
            StopCoroutine(_resetMovingRoutine);
        StartCoroutine(LaughterMove(CurrentOriginPosition + _moveLength, _moveSpeed));
    }

    IEnumerator LaughterMove(Vector3 targetPosition, float duration)
    {
        _laughterMoving = true;
        _lastMove = 0;
        float timer = 0;
        while (timer < duration)
        {
            LinearMove(targetPosition, duration, ref timer);
            yield return null;
        }
        CurrentOriginPosition = targetPosition;
        _laughterMoving = false;
    }

    IEnumerator ResetStep(Vector3 targetPosition, float duration)
    {
        _resetMoving = true;
        _lastMove = 0;
        float timer = 0;
        while (timer < duration)
        {
            LinearMove(targetPosition, duration, ref timer);
            yield return null;
        }
        CurrentOriginPosition = targetPosition;
        _resetMoving = false;
    }

    void LinearMove(Vector3 targetPosition, float duration, ref float timer)
    {
        CurrentPosition = Vector3.Lerp(CurrentOriginPosition, targetPosition, timer / duration);
        timer += Time.deltaTime;
    }
}
