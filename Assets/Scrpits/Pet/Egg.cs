using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    private const string IsEgg = "IsEgg";
    public List<GameObject> HatchPetList;
    public Sprite image;
    public int HatchCount;

    void Awake() => Init();

    private void Init()
    {
        if (PlayerPrefs.HasKey(IsEgg))
        {
            HatchCount = PlayerPrefs.GetInt(IsEgg, HatchCount);
            HatchCount -= (int)GameManager.LastTime / 60;
        }
        else HatchCount = 5000;
    }

    public void Hatch()
    {
        int num = Random.Range(0, HatchPetList.Count);
        GameObject Pet = Instantiate(HatchPetList[num]);
        Pet.name = HatchPetList[num].name;
        GameManager.bag.AddPet(Pet);
        GameManager.bag.RemovePet(gameObject);
    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveQuitTime();
        }
    }

    void OnApplicationQuit()
    {
        SaveQuitTime();
    }

    private void SaveQuitTime()
    {
        PlayerPrefs.SetString("PetStatus", "Egg");
        PlayerPrefs.SetInt(IsEgg, HatchCount);
        PlayerPrefs.Save();
    }
}
