using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    [SerializeField] private float _currentLife;

    public List<UIFillUpdate> uiFillUpdate;

    public bool destroyonkill = false;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> Onkill;

    private void Awake()
    {
        Init();
    }


    public void Init()
    {
        ResetLife();

    }

    public void ResetLife()
    {
        _currentLife = startLife;
        UIGunUpdate();
       
        
    }

    

    protected virtual void Kill()
    {
        if(destroyonkill)
            Destroy(gameObject, 3f);
        
        Onkill?.Invoke(this);

        Debug.Log("MORREU!");
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(5);
    }


    public void  Damage(float f)
    {
        _currentLife -= f;
        
        if (_currentLife <= 0)
        {
            Kill();
        }
        UIGunUpdate();
        OnDamage?.Invoke(this);

    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }


    private void UIGunUpdate()
    {
        if(uiFillUpdate != null)
        {
            uiFillUpdate.ForEach(i => i.UpdateValue((float)_currentLife / startLife));
        }
    }
}
