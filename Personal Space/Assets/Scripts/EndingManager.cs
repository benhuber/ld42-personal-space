using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour {

    public GameObject Endscreen;
    public GameObject E1;
    public GameObject E2;
    public GameObject E3;
    public GameObject E4;

    public static EndingManager em;

    public void Awake()
    {
        em = this;
    }

    public void PlayEnd()
    {
        Time.timeScale = 0f;
        Endscreen.SetActive(true);
        if (PLACEHOLDER_DATA.data.ending == PLACEHOLDER_DATA.Endings.Default) E1.SetActive(true);
        if (PLACEHOLDER_DATA.data.ending == PLACEHOLDER_DATA.Endings.AllFriendsDone) E2.SetActive(true);
        if (PLACEHOLDER_DATA.data.ending == PLACEHOLDER_DATA.Endings.Stress) E3.SetActive(true);
        if (PLACEHOLDER_DATA.data.ending == PLACEHOLDER_DATA.Endings.Time) E4.SetActive(true);
    }

}
