using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private AnimationCurve _jumpArc = null;
    [SerializeField] private float _jumpDuration = 0.5f;

    private bool _jumping = false;

    private Vector3 _originPosition = Vector3.zero;

    private void Start()
    {
        _originPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_jumping)
        {
            _jumping = true;
            StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        float timer = 0;
        while (timer < _jumpDuration)
        {
            transform.position = _originPosition + (Vector3.up * _jumpArc.Evaluate(timer / _jumpDuration));

            timer += Time.deltaTime;
            yield return null;
        }

        _jumping = false;
    }
}
