using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField]
    public GameObject _laserPrefab;
    [SerializeField]
    public GameObject _tripleShotPrefab;
    [SerializeField]
    public GameObject _shieldsPrefab;
    [SerializeField]
    public GameObject _rightWing;
    [SerializeField]
    public GameObject _leftWing;

    public PlayerController _playerController;

    private void Update()
    {
        PlayerMovement();
        _playerController.CheckFireButtonPressed();
        _playerController.SetPlayerBounds();
    }
    public void SetTankControllerReference(PlayerController controller)
    {
        _playerController = controller;
    }

    public void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _playerController._playerModel._speed * Time.deltaTime);
    }

    public void SpeedBoostTimer()
    {
        StartCoroutine(SpeedBoostCooldown());
    }

    IEnumerator SpeedBoostCooldown()
    {
        yield return new WaitForSeconds(5.0f);
        _playerController._playerModel._speed /= _playerController._playerModel._speedBoostMultiplier;
    }

    public void TripleShotTimer()
    {
        StartCoroutine(TripleShotCoolDown());
    }

    IEnumerator TripleShotCoolDown()
    {
        yield return new WaitForSeconds(5.0f);
        _playerController._isTripleShotActive = false;
    }

    public void DestroyShip()
    {
        Destroy(this.gameObject);
    }

    public void SpawnTripleBullet()
    {
        Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
    }

    public void SpawnBullet(GameObject Laser)
    {
        Laser.transform.position = this.transform.position;
        Laser.transform.rotation = this.transform.rotation;
        Laser.SetActive(true);
    }
}
