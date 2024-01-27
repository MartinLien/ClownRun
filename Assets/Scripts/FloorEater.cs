using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorEater : MonoBehaviour
{
    [SerializeField] private AnimationCurve _floorChompMoveCurve = null;
    private GroundScrolling _groundScrolling = null;
    [SerializeField] private KillZoneController _killZoneController = null;

    // TODO: This will be a value grabbed from the position helper class that will be used in junction with obstacles to move the mouth closer to the player as they fail to jump over obstacles.
    private Vector3 _originPosition = Vector3.zero;
    private float _timer = 0;

    void Start()
    {
        _groundScrolling = FindObjectOfType<GroundScrolling>();
    }

    void Update()
    {
        if (_timer >= _groundScrolling.SpawnDelay)
            _timer = 0;
        transform.position = _killZoneController.CurrentPosition - Vector3.forward * _floorChompMoveCurve.Evaluate(_timer);
        _timer += Time.deltaTime;
    }
}
