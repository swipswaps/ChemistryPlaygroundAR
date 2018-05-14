using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Common;
using TMPro;
using Zenject;
using UnityEngine.SceneManagement;

public class TestSceneController : MonoBehaviour
{
    [Header("Modals")]
    [SerializeField]
    private GameObject startModal;
    [SerializeField]
    private TextMeshProUGUI modalFirstSubstance;
    [SerializeField]
    private TextMeshProUGUI modalSecondSubstance;
    [SerializeField]
    private TextMeshProUGUI modalThirdSubstance;
    [SerializeField]
    private GameObject endWinModal;
    [SerializeField]
    private GameObject endLooseModal;

    [Header("BottomPanel")]
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private TextMeshProUGUI firstSubstanceText;
    [SerializeField]
    private GameObject firstSubstanceCheckbox;
    [SerializeField]
    private TextMeshProUGUI secondSubstanceText;
    [SerializeField]
    private GameObject secondSubstanceCheckbox;
    [SerializeField]
    private TextMeshProUGUI thirdSubstanceText;
    [SerializeField]
    private GameObject thirdSubstanceCheckbox;

    [Header("Random")]
    [SerializeField]
    private string menuScene;
    [SerializeField]
    private int seconds;

    private TestSceneState state;

    private Timer timer;

    [Inject, HideInInspector]
    public List<MoleculaInfo> molecules;

    // Use this for initialization
    void Start()
    {
        startModal.SetActive(true);
        molecules.Zip(Extensions.listOf(modalFirstSubstance, modalSecondSubstance, modalThirdSubstance))
            .Zip(Extensions.listOf(firstSubstanceText, secondSubstanceText, thirdSubstanceText),
            (tuple, arg) => Extensions.tupled(tuple.first, tuple.second, arg))
            .ForEach((tuple) =>
            {
                tuple.second.text = tuple.first.title;
                tuple.third.text = tuple.first.title;
            });
    }

    public void StartTest()
    {
        if (state != TestSceneState.PROLOGUE)
        {
            return;
        }
        state = TestSceneState.PLAYABLE;

        FindObjectOfType<MoleculesController>().onMoleculaFound += OnMoleculaFound;

        startModal.SetActive(false);

        timer = gameObject.AddComponent<Timer>();
        timer.seconds = seconds;
        timer.timerStepAction += (elapsed) => { timerText.text = string.Format("{0:0}:{1:00}", elapsed / 60, elapsed % 60); };
        timer.timerEndAction += EndTestLoose;
    }

    public void EndTestLoose()
    {
        if (state != TestSceneState.PLAYABLE)
        {
            return;
        }
        state = TestSceneState.EPILOGUE;

        endLooseModal.SetActive(true);
    }

    public void EndTestWin()
    {
        if (state != TestSceneState.PLAYABLE)
        {
            return;
        }
        state = TestSceneState.EPILOGUE;

        endWinModal.SetActive(true);
    }

    public void OnMoleculaFound(MoleculaInfo molecula)
    {
        if (state != TestSceneState.PLAYABLE)
        {
            return;
        }
        var index = molecules.IndexOf(molecula);
        switch (index)
        {
            case 0:
                firstSubstanceCheckbox.SetActive(true);
                break;
            case 1:
                secondSubstanceCheckbox.SetActive(true);
                break;
            case 2:
                thirdSubstanceCheckbox.SetActive(true);
                break;
            default:
                break;
        }
        if (Extensions.listOf(firstSubstanceCheckbox, secondSubstanceCheckbox, thirdSubstanceCheckbox).All((go) => go.activeSelf))
        {
            EndTestWin();
        }
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

    private enum TestSceneState
    {
        PROLOGUE,
        PLAYABLE,
        EPILOGUE
    }
}
