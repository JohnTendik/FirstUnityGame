using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionsMenuUI : MonoBehaviour {
    public static GameOptionsMenuUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeMenuButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button InteractAltButton;
    [SerializeField] private Button PauseButton;
    [SerializeField] private Button Gamepad_Interact_Button;
    [SerializeField] private Button Gamepad_InteractAlt_Button;
    [SerializeField] private Button Gamepad_Pause_Button;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamepad_interact_text;
    [SerializeField] private TextMeshProUGUI gamepad_interact_alt_text;
    [SerializeField] private TextMeshProUGUI gamepad_pause_text;
    [SerializeField] private Transform RebindKeyUI;

    private Action onCloseButtonAction;

    private void Start() {
        GameManager.Instance.OnGameUnpause += GameManager_OnGameUnpause;

        UpdateVisual();

        Hide();
        HideRebindKeyUI();
    }

    private void GameManager_OnGameUnpause(object sender, EventArgs e) {
        Hide();
    }

    private void Awake() {
        Instance = this;

        soundEffectsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        closeMenuButton.onClick.AddListener(() => {
            Hide();
            onCloseButtonAction?.Invoke();
        });

        moveUpButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Up);
        });

        moveDownButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Down);
        });

        moveLeftButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Left);
        });

        moveRightButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Move_Right);
        });

        interactButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Interact);
        });

        InteractAltButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Interact_alt);
        });

        PauseButton.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Pause);
        });

        Gamepad_Interact_Button.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Gamepad_Interact);
        });

        Gamepad_InteractAlt_Button.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Gamepad_InteractAlt);
        });

        Gamepad_Pause_Button.onClick.AddListener(() => {
            RebindBinding(GameInput.Binding.Gamepad_Pause);
        });
    }


    private void UpdateVisual() {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);

        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_alt);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);

        gamepad_interact_text.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepad_interact_alt_text.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlt);
        gamepad_pause_text.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);

    }

    public void Show(Action onCloseButtonAction) {
        this.onCloseButtonAction = onCloseButtonAction;

        gameObject.SetActive(true);
        soundEffectsButton.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void ShowRebindKeyUI() {
        RebindKeyUI.gameObject.SetActive(true);
    }

    private void HideRebindKeyUI() {
        RebindKeyUI.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding newBinding) {
        ShowRebindKeyUI();
        GameInput.Instance.RebindBinding(newBinding, () => {
            HideRebindKeyUI();
            UpdateVisual();
        });
    }
}
