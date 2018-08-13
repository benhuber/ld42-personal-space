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
        PersistentDataComponent p = FindObjectOfType<PersistentDataComponent>();
        Time.timeScale = 0f;
        Endscreen.SetActive(true);
        if (PLACEHOLDER_DATA.data.ending == PLACEHOLDER_DATA.Endings.Default) p.CompleteAnEnding(PersistentDataComponent.EEndings.EEndings_Default); //E1.SetActive(true);
        if (PLACEHOLDER_DATA.data.ending == PLACEHOLDER_DATA.Endings.AllFriendsDone) p.CompleteAnEnding(PersistentDataComponent.EEndings.EEndings_AllFriendsDone); //E2.SetActive(true);
        if (PLACEHOLDER_DATA.data.ending == PLACEHOLDER_DATA.Endings.Stress) p.CompleteAnEnding(PersistentDataComponent.EEndings.EEndings_Stress); //E3.SetActive(true);
        if (PLACEHOLDER_DATA.data.ending == PLACEHOLDER_DATA.Endings.Time) p.CompleteAnEnding(PersistentDataComponent.EEndings.EEndings_Time); //E4.SetActive(true);
    }

}
