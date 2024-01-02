using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Itens;


public class ActionLifePack : MonoBehaviour
{
    public SOint soint;

    public KeyCode keyCode = KeyCode.L;

    private void Start()
    {
        soint = ItemManager.instance.GetByType(ItemType.LIFE_PACK).sOint;
    }

    private void RecoverLife()
    {
        if (soint.value > 0)
        {
            ItemManager.instance.RemoveByType(ItemType.LIFE_PACK);
            Player.instance.healthBase.ResetLife();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            RecoverLife();
        }
    }
} 
