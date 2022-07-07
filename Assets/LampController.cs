using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampController : MonoBehaviour
{
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        if(camera!=null)
            this.gameObject.transform.position = new Vector3(camera.transform.position.x,camera.transform.position.y-5.0f,0f);
        StartCoroutine(ShowReady());
    }

    IEnumerator ShowReady()
    {
        int count = 0;
        while (count < 3)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 90);
            yield return new WaitForSeconds(0.5f);
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 180);
            yield return new WaitForSeconds(0.5f);
            count++;
        }
    }

}
