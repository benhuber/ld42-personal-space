using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tut : MonoBehaviour {
    public GameObject tutDisplay;
    public GameObject interactDisplay;
    public static tut me;

    private void Awake()
    {
    me = this;    
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") Destroy(tutDisplay);
    }
}
