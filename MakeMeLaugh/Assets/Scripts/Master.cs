using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Master : MonoBehaviour
{
    public GameObject[] sceneCanvas;

    public int activeScene = 0;
    [HideInInspector] public Vector2 mouse;

    public GameState state;
    public enum GameState
    {
        Transition_A, Transition_B, Scene
    }

    [SerializeField] RawImage whiteScreen;
    float fadeDuration = 0.5f;

    void Awake()
    {
        Common.Init();
        whiteScreen.gameObject.SetActive(false);
    }

    void Start()
    {
        activeScene = 1;
        GoToScene(activeScene);
    }

    void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Disable player input during transition
        bool inTransition = (state == GameState.Transition_A || state == GameState.Transition_B);
        sceneCanvas[activeScene].GetComponent<CanvasGroup>().interactable = !inTransition;
    }

    void GoToScene(int index)
    {
        activeScene = index;
        Method.ArraySetActive(sceneCanvas, activeScene);
    }

    public void EndScene(int index)
    {
        activeScene = index switch
        {
            1 => 2,
            2 => 8,
            _ => 0,
        };
        FadeOut();
    }

    void FadeOut()
    {
        state = GameState.Transition_A;
        whiteScreen.gameObject.SetActive(true);
        whiteScreen.DOFade(1, fadeDuration).SetEase(Ease.Linear).OnComplete(FadeOut_End);

        void FadeOut_End()
        {
            whiteScreen.gameObject.SetActive(false);
            GoToScene(activeScene);
            FadeIn();
        }
    }

    void FadeIn()
    {
        state = GameState.Transition_B;
        whiteScreen.gameObject.SetActive(true);
        whiteScreen.color = Color.white;
        whiteScreen.DOFade(0, fadeDuration).SetEase(Ease.Linear).OnComplete(FadeIn_End);

        void FadeIn_End()
        {
            whiteScreen.gameObject.SetActive(false);
            state = GameState.Scene;
        }
    }
}
