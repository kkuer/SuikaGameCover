using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //set gamemanager instance so other scripts can talk to it
    public static GameManager gameManagerInstance {  get; private set; }

    //game state
    public int score = 0;

    public bool gameActive;

    //UI
    public TMP_Text scoreText;
    public TMP_Text finalScoreText;
    public GameObject endScreen;

    //sound effects
    public AudioSource SFX_POP;

    void Awake()
    {
        //assign gamemanager instance
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
        //update score UI
        scoreText.text = score.ToString();

        //lose condition
        if (!gameActive)
        {
            Time.timeScale = 0f;
            endScreen.SetActive(true);
            finalScoreText.text = score.ToString();
        }
    }

    public void addScore(int scoreToAdd)
    {
        //add new score amount to current score
        SFX_POP.Play();
        score += scoreToAdd;
    }
}
