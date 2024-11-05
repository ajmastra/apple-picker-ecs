using UnityEngine;
using Unity.Entities;

public class RandomSpawner : MonoBehaviour
{
    public GameObject spawnableObject; // Prefab to spawn
    public float spawnInterval = 5f; // Time between spawns
    private GameObject currentObject;
    private bool wasClicked;

    void Start()
    {
        InvokeRepeating("SpawnObject", spawnInterval, spawnInterval);
    }

    void SpawnObject()
    {
        if (currentObject != null && !wasClicked)
        {
            // Decrease score if the object was not clicked before new spawn
            UpdateScore(-200);
        }

        // Reset click status for the new object
        wasClicked = false;

        // Destroy old object if still exists
        if (currentObject != null)
        {
            Destroy(currentObject);
        }

        // Spawn at a random position
        Vector3 randomPosition = new Vector3(
            Random.Range(-8f, 8f),
            Random.Range(-4f, 4f),
            0);

        currentObject = Instantiate(spawnableObject, randomPosition, Quaternion.identity);
        ObjectClickHandler clickHandler = currentObject.AddComponent<ObjectClickHandler>();
        clickHandler.onClick = OnObjectClicked; // Set callback for click
    }

    void OnObjectClicked()
    {
        wasClicked = true;
        // You can add positive score if clicked if needed
    }

    private void UpdateScore(int amount)
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var scoreEntity = entityManager.CreateEntityQuery(typeof(PlayerScore)).GetSingletonEntity();
        var score = entityManager.GetComponentData<PlayerScore>(scoreEntity);
        score.Value += amount;
        entityManager.SetComponentData(scoreEntity, score);
    }
}

public class ObjectClickHandler : MonoBehaviour
{
    public System.Action onClick;

    void OnMouseDown()
    {
        onClick?.Invoke(); // Trigger callback
        Destroy(gameObject); // Destroy object on click
    }
}
