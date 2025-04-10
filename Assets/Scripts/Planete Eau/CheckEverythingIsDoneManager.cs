using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEverythingIsDoneManager : MonoBehaviour
{
    // Start is called before the first frame update
    private HashSet<string> visitedCollides=new HashSet<string>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddVisitedCollider(string name)
    {
        visitedCollides.Add(name);
    }

    public bool checkAllStatuesAreVisited()
    {
        return visitedCollides.Count == 3;
    }
}
