using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    public GameObject GameOverPanel;
    [Header("—чет")]
    [SerializeField] private TextMeshProUGUI Score;
    int ScoreNum = 0;

    void Awake()
    {
        ScoreGame(0);
        GameOverPanel.SetActive(false);
    }

    public void ScoreGame(int Cost)
    {
        ScoreNum += Cost;
        Score.text = ScoreNum.ToString();
    }
}
