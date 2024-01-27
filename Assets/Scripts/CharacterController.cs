using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Transform _visual = null;
    [SerializeField] private AnimationCurve _jumpArc = null;
    [SerializeField] private FloatVariable _jumpDuration = null;
    [SerializeField] private FloatVariable _landingParticleTiming = null;

    [Header("Particles")]
    [SerializeField] private ParticleSystem _jumpingParticle = null;
    [SerializeField] private ParticleSystem _landingParticle = null;

    private bool _jumping = false;

    private Vector3 _originPosition = Vector3.zero;

    private void Start()
    {
        _originPosition = _visual.position;
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
        _jumpingParticle.Play();

        bool landingPlayed = false;
        float timer = 0;
        while (timer < _jumpDuration.Variable)
        {
            _visual.position = _originPosition + (Vector3.up * _jumpArc.Evaluate(timer / _jumpDuration.Variable));

            if (timer >= _landingParticleTiming.Variable && !landingPlayed)
            {
                landingPlayed = true;
                _landingParticle.Play();
            }

            timer += Time.deltaTime;
            yield return null;
        }
        
        _jumping = false;
    }
}
