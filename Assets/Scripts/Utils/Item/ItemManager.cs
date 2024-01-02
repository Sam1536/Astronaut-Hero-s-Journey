using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamEbac.Core.Singleton;
using TMPro;

namespace Itens
{
    public enum ItemType
    {
        COIN,
        LIFE_PACK
    }


    public class ItemManager : Singleton<ItemManager>
    {

        public List<ItensSetup> itensSetups;
        
       

        private void Start()
        {
            Reset();
            LoadItensFromSave();

         
        }

        public void LoadItensFromSave()
        {
            AddByType(ItemType.COIN, (int)SaveManager.instance.Setup.Coins);
            AddByType(ItemType.LIFE_PACK, (int)SaveManager.instance.Setup.Health);
        }

        public void Reset()
        {
            foreach(var i in itensSetups)
            {
                i.sOint.value = 0; 
            }            
        }

        public void AddByType(ItemType itemType ,int amount = 1)
        {
            if (amount < 0) return;
            itensSetups.Find(i => i.itemType == itemType).sOint.value += amount;
         
        }
        
        public ItensSetup GetByType(ItemType itemType)
        {
           return  itensSetups.Find(i => i.itemType == itemType);
         
        }

        public void RemoveByType(ItemType itemType, int amount = 1)
        {
            var itens = itensSetups.Find(i => i.itemType == itemType);
            itens.sOint.value -= amount;

            if (itens.sOint.value < 0) itens.sOint.value = 0;   

        }

        [NaughtyAttributes.Button]
        private void AddCoin()
        {
            AddByType(ItemType.COIN);
        }
        
        [NaughtyAttributes.Button]
        private void AddLifePack()
        {
            AddByType(ItemType.LIFE_PACK);
        }

    }

    [System.Serializable]
    public class ItensSetup
    {
        public ItemType itemType;
        public SOint sOint;
        public Sprite icon;
    }
}