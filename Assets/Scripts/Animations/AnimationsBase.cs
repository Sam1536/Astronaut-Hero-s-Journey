using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animation
{

    public enum AnimationType
    {
        NONE,
        IDLE,
        RUN,
        DEATH,
        ATTACK
    }

    public class AnimationsBase : MonoBehaviour
    {
        public Animator anim;
        public List<AnimationSetup> animationSetups;

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            var setup = animationSetups.Find(i => i.animationType == animationType);
            if(setup != null)
            {
                anim.SetTrigger(setup.trigger);
            }
        }

    }


    [System.Serializable]
    public class AnimationSetup
    {
        public AnimationType animationType;
        public string trigger;

    }
}


