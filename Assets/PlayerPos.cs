using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{

    private Point po;
    // Start is called before the first frame update
    void Start()
    {
        po = GameObject.FindGameObjectWithTag("PO").GetComponent<Point>();
         transform.position = po.lastCheckPointPos ;
    }

    // Update is called once per frame
    void Update()
    {
     //   if(Input.GetKeyDown(KeyCode.Space))
      //      {
      //      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
       //     }
    }
}
