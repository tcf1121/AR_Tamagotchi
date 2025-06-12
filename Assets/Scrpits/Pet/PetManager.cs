using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("PetStatus"))
        {
            string Pet = PlayerPrefs.GetString("PetStatus");
            GameObject Petobj = Instantiate(Resources.Load(Pet) as GameObject);
            Petobj.name = Pet;
        }
    }
}
