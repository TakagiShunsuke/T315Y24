using UnityEngine;
using UnityEngine.UI;
public class HPBar : MonoBehaviour
{
    [SerializeField] private Image _hpBarcurrent;
    [SerializeField] private float _maxHealth;
    private float currentHealth;
    void Awake()
    {
        currentHealth = _maxHealth;
    }
    public void UpdateHP(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, _maxHealth);
        _hpBarcurrent.fillAmount = currentHealth / _maxHealth;
    }
}