using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
   // public List<UIFillUpdate> uIFillUpdates;

    public GunBase gunBase;
    public Transform gunPosition;

    private GunBase _currentGunBase;

    public FlashColor _flashColor;

    protected override void Init()
    {
        base.Init();
        CreateGun();
        inputs.Gameplay.Shoot.performed += cts => StartShoot();
        inputs.Gameplay.Shoot.canceled += cts => canceledShoot();
    }

    public void CreateGun()
    {
        _currentGunBase = Instantiate(gunBase, gunPosition);

        _currentGunBase.transform.localPosition = _currentGunBase.transform.localEulerAngles = Vector3.zero;
    }

    private void StartShoot()
    {
        _currentGunBase.StartShoot();
        _flashColor?.Flash();
        Debug.Log("Start Shoot!");
    }

    private void canceledShoot()
    {
        _currentGunBase.StopShoot();
        Debug.Log("Canceled Shoot!");

    }
}
