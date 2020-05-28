using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormManager : MonoBehaviour
{
    public GameObject formScreen;

    static public FormManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FormButtonPressed()
    {
        OpenForm();
    }

    public void OpenForm()
    {
        // show form screen
        formScreen.SetActive(true);
        // de-activate clues, pins and camera navigation
        ClueManager.Instance.SetClueState(Clue.ClueStates.Frozen);
        PinManager.Instance.SetPinsInteractable(false);
        Camera.main.GetComponent<CameraDragMove>().canNavigate = false;
    }

    public void CloseForm()
    {
        // hide form screen
        formScreen.SetActive(false);
        // activate clues, pins and camera navigation
        ClueManager.Instance.SetClueState(Clue.ClueStates.Organize);
        PinManager.Instance.SetPinsInteractable(true);
        Camera.main.GetComponent<CameraDragMove>().canNavigate = true;
    }
}
