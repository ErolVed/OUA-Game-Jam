using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f; // Düþmanýn hareket hýzý
    public float patrolRange = 5f; // Devriye gezme menzili
    public Transform[] patrolPoints; // Devriye noktalarý

    private int currentPatrolPointIndex = 0; // Þu anki devriye noktasý
    private Rigidbody2D rb;
    private Vector2 movement;
    Animator enemeyAnimator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<enemeyAnimator>();
    }

    void Update()
    {
        // Düþman devriye noktasýna ulaþtýysa bir sonraki noktaya geç
        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolPointIndex].position) < 0.1f)
        {
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        }

        // Düþman hareketi
        movement = (patrolPoints[currentPatrolPointIndex].position - transform.position).normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}