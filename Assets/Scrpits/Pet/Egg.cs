using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public List<GameObject> HatchPetList;

    public void Hatch()
    {
        int num = Random.Range(0, HatchPetList.Count);
        GameObject Pet = Instantiate(HatchPetList[num]);
    }
}
