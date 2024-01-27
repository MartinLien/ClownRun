using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScrolling : MonoBehaviour
{
    [SerializeField] private FloatVariable _travelBeforeNextSpawn = null;
    [SerializeField] private FloatVariable _tileSpeed = null;
    [SerializeField] private Transform _spawnPoint = null;
    [SerializeField] private GroundTile _groundPrefab = null;


    private List<GroundTile> _spawnedObjects = new List<GroundTile>();

    private float _spawnTimer = 0;

    public bool ShouldSpawn = true;

    public float SpawnDelay => _travelBeforeNextSpawn.Variable / _tileSpeed.Variable;

    private void Update()
    {
        if (!ShouldSpawn)
            return;
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
        newTile.Speed = _tileSpeed.Variable;
        _spawnedObjects.Add(newTile);
    }
}
