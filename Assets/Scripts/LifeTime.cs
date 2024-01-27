using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    [SerializeField] private float _lifetime = 3f;

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

        if (_timer >= _lifetime)
            Destroy(this.gameObject);

        _timer += Time.deltaTime;
    }
}
