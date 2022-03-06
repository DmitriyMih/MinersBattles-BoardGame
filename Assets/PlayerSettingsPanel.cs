using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingsPanel : MonoBehaviour
{
    public PlayerAccount currentPlayerAccount;

    public Color currentPlayerColor;
    public Color copyCurrentColor;

    [Header("View Settings")]
    public Image backgroundImage;

    public TextMeshProUGUI viewName;
    public string playerName;

    [Header("View Player Icon")]
    public List<Sprite> iconsList = new List<Sprite>();
    public Sprite currentIcon;

    public Image iconImage;
    public int currentIconNumber = -1;

    public Button iconButton;
    public Button playerColorButton;
    public Button destroyPanelButton;

    public TextMeshProUGUI prompt;
    public bool promtIsActive = true;


    public void Start()
    {
        viewName.text = "Your name is: ";
        iconButton.onClick.AddListener(() => CharacterIconChangeSwitch());
        playerColorButton.onClick.AddListener(() => SetPlayerColor());
        destroyPanelButton.onClick.AddListener(() => DestroyCurrentPanel());

        copyCurrentColor = currentPlayerColor;

        CharacterIconChangeSwitch();
        PanelInitialization();
    }

    public void PanelInitialization()
    {
        currentPlayerAccount.playerName = playerName;
        currentPlayerAccount.playerIcon = currentIcon;
        currentPlayerAccount.playerColor = currentPlayerColor;
    }

    public void SetPlayerColor()
    {
        PlayerMenuManager.playerMenuManager.SetPlayerColor(this);
        backgroundImage.color = currentPlayerColor;
    }

    public void ColorReturn()
    {
        PlayerMenuManager.playerMenuManager.ColorReturn(this);
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
        PanelInitialization();
    }

    public void DestroyCurrentPanel()
    {
        ColorReturn();
        PlayerMenuManager.playerMenuManager.RemovePlayer(this);
    }

    public void NameInitialization(InputField currentInputField)
    {
        playerName = currentInputField.text;
        viewName.text = "Your name is: " + playerName;

        PanelInitialization();
    }
}
