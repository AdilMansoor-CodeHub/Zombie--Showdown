using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickPlayerExample : MonoBehaviour
{
    public GameObject Player;
    public float speed;
    public VariableJoystick variableJoystick;
    public Rigidbody rb;

    // Shooting
    public bool isPullingBack = false;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed;

    public GameObject Water;
    public GameObject Fire;
    public GameObject Wood;

    public Button SwitchButton;

    public Sprite WaterImage;
    public Sprite FireImage;
    public Sprite WoodImage;

    private enum BulletType { Water, Fire, Wood }
    private BulletType currentBulletType = BulletType.Water;

    private void Start()
    {
        isPullingBack = false;
        currentBulletType = BulletType.Water;
        SwitchButton.GetComponent<Image>().sprite = WaterImage;
    }

    private void FixedUpdate()
    {
        // Slingshot rotation
        Vector3 rotation = Vector3.up * variableJoystick.Horizontal;
        Player.transform.Rotate(rotation);

        // Shoot slingshot
        if (variableJoystick.Vertical < -0.5f)
        {
            if (!isPullingBack)
            {
                isPullingBack = true;
                Debug.Log("Pulling back");
            }
        }
        else if (isPullingBack && variableJoystick.Vertical >= -0.1f)
        {
            isPullingBack = false;
            Debug.Log("Shoot");
            ShootBullet();
        }
    }

    public void SwitchBulletType()
    {
        Debug.Log("Switched");
        switch (currentBulletType)
        {
            case BulletType.Water:
                currentBulletType = BulletType.Fire;
                SwitchButton.GetComponent<Image>().sprite = FireImage;
                break;
            case BulletType.Fire:
                currentBulletType = BulletType.Wood;
                SwitchButton.GetComponent<Image>().sprite = WoodImage;
                break;
            case BulletType.Wood:
                currentBulletType = BulletType.Water;
                SwitchButton.GetComponent<Image>().sprite = WaterImage;
                break;
        }
    }

    private void ShootBullet()
    {
        GameObject spawnedBullet = null;

        switch (currentBulletType)
        {
            case BulletType.Water:
                spawnedBullet = Instantiate(Water, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                break;
            case BulletType.Fire:
                spawnedBullet = Instantiate(Fire, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                break;
            case BulletType.Wood:
                spawnedBullet = Instantiate(Wood, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                break;
        }

        if (spawnedBullet != null)
        {
            Rigidbody rbBullet = spawnedBullet.GetComponent<Rigidbody>();
            if (rbBullet != null)
            {
                rbBullet.AddForce(bulletSpawnPoint.forward * bulletSpeed, ForceMode.Impulse);
            }
        }
    }
}
