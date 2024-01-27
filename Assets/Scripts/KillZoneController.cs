using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZoneController : MonoBehaviour
{
    private Coroutine _resetMovingRoutine = null;

    private Vector3 _originPosition = Vector3.zero;
    private Vector3 _currentPosition = Vector3.zero;

    public Vector3 CurrentPosition { get; private set; }
    [SerializeField] private Vector3 _moveLength = new(0, 0, 5);

    private bool _laughterMoving = false;
    private bool _resetMoving = false;

    private float _lastMove = 0;

    [SerializeField] private float _moveDelay = 5f;
    [SerializeField] private float _moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _originPosition = transform.position;
        CurrentPosition = _originPosition;
        _currentPosition = CurrentPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !_laughterMoving)
        {
            if(_resetMovingRoutine != null)
                StopCoroutine(_resetMovingRoutine);
            StartCoroutine(LaughterMove(_currentPosition + _moveLength, _moveSpeed));
        }

        if (_lastMove >= _moveDelay)
        {
            _resetMovingRoutine = StartCoroutine(ResetStep(_currentPosition - _moveLength, _moveSpeed));
        }

        if (!_laughterMoving && !_resetMoving && _originPosition.z < _currentPosition.z)
        {
            _lastMove += Time.deltaTime;
        }
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
        _currentPosition = targetPosition;
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
        _currentPosition = targetPosition;
        _resetMoving = false;
    }

    void LinearMove(Vector3 targetPosition, float duration, ref float timer)
    {
        CurrentPosition = Vector3.Lerp(_currentPosition, targetPosition, timer / duration);
        timer += Time.deltaTime;
    }
}
