using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] PlayerInputs inputs;
    [SerializeField] Gun defaultGun;
    private GameObject target;
    private Gun selectedGun;
    private Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
        SelectGun(defaultGun);

        inputs.onShootBtn += Shoot;
    }
    private void OnDestroy()
    {
        inputs.onShootBtn -= Shoot;
    }
    private void Update()
    {
        HandleWeaponHolderAnimations();
        FindClosestShootableTarget();
        UpdateGunDirection();
    }

    #region FindClosestShootableTarget
    void FindClosestShootableTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, selectedGun.gunRange);
        Collider2D closestCollider = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D collider in colliders)
        {
            IShootable shootable = collider.GetComponent<IShootable>();
            if (shootable != null)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCollider = collider;
                }
            }
        }

        if (closestCollider != null)
        {
            target = closestCollider.gameObject;
        }
        else
        {
            target = null;
        }
    }
    #endregion

    #region Select Weapon
    public void SelectGun(Gun weapon)
    {
        if (weapon == null)
            return;

        DestroySelectedWeapons();
        GameObject instantiatedWeapon = Instantiate(weapon.gameObject, transform);
        selectedGun = instantiatedWeapon.GetComponent<Gun>();
    }

    private void DestroySelectedWeapons()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
    }
    #endregion

    #region Handle Display Of Selected Weapon
    private void HandleWeaponHolderAnimations()
    {
        anim.SetFloat("Speed", inputs.movement.sqrMagnitude);
    }
    private void UpdateGunDirection()
    {
        if (target == null)
            return;

        LookAt(target.transform);
    }
    private void LookAt(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    #endregion

    private void Shoot()
    {
        selectedGun.Shoot();
    }
}
