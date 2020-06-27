using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int score = 0;

    public static GameManager instance;

    public GameObject ScoreTextGO;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        ScoreTextGO.GetComponent<Text>().text = "Score: " + score;
    }

}
