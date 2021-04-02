using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Point po;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            po.lastCheckPointPos = transform.position;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        po = GameObject.FindGameObjectWithTag("PO").GetComponent<Point>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
