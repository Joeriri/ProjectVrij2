using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Button formButton;
    public Button pinButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartFormButtonTutorial()
    {
        formButton.onClick.AddListener(FormButtonClicked);
        formButton.GetComponent<Canvas>().sortingOrder = 1;
    }

    void FormButtonClicked()
    {
        EndFormButtonTutorial();
    }

    void EndFormButtonTutorial()
    {
        formButton.onClick.RemoveListener(FormButtonClicked);
    }
}
