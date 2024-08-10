using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI KeyUpText;
    [SerializeField] private TextMeshProUGUI KeyDownText;
    [SerializeField] private TextMeshProUGUI KeyLeftText;
    [SerializeField] private TextMeshProUGUI KeyRightText;
    [SerializeField] private TextMeshProUGUI KeyInteractText;
    [SerializeField] private TextMeshProUGUI KeyAltInteractText;
    [SerializeField] private TextMeshProUGUI KeyPauseText;

    [SerializeField] private TextMeshProUGUI GamepadInteractText;
    [SerializeField] private TextMeshProUGUI GamepadInteractAltText;
    [SerializeField] private TextMeshProUGUI GamepadPauseText;

    private void Start() {
        GameManager.Instance.OnStateChange += GameManager_OnStateChanged;

        UpdateVisual();

        Show();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e) {
        if (GameManager.Instance.IsCountdownActive()) {
            Hide();
        }
    }

    private void UpdateVisual() {
        KeyUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        KeyDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        KeyLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        KeyRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);

        KeyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        KeyAltInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_alt);
        KeyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);

        GamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        GamepadInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlt);
        GamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}
