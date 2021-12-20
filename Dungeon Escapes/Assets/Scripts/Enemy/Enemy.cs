using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform pointA, pointB;

    protected Vector3 currentTarget;
    protected Animator anim;
    protected SpriteRenderer sprite;

    protected bool isHit = false;
    protected Transform player;
    protected float distance;
    protected bool isDead = false;

    public GameObject diamondPrefab;

    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }
    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetBool("InCombat")==false)
        {
            return;
        }
        distance = Vector2.Distance(transform.position, player.position);

        if (isDead == false)
        {
            Movement();
        }
        
    }

    public virtual void Movement()
    {
        if (currentTarget == pointA.position)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            anim.SetTrigger("Idle");
        }
        else if (transform.position == pointB.position)
        {
            currentTarget = pointA.position;
            anim.SetTrigger("Idle");
        }

        if (isHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }       

        if (distance > 2.0f)
        {
            isHit = false;
            anim.SetBool("InCombat", false);
        }

        Vector3 direction = player.localPosition - transform.localPosition;

        if (direction.x < 0 && anim.GetBool("InCombat") == true)
        {
            sprite.flipX = true;
            currentTarget = pointA.position;
        }
        else if (direction.x > 0 && anim.GetBool("InCombat") == true)
        {
            sprite.flipX = false;
            currentTarget = pointB.position;
        }
    }
    public virtual void Damage()
    {
        if (isDead == true)
        {
            return;
        }

        health--;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);

        if (health < 1)
        {
            isDead = true;
            anim.SetTrigger("Death");
            GameObject diamond =  Instantiate(diamondPrefab, transform.position, Quaternion.identity);
            diamond.GetComponent<Diamond>().gems = gems;
            Destroy(this.gameObject,5f);
        }
    }

    
}
