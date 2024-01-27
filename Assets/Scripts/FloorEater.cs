using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorEater : MonoBehaviour
{
    [SerializeField] private AnimationCurve _floorChompMoveCurve = null;
    private GroundScrolling _groundScrolling = null;
    [SerializeField] private KillZoneController _killZoneController = null;

    private float _timer = 0;

    void Start()
    {
        _groundScrolling = FindObjectOfType<GroundScrolling>();
    }

    void Update()
    {
        if (!_killZoneController.ShouldEat && transform.position == _killZoneController.CurrentPosition)
            return;
        if (_timer >= _groundScrolling.SpawnDelay)
            _timer = 0;
        transform.position = _killZoneController.CurrentPosition - Vector3.forward * _floorChompMoveCurve.Evaluate(_timer);
        _timer += Time.deltaTime;
    }
}
