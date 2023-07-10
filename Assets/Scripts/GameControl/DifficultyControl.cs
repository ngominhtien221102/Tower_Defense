using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultyControl : MonoBehaviour
{
    [SerializeField]
    private Button easyButton;
    [SerializeField]
    private Button mediumButton;
    [SerializeField]
    private Button hardButton;
    [SerializeField]
    private Button homeButton;
    // Start is called before the first frame update
    void Start()
    {
        // Sử dụng hàm onClick.AddListener để gán hàm xử lý sự kiện click cho các button
        easyButton.onClick.AddListener(SetEasyDifficulty);
        mediumButton.onClick.AddListener(SetMediumDifficulty);
        hardButton.onClick.AddListener(SetHardDifficulty)
        homeButton.onClick.AddListener(BackToHomeSence);
    }

    void SetEasyDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", 0);
        SceneManager.LoadScene("Map1");
    }

    void SetMediumDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", 1);
        SceneManager.LoadScene("Map1");
    }

    void SetHardDifficulty()
    {
        PlayerPrefs.SetInt("Difficulty", 2);
        SceneManager.LoadScene("Map1");
    }

    void BackToHomeSence()
    {
        SceneManager.LoadScene("StartSence");
    }
}
