using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image _fillBar;
    //[SerializeField]
    //private Text _text;

    private Health _playerHealth;
    private PlayerInitializationOnLevel _playerInit;

    private float maxHealth;

    [Inject]
    private void Construct(PlayerInitializationOnLevel playerInit)
    {
        _playerInit = playerInit;
        playerInit.OnPlayerUpdate += Init;
    }

    private void OnDestroy()
    {
        _playerInit.OnPlayerUpdate -= Init;
    }

    private void Init(Player player)
    {
        _playerHealth = player.GetComponent<Health>();

        _playerHealth.OnUpdateHealth += UpdateBar;
        maxHealth = _playerHealth.GetMaxHealth();
        UpdateBar(maxHealth);
    }
    void OnDisable()
    {
        _playerHealth.OnUpdateHealth -= UpdateBar;
    }

    private void UpdateBar(float hp)
    {
        _fillBar.fillAmount = hp/maxHealth;
    }
}