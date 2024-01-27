using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkOnPress : MonoBehaviour
{
    [SerializeField] private float _blinkDuration = 2f;
    [SerializeField] private AnimationCurve _curve = null;
    [SerializeField] private CanvasGroup _canvasGroup = null;

    private bool _blinking = false;

    public System.Action OnPressed;

    public void Blink()
    {
        if (_blinking)
            return;

        _blinking = true;
        StartCoroutine(BlinkAnim());
    }

    private IEnumerator BlinkAnim()
    {
        float timer = 0;
        while (timer < _blinkDuration)
        {
            _canvasGroup.alpha = _curve.Evaluate(timer / _blinkDuration);

            timer += Time.deltaTime;
            yield return null;
        }
        OnPressed?.Invoke();
    }
}
