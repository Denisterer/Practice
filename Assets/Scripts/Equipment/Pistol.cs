using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    [SerializeField]
    private Transform firePoint;
    private bool canShoot = true;
    public WeaponParams weaponParams { get; set; }
    public GameObject bulletPrefab { get; set; }

    public void Shoot(Vector3 targetPosition)
    {
        if (!canShoot)
        {
            return;
        }
        IUnit source = GetComponentInParent<IUnit>();
        Vector2 shootDirection = (targetPosition  - firePoint.position).normalized;
        shootDirection.y = 0f;
        Debug.LogError("Direction" + shootDirection);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.velocity = shootDirection * weaponParams.bulletSpeed;
        bullet.GetComponent<Bullet>().source = source;

        canShoot = false;
        StartCoroutine(ShootDelay());

    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(1/weaponParams.firerate);
        canShoot = true;
    }

    public float GetRange()
    {
        return weaponParams.range;
    }
    public Transform GetTransform()
    {
        return transform;
    }
}
