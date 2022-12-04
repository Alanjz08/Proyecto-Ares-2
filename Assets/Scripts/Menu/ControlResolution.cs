using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewBehaviourScript : MonoBehaviour
{
    public float llave;
    public float rounded;
    UnityEngine.UI.CanvasScaler cv;

    // Start is called before the first frame update
    void Start()
    {
        cv = this.GetComponent<UnityEngine.UI.CanvasScaler>();
        Scalar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Scalar()
    {
        llave = cv.transform.position.x;
        if (llave == 960)
        {
            cv.scaleFactor = 0.45f;
        }
        else if (llave==683)
        {
            cv.scaleFactor = 0.33f;
        }
    }
}
