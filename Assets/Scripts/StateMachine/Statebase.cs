using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SamEbac.StateMachine
{
    public class Statebase 
    {
        public virtual void OnStateEnter(params object[] objects)
        {
            Debug.Log("OnStateEnter");
        }
    
        public virtual void OnStateStay(object o = null)
        {
            Debug.Log("OnStateStay");
        } 
    
        public virtual void OnStateExit(object o = null)
        {
            Debug.Log("OnStateExit");
        }
    
    }

}


