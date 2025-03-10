using System;
using System.Collections.Generic;
using LTX.Singletons;
using UnityEngine;
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

    
    private float xoffset = 2.5f;
    private float zoffset = -1f;
    private int xmax = 2;
    
    private Dictionary<GameObject, (Question,bool)> answersDictionary;
    private List<(Question, bool)> playerAnswers;
    
    public event Action<Question> OnMascotSelected;
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
            GameObject mascot_body = Instantiate(answer.Item1.MascotData.Avatar,position, spawnTransform.rotation,spawnTransform);
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
    
    public void SelectMascot(GameObject mascot)
    {
        if (!selectedMascot)
        {
            selectedMascot = mascot;
            selectedMascot.GetComponent<Transform>().SetParent(selectionTransform);
            MoveCurrentMascot(Vector3.zero);
            OnMascotSelected?.Invoke(answersDictionary[selectedMascot].Item1);
        }
    }
    
    public void MoveCurrentMascot(Vector3 position)
    {
        selectedMascot.GetComponent<Mascot>().MoveToPosition(position);
    }

    public void ResetCurrentMascot()
    {
        selectedMascot.GetComponent<Transform>().SetParent(spawnTransform);
        selectedMascot.GetComponent<Mascot>().ResetPosition();
        selectedMascot = null;
        OnMascotUnselected?.Invoke();
    }
}
