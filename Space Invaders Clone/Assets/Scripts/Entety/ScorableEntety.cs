using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorableEntety : MonoBehaviour
{
    public event Action OnGetScore = delegate { };

    protected void GetScore()
    {
        OnGetScore();
    }

}
