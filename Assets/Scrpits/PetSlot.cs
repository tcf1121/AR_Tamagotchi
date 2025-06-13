using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PetSlot : MonoBehaviour
{
    public GameObject pet;

    [SerializeField] private Image petImage;
    [SerializeField] private TMP_Text petName;
    [SerializeField] private TMP_Text stress;
    [SerializeField] private TMP_Text statiety;
    [SerializeField] private TMP_Text exp;

    private void Awake() => Init();

    private void Init()
    {
        SetPetSlot();
    }

    private void SetPetSlot()
    {
        if (pet != null)
        {
            petName.text = pet.gameObject.name;
            if (pet.GetComponent<Egg>() != null)
            {
                Egg egg = pet.GetComponent<Egg>();
                petImage.sprite = egg.image;
                stress.text = "-";
                statiety.text = "-";
                exp.text = $"{egg.HatchCount}";
            }
            else if (pet.GetComponent<Pet>() != null)
            {
                Pet petobj = pet.GetComponent<Pet>();
                petImage.sprite = petobj.image;
                stress.text = $"{petobj.Stress}";
                statiety.text = $"{petobj.Satiety}";
                exp.text = $"{petobj.Exp}";
            }
        }
    }

    public void TakeOut()
    {
        GameManager.CurrentPet = pet;
    }

    public void Playing()
    {
        GameManager.CurrentPet = pet;
        SceneManager.LoadScene(3);
    }
}
