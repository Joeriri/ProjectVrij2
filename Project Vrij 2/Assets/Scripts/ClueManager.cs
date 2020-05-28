using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClueManager : MonoBehaviour
{
    public LayerMask cluesLayer;
    public List<Clue> clueSortation = new List<Clue>();

    [Header("Organizing")]
    public float minDistanceBeforeDrag = 5f;
    public GameObject itemViewer;
    public Image itemViewImage;

    [Header("Pinning")]
    //public GameObject pinPrefab;
    //public Pin draggedPin;

    Clue[] allClues;

    // Singleton
    static public ClueManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // verzamel alle clues
        allClues = FindObjectsOfType<Clue>();
        // sorteer alle clues
        SortCluesAlongZ();
    }
    
    void Update()
    {
        
    }

    //public void PinButtonPressed()
    //{
    //    foreach(Clue clue in allClues)
    //    {
    //        if (clue.state == Clue.ClueStates.Organize)
    //        {
    //            clue.state = Clue.ClueStates.Pin;
    //        }
    //        else if (clue.state == Clue.ClueStates.Pin)
    //        {
    //            clue.state = Clue.ClueStates.Organize;
    //        }
    //    }
    //}

    public void SortCluesAlongZ()
    {
        // NOTE: clues move freely across the z axis to make them appear on top of one another.
        foreach (Clue clue in clueSortation)
        {
            clue.GetComponent<SpriteRenderer>().sortingOrder = clueSortation.IndexOf(clue);
            clue.transform.position = new Vector3(clue.transform.position.x, clue.transform.position.y, clueSortation.Count - clueSortation.IndexOf(clue));
            Pin tempPin = clue.GetComponentInChildren<Pin>();
            if (tempPin != null)
            {
                tempPin.transform.position = new Vector3(tempPin.transform.position.x, tempPin.transform.position.y, 0f);
            }
        }
    }

    public void OpenItemViewer(Clue clue)
    {
        // set item viewer active and set sprite
        itemViewer.SetActive(true);
        itemViewImage.sprite = clue.GetComponent<SpriteRenderer>().sprite;
        itemViewImage.SetNativeSize();
        // start fade-in animation
        itemViewer.GetComponent<Animator>().Play("FadeIn");
        // de-activate clues, pins and camera navigation
        SetClueState(Clue.ClueStates.Frozen);
        PinManager.Instance.SetPinsInteractable(false);
        Camera.main.GetComponent<CameraDragMove>().canNavigate = false;
    }

    public void CloseItemViewer()
    {
        // de-active item viewer
        itemViewer.SetActive(false);
        // activate clues, pins and camera navigation
        SetClueState(Clue.ClueStates.Organize);
        PinManager.Instance.SetPinsInteractable(true);
        Camera.main.GetComponent<CameraDragMove>().canNavigate = true;
    }

    public void SetClueState(Clue.ClueStates newState)
    {
        foreach (Clue everyClue in allClues)
        {
            everyClue.state = newState;
        }
    }
}
