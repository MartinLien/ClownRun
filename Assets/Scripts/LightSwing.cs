using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwing : MonoBehaviour
{
    [SerializeField] private float _swingSpeed = 1;
    [SerializeField] private float _swingTimeOffset = 0;

    private float _swingTime = 0;

    private float _radius = 0;
    private Vector3 _center = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        _radius = transform.localPosition.x;
        _swingTime = _swingTimeOffset;
        _center = transform.position - new Vector3(_radius, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Cos(_swingTime) * _radius;
        float z = Mathf.Sin(_swingTime) * _radius;

        transform.position = _center + new Vector3(x, 0, z);

        _swingTime += Time.deltaTime * _swingSpeed;
    }
}
