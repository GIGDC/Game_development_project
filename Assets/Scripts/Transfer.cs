using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transfer : MonoBehaviour
{
    GameManager gameManager;
    Animator doorAnimator;
    public string GoTo;
    public string direction;
    StartPoint start;
    KeyController key;
    public Image WarningUI;
    // Start is called before the first frame update
    void Start()
    {
        start = GameObject.FindObjectOfType<StartPoint>();
        key = GameObject.FindObjectOfType<KeyController>();
        doorAnimator = GetComponent<Animator>();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
            return; //여기에 objcet 숨겨진 후 몇초 뒤에 등장하도록 구현 (ex: 몹)
                    // manager.transferScene = "SampleScene";

        StartPoint.MapNum = SceneManager.GetActiveScene().buildIndex;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (key.isLock && key.SceneNum == StartPoint.MapNum)
            {
                if (direction != "")
                {
                    StartPoint.direction = direction;
                }
                doorAnimator.SetTrigger("OpenDoor");
                SceneTransition();

            }
            else
            {
                if (WarningUI != null)
                {
                    WarningUI.gameObject.SetActive(true);
                }
            }
        }
    }
    /*
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name != "Player")
            return; //여기에 objcet 숨겨진 후 몇초 뒤에 등장하도록 구현 (ex: 몹)
                    // manager.transferScene = "SampleScene";


        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartPoint.MapNum = SceneManager.GetActiveScene().buildIndex;

            if (direction != "")
            {
                StartPoint.direction = direction;
            }
            SceneTransition();
        }
    }
    */
    public void SceneTransition()
    {
        StartCoroutine(FadeOut());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        WarningUI.gameObject.SetActive(false);
    }
    IEnumerator FadeOut()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.transferScene = GoTo;
        Debug.Log(gameManager.transferScene);
        gameManager.GetTransitionAnimator().SetBool("FadeOut", true);
        gameManager.GetTransitionAnimator().SetBool("FadeIn", false);
        //doorAnimator.SetTrigger("OpenDoor");
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(gameManager.transitionTime);
        StartCoroutine(gameManager.AsyncLoadMap());
        yield return null;
    }
}