using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private FloatVariable _shakeDuration = null;
    [SerializeField] private FloatVariable _intensityMultiplier = null;
    [SerializeField] private AnimationCurve _intensityCurve = null;

    private Coroutine _coroutine = null;
    private Vector3 _originPosition = Vector3.zero;

    private void Start()
    {
        _originPosition = transform.position;

        FindObjectOfType<ObstacleSpawner>().OnObstacleTriggered += StartShake;
    }
    
    public void StartShake()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float timer = 0;
        while (timer < _shakeDuration.Variable)
        {
            float t = timer / _shakeDuration.Variable;

            transform.position = _originPosition + Vector3.up * _intensityCurve.Evaluate(t) * _intensityMultiplier.Variable;

            timer += Time.deltaTime;
            yield return null;
        }
    }
}
