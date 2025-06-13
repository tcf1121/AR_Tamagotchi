using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public List<GameObject> Pets;

    void Awake() => Init();

    private void Init()
    {
        Pets = new();
    }

    public void AddPet(GameObject Pet)
    {
        Pets.Add(Pet);
    }

    public void RemovePet(GameObject Pet)
    {
        Pets.Remove(Pet);
    }

    public void TakeOutThePet(int index)
    {
        GameObject Pet = Instantiate(Pets[index]);
    }
}
