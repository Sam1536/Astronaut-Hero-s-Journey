using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamEbac.StateMachine;


namespace Boss
{
    public class BossState : Statebase
    {
        protected BossBase bossBase;

        public override void OnStateEnter(params object[] objects)
        {
            base.OnStateEnter(objects);
            bossBase = (BossBase)objects[0];
        }
    }

    public class BossStateInit : BossState
    {
        public override void OnStateEnter(params object[] objects)
        {
            base.OnStateEnter(objects);
            bossBase.StartInitAnimation();
            Debug.Log("Boss" + bossBase);
        }
    }

    public class BossStateWalk : BossState
    {
        public override void OnStateEnter(params object[] objects)
        {
            base.OnStateEnter(objects);
            bossBase.GoToRandomPoint(OnArrive);
        }

        private void OnArrive()
        {
            bossBase.SwitchState(BossAction.ATTACK);
        }

        public override void OnStateExit(object o = null)
        {
            base.OnStateExit(o);
            bossBase.StopAllCoroutines();
        }
    }

    public class BossStateAttack : BossState
    {
        public override void OnStateEnter(params object[] objects)
        {
            base.OnStateEnter(objects);
            bossBase.StartAttack(EndAttack);

        }

        private void EndAttack()
        {
            bossBase.SwitchState(BossAction.WALK);
        }

        public override void OnStateExit(object o = null)
        {
            base.OnStateExit(o);
            bossBase.StopAllCoroutines();
        }


    }

    public class BossStateKill : BossState
    {
        public override void OnStateEnter(params object[] objects)
        {
            base.OnStateEnter(objects);
            bossBase.transform.localScale = Vector3.one * .2f;

        }

    }
}


