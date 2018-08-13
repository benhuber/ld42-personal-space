using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionManager : MonoBehaviour {
    public class InteractionDetails {
        public Transform center;
        public float sizeOfRect = 1f;
        public string text = "interact";
    }
    public static List<InteractionDetails> possibleInteractions = new List<InteractionDetails>();
    
    public static void RegisterInteraction(Transform position, string text = "interact", float sizeOfRect = 1f) {
        possibleInteractions.Add(new InteractionDetails(){center=position, sizeOfRect=sizeOfRect, text=text});
    }

    public static void RemoveInteraction(Transform position) {
        possibleInteractions.RemoveAll(x => x.center == position);
    }

    // non-static stuff

    public TextMeshProUGUI interactText;

    private void Update() {
        
    }
}