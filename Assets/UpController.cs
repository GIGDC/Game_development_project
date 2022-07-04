using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpController : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(lerpCoroutine(this.transform.position, new Vector2(this.transform.position.x, 10), 1f));
    }
    IEnumerator lerpCoroutine(Vector2 current, Vector2 target, float time)
    {
        yield return new WaitForSeconds(1f);
        float elapsedTime = 0.0f;

        while (elapsedTime < time)
        {
            elapsedTime += (Time.deltaTime);

            this.transform.position
                = Vector3.Lerp(current, target, elapsedTime / time);

            yield return null;
        }
        this.transform.position = target;

        if (elapsedTime >= time)
            this.gameObject.SetActive(false);
        yield return null;
    }
}
