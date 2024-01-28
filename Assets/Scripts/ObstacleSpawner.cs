using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Obstacle _obstaclePrefab = null;
    [SerializeField] private Obstacle _pickupPrefab = null;
    [SerializeField] private FloatVariable _spawnInterval = null;
    [SerializeField] private FloatVariable _obstacleSpeed = null;
    [SerializeField] private List<Transform> _spawnPositions = new List<Transform>();

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
        Obstacle newObstacle = Instantiate(_obstaclePrefab, GetRandomSpawnPoint(out int index), Quaternion.identity);
        newObstacle.Speed = _obstacleSpeed.Variable;
        newObstacle.OnTrigger += ObstacleTriggered;

        Obstacle newPickup = Instantiate(_pickupPrefab, GetRandomSpawnPointExcludingIndex(index), Quaternion.identity);
        newPickup.Speed = _obstacleSpeed.Variable;
    }

    private void ObstacleTriggered()
    {
        OnObstacleTriggered?.Invoke();
    }

    private Vector3 GetRandomSpawnPoint(out int index)
    {
        index = Random.Range(0, 3);
        return new Vector3(_spawnPositions[index].position.x, 0.5f, transform.position.z);
    }

    private Vector3 GetRandomSpawnPointExcludingIndex(int index)
    {
        Vector3 position = transform.position;
        int lane = index;
        while (lane == index)
            lane = Random.Range(0, 3);

        return new Vector3(_spawnPositions[lane].position.x, 0.5f, transform.position.z);
    }
}
