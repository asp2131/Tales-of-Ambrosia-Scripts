using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    #region Singleton
    public static DialogManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject dialogBox;

    public GameObject[] dialogOptions;

    public bool dialogActive;

    public string dialogText;

    string[] dialogPages;

    public int dialogIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(false);
        for (int i = 0; i < dialogOptions.Length; i++)
        {
            dialogOptions[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() { }

    public void ShowDialog(string npc, string[] dialogMesssage)
    {
        dialogPages = dialogMesssage;
        print(npc + ": " + dialogMesssage);
        //access first child of dialogBox
        dialogBox.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = npc;
        if (dialogIndex <= dialogPages.Length - 1)
        {
            dialogBox.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text =
                dialogMesssage[dialogIndex];
            dialogActive = true;
            dialogBox.SetActive(true);
        }
        else
        {
            dialogBox.SetActive(false);
            // dialogIndex = 0;
        }
        // dialogOptions[0].SetActive(true);
    }

    public void NextDialog()
    {
        print(dialogPages.Length);
        // if (dialogIndex >= dialogPages.Length - 1)
        // {
        //     dialogBox.SetActive(false);
        //     dialogIndex = 0;
        //     return;
        // }
        dialogIndex++;
    }
}
