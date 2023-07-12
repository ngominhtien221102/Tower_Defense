using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [SerializeField] public Canvas PausePanel;

    public void Pause()
    {
        Time.timeScale = 0f;
        PausePanel.gameObject.SetActive(true);
    }

    public void continute()
    {
        Time.timeScale = 1f;
        PausePanel.gameObject.SetActive(false);
    }

}
