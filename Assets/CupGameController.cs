using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CupGameController : MonoBehaviour
{
    bool isClick = false;
    GameObject click=null;
    int eyeNum = -1;
    Button[] Objects;
    public Image eye;
    public float lerpTime = 1.0f;
    List<Button> Loc=new List<Button>();
    bool isCheck = false;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        eyeNum = Random.Range(0, 3); //1부터 3까지.. 지정된 위치로 계속 이동하면된다.
        Objects = this.gameObject.GetComponentsInChildren<Button>(); 
        
        for(int i = 0; i < 3; i++)
        {
            Loc.Add(Objects[i]);
        }
        Debug.Log("눈 번호" + eyeNum);
        Objects[eyeNum].transform.position = new Vector3(Objects[eyeNum].transform.position.x, Objects[eyeNum].transform.position.y+400, 0);

        eye.transform.position= new Vector3(Objects[eyeNum].transform.position.x, eye.transform.position.y,0);

        StartCoroutine(
             lerpCoroutine(Objects[eyeNum].transform.position, new Vector3(Objects[eyeNum].transform.position.x, 573,0), lerpTime,eyeNum));


    }

    void Setting()
    {
        count ++;
        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, Loc.Count);
            Debug.Log("랜덤수" + rand);
            StartCoroutine(lerpCoroutine(Objects[i].transform.position, new Vector3(Loc[rand].transform.position.x, 573, 0), lerpTime, i));
            Loc.RemoveAt(rand);
        }
        for (int i = 0; i < 3; i++)
        {
            Loc.Add(Objects[i]);
        }
    }
    IEnumerator lerpCoroutine(Vector2 current, Vector2 target, float time,int cupNum)
    {
        yield return new WaitForSeconds(1f);
        float elapsedTime = 0.0f;

        while (elapsedTime < time)
        {
            elapsedTime += (Time.deltaTime);

            Objects[cupNum].transform.position
                = Vector3.Lerp(current, target, elapsedTime / time);
            
            yield return null;
        }
        eye.gameObject.SetActive(false);
        Objects[cupNum].transform.position = target;
        yield return null;
        if (count < 6)
            Setting();
        else
            isClick = true;
        yield return null;
    }

    public void OnClick()
    {
        if (isClick)
        {
            click = EventSystem.current.currentSelectedGameObject;

            int Num = eyeNum + 1;
            if (click.name.Contains(Num.ToString()))
            {
                this.transform.gameObject.SetActive(false);
                FirstConversation.isSuccess = true;
            }
        }
    }
}
