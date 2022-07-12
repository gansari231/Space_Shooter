using UnityEngine;
using UnityEngine.UI;

public class BossHealthSlider : SingletonGeneric<BossHealthSlider>
{
    int _maxhealth = 40;
    [HideInInspector]
    public int _currentHealth;
    [SerializeField]
    public Slider _bossHealthSlider;
    [SerializeField]
    public Text _sliderText;
    [SerializeField]
    Image _healthFill;

    void Start()
    {
        _currentHealth = _maxhealth;
        _bossHealthSlider.maxValue = _maxhealth;
        _bossHealthSlider.value = _currentHealth;     
    }

    public void DecreaseSliderValue(int damage)
    {
        if (_currentHealth - damage >= 0)
        {
            _currentHealth -= damage;
            _bossHealthSlider.value = _currentHealth;
            Debug.Log("health : " + _currentHealth);
        }
    }
}
