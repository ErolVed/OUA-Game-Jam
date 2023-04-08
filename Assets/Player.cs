using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    new Rigidbody2D rigidbody;
        private void Awake()
    {
       rigidbody = GetComponent<Rigidbody2D>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DialogueManager.isActive == true)
            return;
        float movement= Input.GetAxis("Horizontal")* speed*Time.deltaTime;
        transform.Translate(new Vector2(movement, 0));

        
    }
}
