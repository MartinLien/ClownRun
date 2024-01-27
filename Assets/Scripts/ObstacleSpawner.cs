using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Obstacle _obstaclePrefa = null;
    [SerializeField] private FloatVariable _spawnInterval = null;
    [SerializeField] private FloatVariable _obstacleSpeed = null;

    private float _spawnTimer = 0;
}
