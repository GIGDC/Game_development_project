using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CountController : MonoBehaviour
{
    public static int SocCount=0;
    public static int DodCount=0;
    public static int VolCount=0;
    public static int BasCount=0;

    Text[] Count;
    public Image[] SoccurBall;
    public Image[] VolleyBall;
    public Image[] DodgeBall;
    public Image[] Basketball;
    // Update is called once per frame
    private void Start()
    {
        Count = this.gameObject.GetComponentsInChildren<Text>();
    }
    void Update()
    {
        if (Count != null)
        {
            Count[0].text = SocCount.ToString();
            Count[1].text = VolCount.ToString();
            Count[2].text = BasCount.ToString();
            Count[3].text = DodCount.ToString();

            for (int i = 0; i < SocCount; i++)
                if (SoccurBall != null)
                    SoccurBall[i].gameObject.SetActive(true);

            for (int i = 0; i < DodCount; i++)
                if (DodgeBall != null)
                    DodgeBall[i].gameObject.SetActive(true);

            for (int i = 0; i < VolCount; i++)
                if (VolleyBall != null)
                    VolleyBall[i].gameObject.SetActive(true);

            for (int i = 0; i < BasCount; i++)
                if (Basketball != null)
                    Basketball[i].gameObject.SetActive(true);
        }
    }
}
