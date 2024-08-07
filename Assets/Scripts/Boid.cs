public class Boid : SteeringAgent
{
    FSM<Enums.BoidStateID> _fsm;

    public bool RedElseBlue;

    private void Start()
    {
        if (RedElseBlue)
        {
            GameManager.Instance.boidConfig.RedAgents.Add(this);
        }
        else
        {
            GameManager.Instance.boidConfig.BlueAgents.Add(this);
        }
    }

    private void LeadFlocking()
    {
        var boids = RedElseBlue?GameManager.Instance.boidConfig.RedAgents: GameManager.Instance.boidConfig.BlueAgents;
        AddForce(Spacing(boids, GameManager.Instance.boidConfig.separationRadius) * GameManager.Instance.boidConfig.separationWeight);

    }



    private void Update()
    {

    }



}
