using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public CanvasGroup Fade_img;
    float fadeDuration = 2;

    public TextMeshProUGUI endingText;
    float speed = 0.2f;

    public GameObject Loading;
    public TextMeshProUGUI Loading_text;

    public bool changeScene;
    public bool typingEnd;


    public static MySceneManager Instance
    {
        get
        {
            return instance;
        }
    }

    private static MySceneManager instance;

    void Start()
    {
        changeScene = false;
        typingEnd = false;
        endingText.enabled = false;

        if (instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Fade_img.DOFade(0, fadeDuration)
        .OnStart(() => {
            Loading.SetActive(false);
        })
        .OnComplete(() => {
            Fade_img.blocksRaycasts = false;
        });
    }

    public void ChangeScene(string sceneName)
    {
        Fade_img.DOFade(1, fadeDuration)
        .OnStart(() => {
            Fade_img.blocksRaycasts = true;
        })
        .OnComplete(() => {
            StartCoroutine("LoadScene", sceneName);
            SoundManager.instance.PlaySound("Check", 1);
            changeScene = true;
        });
    }

    public void EndingCredit(string endingName)
    {
        Fade_img.DOFade(1, fadeDuration)
        .OnStart(() => {
            Fade_img.blocksRaycasts = true;
            StartCoroutine(Typing(endingName));
        })
        .OnComplete(() => {
            endingText.enabled = false;
            StartCoroutine("LoadScene", "00_StartMenu");
            SoundManager.instance.PlaySound("Check", 1);
            changeScene = true;
        });
    }

    IEnumerator LoadScene(string sceneName)
    {
        Loading.SetActive(true);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;

        float past_time = 0;
        float percentage = 0;

        while (!(async.isDone))
        {
            yield return null;

            past_time += Time.deltaTime;

            if (percentage >= 90)
            {
                percentage = Mathf.Lerp(percentage, 100, past_time);

                if (percentage == 100)
                {
                    async.allowSceneActivation = true;
                }
            }
            else
            {
                percentage = Mathf.Lerp(percentage, async.progress * 100f, past_time);
                if (percentage >= 90) past_time = 0;
            }
            Loading_text.text = percentage.ToString("0") + "%";
        }
    }

    IEnumerator Typing(string message)
    {
        endingText.enabled = true;

        for (int i = 0; i < message.Length; i++)
        {
            endingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }
        typingEnd = true;
    }
}
