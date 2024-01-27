using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField] private FloatVariable _lifetime = null;
    [SerializeField] private FloatVariable _fastLifetime = null;
    [SerializeField] private FloatVariable _groundSpeed = null;

    private float _timer = 0;

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

        if (_timer >= _lifetime.Variable)
        {
            if (_timer - _lifetime.Variable >= _fastLifetime.Variable)
                Destroy(this.gameObject);

            transform.position -= Vector3.forward * _groundSpeed.Variable * 5 * Time.deltaTime;
        }

        _timer += Time.deltaTime;
    }
}
