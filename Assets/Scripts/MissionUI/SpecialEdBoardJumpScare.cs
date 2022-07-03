using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialEdBoardJumpScare : MonoBehaviour
{
    public Image monsterJumpScare;
    public Image boardUI;
    Button exitBtn;

    private void Start()
    {
        exitBtn = boardUI.transform.Find("exitBtn").GetComponent<Button>();
        exitBtn.onClick.AddListener(OnExitBtnClick);
    }

    void OnExitBtnClick()
    {
        if(this.GetComponent<ActivateBoardUI>().MissionItemUsed)
            monsterJumpScare.gameObject.SetActive(true);
    }
}
