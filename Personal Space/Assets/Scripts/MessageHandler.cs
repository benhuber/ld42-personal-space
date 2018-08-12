using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageHandler : MonoBehaviour {

    public RectTransform Rtext;
    public TextMeshProUGUI text;
    public float waittime;

    Vector3 originalPos;

    bool coming;
    bool waiting;
    bool going;
    float waituntill;

    Queue<string> messagequeue = new Queue<string>();

    public float Debug;

    public static MessageHandler me;

    public float speed = 3f;


    private void Awake()
    {
        me = this;
    }

    // Use this for initialization
    void Start () {
        originalPos = Rtext.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        Debug = Rtext.transform.position.x;

        if (coming)
        {
            if (Rtext.transform.position.x >= 900f)
            {
                coming = false;
                waiting = true;
                waituntill = Time.time + waittime;
            }
            else Rtext.transform.position += new Vector3(1f*speed, 0, 0);
        }
        if (waiting && Time.time > waituntill)
        {
            waiting = false;
            going = true;
        }
        if (going)
        {
            if (Rtext.transform.position.x >= 3000f)
            {
                going = false;
                Rtext.transform.position = originalPos;
                TryNext();
            }
            else Rtext.transform.position += new Vector3(1f * speed, 0, 0);
        }

    }

    void TryNext()
    {
        if (!waiting && !coming && !going && messagequeue.Count >0)
        {
            text.text = messagequeue.Dequeue();
            coming = true;
        }
    }

    public void EnqueMessage(string message)
    {
        messagequeue.Enqueue(message);
        TryNext();
    }


}
