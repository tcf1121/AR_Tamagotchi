namespace Mapbox.Examples
{
    using UnityEngine;
    using Mapbox.Utils;
    using Mapbox.Unity.Map;
    using Mapbox.Unity.MeshGeneration.Factories;
    using Mapbox.Unity.Utilities;
    using System.Collections.Generic;

    public class SpawnOnGPS : MonoBehaviour
    {
        [SerializeField]
        float _spawnScale = 100f;

        [SerializeField]
        GameObject _markerPrefab;

        GameObject _spawnedObjects;

        void Start()
        {
            _spawnedObjects = Instantiate(_markerPrefab);
            _spawnedObjects.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            GameManager.IsChangedPet += ChangePet;
        }



        public void ChangePet(GameObject _PetPrefab)
        {
            Destroy(_spawnedObjects);
            _spawnedObjects = Instantiate(_PetPrefab);
            _spawnedObjects.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
        }
    }
}