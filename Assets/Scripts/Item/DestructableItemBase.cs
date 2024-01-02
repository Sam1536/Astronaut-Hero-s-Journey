using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DestructableItemBase : MonoBehaviour
{
    public HealthBase healthBase;
    
    public float shakeDuration = .1f;
    public int shakeForce = 1;
    
    public int dropCoinAmount = 10;
    public GameObject CoinPrefab;
    public Transform dropCoinPosition;

    private void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();
        healthBase.OnDamage += OnDamage;
    }

    
    private void OnDamage(HealthBase h)
    {
        gameObject.transform.DOShakeScale(shakeDuration, Vector3.up/2, shakeForce);
        DropCoins();
    }


    [NaughtyAttributes.Button]
    private void DropCoins()
    {
        var i = Instantiate(CoinPrefab);

        i.transform.position = dropCoinPosition.position;
        i.transform.DOScale(0, 1f).SetEase(Ease.OutBack).From();
    }

    [NaughtyAttributes.Button]
    private void DropGrouOfCoins()
    {
        StartCoroutine(DropGrouOfCoinsCoroutine());  
    }    
    
    private IEnumerator DropGrouOfCoinsCoroutine()
    {
        for (int i = 0; i < dropCoinAmount; i++)
        {
            DropCoins();
            yield return new WaitForSeconds(.1f);
        }
    }
}
