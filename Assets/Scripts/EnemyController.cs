using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f; // Düþmanýn hareket hýzý
    public int damageAmount = 20; // Düþmanýn verdiði hasar miktarý
    public int health = 50; // Düþmanýn can puaný
    public float patrolDistance = 5f; // Düþmanýn devriye gezmesi için ileri gideceði mesafe
    public Transform groundDetection; // Düþmanýn yere temas edip etmediðini kontrol etmek için kullanýlacak nokta
    public float attackDistance = 1f; // Düþmanýn oyuncuya saldýrmak için yaklaþmasý gereken mesafe
    public Transform target; // Düþmanýn hedef alacaðý nesne, burada oyuncu

    private Rigidbody2D rb;
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Düþmanýn Rigidbody bileþenine eriþir
    }

    void FixedUpdate()
    {
        // Oyuncuya yaklaþmasý gereken mesafeye gelince ona doðru hareket eder
        if (Vector2.Distance(transform.position, target.position) < attackDistance)
        {
            Attack(target.GetComponent<Player>());
        }
        else
        {
            // Düþmanýn saða veya sola hareket etmesini saðlar
            if (movingRight)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }

            // Düþmanýn devriye gezmesini saðlar
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
            if (groundInfo.collider == false)
            {
                if (movingRight == true)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Eðer düþman oyuncuya çarparsa saldýrýr
        if (other.gameObject.CompareTag("Player"))
        {
            Attack(other.GetComponent<Player>());
        }
    }

    public void TakeDamage(int amount)
    {
        // Düþmanýn can puanýný azaltýr ve ölüp ölmediðini kontrol eder
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Düþmanýn ölüm animasyonunu çalýþtýrýr ve nesneyi yok eder
        Destroy(gameObject);
    }

    public void Attack(Player player)
    {
        // Düþmanýn saldýrý animasyonunu çalýþtýrýr ve oyuncuya hasar verir
        // Burada saldýrý animasyonu ve saldýrý sýrasýnda alýnacak aksiyonlar yazýlabilir
    }
}