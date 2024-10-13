using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockScrollBarValue : MonoBehaviour
{
    public Scrollbar scrollbar;

    void Start()
    {
        SetScrollBarValue();
    }

    void SetScrollBarValue()
    {
        if (scrollbar != null)
        {
            scrollbar.value = 1f;
        }
    }
}
