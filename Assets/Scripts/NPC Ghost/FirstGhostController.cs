using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class FirstGhostController : ActiveConversation
{
    static bool isSaving = false;
    public GameObject Select;
    public GameObject DeLight;
    public GameObject SaLight;
    public GameObject Audio;
    public AudioClip ac;

    GameObject Click;

    // Start is called before the first frame update
    private void Start()
    {
        Setting();
        ThrowKey = false;
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isChating)
            {
                if (Key != null)
                    Key.SetActive(true);
                message.SetActive(false);
                isChating = false;

                if (GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("������") != null
            && GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("����") != null)
                {
                    SaLight.SetActive(true);
                    this.gameObject.SetActive(false);
                    HandsController.isStarting = false;
                }
            }
        }
    }
    public void onClick()
    {
        Click = EventSystem.current.currentSelectedGameObject;

        if (Click.name.Contains("����"))
        {
            isSaving = true;
        }
        else
        {
            isSaving = false;
            DeLight.SetActive(true);
            Audio.GetComponent<AudioSource>().clip = ac;
            Audio.GetComponent<AudioSource>().Play();
            this.gameObject.SetActive(false);
        }
        Select.SetActive(false);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name != "Player")
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            messageImg.sprite = img;
            if ((GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("������") != null
            && GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("����") != null) 
            || isSaving)
            {
                message.SetActive(true);
                isChating = true;
                StartCoroutine(SelectTalk());
            }
            else
            {
                Select.SetActive(true);
            }
        }
    }
    protected IEnumerator SelectTalk()
    {
        string[] narrators = null;
        if (GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("������") != null
            && GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("����") != null)
        {
            narrators = "��� �̰� �ʸ� ���� ���� ���� ���� �ž�.$ ������ �о������� �𸣰����� �ű⿣ �� ������ ��� �־�.$ �ʰ� �����̿��� �������� ���� �� ó������ �������� ����ع��Ⱦ���.$������ ���Ŀ� �ʿ��� ������ �ǰ� �; ��⸦ ���� �����Բ� �� ����� �˷Ⱦ�.$ �׷��� �������� �� ���� �����ϼ̰�, �����̳��� �������� �� ����������.$ �ʴ� ����ؼ� ȥ�ڸ��� ���뽺���� �ð��� ���¾���......$���� �� �� ���������� �ൿ�ߴٸ� ������ �츮 ���̴� �޶�������?$���ݿͼ� ��ȸ�ѵ� �̹� ���� �� ������......$�ٽ� �� �� �� ����Ұ�. �׸��� ���� �̾߱⸦ ����༭ ����.$".Split('$'); ;
        }
        else
        {
            if (isSaving)
                narrators = "�� ������� ������ �Ҿ���Ⱦ�...!$ ��򰡿� �ִ� �� ���ǵ��� ã����!!$".Split('$');
        }

        foreach (string narrator in narrators)
        {
            yield return StartCoroutine(Chat("�л� 1", narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }
    }
}
