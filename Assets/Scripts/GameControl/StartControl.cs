using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartControl : MonoBehaviour
{

    [SerializeField] public Button startButton;

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
