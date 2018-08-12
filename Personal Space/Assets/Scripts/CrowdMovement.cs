using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class CrowdMovement : MonoBehaviour {
    public float personalSpaceRadius = -1f;
    public float initTalkRadius = -1f;
    public float maxWaitTime = -1f;
    public float maxAvoidTime = -1f;
    public float talkAfinity = -1f;
    public float danceAfinity = -1f;

    enum State {
        WAITING, TALKING, TALKING_AND_WALKING, WALKING, WALKING_TO_DANCEFLOOR, DANCING
    }
    State currentState = State.WAITING;
    float nextStateChange = 0f;
    
    Vector3 targetPosition = new Vector3();
    
    float talkUntil = 0f;
    Transform talkPartner;

    float nextDanceStep = 0f;
    Vector3 danceTarget;

    float myTime = 0f;

    public LayerMask crowdLayer;

    PersonMotor motor;
    Rigidbody2D rb;
    GameObject GFX;

    private void Start() {
        // replace all negative values by random values
        if (personalSpaceRadius < 0) personalSpaceRadius = Random.Range(.5f,1.5f);
        if (initTalkRadius < 0) initTalkRadius = Random.Range(0.5f, 2f);
        if (maxWaitTime < 0) maxWaitTime = Random.Range(3f, 20f);
        if (maxAvoidTime < 0) maxAvoidTime = Random.Range(1f,10f);
        if (talkAfinity < 0) talkAfinity = Random.Range(0f, .5f);
        if (danceAfinity < 0) danceAfinity = Random.Range(0f, 1f);
        
        motor = GetComponent<PersonMotor>();
        rb = GetComponent<Rigidbody2D>();
        GFX = transform.Find("GFX").gameObject;
    }

    Collider2D[] lastCollision = new Collider2D[0];

    private void FixedUpdate() {
        myTime += Time.fixedDeltaTime;
        if (myTime > nextStateChange) ChangeState();
        switch (currentState) {
            case State.WAITING:
            case State.WALKING:
                Collider2D[] talkColliders = Physics2D.OverlapCircleAll(transform.position, initTalkRadius, crowdLayer);
                if (talkColliders != null) {
                    var newColliders = talkColliders.Where(x => !lastCollision.Contains(x)).ToList();
                    lastCollision = talkColliders;
                    foreach (var c in newColliders) {
                        if (ConsiderTalking(c)) {
                            break;
                        }
                    }
                }
                
                
                if (currentState == State.WALKING && rb.velocity.magnitude < 0.1f) {
                    Wait();
                }
                break;
            case State.WALKING_TO_DANCEFLOOR:
                if ((transform.position - targetPosition).magnitude < 2f) {
                    ChangeState();
                }
                break;
            case State.TALKING:
                var diff = targetPosition - transform.position;
                GFX.transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(diff.x, diff.y, 0));
                if (myTime > talkUntil) {
                    Wait();
                }
                break;
            case State.TALKING_AND_WALKING:
                if (Vector2.Distance(transform.position, targetPosition) < 0.1f) {
                    ChangeState();
                } else {
                    var diff2 = targetPosition - transform.position;
                    rb.velocity = diff2.normalized * motor.speed;
                    GFX.transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(diff2.x, diff2.y, 0));
                }
                break;
            case State.DANCING:
                if (myTime > nextDanceStep) {
                    danceTarget = targetPosition + new Vector3(Random.Range(-3f, 3f), Random.Range(-3f,3f), 0f);
                    nextDanceStep = myTime + 2f;
                }
                if ((transform.position - danceTarget).magnitude > Time.fixedDeltaTime * 50f) {
                    rb.velocity = (danceTarget - transform.position).normalized * Time.fixedDeltaTime * 50f;
                }
                // GFX.transform.LookAt();
                // wiggle in beat of music? all synchronously!
                break;
        }
    }



    void ChangeState() {
        switch (currentState) {
            case State.WAITING:
                if (Random.Range(0f,1f) < danceAfinity) {
                    // go dancing
                    Transform target = PointOfInterest.GetPoIWithTag("dancefloor")[0].transform;
                    // TODO pathfinding
                    motor.StartWalking(new Transform[]{target}, StartDancing);
                    targetPosition = target.position;
                    currentState = State.WALKING_TO_DANCEFLOOR;
                    nextStateChange = float.PositiveInfinity;
                } else {
                    // pick point of interest and go there
                    // TODO pathfinding
                    motor.StartWalking(new Transform[]{PointOfInterest.RandomPoI().transform}, Wait);
                    currentState = State.WALKING;
                    nextStateChange = float.PositiveInfinity;
                }
                break;
            case State.TALKING:
                Debug.Log("check for personal space distance");
                if (Mathf.Abs((transform.position - talkPartner.position).magnitude - personalSpaceRadius) > 0.1f) {
                    targetPosition = (talkPartner.position - transform.position).normalized * personalSpaceRadius + transform.position;
                    currentState = State.TALKING_AND_WALKING;
                    nextStateChange = myTime + 1f;
                } else {
                    nextStateChange = myTime + Random.Range(maxAvoidTime/2, maxAvoidTime);
                }
                break;
            case State.TALKING_AND_WALKING:
                targetPosition = transform.position;
                currentState = State.TALKING;
                nextStateChange = myTime + Random.Range(maxAvoidTime/2, maxAvoidTime);
                break;
            case State.WALKING:
                // change will come from personMotor or collision
                break;
            case State.WALKING_TO_DANCEFLOOR:
                motor.StopWalking(); // calls StartDancing automatically
                break;
            case State.DANCING:
                motor.enabled = true;
                currentState = State.WAITING;
                ChangeState();
                break;
        }
    }

    void Wait() {
        currentState = State.WAITING;
        nextStateChange = myTime + Random.Range(0f, maxWaitTime);
    }

    void StartDancing() {
        motor.enabled = false;
        currentState = State.DANCING;
        nextStateChange = myTime + 10f + Random.Range(0f,60f * danceAfinity);
    }

    private bool ConsiderTalking(Collider2D other) {
        CrowdMovement otherCrowd = other.gameObject.GetComponent<CrowdMovement>();
        if (otherCrowd != null && freeToTalk() && otherCrowd.freeToTalk()) {
            if (Random.Range(0f,1f) < talkAfinity) {
                motor.StopWalking();
                float duration = Random.Range(3f, 30f * (talkAfinity + otherCrowd.talkAfinity)/2f);
                talkUntil = myTime + duration;
                talkPartner = otherCrowd.transform;
                currentState = State.TALKING;
                ChangeState(); // to update the targetPosition for talk&walk
                otherCrowd.GetSpokenTo(duration, this.transform);
                Debug.Log("started to talk!");
                return true;
            }
        }
        return false;
    }

    public bool freeToTalk() {
        return (currentState == State.WAITING || currentState == State.WALKING);
    }

    public void GetSpokenTo(float duration, Transform other) {
        motor.StopWalking();
        talkPartner = other;
        talkUntil = myTime + duration;
        currentState = State.TALKING;
        nextStateChange = myTime + Random.Range(maxAvoidTime/2, maxAvoidTime);
    }



    private void OnDrawGizmos()  {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, personalSpaceRadius);
    }

}