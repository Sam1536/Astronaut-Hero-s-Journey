using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Itens
{
    public class ItemCollactableBase : MonoBehaviour
    {

        public SFXType sfxType;  

        public ItemType itemType;

        [Header("Sounds")]
        public AudioSource audioSource;

        public Animator anim;
        public string compareTag = "Player";
        public ParticleSystem particleSystem;
        public float particlesTimeCoin = 3f;
        public GameObject graphicCoin;
        public Collider colliderLifePack;


        private void Awake()
        {
            //if (particleSystem != null)
            //{
            //    particleSystem.transform.SetParent(null);
            //}


        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag(compareTag))
            {
                Collect();

            }


        }

        private void PlaySFX()
        {
            SFXPool.instance.Play(sfxType);
        }


        protected virtual void Collect()
        {
            PlaySFX();
            if (graphicCoin != null)
            {
                graphicCoin.SetActive(false);


            }
            Invoke("HiderObject", particlesTimeCoin);

            OnCollect();

        }

        private void HiderObject()
        {
            // Debug.Log("pegou");
            gameObject.SetActive(false);


        }


        protected virtual void OnCollect()
        {
            if(colliderLifePack != null)
            {
                colliderLifePack.enabled = false;
            }

            if (particleSystem != null)
            {
                particleSystem.Play();
            }

            if (audioSource != null)
            {
                audioSource.Play();
            }

            ItemManager.instance.AddByType(itemType);
        }







    }
}
