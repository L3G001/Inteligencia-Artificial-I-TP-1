using UnityEngine;

public class ArpShoter : SteeringAgent
{
    [SerializeField] GameObject spawner;
    private Arp arpon;

    public void AimToTarget(SteeringAgent targetAgent)
    {
        AddForce(Pursuit(targetAgent));
    }
    public void ApplyRotation()
    {
        transform.right = _velocity;
    }
    public bool CompareAngle(SteeringAgent targetAgent)
    {
        Vector3 dir = PrePursuit(targetAgent) - transform.position;
        float angle = Vector3.Angle(transform.right, dir);
        return angle < 5;
    }
    public void Shoot()
    {
        if (arpon == null) arpon = Instantiate(Resources.Load<GameObject>("Arpon").GetComponent<Arp>());
        if (!arpon.move)
        {
            arpon.transform.position = spawner.transform.position;
            arpon.transform.right = spawner.transform.right;
        }
        arpon.move = true;
    }


}
