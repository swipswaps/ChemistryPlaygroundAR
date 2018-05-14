using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using SubjectNerd.Utilities;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private string sandboxSceneName = "SceneSandbox";

    [SerializeField]
    private string testSceneName = "SceneTest";

    [SerializeField]
    private GameObject mainMenuScreenGameObject;

    [SerializeField]
    private GameObject aboutScreenGameObject;

    [SerializeField]
    private GameObject testScreenGameObject;

    [SerializeField, Reorderable]
    private List<MoleculaScriptableObject> gases;

    [SerializeField, Reorderable]
    private List<MoleculaScriptableObject> aroundUs;

    [SerializeField, Reorderable]
    private List<MoleculaScriptableObject> acids;

    [Inject, HideInInspector]
    public ZenjectSceneLoader _sceneLoader;

    public void OnStartSandboxClicked()
    {
        SceneManager.LoadScene(sandboxSceneName);
    }

    public void OnStartGasesTestClicked()
    {
        LaunchTest(gases);
    }

    public void OnStartAroundUsTestClicked()
    {
        LaunchTest(aroundUs);
    }

    public void OnAcidsTestClicked()
    {
        LaunchTest(acids);
    }

    public void LaunchTest(List<MoleculaScriptableObject> molecules)
    {
        _sceneLoader.LoadScene(testSceneName, LoadSceneMode.Single, (container) =>
        {
            container.BindInstance(molecules).WhenInjectedInto<TestSceneIntaller>();
        });
    }

    public void OnAboutClicked()
    {
        aboutScreenGameObject.SetActive(true);
        mainMenuScreenGameObject.SetActive(false);
    }

    public void OnMenuClicked()
    {
        testScreenGameObject.SetActive(false);
        aboutScreenGameObject.SetActive(false);
        mainMenuScreenGameObject.SetActive(true);
    }

    public void OnTestClicked()
    {
        testScreenGameObject.SetActive(true);
        mainMenuScreenGameObject.SetActive(false);
    }
}
