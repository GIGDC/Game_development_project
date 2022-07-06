using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickController : MonoBehaviour
{
    public Image EventImage;
    GameObject target;
    public GameObject glass;
    bool isCollider = false;

    private void FixedUpdate()
    {
        target = null;
        CastRay();
        if (isCollider && target.CompareTag("magnifiedObj") && (target != null))
        {
            glass.SetActive(true);
            glass.transform.position = Input.mousePosition;
            isCollider = false;
        }
        else
        {
            glass.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (target == this.gameObject)
            {
                if (EventImage != null)
                {
                    EventImage.gameObject.SetActive(true);
                    glass.SetActive(false);
                }
                if (target.name == "Frige")
                    FrigeGhostAudioController.isPlaying = true;

            }
        }

        if (EventImage.gameObject.activeSelf == true)
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                FrigeGhostAudioController.isPlaying = false;
                EventImage.gameObject.SetActive(false);
            }

    }
    public void CastRay()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit;

        RaycastHit2D[] tmphit;
        hit = Physics2D.Raycast(pos, Vector2.zero, 0f, 1 << LayerMask.NameToLayer("ItemLayout"));
        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
            isCollider = true;
        }
    }
}