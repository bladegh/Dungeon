using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public GameObject acidEffectPrefab;
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();

        Health = base.health;
    }

    public override void Update()
    {
        //base.Update();
    }

    public override void Movement()
    {

        //base.Movement();
    }

    public override void Damage()
    {
        if (isDead == true)
        {
            return;
        }
        Health--;
        anim.SetBool("InCombat", true);

        if (Health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject diamond = Instantiate(diamondPrefab, transform.position, Quaternion.identity);
            diamond.GetComponent<Diamond>().gems = gems;
            Destroy(this.gameObject, 5f);
        }
        /*
        Health--;
        if (Health < 1)
        {
            Destroy(this.gameObject);
        }
        */
    }

    public void Attack()
    {
        Instantiate(acidEffectPrefab, transform.position, Quaternion.identity);   
    }
}
