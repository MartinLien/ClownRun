using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private BlinkOnPress _start = null;
    [SerializeField] private BlinkOnPress _quit = null;

    private void Start()
    {
        _start.OnPressed += StartGame;
        _quit.OnPressed += Exit;
    }

    public void StartGame()
    {
        StartCoroutine(StartGameAnim());
    }

    private IEnumerator StartGameAnim()
    {
        FindObjectOfType<Curtains>().Enter();
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
