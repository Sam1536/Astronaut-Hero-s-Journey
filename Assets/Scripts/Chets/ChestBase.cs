using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestBase : MonoBehaviour
{
    public Animator anim;
    public string open = "Open";
    public string Close = "Close";

    public KeyCode keyCode = KeyCode.E;

    [Space]
    public ChestItemCoin chestItemCoin; 

    [Header("Notification")]
    public GameObject notification;
    public float TweenDuration = .2f;
    public Ease ease = Ease.OutBack;
    private float _startScale;
    private bool _chestOpen = false; 

    private void Start()
    {
        _startScale = notification.transform.localScale.x;
        HideNotification();
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode) && notification.activeSelf)
        {
            OpenChest();
        }
    }

    [NaughtyAttributes.Button]
    private void OpenChest()
    {
        if (_chestOpen) return;
        
        anim.SetTrigger("Open");
        _chestOpen = true;
        HideNotification();
        Invoke(nameof(ShowItem), 1f);
    }

    private void ShowItem()
    {
        chestItemCoin.ShowItem();
        Invoke(nameof(CollectItem), 1f);

    }

    private void CollectItem()
    {
        chestItemCoin.Collect();

    }

    [NaughtyAttributes.Button]
    private void CloseChest()
    {
        anim.SetTrigger("Close");

    }

    [NaughtyAttributes.Button]
    private void ShowNotification()
    {
        notification.SetActive(true);
        notification.transform.localScale = Vector3.zero;
        notification.transform.DOScale(_startScale, TweenDuration);
    }

    [NaughtyAttributes.Button]
    private void HideNotification()
    {
        notification.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        Player p =other.transform.GetComponent<Player>();

        if(p != null)
        {
            ShowNotification();
        }
      
    }

    
    private void OnTriggerExit(Collider other)
    {
        Player p = other.transform.GetComponent<Player>();

        if (p != null)
        {
            HideNotification();
        }
    }
}
