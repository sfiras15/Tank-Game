using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    private GameObject player;

    public static GameManager instance;

    [SerializeField] private RawImage defeatImage;
    [SerializeField] private RawImage victoryImage;
    [SerializeField] private float restartTime = 3f;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Defeat()
    {
        StartCoroutine(ShowDefeatScreen());
    }

    private IEnumerator ShowDefeatScreen()
    {
        Debug.Log("reload");
        //defeatImage.gameObject.SetActive(true);
        float currentTime = 0f;
        while (currentTime < restartTime)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, currentTime / restartTime);
            defeatImage.color = new Color(1f, 1f, 1f, alpha);
            //Debug.Log(logo.color);
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Victory()
    {
        StartCoroutine(ShowVictoryScreen());
    }
    private IEnumerator ShowVictoryScreen()
    {
        Debug.Log("reload");
        //victoryImage.gameObject.SetActive(true);
        float currentTime = 0f;
        while (currentTime < restartTime)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, currentTime / restartTime);
            victoryImage.color = new Color(1f, 1f, 1f, alpha);
            //Debug.Log(logo.color);
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
