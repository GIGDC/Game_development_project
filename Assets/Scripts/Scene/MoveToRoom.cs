using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToRoom : MonoBehaviour
{
    public float transitionTime = 1f;
    public GameObject roomLoader;

    Animator doorAnim;
    public string transferMapName; // �̵��� ���� �̸�
    [Tooltip("���� ���� ���� (front, back, right, left)")]
    public string direction;
    bool playerNearDoor;

    private void Awake()
    {
        doorAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Player�� ���ؼ��� üũ
        if (collision.gameObject.name != "Player")
            return;
        playerNearDoor = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Player�� ���ؼ��� üũ
        if (collision.gameObject.name != "Player")
            return;
        playerNearDoor = false;
    }

    private void Update()
    {
        if (playerNearDoor != true)
            return;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Open");
            StartCoroutine(LoadMap(transferMapName));
        }
    }

    private IEnumerator LoadMap(string transferMapName)
    {
        roomLoader.GetComponent<Animator>().SetTrigger("FadeOut");
        doorAnim.SetBool("DoorOpen", true);
        yield return new WaitForSeconds(0.5f);

        doorAnim.SetBool("DoorOpen", false);
        yield return new WaitForSeconds(0.5f);

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(transferMapName);
    }
}
