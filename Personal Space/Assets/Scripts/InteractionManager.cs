using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class InteractionManager : MonoBehaviour {
    public delegate void CallbackType();
    public class InteractionDetails {
        public Transform center;
        public float sizeOfRect = 1f;
        public string text = "interact";
        public CallbackType callback;
    }
    public static List<InteractionDetails> possibleInteractions = new List<InteractionDetails>();
    
    public static void RegisterInteraction(CallbackType callback, Transform position, string text = "interact", float sizeOfRect = 1f) {
        possibleInteractions.Add(new InteractionDetails(){center=position, sizeOfRect=sizeOfRect, text=text, callback=callback});
    }

    public static void RemoveInteraction(Transform position) {
        possibleInteractions.RemoveAll(x => x.center == position);
    }

    // non-static stuff

    public TextMeshProUGUI interactText;
    public float interactRadius = 2f;
    public GameObject interactionMarker;

    static float AngleOfInteraction(InteractionDetails i) {
        Vector3 direction = (i.center.position - PlayerStatus.thePlayer.transform.position);
        float angle = Quaternion.Angle(PlayerStatus.thePlayer.transform.rotation, Quaternion.FromToRotation(Vector3.up, new Vector3(direction.x, direction.y, 0)));
        return angle;
    }

    float nextCheck = 0f;
    InteractionDetails currentInteraction;
    private void Update() {
        if (Time.time > nextCheck) {
            var nearby = possibleInteractions.Where(x => (x.center.position - PlayerStatus.thePlayer.transform.position).magnitude < interactRadius);
            nearby = nearby.OrderBy(x => 180 - AngleOfInteraction(x));
            currentInteraction = null;
            if (nearby.Count() > 0) {
                var best = nearby.First();
                if (AngleOfInteraction(best) < 90) {
                    currentInteraction = best;
                }
            }
            
            nextCheck = Time.time + .5f;
        }

        if (currentInteraction != null) {
            interactText.text = "press 'e' to " + currentInteraction.text;
            interactionMarker.transform.position = currentInteraction.center.position;
            interactionMarker.transform.localScale = new Vector3(currentInteraction.sizeOfRect, currentInteraction.sizeOfRect, 1f);
            interactionMarker.SetActive(true);
            if (Time.fixedDeltaTime > 0.1f && Input.GetKey(KeyCode.E)) {
                currentInteraction.callback();
            }
        }
        
    }
}