using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Copytext : MonoBehaviour
{
    [SerializeField] TMP_Text textToCopy;

    public void CopyText()
    {
        GUIUtility.systemCopyBuffer = textToCopy.text;
    }
}
