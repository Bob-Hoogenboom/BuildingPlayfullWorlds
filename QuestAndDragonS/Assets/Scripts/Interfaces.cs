using UnityEngine;
using UnityEngine.Events;

//basic damage interface if something can break or die
public interface IDamageable
{ 
    public void Damage (float amount);
}

//if something can get stunned
public interface IStunned
{
    public void Stunned();
}
