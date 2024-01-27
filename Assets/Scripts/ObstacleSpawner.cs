using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Obstacle _obstaclePrefab = null;
    [SerializeField] private FloatVariable _spawnInterval = null;
    [SerializeField] private FloatVariable _obstacleSpeed = null;

    private float _spawnTimer = 0;

    public bool ShouldSpawn = true;

    public System.Action OnObstacleTriggered;

    private void Update()
    {
        if (!ShouldSpawn)
            return;
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _spawnInterval.Variable)
        {
            _spawnTimer = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        Obstacle newObstacle = Instantiate(_obstaclePrefab, transform);
        newObstacle.Speed = _obstacleSpeed.Variable;
        newObstacle.OnTrigger += ObstacleTriggered;
    }

    private void ObstacleTriggered()
    {
        OnObstacleTriggered?.Invoke();
    }
}
