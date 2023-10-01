using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField]
    private Health _health;

    [SerializeField]
    private Image _fillBar;

    private float _maxHealth;

    private void OnEnable()
    {
        _health.OnUpdateHealth += UpdateBar;
        _maxHealth = _health.GetMaxHealth();
        UpdateBar(_maxHealth);
    }

    private void UpdateBar(float hp)
    {
        _fillBar.fillAmount = hp / _maxHealth;
    }
}
