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
    private PlayerInitializationOnLevel _playerInit;

    [Inject]
    private void Construct(PlayerInitializationOnLevel playerInit)
    {
        _playerInit = playerInit;
        _playerInit.OnPlayerUpdate += Init;
    }

    private void Init(Player player)
    {
        _playerHealth = player.GetComponent<Health>();

        _playerHealth.OnDie += OnPlayerDie;
    }

    private void OnDestroy()
    {
        _playerInit.OnPlayerUpdate -= Init;
    }

    private void Start()
    {
        _panel.SetActive(false);
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
