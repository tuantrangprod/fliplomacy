using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACell : MonoBehaviour
{
    // Start is called before the first frame update
    public int idX;
    public int idY;
    public string cellID = "0";
    void Start()
    {
        idX = (int)transform.position.x;
        idY = (int)transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
