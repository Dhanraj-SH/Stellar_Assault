using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour {
    [SerializeField] private Image pauseImage;
    [SerializeField] private Image resumeImage;

    private float originalTimeScale;

    private void Start() {
        originalTimeScale = Time.timeScale;
    }

    public void TogglePause() {
        if (GameController.Instance.IsGamePaused) {
            ResumeGame();
        } else {
            PauseGame();
        }
    }

    public void PauseGame() {
        Time.timeScale = 0;
        pauseImage.gameObject.SetActive(false);
        resumeImage.gameObject.SetActive(true);
        GameController.Instance.IsGamePaused = true;
    }

    public void ResumeGame() {
        Time.timeScale = originalTimeScale;
        pauseImage.gameObject.SetActive(true);
        resumeImage.gameObject.SetActive(false);
        GameController.Instance.IsGamePaused = false;
    }
}
