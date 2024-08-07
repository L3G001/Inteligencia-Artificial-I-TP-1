using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderCC : SteeringAgent
{
    FSM<Enums.LeaderStateID> _fsm;
    public bool RedElseBlue = false;
    public List<Node> Path;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _fsm.OnUpdate();
    }
}
