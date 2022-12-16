using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = .5f;
    [SerializeField] float reloadTime = .5f;

    [SerializeField] Animator animator;
    [SerializeField] WeaponSounds weaponSounds;

    [SerializeField] TextMeshProUGUI ammoText;

    bool canShoot = true;

    private void OnEnable()
    {
        canShoot = true;
    }

    void Update()
    {
        ShootAndEmptyAnim();
        if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
            StartCoroutine(Shoot());
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (ammoSlot.GetCurrentAmmo(ammoType) == ammoSlot.GetMagSize(ammoType) || ammoSlot.GetMaxAmmo(ammoType) == 0)
            { return; }

            StartCoroutine(Reload());
        }
        DisplayAmmo();
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
            weaponSounds.ShoutSound();
            //animator.SetBool("Shooting", true);
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
        //animator.SetBool("Shooting", false);
    }

    IEnumerator Reload()
    {
        canShoot = false;
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(.2f);
        if (ammoSlot.GetMaxAmmo(ammoType) > 0)
        {
            weaponSounds.ReloadSound();
            ammoSlot.IncreaseReloadAmmo(ammoType);
        }
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
        animator.SetBool("Reloading", false);
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        int maxAmmo = ammoSlot.GetMaxAmmo(ammoType);
        ammoText.text = currentAmmo.ToString() + " / " + maxAmmo.ToString();
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    void ShootAndEmptyAnim()
    {
        if (canShoot == true)
        {
            animator.SetBool("Empty", false);
        }
        else if (canShoot == false)
        {
            animator.SetBool("Empty", true);
        }
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }
        else { return; }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .1f);
    }
}
