using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class GameOverMenu : MonoBehaviour
{
    private void Start()
    {
        GameManager.OnGameOver += ShowMenu;
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameManager.OnGameOver -= ShowMenu;
    }

    private void ShowMenu()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
        GameManager.Pause();
    }
}