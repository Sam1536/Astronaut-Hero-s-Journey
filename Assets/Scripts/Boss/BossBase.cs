using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SamEbac.StateMachine;
using DG.Tweening;


namespace Boss 
{

    public enum BossAction
    {
        INIT,   
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }

    public class BossBase : MonoBehaviour
    {
        private StateMachine<BossAction> stateMachine;

        [Header("Animations")]
        public float startAnimationDuration = .5f;
        public Ease startAnimationEase = Ease.OutBack;

        [Header("Attack")]
        public int AttackAmount = 5;
        public float timeBetweenAttacks = .5f;
        
        public float speed = 10f;
        public List<Transform> waypoints;

        public HealthBase healthBase;

        private void OnValidate()
        {
            if (healthBase != null) healthBase = GetComponent<HealthBase>();
        }

        private void Awake()
        {
            Init();
            OnValidate();
            if(healthBase != null)
            {
                healthBase.Onkill += OnBossKill;

            }
        }

        private void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();


            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateKill());
        }

        private void OnBossKill(HealthBase b)
        {
            SwitchState(BossAction.DEATH);
        }

        #region Attack
        
        public void StartAttack(Action endCallBack = null)
        {
            StartCoroutine(AttackCoroutine(endCallBack));
        }

        IEnumerator AttackCoroutine(Action endCallBack)
        {
            int attack = 0;
            while (attack < AttackAmount)
            {
                attack++;
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(timeBetweenAttacks);
            }
            endCallBack?.Invoke();
        }

        #endregion


        #region Walk


        public void GoToRandomPoint(Action onArrive = null)
        {
            StartCoroutine(GoToPointCoroutine(waypoints[UnityEngine.Random.Range(0,waypoints.Count)], onArrive));
        }

        IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
        {
            while (Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();   
            }
            onArrive?.Invoke();
        }

        #endregion


        #region Animation

        public void StartInitAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        #endregion

        #region DEBUG

        [NaughtyAttributes.Button]
        private void SwitchInit( )
        {
            SwitchState(BossAction.INIT);
        }
        
        [NaughtyAttributes.Button]
        private void SwitchWalk( )
        {
            SwitchState(BossAction.WALK);
        }
        
        [NaughtyAttributes.Button]
        private void SwitchAttack( )
        {
            SwitchState(BossAction.ATTACK);
        }

        #endregion

        #region State Machine

        public void  SwitchState(BossAction state)
        {
            stateMachine.SwitchState(state,this);
        }

        #endregion
    }

}

