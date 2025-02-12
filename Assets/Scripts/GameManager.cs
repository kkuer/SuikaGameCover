using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //set gamemanager instance so other scripts can talk to it
    public static GameManager gameManagerInstance {  get; private set; }

    //numerics
    public int score = 0;

    //UI
    public TMP_Text scoreText;

    void Awake()
    {
        if (gameManagerInstance == null)
        {
            gameManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        scoreText.text = score.ToString();
    }

    public void addScore(int scoreToAdd)
    {
        //add new score amount to current score
        score += scoreToAdd;
    }
}
