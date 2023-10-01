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

    private float maxHealth;

    [Inject]
    private void Construct(Player player)
    {
        _playerHealth = player.GetComponent<Health>();
    }

    void OnEnable()
    {
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