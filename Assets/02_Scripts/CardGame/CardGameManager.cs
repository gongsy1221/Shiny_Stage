using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

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

        isFlipping = false;
        flippedCard = null;
    }

    void GameOver(bool success)
    {
        StopCoroutine("CountDownTimerRoutine");

        if (!isGameOver)
        {
            isGameOver = true;

            if (!secondGame)
            {
                if (success)
                {
                    firstScore = 10 + 100 / Mathf.CeilToInt(currentTime);
                }
                else
                {
                    firstScore = matchesFound;
                }
                scoreOne.text = firstScore.ToString();
            }
            else if (secondGame)
            {
                if (success)
                {
                    secondScore = 10 + 100 / Mathf.CeilToInt(currentTime);
                }
                else
                {
                    secondScore = matchesFound;
                }
                scoreTwo.text = secondScore.ToString();
            }

            Invoke("ShowGameOverPanel", 2f);
        }
    }

    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        if(secondGame)
        {
            restartButton.SetActive(false);
        }
        secondGame = true;
    }

    public void GameRestart()
    {
        currentTime = timeLimit;
        matchesFound = 0;
        board.GameSetting();
        gameOverPanel.SetActive(false);
        StartCoroutine("CountDownTimerRoutine");
    }
}