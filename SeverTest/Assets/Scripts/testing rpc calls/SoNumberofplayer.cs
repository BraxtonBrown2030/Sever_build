using UnityEngine;

[CreateAssetMenu(menuName = "scrpitable object/number of player")]
public class SoNumberofplayer : ScriptableObject
{
    public int numberofplayer;
    
    public void ChangeNumber(int newNumber)
    {
        numberofplayer = newNumber;
    }
    
}
