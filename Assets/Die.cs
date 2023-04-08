using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DevriyeDusman : MonoBehaviour
{

    public float speed = 2f; // D��man�n h�z�

    private Transform target; // Hedef nesne

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; // Hedef nesneyi "Player" olarak etiketlemi� bir nesne olarak tan�ml�yoruz.
    }

    void Update()
    {
        // D��man karakterimizin hedefe do�ru hareket etmesi i�in bir vekt�r olu�turuyoruz.
        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        // D��man karakterimizi hedefe do�ru hareket ettiriyoruz.
        transform.position += direction * speed * Time.deltaTime;
    }

    // Karakterimiz temas halinde oldu�unda ne olaca��n� belirliyoruz.
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Oyuncu ile temas halinde");
            // Oyuncuyu �ld�rme kodu
            Destroy(other.gameObject);
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}