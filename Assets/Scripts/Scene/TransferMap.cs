using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public Animator sceneTransition;
    public float transitionTime = 1f;

    Animator doorAnim;
    public string transferMapName; // �̵��� ���� �̸�
    [Tooltip("���� ���� ���� (front, back, right, left)")]
    public string direction;
    Vector2 playerDirection; // �÷��̾ ���ϰ� �ִ� ����

    private void Awake()
    {
        doorAnim = GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Player�� ���ؼ��� üũ
        if (other.gameObject.name != "Player")
            return;

        playerDirection = GameObject.Find("Player").GetComponent<PlayerMovement>().GetDirectionNormalized();
        Debug.Log(playerDirection);
        if (Input.GetKeyDown(KeyCode.Z))
        {
            switch (direction)
            {
                case "front":
                    if (playerDirection.y > 0)
                        StartCoroutine(LoadMap(transferMapName));
                    break;
                case "back":
                    if (playerDirection.y < 0)
                        StartCoroutine(LoadMap(transferMapName));
                    break;
                case "right":
                    if (playerDirection.x > 0)
                        StartCoroutine(LoadMap(transferMapName));
                    break;
                case "left":
                    if (playerDirection.x < 0)
                        StartCoroutine(LoadMap(transferMapName));
                    break;
            }
        }
    }

    IEnumerator LoadMap(string transferMapName)
    {
        sceneTransition.SetTrigger("Start");
        doorAnim.SetBool("DoorOpen", true);
        yield return new WaitForSeconds(0.5f);

        doorAnim.SetBool("DoorOpen", false);
        yield return new WaitForSeconds(0.5f);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(transferMapName);
    }
}

