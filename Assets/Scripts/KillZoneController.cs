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

    [SerializeField] private AudioSource _laughSound = null;

    [SerializeField] private Animator _anim = null;

    [Header("Particles")]
    [SerializeField] private List<ParticleSystem> _particles = new List<ParticleSystem>();

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
        _currentOriginPosition = CurrentPosition;
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
            _resetMovingRoutine = StartCoroutine(ResetStep(_currentOriginPosition - _moveLength, _moveSpeed));
        }

        if (!_laughterMoving && !_resetMoving && _originPosition.z < _currentOriginPosition.z)
        {
            _lastMove += Time.deltaTime;
        }
    }

    void CheckDistance(Vector3 targetPosition)
    {
        float distance = 35 + targetPosition.z;
        if (distance >= 35)
        {
            StartCoroutine(EndAnimator());
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

    IEnumerator EndAnimator()
    {
        ShouldEat = false;
        float timer = 0;
        while (timer < .3)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        _obstacleSpawner.ShouldSpawn = false;
        _groundScrolling.ShouldSpawn = false;

        FindObjectOfType<CharacterController>().Killed();
        FindObjectOfType<KillScreen>().PlayKillScreen();
    }

    void TriggerMove()
    {
        if (_resetMovingRoutine != null)
            StopCoroutine(_resetMovingRoutine);
        StartCoroutine(LaughterMove(_currentOriginPosition + _moveLength, _moveSpeed));

        _laughSound.Play();
        TriggerParticles();
        _anim.SetTrigger("Laugh");
    }

    private void TriggerParticles()
    {
        foreach (var particle in _particles)
            particle.Play();
    }

    IEnumerator LaughterMove(Vector3 targetPosition, float duration)
    {
        _laughterMoving = true;
        _lastMove = 0;
        float timer = 0;
        CheckDistance(targetPosition);
        while (timer < duration)
        {
            LinearMove(targetPosition, duration, ref timer);
            yield return null;
        }
        _currentOriginPosition = targetPosition;
        _laughterMoving = false;
    }

    IEnumerator ResetStep(Vector3 targetPosition, float duration)
    {
        _resetMoving = true;
        _lastMove = 0;
        float timer = 0;
        CheckDistance(targetPosition);
        while (timer < duration)
        {
            LinearMove(targetPosition, duration, ref timer);
            yield return null;
        }
        _currentOriginPosition = targetPosition;
        _resetMoving = false;
    }

    void LinearMove(Vector3 targetPosition, float duration, ref float timer)
    {
        CurrentPosition = Vector3.Lerp(_currentOriginPosition, targetPosition, timer / duration);
        timer += Time.deltaTime;
    }
}
