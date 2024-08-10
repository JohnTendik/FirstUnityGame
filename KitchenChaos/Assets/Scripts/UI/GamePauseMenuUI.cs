using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseMenuUI : MonoBehaviour {

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsMenuButton;

    private void Awake() {
        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.TogglePauseGame();
        });

        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.GameStartScene);
        });

        optionsMenuButton.onClick.AddListener(() => {
            Hide();
            GameOptionsMenuUI.Instance.Show(Show);
        });
    }

    private void Start() {
        GameManager.Instance.OnGamePause += GameManager_OnGamePause;
        GameManager.Instance.OnGameUnpause += GameManager_OnGameUnpause;
        Hide();
    }

    private void GameManager_OnGameUnpause(object sender, EventArgs e) {
        Hide();
    }

    private void GameManager_OnGamePause(object sender, EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
        resumeButton.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
