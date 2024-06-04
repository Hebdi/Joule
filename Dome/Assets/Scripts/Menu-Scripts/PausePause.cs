using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePause : MonoBehaviour
{
    // Start is called before the first frame update
    public void timeStop()
    {
        Time.timeScale = 0.0f;
    }

    public void timeStart()
    {
        Time.timeScale = 1.0f;
    }

}
