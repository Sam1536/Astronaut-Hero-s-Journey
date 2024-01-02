using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class GunShootLimit : GunBase
{
    public List<UIFillUpdate> uIFillUpdates;
   



    public float maxShoot = 10f;
    public float timeToRecChange = 1f;
    private float _currentShoots;
    private bool _reCharging = false;


    private void Awake()
    {
        GetAllUIs();
    }

    protected override IEnumerator ShootCoroutine()
    {
        //while (true)
        //{
        //    Shoot();
        //    yield return new WaitForSeconds(timeBetweenShoot);
        //}

        if (_reCharging) yield break;

        while (true)
        {
            if(_currentShoots  < maxShoot)
            {
                Shoot();
                _currentShoots++;
                CheckReCharge();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
            
        }
    }

    private void CheckReCharge()
    {
        if(_currentShoots >= maxShoot)
        {
            StopShoot();
            StartReCharge();
        }
    }

    private void StartReCharge()
    {
        _reCharging = true;
        StartCoroutine(ReChargeCoroutine());
    }

    IEnumerator ReChargeCoroutine()
    {
        float time = 0;
        while(time < timeToRecChange)
        {
            time += Time.deltaTime;
            uIFillUpdates.ForEach(i => i.UpdateValue(time/timeToRecChange));
            yield return new WaitForEndOfFrame();        
        }

        _currentShoots = 0;
        _reCharging = false;
    }

    private void UpdateUI()
    {
        uIFillUpdates.ForEach(i => i.UpdateValue(maxShoot, _currentShoots));
    }

    private void GetAllUIs()
    {
        uIFillUpdates = GameObject.FindObjectsOfType<UIFillUpdate>().ToList();
    }
}

