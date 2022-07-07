using UnityEngine;

public class CameraShaker : SingletonGeneric<CameraShaker>
{
    [SerializeField]
    float _shakeTime;
    [SerializeField]
    float _shakePower;
    float _shakeFadeTime;
    float _shakeRotation;
    [SerializeField]
    float _rotationMultiplier;

    public void CameraShake(float duration, float magnitude)
    {
        _shakeTime = duration;
        _shakePower = magnitude;
        _shakeFadeTime = magnitude / duration;
        _shakeRotation = magnitude * _rotationMultiplier;
    }
   
    void LateUpdate()
    {
        if(_shakeTime > 0)
        {
            _shakeTime -= Time.deltaTime;

            float x = Random.Range(-1, 1) * _shakePower;
            float y = Random.Range(-1, 1) * _shakePower;

            transform.position = new Vector3(x, y, transform.position.z);
            _shakePower = Mathf.MoveTowards(_shakePower, 0, _shakeFadeTime * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0, 0, Random.Range(-1, 1) * _shakeRotation);
            _shakeRotation = Mathf.MoveTowards(_shakeRotation, 0, (_shakeFadeTime * _rotationMultiplier) * Time.deltaTime);
        }
    }
}
