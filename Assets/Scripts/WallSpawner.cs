using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    [SerializeField] private FloatVariable _travelBeforeNextSpawn = null;
    [SerializeField] private FloatVariable _tileSpeed = null;
    [SerializeField] private Transform _spawnPoint = null;
    [SerializeField] private List<GroundTile> _wallPrefabs = new List<GroundTile>();
    [SerializeField] private bool _alternating = true;

    private List<GroundTile> _spawnedObjects = new List<GroundTile>();

    private int _spawnIndex = 0;
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
        GroundTile newTile = Instantiate(GetTile(), _spawnPoint);
        newTile.Speed = _tileSpeed.Variable;
        _spawnedObjects.Add(newTile);
    }

    private GroundTile GetTile()
    {
        if (!_alternating)
        {
            ++_spawnIndex;
            if (_spawnIndex >= _wallPrefabs.Count)
                _spawnIndex = 0;

            return _wallPrefabs[_spawnIndex];
        }
        else
        {
            if (_spawnIndex == 0)
                _spawnIndex = 1;
            else
                _spawnIndex = 0;

            return _wallPrefabs[_spawnIndex];
        }
    }
}
