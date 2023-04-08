using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f; // D��man�n hareket h�z�
    public float patrolRange = 5f; // Devriye gezme menzili
    public Transform[] patrolPoints; // Devriye noktalar�

    private int currentPatrolPointIndex = 0; // �u anki devriye noktas�
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
        // D��man devriye noktas�na ula�t�ysa bir sonraki noktaya ge�
        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolPointIndex].position) < 0.1f)
        {
            currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Length;
        }

        // D��man hareketi
        movement = (patrolPoints[currentPatrolPointIndex].position - transform.position).normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}