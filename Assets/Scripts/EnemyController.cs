using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f; // D��man�n hareket h�z�
    public int damageAmount = 20; // D��man�n verdi�i hasar miktar�
    public int health = 50; // D��man�n can puan�
    public float patrolDistance = 5f; // D��man�n devriye gezmesi i�in ileri gidece�i mesafe
    public float chaseDistance = 10f; // D��man�n oyuncuyu takip edece�i mesafe
    public Transform groundDetection; // D��man�n yere temas edip etmedi�ini kontrol etmek i�in kullan�lacak nokta

    private Rigidbody2D rb;
    private bool movingRight = true;
    private Transform playerTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // D��man�n Rigidbody bile�enine eri�ir
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        // D��man�n oyuncuya do�ru y�r�mesini sa�lar
        if (Vector2.Distance(transform.position, playerTransform.position) <= chaseDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            // D��man�n sa�a veya sola hareket etmesini sa�lar
            if (movingRight)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }

            // D��man�n devriye gezmesini sa�lar
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
        // E�er d��man oyuncuya �arparsa sald�r�r
        if (other.gameObject.CompareTag("Player"))
        {
            Attack(other.GetComponent<Player>());
        }
    }

    public void TakeDamage(int amount)
    {
        // D��man�n can puan�n� azalt�r ve �l�p �lmedi�ini kontrol eder
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // D��man�n �l�m animasyonunu �al��t�r�r ve nesneyi yok eder
        Destroy(gameObject);
    }

    public void Attack(Player player)
    {
        // D��man�n sald�r� animasyonunu �al��t�r�r ve oyuncuya hasar verir
        // Burada sald�r� animasyonu ve sald�r� s�ras�nda al�nacak aksiyonlar yaz�labilir
        
    }
}
        