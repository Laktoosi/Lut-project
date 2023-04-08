using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    public TMP_Text scoreText;
    public int score = 0;

    [SerializeField]
    private TextMeshProUGUI inputScore;
    [SerializeField]
    private TMP_InputField inputName;
    GameOver gameover;

    // Start is called before the first frame update
    void Start()
    {
        gameover = GameObject.Find("GameOver").GetComponent<GameOver>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameover.gameIsOver == false)
            scoreText.text = score.ToString();
    }

    public UnityEvent<string, int> SubmitScoreEvent;
    public void SubmitScore()
    {
        SubmitScoreEvent.Invoke(inputName.text, int.Parse(inputScore.text));
    }
}
