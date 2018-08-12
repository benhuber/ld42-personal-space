using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tut : MonoBehaviour {
    public GameObject tutDisplay;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") Destroy(tutDisplay);
    }
}
