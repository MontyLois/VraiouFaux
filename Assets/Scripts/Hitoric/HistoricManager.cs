using System;
using System.Collections.Generic;
using LTX.Singletons;
using UnityEngine;
using UnityEngine.Rendering;
using VraiOuFaux.Core;
using VraiOuFaux.Core.Mascots;
using VraiOuFaux.Game;

public class HistoricManager : MonoSingleton<HistoricManager>
{

    private GameObject selectedMascot;
    [field: SerializeField]
    private Transform selectionTransform;
    [field: SerializeField]
    private Transform spawnTransform;
    
    [field: SerializeField]
    private Transform spawnRightAnswers;
    [field: SerializeField]
    private Transform spawnWrongAnswers;

    
    private float xoffset = 2.5f;
    private float zoffset = 1f;
    private int xmax = 2;
    
    private Dictionary<GameObject, (Question,bool)> answersDictionary;
    private List<(Question, bool)> playerAnswers;
    
    public event Action<Question> OnMascotSelected;
    public event Action<Question> OnInfoAnimalOpen;
    public event Action OnMascotUnselected;

    private void Start()
    {
        playerAnswers = new List<(Question, bool)>();
        playerAnswers = GameManager.Instance.playerAnswers;
        Debug.Log(GameManager.Instance.playerAnswers);
        answersDictionary = new Dictionary<GameObject, (Question, bool)>();
        SpawnMascot();
    }

    private void SpawnMascot()
    {
        Vector3 position = new Vector3(spawnTransform.position.x,spawnTransform.position.y, spawnTransform.position.z );
        int x = 0;
        int z = 0;
        
        foreach (var answer in playerAnswers)
        {
            Debug.Log("for : " + answer.Item1.GetAffirmation().GetLocalizedString() + "the playser answer is " + answer.Item2);
            if (answer.Item2)
            {
                GameObject mascot_body = Instantiate(answer.Item1.MascotData.Avatar,position, spawnTransform.rotation,spawnRightAnswers);
                answersDictionary.Add(mascot_body, answer);
            
                //handle position offset for spawning mascot 
                if (x == xmax)
                {
                    z++;
                    position.x = spawnTransform.position.x;
                    position.z += zoffset;
                    x = 0;
                    if (z % 2 != 0)
                    {
                        position.x += 1.25f;
                    }
                }
                else
                {
                    x++;
                    position.x += xoffset ;
                }
            }
        }
        
        
        position = new Vector3(spawnTransform.position.x,spawnTransform.position.y, spawnTransform.position.z );
        x = 0;
        z = 0;
        
        foreach (var answer in playerAnswers)
        {
            if (!answer.Item2)
            {
                GameObject mascot_body = Instantiate(answer.Item1.MascotData.Avatar,position, spawnTransform.rotation,spawnWrongAnswers);
                answersDictionary.Add(mascot_body, answer);
            
                //handle position offset for spawning mascot 
                if (x == xmax)
                {
                    z++;
                    position.x = spawnTransform.position.x;
                    position.z += zoffset;
                    x = 0;
                    if (z % 2 != 0)
                    {
                        position.x += 1.25f;
                    }
                }
                else
                {
                    x++;
                    position.x += xoffset ;
                }
            }
        }
    }
    
    public void SelectMascot(GameObject mascot)
    {
        if (!selectedMascot)
        {
            selectedMascot = mascot;
            selectedMascot.GetComponent<Transform>().SetParent(selectionTransform);
            MoveCurrentMascot(Vector3.zero);
           // selectedMascot.GetComponent<SortingGroup>().sortingLayerID =
             //   SortingLayer.GetLayerValueFromName("selectedMascot");
             if (answersDictionary.ContainsKey(selectedMascot))
             {
                 OnMascotSelected?.Invoke(answersDictionary[selectedMascot].Item1);
                 selectedMascot.GetComponent<Mascot>().Talk();
             }
             else
             {
                 Debug.Log("wtf the mascot isn't in the dictionary??");
             }
           
        }
    }
    
    public void MoveCurrentMascot(Vector3 position)
    {
        selectedMascot.GetComponent<Mascot>().MoveToPosition(position);
    }

    public void ResetCurrentMascot()
    {
        if (answersDictionary[selectedMascot].Item2)
        {
            selectedMascot.GetComponent<Transform>().SetParent(spawnRightAnswers);
        }
        else
        {
            selectedMascot.GetComponent<Transform>().SetParent(spawnWrongAnswers);
        }
        
        selectedMascot.GetComponent<Mascot>().ResetPosition();
        selectedMascot.GetComponent<Mascot>().Drop();
        selectedMascot = null;
        OnMascotUnselected?.Invoke();
    }

    public void OpenInfoMascot()
    {
        if (answersDictionary.ContainsKey(selectedMascot))
        {
            OnInfoAnimalOpen?.Invoke(answersDictionary[selectedMascot].Item1);
        }
    }

    public void ResetSingleton()
    {
        playerAnswers = new List<(Question, bool)>();
        playerAnswers = GameManager.Instance.playerAnswers;
        answersDictionary = new Dictionary<GameObject, (Question, bool)>();
        SpawnMascot();
    }
}
