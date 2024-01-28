using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatyMovement : MonoBehaviour
{
    [SerializeField] private float _floatSpeed = 1f;
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private float _heightMultiplier = 1f;
    [SerializeField] private Transform _visual = null;

    private Vector3 _originPosition = Vector3.zero;

    private void Start()
    {
        _originPosition = _visual.localPosition;
    }

    private void Update()
    {
        _visual.localPosition = _originPosition + Vector3.up * Mathf.Sin(Time.time * _floatSpeed) * _heightMultiplier;
        _visual.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
    }
}
