using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBulletBase projectileBulletPrefab;
    public Transform pointShoot;
    public float timeBetweenShoot = .3f;
    public float speed = 40f;
    private Coroutine _currentCoroutine;

    public KeyCode keyCodeTest = KeyCode.X;


 

    private void Update()
    {
        //if (Input.GetKeyDown(keyCodeTest))
        //{
        //    _currentCoroutine = StartCoroutine(ShootCoroutine());
        //}
        //else if (Input.GetKeyUp(keyCodeTest))
        //{
        //    if (_currentCoroutine != null)
        //        StopCoroutine(_currentCoroutine);
        //}

        //if (Input.GetMouseButtonDown(1))
        //{
        //    _currentCoroutine = StartCoroutine(StartShoot());
        //}
        //else if (Input.GetMouseButtonUp(1))
        //{
        //    if (_currentCoroutine != null)
        //        StopCoroutine(_currentCoroutine);
        //}
    }


    protected virtual IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoot);
        }
    }

    public virtual void Shoot()
    {
        
        var projectile = Instantiate(projectileBulletPrefab);
        projectile.transform.position = pointShoot.transform.position;
        projectile.transform.rotation = pointShoot.transform.rotation;
        projectile.bulletSpeed = speed;
      
        //  ShakeCamera.instance.Shake(); 
 
      
    }

    public void StartShoot()
    {
        _currentCoroutine = StartCoroutine(ShootCoroutine());
    }  
    
    public void StopShoot()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
    }
}
