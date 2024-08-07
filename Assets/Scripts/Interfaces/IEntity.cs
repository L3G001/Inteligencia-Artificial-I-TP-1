using System.Collections;
using UnityEngine;

public interface IEntity
{
    public float currentlife { get; set; }
    public float speedModifier { get ; set; }
    public float speed { get; set; }

    public IEnumerator SpeedReset()
    {
        yield return new WaitForSeconds(2);
        speedModifier = 1;
    }
}
