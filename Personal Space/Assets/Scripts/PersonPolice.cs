using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonPolice : Talktome
{

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        //BC
        if (myTime > 135)
        {
            GetComponent<CrowdMovement>().enabled = true;
        }

    }
}
