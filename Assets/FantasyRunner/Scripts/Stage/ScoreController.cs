using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour 
{
    [SerializeField] private CumulativeUIBaseController _starsController;
    [SerializeField] private TextMesh scoreText;

    private int score = 0;

    private void Start()
    {
        this.AddScore(0);
    }
   
    public void AddScore(int deltaScore)
    {
        score += deltaScore;
        scoreText.text = score.ToString();
        //TODO: Check Stars
    }
}
