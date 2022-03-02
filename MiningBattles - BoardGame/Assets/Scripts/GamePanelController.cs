using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanelController : MonoBehaviour
{
    [Header("Bottom panel")]
    public GameObject bottomPanel;
    private Animator bottomAnimator;
    public bool customBottomToggle = false;

    public void Awake()
    {
        //  bottom panel
        bottomAnimator = bottomPanel.GetComponent<Animator>();
        ConditionToggle();

    }

    public void ConditionToggle()
    {
        customBottomToggle = !customBottomToggle;
        switch(customBottomToggle)
        {
            case true:
                OpenBottomPanel();
                break;

            case false:
                ClosedBottomPanel();
                break;
        }
    }

    public void OpenBottomPanel()
    {
        bottomAnimator.SetBool("Condition", true);
    }

    public void ClosedBottomPanel()
    {
        bottomAnimator.SetBool("Condition", false);

    }
}
