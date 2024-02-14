using UnityEngine;

public interface IWeapon
{
    void Shoot(Vector3 targetPosition);
    float GetRange();
    Transform GetTransform();
    WeaponParams weaponParams { get; set; }
    GameObject bulletPrefab { get; set; }
}
