using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LibraryBook : MonoBehaviour
{
    Animator animator;
    Button nextPageBtn;
    Button keyObtainBtn;
    Text text;
    [Tooltip("Ã¥ ³Ñ±â´Â ¼Ò¸®")]
    [SerializeField] AudioSource flipPageAudio;
    [Tooltip("¿ôÀ½ ¼Ò¸®")]
    [SerializeField] AudioSource gigglingAudio;

    void Awake()
    {
        animator = GetComponent<Animator>();
        nextPageBtn = transform.Find("nextPageButton").GetComponent<Button>();
        keyObtainBtn = transform.Find("keyObtainButton").GetComponent<Button>();
        keyObtainBtn.gameObject.SetActive(false);   
        keyObtainBtn.onClick.AddListener(ObtainKey);
        text = transform.Find("Text").GetComponent<Text>();
    }

    private void OnEnable()
    {
        if (GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("1-4 ¿­¼è") != null)
            animator.SetBool("keyObtained", true);
        text.gameObject.SetActive(false);
        animator.SetInteger("page", 1);
        keyObtainBtn.gameObject.SetActive(false);
        nextPageBtn.gameObject.SetActive(true);
    }

    public void NextPage()
    {
        if (animator.GetInteger("page") == 7)
        {
            keyObtainBtn.gameObject.SetActive(true);
            nextPageBtn.gameObject.SetActive(false);
        }
        flipPageAudio.Play();
        animator.SetInteger("page", animator.GetInteger("page") + 1);
    }

    void ObtainKey()
    {
        if (animator.GetBool("keyObtained") == false)
        {
            text.gameObject.SetActive(true);
            gigglingAudio.Play();
            GameObject.Find("Player").GetComponent<PlayerMissionItem>().AddMissionItem("1-4 ¿­¼è");
            animator.SetBool("keyObtained", true);
        }
        else
        {
            keyObtainBtn.gameObject.SetActive(false);
        }
    }
}
