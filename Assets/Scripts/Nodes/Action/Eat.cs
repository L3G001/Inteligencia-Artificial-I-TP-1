public class Eat : Node
{
    public override void ExecuteNode()
    {
        if (EnviromentData.Instance.citizen.ActualAction == ActualAction.Idle)
        {
            EnviromentData.Instance.citizen.Eat();

        }
    }


}
