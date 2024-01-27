using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    private float _speed = 1f;
    public float Speed { set { _speed = value; } }

    private GroundScrolling _groundScrolling;
    private ObstacleSpawner _obstacleSpawner;

    void Start()
    {
        _groundScrolling = FindObjectOfType<GroundScrolling>();
        _obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
    }

    void Update()
    {
        if (_groundScrolling is { ShouldSpawn: false } && _obstacleSpawner is { ShouldSpawn: false })
            return;
        transform.position -= Vector3.forward * _speed * Time.deltaTime;
    }
}
