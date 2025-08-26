using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideUI : MonoBehaviour
{

    [SerializeField] GameObject UIItem;

    public void ShowUI(bool state)
    {
        UIItem.SetActive(state);
    }

}
