using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;
using UnityEngine.Events;


namespace Enemy
{
    public class EnemyBase : MonoBehaviour , IDamageable
    {
        public Collider colliderEnemy;
        public FlashColor flashColor;
        public ParticleSystem particleEnemy;
        public bool lookAtPlayer = false;

        private Player _player;

        public float startLife = 10f;
        [SerializeField]private float _currentLife;
    
        [Header("Animation Player")]
        [SerializeField]private AnimationsBase _animationsBase;

        [Header("Start Animation")]
        public float startAnimationDuration = 1f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startwithBornAnimation = true;

        [Header("Events")]
        public UnityEvent onKillEvent;

        private void Awake()
        {
            Init();
        }


        private void Start()
        {
            _player = GameObject.FindObjectOfType<Player>();
        }

        protected virtual void ResetLife()
        {
            _currentLife = startLife;
        }

        protected virtual void Init()
        {
            ResetLife();
            if(startwithBornAnimation)
                BornAnimations();
        }

        protected virtual void Kill()
        {
            OnKill();
        } 
        
        protected virtual void OnKill()
        {
            if (colliderEnemy != null) colliderEnemy.enabled = false;
            Destroy(gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
            onKillEvent?.Invoke();
            Debug.Log("MORREU!");
        }

        public void OnDamage(float f)
        {
            if (flashColor != null) flashColor.Flash();
            if (particleEnemy != null) particleEnemy.Emit(30);

            transform.position -= transform.forward;

            _currentLife -= f;
            if(_currentLife <= 0)
            {
                Kill();
            }

        }


        #region Animations

        private void BornAnimations()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationsBase.PlayAnimationByTrigger(animationType);
        }

        #endregion

        //debug
        //private void Update()
        //{

        //    if (Input.GetKeyDown(KeyCode.Z))
        //    {
        //        OnKill();
        //        OnDamage(5);
        //    } 
            
        //    if (Input.GetKeyDown(KeyCode.C))
        //    {
        //        OnKill();
        //        OnDamage(5);
        //    }
        //}

        public void Damage(float damage)
        {
            Debug.Log("pow!");
            OnDamage(damage);
        } 
        
        public void Damage(float damage ,Vector3 dir)
        {
            OnDamage(damage);
            transform.DOMove(transform.position - dir, .1f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Player p = collision.transform.GetComponent<Player>();

            if(p != null)
            {
               p.healthBase.Damage(1);
            }
        }

        public virtual void Update()
        {

            if (lookAtPlayer)
            {
                transform.LookAt(_player.transform.position);
            }

        }
    }

}
