using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeRespawn : MonoBehaviour
{
    //Funci�n que permite guardar la posici�n de un checkpoint
    private float checkPointPositionX, checkPointPositionY;

    public void reachedCheckPoint(float x, float y)
    {
        PlayerPrefs.SetFloat("checkPointPositionX", x);
        PlayerPrefs.SetFloat("checkPointPositionY", y);

    }

}
