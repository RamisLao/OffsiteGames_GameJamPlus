using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAnimationsCommand
{
    public virtual void Execute(Animator anim) { }

    public virtual void Cancel(Animator anim) { }
}

public class DoMove: PlayerAnimationsCommand
{
    public override void Execute(Animator anim) 
    {
        anim.SetBool("isWalking", true);
    }

    public override void Cancel(Animator anim) 
    {
        anim.SetBool("isWalking", false);
    }
}

public class DoDead: PlayerAnimationsCommand
{
    public override void Execute(Animator anim)
    {
        anim.SetTrigger("isDead");
    }
}
