using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sr;
    public LayerMask layerMask;
    public int dmg = 1;
    public float icount = 2f;

    Color goldenWhite = new Color(1, 0f / 255f, 0f / 255f);//Normally 1,222,155
    public int hp = 3;
    float lasposy;
    float hspeed = 10, vspeed = 0;
    Vector2 hvel = Vector2.zero, vvel = Vector2.zero;
    Vector2 lasthvel;
    bool onAir = true; //Physically on air
    bool invincible = false;
    bool isPaused = false;
    bool moving = false, jumping = false, falling = false, attacking = false; //Animation States
    void Update()
    {
        Move();
        Animation();
        Attack();
        CheckOnAir();
        PauseWithKey();
        Debug.Log(onAir);
    }
    void Move()
    {
        hvel = Vector2.right * Input.GetAxis("Horizontal") * hspeed;
        Jump();
        vvel = Vector2.up * vspeed;

        if (attacking)
        {
            hvel = lasthvel;
        }

        rb.velocity = hvel + vvel;
    }
    void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && !onAir && !attacking)
        {
            vspeed = 16;
            onAir = true;
        }
        else if (onAir)
        {
            vspeed -= 32 * Time.deltaTime;
        }
        else
        {
            vspeed = 0;
        }
    }
    void Animation()
    {
        if (Input.GetAxis("Horizontal") > 0.3 && !attacking)
        {
            moving = true;
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetAxis("Horizontal") < -0.3 && !attacking)
        {
            moving = true;
            this.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            moving = false;
        }

        if (rb.velocity.y > 0)
        {
            jumping = true;
            falling = false;
        }
        else if (rb.velocity.y < 0)
        {
            jumping = false;
            falling = true;
        }
        else
        {
            jumping = false;
            falling = false;
        }

        if (attacking)
        {
            anim.SetInteger("animNo", 4);
        }
        else if (moving && !jumping && !falling)
        {
            anim.SetInteger("animNo", 1);
        }
        else if (jumping)
        {
            anim.SetInteger("animNo", 2);
        }
        else if (falling)
        {
            anim.SetInteger("animNo", 3);
        }
        else
        {
            anim.SetInteger("animNo", 0);
        }
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            attacking = true;
            lasthvel = Vector2.right * Input.GetAxis("Horizontal") * hspeed;
        }
    }
    void EndAttack()
    {
        attacking = false;
    }
    void CheckOnAir()
    {
        if (rb.velocity.y != 0)
        {
            onAir = true;
            Debug.Log("0");
        }
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position + new Vector3(0, 1f, 0), transform.TransformDirection(Vector3.down), Mathf.Infinity, layerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position - new Vector3(0.7f, -1, 0), transform.TransformDirection(Vector3.down), Mathf.Infinity, layerMask);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position + new Vector3(0.7f, 1, 0), transform.TransformDirection(Vector3.down), Mathf.Infinity, layerMask);
        if (hit1.distance < 2.45)
        {
            onAir = false;
            Debug.Log(hit1.distance);
        }
        else if (hit2.distance < 2.45)
        {
            onAir = false;
            Debug.Log("2");
        }
        else if (hit3.distance < 2.45)
        {
            onAir = false;
            Debug.Log("3");
        }
        else
        {
            onAir = true;
            Debug.Log("hi!");
        }
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "EHitBox" && !invincible)
        {
            //hp -= c.transform.parent.GetComponent<EnemyNew>().dmg;
            StartCoroutine(Invincibility());
            float diff = c.transform.position.x - transform.position.x;
            hvel = Vector2.zero;
            vvel = Vector2.zero;
            if (diff > 0)
            {
                rb.AddRelativeForce(50000 * (Vector2.up + Vector2.left), ForceMode2D.Impulse);
            }
            else if (diff < 0)
            {
                rb.AddRelativeForce(50000 * (Vector2.up + Vector2.right), ForceMode2D.Impulse);
            }
        }
    }
    IEnumerator Invincibility()
    {
        invincible = true;
        StartCoroutine(InvinciblityColorSwitch());
        yield return new WaitForSeconds(icount);
        invincible = false;
    }
    void Pause()
    {
        if (isPaused == true)
        {
            Time.timeScale = 1;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
        }
    }
    void PauseWithKey()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused == true)
            {
                Time.timeScale = 1;
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
            }
        }
    }
    IEnumerator InvinciblityColorSwitch()
    {
        this.GetComponent<SpriteRenderer>().color = goldenWhite;
        yield return new WaitForSeconds(icount / 4);
        this.GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(icount / 4);
        this.GetComponent<SpriteRenderer>().color = goldenWhite;
        yield return new WaitForSeconds(icount / 4);
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
