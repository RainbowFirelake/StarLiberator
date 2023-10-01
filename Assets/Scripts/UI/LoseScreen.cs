using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class LoseScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject _panel = null;

    private Health _playerHealth = null;

    [Inject]
    private void Construct(Player player)
    {
        _playerHealth = player.GetComponent<Health>();
    }

    private void Start()
    {
        _panel.SetActive(false);
    }

    private void OnEnable()
    {
        _playerHealth.OnDie += OnPlayerDie;
    }

    private void OnDisable()
    {
        _playerHealth.OnDie -= OnPlayerDie;
    }

    private void OnPlayerDie()
    {
        _panel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
