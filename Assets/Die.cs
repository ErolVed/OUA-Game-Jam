using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DevriyeDusman : MonoBehaviour
{

    public float speed = 2f; // Düþmanýn hýzý

    private Transform target; // Hedef nesne

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; // Hedef nesneyi "Player" olarak etiketlemiþ bir nesne olarak tanýmlýyoruz.
    }

    void Update()
    {
        // Düþman karakterimizin hedefe doðru hareket etmesi için bir vektör oluþturuyoruz.
        Vector3 direction = target.position - transform.position;
        direction.Normalize();

        // Düþman karakterimizi hedefe doðru hareket ettiriyoruz.
        transform.position += direction * speed * Time.deltaTime;
    }

    // Karakterimiz temas halinde olduðunda ne olacaðýný belirliyoruz.
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Oyuncu ile temas halinde");
            // Oyuncuyu öldürme kodu
            Destroy(other.gameObject);
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}