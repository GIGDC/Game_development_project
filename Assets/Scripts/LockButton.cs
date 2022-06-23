using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockButton : MonoBehaviour
{
    Button lockButton;

    private void Start()
    {
        lockButton = transform.GetComponent<Button>();
        lockButton.onClick.AddListener(ButtonPressed);
    }

    void ButtonPressed()
    {
        GameObject lock_magnified = transform.parent.gameObject;
        switch (transform.name)
        {
            case "1":
                lock_magnified.GetComponent<Lock>().Password += "1";
                break;
            case "2":
                lock_magnified.GetComponent<Lock>().Password += "2";
                break;
            case "3":
                lock_magnified.GetComponent<Lock>().Password += "3";
                break;
            case "4":
                lock_magnified.GetComponent<Lock>().Password += "4";
                break;
            case "5":
                lock_magnified.GetComponent<Lock>().Password += "5";
                break;
            case "6":
                lock_magnified.GetComponent<Lock>().Password += "6";
                break;
            case "7":
                lock_magnified.GetComponent<Lock>().Password += "7";
                break;
            case "8":
                lock_magnified.GetComponent<Lock>().Password += "8";
                break;
        }
        Debug.Log(lock_magnified.GetComponent<Lock>().Password);
    }
}
