using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillScreen : MonoBehaviour
{
    [SerializeField] private AudioSource _sound = null;
    [SerializeField] private AudioSource _music = null;
    [SerializeField] private Transform _uiToAnimate = null;
    [SerializeField] private float _animDuration = 1f;
    [SerializeField] private CanvasGroup _background = null;
    [SerializeField] private AnimationCurve _animCurve = null;

    [Header("Buttons")]
    [SerializeField] private GameObject _tryAgain = null;
    [SerializeField] private GameObject _quit = null;

    private Vector3 _originScale = Vector3.zero;

    private void Start()
    {
        _originScale = _uiToAnimate.localScale;
        _uiToAnimate.localScale = Vector3.zero;
        _background.alpha = 0;

        _tryAgain.SetActive(false);
        _quit.SetActive(false);
    }

    public void PlayKillScreen()
    {
        StartCoroutine(KillScreenAnim());
    }

    private IEnumerator KillScreenAnim()
    {
        // Wait one second before starting anim
        yield return new WaitForSeconds(1);

        float timer = 0;
        while (timer < 1)
        {
            _background.alpha = timer / 1f;

            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1);

        _music.volume = 0.3f;
        _sound.Play();
        timer = 0;
        while (timer < _animDuration)
        {
            _uiToAnimate.localScale = _originScale * _animCurve.Evaluate(timer / _animDuration);

            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1);
        _tryAgain.SetActive(true);
        _quit.SetActive(true);
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        SceneManager.LoadScene(0);
    }
}
