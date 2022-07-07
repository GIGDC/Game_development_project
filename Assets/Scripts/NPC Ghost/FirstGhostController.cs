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

                if (GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("실팔찌") != null
            && GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("편지") != null)
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

        if (Click.name.Contains("구원"))
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
            if ((GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("실팔찌") != null
            && GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("편지") != null) 
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
        if (GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("실팔찌") != null
            && GameObject.Find("Player").GetComponent<PlayerMissionItem>().GetMissionItem("편지") != null)
        {
            narrators = "사실 이건 너를 위해 내가 직접 만든 거야.$ 편지를 읽었는지는 모르겠지만 거기엔 내 진심이 담겨 있어.$ 너가 지연이에게 괴롭힘을 당할 때 처음에는 무서워서 방관해버렸었지.$하지만 이후에 너에게 도움이 되고 싶어서 용기를 내서 선생님께 그 사실을 알렸어.$ 그런데 선생님은 내 말을 무시하셨고, 지연이네의 괴롭힘은 더 심해졌었지.$ 너는 계속해서 혼자만의 고통스러운 시간을 보냈었고......$내가 좀 더 적극적으로 행동했다면 지금쯤 우리 사이는 달라졌을까?$지금와서 후회한들 이미 늦은 거 같지만......$다시 한 번 더 사과할게. 그리고 나의 이야기를 들어줘서 고마워.$".Split('$'); ;
        }
        else
        {
            if (isSaving)
                narrators = "내 실팔찌와 편지를 잃어버렸어...!$ 어딘가에 있는 내 물건들을 찾아줘!!$".Split('$');
        }

        foreach (string narrator in narrators)
        {
            yield return StartCoroutine(Chat("학생 1", narrator));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Tab));
        }
    }
}
