using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagManager : MonoBehaviour
{
    [SerializeField] private GameObject PetSlotPrefab;
    [SerializeField] private GameObject _content;

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
            GameObject slot = Instantiate(GameManager.bag.Pets[i]);
            slot.transform.parent = _content.transform;
        }
    }

    public void CloseBag()
    {
        gameObject.SetActive(false);
    }
}
