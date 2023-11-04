using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NameGameAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public List<AWord> Word;
    public event Action EndAWord;
    public int curentWord = 0;
    public float speedFlip = 0.3f;
    void Start()
    {
        Word = GetComponentsInChildren<AWord>().ToList();
        for (int i = 0; i < Word.Count; i++)
        {
            Word[i].ID = i;
            Word[i].RegritEvent(gameObject);
        }
        EvenAwordAnim();
    }

    public void EvenAwordAnim()
    {
        EndAWord?.Invoke();
    }

}
