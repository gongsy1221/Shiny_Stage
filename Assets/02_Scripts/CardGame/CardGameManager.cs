using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using System.IO;



public class CardGameManager : MonoBehaviour
{
    
    

    public static CardGameManager instance;
    private List<Card> allCards;
    Board board;

    private Card flippedCard;

    private bool isFlipping = false;

    [SerializeField] private Slider timeOutSlider;
    [SerializeField] private TextMeshProUGUI timeOutText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject restartButton;
    [SerializeField] TextMeshProUGUI scoreOne;
    [SerializeField] TextMeshProUGUI scoreTwo;
    private bool isGameOver = false;
    public bool secondGame = false;

    [SerializeField] private float timeLimit = 60f;
    private float currentTime;

    private int totalMatches = 9;
    private int matchesFound = 0;

    private int firstScore;
    private int secondScore;

    [SerializeField] TextMeshProUGUI firstGameScore;
    [SerializeField] TextMeshProUGUI secondGameScore;
    private int firstSetScore;
    private int secondSetScore;

    public int endNum = 0;

    [SerializeField] TextMeshProUGUI turntext;
    public bool myTurn = true;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        
    }

    private void Start()
    {
        board = FindObjectOfType<Board>();
        allCards = board.GetCards();

        currentTime = timeLimit;
        SetCurrentTimeText();
        StartCoroutine("FlipAllCardsRoutine");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            currentTime = 1;
        }
    }

    private void SetCurrentTimeText()
    {
        int timeSec = Mathf.CeilToInt(currentTime);
        timeOutText.SetText(timeSec.ToString());
    }

    IEnumerator FlipAllCardsRoutine()
    {
        isFlipping = true;
        yield return new WaitForSeconds(0.5f);
        FlipAllCards();
        yield return new WaitForSeconds(3f);
        FlipAllCards();
        yield return new WaitForSeconds(0.5f);
        isFlipping = false;

        yield return StartCoroutine("CountDownTimerRoutine");
    }

    IEnumerator CountDownTimerRoutine()
    {
        while(currentTime>0)
        {
            currentTime -= Time.deltaTime;
            timeOutSlider.value = currentTime / timeLimit;
            SetCurrentTimeText();
            yield return null;
        }

        GameOver(false);
    }

    void FlipAllCards()
    {
        foreach (Card card in allCards)
        {
            card.FlipCard();
        }
    }

    public void CardClicked(Card card)
    {
        if(isFlipping || isGameOver)
        {
            return;
        }

        card.FlipCard();

        if(flippedCard == null)
        {
            flippedCard = card;
        }
        else
        {
            StartCoroutine(CheckMatchRoutine(flippedCard, card));
        }
    }

    IEnumerator CheckMatchRoutine(Card card1, Card card2)
    {
        if(card1.cardID == card2.cardID)
        {
            card1.SetMatched();
            card2.SetMatched();
            matchesFound++;

            if(myTurn)
            {
                firstScore += 10;
            }
            else if(!myTurn)
            {
                secondScore += 10;
            }

            if(matchesFound == totalMatches)
            {
                GameOver(true);
            }
        }
        else
        {
            yield return new WaitForSeconds(1f);

            card1.FlipCard();
            card2.FlipCard();

            yield return new WaitForSeconds(0.4f);
        }

        if (myTurn)
        {
            myTurn = false;
            turntext.text = "±èÀ±";
        }
        else if (!myTurn)
        {
            myTurn = true;
            turntext.text = "ÃÖ¹Î¿ì";
        }

        isFlipping = false;
        flippedCard = null;
    }

    void GameOver(bool success)
    {
        StopCoroutine("CountDownTimerRoutine");

        if (!isGameOver)
        {
            isGameOver = true;

            scoreOne.text = firstScore.ToString();
            scoreTwo.text = secondScore.ToString();

            Invoke("ShowGameOverPanel", 2f);
        }
    }

    void ShowGameOverPanel()
    {
        GameEndEvent();
        gameOverPanel.SetActive(true);
        secondGame = true;
    }

    public void GameRestart()
    {
        SceneManager.LoadScene("MemoryGame");
    }

    public void GameEndEvent()
    {
        if (File.Exists(CardGameData.instance.path + "ScoreData"))
        {
            CardGameData.instance.LoadData();

            firstSetScore = CardGameData.instance.nowPlayer.cmwSetScore;
            secondSetScore = CardGameData.instance.nowPlayer.kySetScore;
        }

        if (firstScore > secondScore)
        {
            firstSetScore++;
            firstGameScore.text = firstSetScore.ToString();
        }
        else if (firstScore < secondScore)
        {
            secondSetScore++;
            secondGameScore.text = secondSetScore.ToString();
        }

        CardGameData.instance.nowPlayer.cmwSetScore = firstSetScore;
        CardGameData.instance.nowPlayer.kySetScore = secondSetScore;
        CardGameData.instance.SaveData();

        if (firstSetScore >= 3 || secondSetScore >= 3)
        {
            MySceneManager.Instance.ChangeScene("04_LastStage", "Main Stage\n~ÃÖÁ¾ ½ÂÀÚ~");
        }
    }
}