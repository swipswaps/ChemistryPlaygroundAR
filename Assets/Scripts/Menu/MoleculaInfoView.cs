using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MoleculaInfoView : MonoBehaviour {

    [SerializeField]
    private GameObject root;
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private TextMeshProUGUI infoText;
    [SerializeField]
    private Image image;

    void Start()
    {
        FindObjectOfType<MoleculesController>().onMoleculaFound += ShowMoleculaInfo;
    }

	public void ShowMoleculaInfo(MoleculaInfo moleculaInfo)
    {
        if (moleculaInfo != null)
        {
            root.SetActive(true);
            titleText.text = moleculaInfo.title;
            infoText.text = moleculaInfo.info;
            image.sprite = moleculaInfo.sprite;
        }
        else
        {
            root.SetActive(false);
        }
    }


    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
