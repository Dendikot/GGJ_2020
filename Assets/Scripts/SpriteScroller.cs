using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    //It is created as a singleton to make sure it is the one common variable for all players to update. Latency may cause a problem 
    public float _drillSpeed;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        MoveUp(this.gameObject, 10);
    }

    public void MoveUp(GameObject panel, float speed)
    {
        float panelYpos = panel.transform.position.y;
        if (panelYpos > 40)
        {
            panel.transform.position -= new Vector3(0, 60, 0);
        }
        else
        {
            panel.transform.position += new Vector3(0,Time.deltaTime * speed,0);
        }
    }
}
