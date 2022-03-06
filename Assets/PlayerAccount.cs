using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerAccount
{
    [Header("Player data")]
    public string playerName;
    public int playerNumber;
    public Sprite playerIcon;
    public Color playerColor;
    
    public List<Building> playerBuildings = new List<Building>();
}
