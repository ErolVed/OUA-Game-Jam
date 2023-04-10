using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kod : MonoBehaviour
{
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x+30,this.transform.position.y,this.transform.position.z);
    }
}
