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
    public bool dontLeaveTheRoom = false;

    public enum State {
        WAITING, TALKING, TALKING_AND_WALKING, WALKING, WALKING_TO_DANCEFLOOR, DANCING
    }
    public State currentState = State.WAITING;
    float nextStateChange = 0f;
    
    Vector3 targetPosition = new Vector3();
    List<Vector3> walkPath = new List<Vector3>();
    float annoyance = 0f;
    
    bool dontTalk = false;
    float talkUntil = 0f;
    Transform talkPartner;

    float nextDanceStep = 0f;
    Vector3 danceTarget;

    float myTime = 0f;

    public LayerMask peopleLayer;
    public float walkSpeed = 2f;

    Rigidbody2D rb;
    GameObject GFX;

    AudioSource audioSource;
    public AudioClip[] blaBlaClips;

    private void Start() {
        // replace all negative values by random values
        if (personalSpaceRadius < 0) personalSpaceRadius = Random.Range(1f,3f);
        if (initTalkRadius < 0) initTalkRadius = Random.Range(1f, 4f);
        if (maxWaitTime < 0) maxWaitTime = Random.Range(3f, 20f);
        if (maxAvoidTime < 0) maxAvoidTime = Random.Range(1f,10f);
        if (talkAfinity < 0) talkAfinity = Random.Range(0f, 1f);
        if (danceAfinity < 0) danceAfinity = Random.Range(0f, .25f);
        
        rb = GetComponent<Rigidbody2D>();
        GFX = transform.Find("GFX").gameObject;
        audioSource = GetComponent<AudioSource>();
    }

    Collider2D[] lastCollision = new Collider2D[0];
    float stuckSince = float.PositiveInfinity;

    private void FixedUpdate() {
        myTime += Time.fixedDeltaTime;
        if (myTime > nextStateChange) ChangeState();
        switch (currentState) {
            case State.WAITING:
            case State.WALKING:
                Collider2D[] talkColliders = Physics2D.OverlapCircleAll(transform.position, initTalkRadius, peopleLayer);
                if (talkColliders != null) {
                    var newColliders = talkColliders.Where(x => !lastCollision.Contains(x)).ToList();
                    lastCollision = talkColliders;
                    foreach (var c in newColliders) {
                        if (ConsiderTalking(c)) {
                            break;
                        }
                    }
                }
                break;
            case State.WALKING_TO_DANCEFLOOR:
                if ((transform.position - targetPosition).magnitude < 3f && walkPath.Count == 0) {
                    ChangeState();
                }
                break;
            case State.TALKING:
                var diff = talkPartner.position - transform.position;
                GFX.transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(diff.x, diff.y, 0));
                if (myTime > talkUntil) {
                    audioSource.Stop();
                    Wait();
                }
                break;
            case State.TALKING_AND_WALKING:
                if (Vector2.Distance(transform.position, targetPosition) < 0.1f) {
                    ChangeState();
                } else {
                    var diff2 = targetPosition - transform.position;
                    rb.velocity = diff2.normalized * walkSpeed;
                    diff2 = talkPartner.position - transform.position;
                    GFX.transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(diff2.x, diff2.y, 0));
                }
                break;
            case State.DANCING:
                if (myTime > nextDanceStep) {
                    danceTarget = targetPosition + new Vector3(Random.Range(-7f, 7f), Random.Range(-7f,7f), 0f);
                    nextDanceStep = myTime + 2f;
                }
                if ((transform.position - danceTarget).magnitude > Time.fixedDeltaTime * walkSpeed * 2.5f) {
                    rb.velocity = (danceTarget - transform.position).normalized * walkSpeed * 2.5f;
                }
                bool beat1 = (myTime % 1) < 0.5;
                GFX.transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(1, beat1?0.2f:-.2f, 0));
                break;
        }

        if (currentState == State.TALKING || currentState == State.TALKING_AND_WALKING) {
            audioSource.volume = Mathf.Max(2f - (transform.position - PlayerStatus.thePlayer.transform.position).magnitude*.1f, 0f);
        }

        if (currentState == State.WALKING || currentState == State.WALKING_TO_DANCEFLOOR) {
            if (rb.velocity.magnitude < 0.1f) {
                stuckSince = Mathf.Min(stuckSince, myTime);
                if (stuckSince + 1f < myTime) {
                    Wait();
                }
            } else {
                stuckSince = float.PositiveInfinity;
            }
        } else {
            stuckSince = float.PositiveInfinity;
        }

        if (currentState == State.WALKING || currentState == State.WALKING_TO_DANCEFLOOR) {
            Collider2D[] personalSpaceColliders = Physics2D.OverlapCircleAll(transform.position, personalSpaceRadius, peopleLayer);
            annoyance = Mathf.Lerp(annoyance, personalSpaceColliders.Length, 0.1f);
            Vector2 direction = targetPosition - transform.position;
            direction.Normalize();
            Vector2 directionChanges = new Vector2();
            foreach (var c in personalSpaceColliders) {
                if (c.gameObject == this.gameObject) continue;
                Vector2 directionToPerson = c.transform.position - transform.position;
                directionToPerson.Normalize();
                float distance = (c.transform.position - transform.position).magnitude - 1f;
                float intensity = (personalSpaceRadius - distance) * 2f / (annoyance + 1);
                // Debug.Log(distance + " " + annoyance);
                float dotP = Vector2.Dot(direction, directionToPerson);
                if (dotP < 0) continue;
                var commonComponent = dotP * directionToPerson;
                var normal = direction - commonComponent;
                normal.Normalize();
                if (commonComponent.magnitude > 0.95f) {
                    normal = new Vector2(directionToPerson.y, -directionToPerson.x);
                }
                directionChanges += intensity * normal; // - intensity * commonComponent;
            }
            direction += directionChanges;
            rb.velocity = direction.normalized * walkSpeed;
            GFX.transform.rotation = Quaternion.FromToRotation(Vector3.up, new Vector3(direction.x, direction.y, 0));
            if (CheckWalkingDone()) {
                Wait();
            }
        } else {
            annoyance *= 0.9f;
            rb.velocity *= 0.3f;
        }
        rb.mass = .1f + annoyance/10f;
    }



    void ChangeState() {
        switch (currentState) {
            case State.WAITING:
                if (dontLeaveTheRoom) {
                    targetPosition = PathManager.manager.GetMyRoom(transform.position).RandomPoint();
                    currentState = State.WALKING;
                    nextStateChange = float.PositiveInfinity;
                    break;
                }
                if (Random.Range(0f,1f) < danceAfinity) {
                    // go dancing
                    var poi = PointOfInterest.GetPoIWithTag("dancefloor")[0];
                    FindPathTo(poi.transform.position, poi.myRoom);
                    currentState = State.WALKING_TO_DANCEFLOOR;
                    nextStateChange = float.PositiveInfinity;
                } else {
                    // pick point of interest and go there
                    var pois = PointOfInterest.poi.Where(x => !x.tags.Contains("dancefloor")).ToList();
                    //pois = pois.OrderBy(x => x.transform.position - transform.position).ToList(); BROKE UNITY; THEREFORE TEMP REPLACED BY NEXT LINE (cannot compare Vector3)
                    pois = pois.OrderBy(x => (x.transform.position - transform.position).magnitude).ToList();
                    var poi = pois[Random.Range(0, Mathf.Min(pois.Count, 3))];
                    FindPathTo(poi.transform.position, poi.myRoom);
                    currentState = State.WALKING;
                    nextStateChange = float.PositiveInfinity;
                }
                break;
            case State.TALKING:
                // Debug.Log("check for personal space distance");
                if (Mathf.Abs((transform.position - talkPartner.position).magnitude - personalSpaceRadius) > 0.1f) {
                    targetPosition = (transform.position - talkPartner.position).normalized * (personalSpaceRadius+1f) + talkPartner.position;
                    currentState = State.TALKING_AND_WALKING;
                    nextStateChange = myTime + 3f;
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
                StartDancing();
                break;
            case State.DANCING:
                currentState = State.WAITING;
                nextStateChange = 0f;
                ChangeState();
                break;
        }
    }

    public void ForceWalkTo(Vector3 pos, Room r = null) {
        FindPathTo(pos, r);
        currentState = State.WALKING;
        nextStateChange = float.PositiveInfinity;
        nextStateChange = float.PositiveInfinity;
        dontTalk = true;
    }

    void FindPathTo(Vector3 pos, Room r = null) {
        walkPath.Clear();
        var currentRoom = PathManager.manager.GetMyRoom(transform.position);
        if (r == null) {
            r = PathManager.manager.GetMyRoom(pos);
        }
        if (currentRoom == null) {
            //Debug.LogError("I don't know where I am!!!!");
            targetPosition = transform.position;
            return;
        } else if (currentRoom == r) {
            targetPosition = pos;
        } else {
            var roomTransitions = PathManager.manager.GetPathFromAToB(currentRoom, r);
            if (roomTransitions == null) {
                Debug.LogError("could not find a path from " + currentRoom.RoomName + " to " + r.RoomName);
                targetPosition = transform.position;
                return;
            }
            walkPath = roomTransitions;
            walkPath.Add(pos);
            targetPosition = transform.position;
        }
        CheckWalkingDone();
    }

    bool CheckWalkingDone() {
        if ((transform.position - targetPosition).magnitude < 1f) {
            if (walkPath.Count == 0) {
                dontTalk = false;
                return true;
            }
            targetPosition = walkPath[0];
            walkPath.RemoveAt(0);
        }
        return false;
    }

    public void Wait() {
        currentState = State.WAITING;
        nextStateChange = myTime + Random.Range(0f, maxWaitTime);
    }

    void StartDancing() {
        currentState = State.DANCING;
        nextStateChange = myTime + 10f + Random.Range(0f,60f * danceAfinity);
    }

    private bool ConsiderTalking(Collider2D other) {
        CrowdMovement otherCrowd = other.gameObject.GetComponent<CrowdMovement>();
        if (otherCrowd != null && otherCrowd != this && freeToTalk() && otherCrowd.freeToTalk()) {
            if (Random.Range(0f,1f) < talkAfinity) {
                float duration = Random.Range(10f, 60f * (talkAfinity + otherCrowd.talkAfinity)/2f);
                talkUntil = myTime + duration;
                talkPartner = otherCrowd.transform;
                currentState = State.TALKING;
                ChangeState(); // to update the targetPosition for talk&walk
                otherCrowd.GetSpokenTo(duration, this.transform);
                audioSource.clip = blaBlaClips[Random.Range(0, blaBlaClips.Length)];
                audioSource.Play();
                return true;
            }
        }
        return false;
    }

    public bool freeToTalk() {
        return (currentState == State.WAITING || currentState == State.WALKING) && !dontTalk;
    }

    public void GetSpokenTo(float duration, Transform other) {
        talkPartner = other;
        talkUntil = myTime + duration;
        currentState = State.TALKING;
        nextStateChange = myTime + Random.Range(maxAvoidTime/2, maxAvoidTime);
    }



    private void OnDrawGizmos()  {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, personalSpaceRadius);
        if (currentState == State.TALKING || currentState == State.TALKING_AND_WALKING) {
            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.position, talkPartner.position);
        } else {
            Gizmos.color = Color.red;
            if (targetPosition != new Vector3()) {
                Gizmos.DrawLine(this.transform.position, targetPosition);
            }
            if (currentState == State.DANCING) {
                Gizmos.DrawLine(this.transform.position, danceTarget);
            }
        }
    }

}