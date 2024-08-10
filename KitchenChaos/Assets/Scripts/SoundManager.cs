using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    private const string PLAYER_PREFS_SOUND_EFFECT_VOLUME = "SoundEffectsVolume";

    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO audioClipRefsSo;

    private float volume = 1f;

    private void Awake() {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECT_VOLUME, 1f);
    }

    private void Start() {
        DeliveryManager.Instance.OnRecipeDelivered += DeliveryManager_Delivered;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_Failed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomething += Player_OnPickedSomething;
        BaseCounter.OnAnyDropedItem += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyTrashedItem += TrashCounter_OnItemDestroyed;
    }

    private void TrashCounter_OnItemDestroyed(object sender, EventArgs e) {
        TrashCounter trashcounter = sender as TrashCounter;
        PlaySound(audioClipRefsSo.trash, trashcounter.gameObject.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e) {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSo.objectDrop, baseCounter.gameObject.transform.position);
    }

    private void Player_OnPickedSomething(object sender, EventArgs e) {
        PlaySound(audioClipRefsSo.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e) {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSo.chop, cuttingCounter.gameObject.transform.position);
    }

    private void DeliveryManager_Failed(object sender, EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSo.deliveryFailed, deliveryCounter.transform.position);
    }

    private void DeliveryManager_Delivered(object sender, EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSo.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    private void PlaySound(AudioClip[] audioClips, Vector3 position, float volumeMultiplier = 1f) {
        AudioSource.PlayClipAtPoint(audioClips[UnityEngine.Random.Range(0, audioClips.Length)], position, volumeMultiplier * volume);
    }

    public void PlayFootstepSounds(Vector3 position, float volume = 1f) {
        PlaySound(audioClipRefsSo.footsteps, position, volume);
    }

    public void PlayCountdownSounds() {
        PlaySound(audioClipRefsSo.warning, Vector3.zero);
    }

    public void ChangeVolume() {
        volume += 0.1f;

        if (volume > 1f) {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECT_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() {
        return volume;
    }

}
