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

    public bool wearHead = false;
    public bool wearSaber = false;

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
            2 => 3,
            3 => 8,
            8 => 7,
            7 => 9,
            9 => 10,
            10 => 11,
            _ => 0,
        };

        if (activeScene != 7 && activeScene != 9)
            FadeOut();

        if (activeScene == 7)
        {
            GoToScene(activeScene);
            sceneCanvas[activeScene].GetComponent<Scene_07>().AutoSwitch();
        }

        if (activeScene == 9)
        {
            GoToScene(activeScene);
            sceneCanvas[activeScene].GetComponent<Scene_09>().Init();
        }

        if (activeScene == 10)
        {
            sceneCanvas[activeScene].GetComponent<Scene_10>().Init();
        }
    }

    public void Button_10()
    {
        EndScene(10);
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
