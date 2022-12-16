using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots;

    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int magSize, currentAmmo, maxAmmo;
    }

    public int GetCurrentAmmo(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).currentAmmo;
    }
    public int GetMaxAmmo(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).maxAmmo;
    }
    public int GetMagSize(AmmoType ammoType)
    {
        return GetAmmoSlot(ammoType).magSize;
    }

    public void ReduceCurrentAmmo(AmmoType ammoType)
    {
        GetAmmoSlot(ammoType).currentAmmo--;
    }
    
    public void IncreaseReloadAmmo(AmmoType ammoType)
    {
        int reloadAmout = GetAmmoSlot(ammoType).magSize - GetAmmoSlot(ammoType).currentAmmo;
        if (reloadAmout <= GetAmmoSlot(ammoType).maxAmmo)
        {
            GetAmmoSlot(ammoType).currentAmmo += reloadAmout;
            GetAmmoSlot(ammoType).maxAmmo -= reloadAmout;
        }
        else if (reloadAmout > GetAmmoSlot(ammoType).maxAmmo)
        {
            GetAmmoSlot(ammoType).currentAmmo += GetAmmoSlot(ammoType).maxAmmo;
            GetAmmoSlot(ammoType).maxAmmo -= GetAmmoSlot(ammoType).maxAmmo;
        }
    }
    //public void ReduceReloadAmmo(AmmoType ammoType)
    //{
    //    int reloadAmout = GetAmmoSlot(ammoType).magSize - GetAmmoSlot(ammoType).currentAmmo;
    //    if (reloadAmout <= GetAmmoSlot(ammoType).maxAmmo)
    //    {
            
    //    }
    //    else if (reloadAmout > GetAmmoSlot(ammoType).maxAmmo)
    //    {
            
    //    }
    //}

    public void IncreaseMaxAmmo(AmmoType ammoType, int ammoAmount)
    {
        GetAmmoSlot(ammoType).maxAmmo += ammoAmount;
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (AmmoSlot slot in ammoSlots)
        {
            if (slot.ammoType == ammoType)
            {
                return slot;
            }
        }
        return null;
    }
}
