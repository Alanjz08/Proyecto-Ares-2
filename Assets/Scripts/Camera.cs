using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject Gura;

    // Start is called before the first frame update
    void Start() 
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Gura != null)
        {
            Vector3 position = transform.position;
            position.x = Gura.transform.position.x;
            position.y = Gura.transform.position.y;
            transform.position = position;
        }
    }
}
