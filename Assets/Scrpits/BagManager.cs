using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    [SerializeField] public GameObject Bag;
    [SerializeField] private GameObject PetSlotPrefab;
    private PetSlot _petSlot;
    [SerializeField] private GameObject _content;

    void Awake() => Init();

    private void Init()
    {
        _petSlot = PetSlotPrefab.GetComponent<PetSlot>();
    }

    private void OnEnable()
    {
        AddSlot();
    }

    private void OnDisable()
    {
        RemoveSlot();
    }

    private void RemoveSlot()
    {
        foreach (Transform child in _content.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void AddSlot()
    {

        for (int i = 0; i < GameManager.bag.Pets.Count; i++)
        {
            _petSlot.pet = GameManager.bag.Pets[i];
            GameObject slot = Instantiate(PetSlotPrefab);
            slot.transform.parent = _content.transform;
            slot.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void OpenBag()
    {
        Bag.SetActive(true);
        AddSlot();
    }

    public void CloseBag()
    {
        RemoveSlot();
        Bag.SetActive(false);
    }
}
