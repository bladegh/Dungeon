using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    private Animator _swordAnimation;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        _swordAnimation = transform.GetChild(1).GetComponent<Animator>();
    }

    public void Move(float move)
    {
        anim.SetFloat("Move", Mathf.Abs(move));
    }
    public void Jump(bool jumping)
    {
        anim.SetBool("Jumping",jumping);
    }
    public void Attack()
    {
        anim.SetTrigger("Attack");
        _swordAnimation.SetTrigger("SwordAnimation");
    }
}
