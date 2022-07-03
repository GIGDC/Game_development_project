using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEdGhostGirl : MonoBehaviour
{
    Animator animator;
    public Image EventImage;
    bool isObjectTargeted = false;
    GameObject player;
    Camera camera;
    Vector3 cameraPos; // 카메라의 원래 위치
    [SerializeField][Range(0.01f, 0.5f)] float shakeRange = 0.5f;
    [SerializeField][Range(0.1f, 1f)] float duration = 1f;
    bool isTriggered; // 점프 스케어 발동 여부

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.Find("Player").gameObject;
        isTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isObjectTargeted)
        {
            if(Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("isSelected");
                player.GetComponent<PlayerMovement>().CanPlayerMove = false;
            }
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("SpecialEdGhostGirl_NeckFlipped")
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f
            && !isTriggered)
        {
            EventImage.gameObject.SetActive(true);
            cameraPos = camera.transform.position;
            InvokeRepeating("StartShaking", 0f, 0.005f);
            Invoke("StopShaking", 1f);
        }
    }

    private void FixedUpdate()
    {
        CastRay();
    }

    public void CastRay()
    {
        int layerMask = LayerMask.NameToLayer("Ghost");

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

        if (hit.collider != null && hit.collider.gameObject.layer == layerMask)
            isObjectTargeted = true;
        else
            isObjectTargeted = false;
    }

    void StartShaking()
    {
        float cameraPosX = Random.value * shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 cameraPos = camera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        camera.transform.position = cameraPos;
        isTriggered = true;
    }

    void StopShaking()
    {
        CancelInvoke("StartShaking");
        camera.transform.position = cameraPos;
        EventImage.gameObject.SetActive(false);
        isObjectTargeted = false;
        player.GetComponent<PlayerMovement>().CanPlayerMove = true;
        this.gameObject.SetActive(false);
    }
}
