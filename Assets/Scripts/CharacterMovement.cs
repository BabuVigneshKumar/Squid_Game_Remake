using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] protected float movementSpeed = 100f;
    [SerializeField] private ParticleSystem bloodEfx;
    [SerializeField] private Transform bloodSpawnPoint;
    protected Rigidbody rb;


    protected float verticalDirection = 1;
    protected float sprintValue = 0f;
    public bool isInvulnerable { get; private set; }

    private bool canMove = true;

    protected Animator animator;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

 
    void FixedUpdate()
    {
        if (canMove == true)
        {
            rb.velocity = Vector3.forward * verticalDirection * (sprintValue + 2) * movementSpeed * Time.fixedDeltaTime;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        
    }

    public bool isMoving()
    {
        return rb.velocity.magnitude > 0.1f;
    }

    public virtual void Die()
    {
        Debug.Log("<color=green>Death animation working</color>");
        animator.SetTrigger("Death");
         var spawnedEfx = Instantiate(bloodEfx, bloodSpawnPoint.position, bloodEfx.transform.rotation);
        Destroy(spawnedEfx, 5f);
        canMove = false;
        Debug.Log(name + "<color=green>has died!</color>");
        //isInvulnerable = true;
    }


    public virtual void Win()
    { 
        isInvulnerable = true;

    }



}
