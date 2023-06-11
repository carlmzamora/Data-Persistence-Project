using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntryRow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI number;
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private TextMeshProUGUI score;

    public void UpdateInfo(int number, string playerName, int score)
    {
        this.number.text = number.ToString();
        this.playerName.text = playerName;
        this.score.text = score.ToString();
    }
}
