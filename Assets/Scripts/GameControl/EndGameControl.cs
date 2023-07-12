using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameControl : MonoBehaviour
{

    [SerializeField] public Button homeButton;
    [SerializeField] public Button replayButton;
    // Start is called before the first frame update
    void Start()
    {
        homeButton.onClick.AddListener(returnHome);
        replayButton.onClick.AddListener(rePlay);
    }

    public void returnHome()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void rePlay()
    {
        //SceneManager.LoadScene("Map1");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;

    }
}
