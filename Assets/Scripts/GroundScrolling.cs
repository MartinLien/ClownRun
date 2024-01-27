using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScrolling : MonoBehaviour
{
    [SerializeField] private float _travelBeforeNextSpawn = 5f;
    [SerializeField] private float _tileSpeed = 10f;
    [SerializeField] private Transform _spawnPoint = null;
    [SerializeField] private GroundTile _groundPrefab = null;


    private List<GroundTile> _spawnedObjects = new List<GroundTile>();

    private float _spawnTimer = 0;

    public float SpawnDelay => _travelBeforeNextSpawn / _tileSpeed;

    private void Update()
    {
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= SpawnDelay)
        {
            _spawnTimer = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        GroundTile newTile = Instantiate(_groundPrefab, _spawnPoint);
        newTile.Speed = _tileSpeed;
        _spawnedObjects.Add(newTile);
    }
}
