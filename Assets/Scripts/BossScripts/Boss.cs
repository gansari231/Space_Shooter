using UnityEngine;

public class Boss : SingletonGeneric<Boss>
{
    BossGun[] _guns;
    float _canFire = -1;
    float _fireRate = 2.5f;
    float _speed = 3f;
    int _amplitude = 1; 

    [SerializeField]
    GameObject _explosionPrefab;

    void Start()
    {
        _guns = transform.GetComponentsInChildren<BossGun>();
        BossHealthSlider.Instance._bossHealthSlider.gameObject.SetActive(true);
        BossHealthSlider.Instance._sliderText.gameObject.SetActive(true);
    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y <= 4.0f)
        {
            float x = (Mathf.Cos(Time.time) * _amplitude);
            float y = 4.0f;
            float z = transform.position.z;
            transform.position = new Vector3(x, y, z);
        }

        if(Time.time > _canFire)
        {
            ActivateBossFireBall();
        }
    }

    void ActivateBossFireBall()
    {
        _canFire = Time.time + _fireRate;
        foreach(var gun in _guns)
        {
            gun.Shoot();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Missile"))
        {
            Damage(5);
        }
        if(collision.CompareTag("Laser"))
        {
            Damage(2);
        }
    }

    void Damage(int damage)
    {
        BossHealthSlider.Instance.DecreaseSliderValue(damage);

        if(BossHealthSlider.Instance._currentHealth <= 0)
        {
            Debug.Log("Health empty");
            BossHealthSlider.Instance._currentHealth = 0;
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            UIManager.Instance.GameCompleted();
        }
    }

    
}
