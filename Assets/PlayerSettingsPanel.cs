using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingsPanel : MonoBehaviour
{
    public PlayerAccount currentPlayerAccount;

    public Color currentPlayerColor;

    [Header("View Settings")]
    public TextMeshProUGUI viewName;
    public string playerName;

    [Header("View Player Icon")]
    public List<Sprite> iconsList = new List<Sprite>();
    public Sprite currentIcon;

    public Image iconImage;
    public int currentIconNumber = -1;

    public Button iconButton;

    public TextMeshProUGUI prompt;
    public bool promtIsActive = true;


    public void Start()
    {
        iconButton.onClick.AddListener(() => CharacterIconChangeSwitch());
        CharacterIconChangeSwitch();
    }

    public void PanelInitialization()
    {

    }

    public void CharacterIconChangeSwitch()
    {
        if (promtIsActive == true)
        {
            prompt.text = "click to change player icon";

            currentIcon = null;
            iconImage.sprite = currentIcon;
            
            promtIsActive = false;
            return;
        }

        currentIconNumber += 1;
        if (currentIconNumber >= iconsList.Count)
            currentIconNumber = 0;

        currentIcon = iconsList[currentIconNumber];
        iconImage.sprite = currentIcon;

        prompt.text = "";
    }

    public void NameInitialization(InputField currentInputField)
    {
        playerName = currentInputField.text;
        viewName.text = "Your name is: " + playerName;
    }
}
