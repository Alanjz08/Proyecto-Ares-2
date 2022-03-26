using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiempoDeVida : MonoBehaviour
{
    public float tiempodevida;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, tiempodevida);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
