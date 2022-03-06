using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMenuManager : MonoBehaviour
{
    public static PlayerMenuManager playerMenuManager;
    [Header("Player list settings")]
    public int minimumNimberOfPlayers = 2;
    public int maximumNumberOfPlayers = 6;
    public List<PlayerAccount> playerAccountList = new List<PlayerAccount>();
    public List<GameObject> playerAccountPanel = new List<GameObject>();

    [Header("Player content settings")]
    public GridLayoutGroup group;
    public GameObject playerPanel;

    public GameObject addPlayerPanel;
    public Button addPlayerButton;

    public void Awake()
    {
        playerMenuManager = this;

        availableColors.Clear();
        availableColors.AddRange(colorsList);
    }

    public void Start()
    {
        addPlayerButton.onClick.AddListener(()=> AddPlayer());
        addPlayerPanel.transform.SetSiblingIndex(playerAccountList.Count);
    }

    public void StartGameButton()
    {
        if (playerAccountList.Count < minimumNimberOfPlayers || playerAccountList.Count > maximumNumberOfPlayers)
            return;

        for(int i = 0; i < playerAccountList.Count; i++)
        {
            playerAccountList[i].playerNumber = i;
        }
    }
    [Header("Coloring settings")]
    public List<Color> colorsList = new List<Color>();
    public int currentColorNumber = 0;

    public List<Color> availableColors = new List<Color>();
    public void SetPlayerColor(PlayerSettingsPanel playerPanel)
    {
        currentColorNumber += 1;
        if (currentColorNumber >= availableColors.Count)
            currentColorNumber = 0;

        Color oldColor = playerPanel.currentPlayerColor;

        //if(currentColorNumber == 0)
        //    playerPanel.currentPlayerColor = availableColors[availableColors.Count - 1];
        //else
        if (availableColors.Remove(Color.white))
            Debug.Log("Remove");
        
        if (currentColorNumber >= availableColors.Count)
            currentColorNumber = 0;

        playerPanel.currentPlayerColor = availableColors[currentColorNumber];
        
        availableColors.Remove(availableColors[currentColorNumber]);
        availableColors.Add(oldColor);

    }

    public void ColorReturn(PlayerSettingsPanel playerPanel)
    {
        //if(availableColors.Remove(playerPanel.copyCurrentColor))
        //{
        //    Debug.Log("Color");
            availableColors.Add(playerPanel.currentPlayerColor);
        //}
    }

    public void RemovePlayerColor()
    {

    }

    [ContextMenu("Add Player")]
    public void AddPlayer()
    {
        if (playerAccountPanel.Count - 1> maximumNumberOfPlayers)
            return;

        //  создание панели
        GameObject newPanel = Instantiate(playerPanel, group.transform);
        playerAccountPanel.Add(newPanel);
        //  создаание аккаунта
        PlayerAccount newPlayer = new PlayerAccount();
        newPlayer.playerNumber = playerAccountList.Count;
        playerAccountList.Add(newPlayer);
        //  добавление аккаунта в панель
        PlayerSettingsPanel playerSettings = newPanel.GetComponent<PlayerSettingsPanel>();
        playerSettings.currentPlayerAccount = newPlayer;

        addPlayerPanel.SetActive(true);
        addPlayerPanel.transform.SetSiblingIndex(playerAccountPanel.Count);

        if (playerAccountPanel.Count > maximumNumberOfPlayers + 1)
            addPlayerPanel.SetActive(false);
        else
            addPlayerPanel.SetActive(true);
    }

    [ContextMenu("Remove Last Player")]
    public void RemovePlayer(PlayerSettingsPanel playerPanel)
    {
        if (playerAccountPanel.Count < 1)
            return;

        Debug.Log(playerPanel.gameObject);
        playerAccountList.Remove(playerPanel.currentPlayerAccount);

        playerAccountPanel.Remove(playerPanel.gameObject);
        Destroy(playerPanel.gameObject);


        if (playerAccountPanel.Count > maximumNumberOfPlayers + 1)
            addPlayerPanel.SetActive(false);
        else
            addPlayerPanel.SetActive(true);
    }
    
    //[ContextMenu("Remove All")]
    //public void RemoveallPlayers()
    //{
    //    playerList.Clear();
    //}
}
