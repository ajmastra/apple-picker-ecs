using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public struct AppleTreeTag : IComponentData { }

public struct AppleTreeSpeed : IComponentData
{
    public float HorizontalSpeed;
    public float VerticalSpeed;
}

public struct AppleTreeBounds : IComponentData
{
    public float Left;
    public float Right;
    public float Top;    // New vertical boundary
    public float Bottom; // New vertical boundary
}

public struct AppleTreeDirectionChangeChance : IComponentData
{
    public float Value;
}

public struct AppleTreeRandom : IComponentData
{
    public Random Value;
}

public class AppleTreeAuthoring : MonoBehaviour
{
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 5f;
    [SerializeField] private float leftAndRightEdge = 24f;
    [SerializeField] private float topAndBottomEdge = 10f; // New field for vertical bounds
    [SerializeField] private float directionChangeChance = 0.1f;

    private class AppleTreeAuthoringBaker : Baker<AppleTreeAuthoring>
    {
        public override void Bake(AppleTreeAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<AppleTreeTag>(entity);

            AddComponent(entity, new AppleTreeSpeed
            {
                HorizontalSpeed = authoring.horizontalSpeed,
                VerticalSpeed = authoring.verticalSpeed
            });

            AddComponent(entity, new AppleTreeBounds
            {
                Left = -authoring.leftAndRightEdge,
                Right = authoring.leftAndRightEdge,
                Top = authoring.topAndBottomEdge,    // Set top boundary
                Bottom = -authoring.topAndBottomEdge // Set bottom boundary
            });

            AddComponent(entity, new AppleTreeDirectionChangeChance
            {
                Value = authoring.directionChangeChance
            });
            AddComponent(entity, new AppleTreeRandom
            {
                Value = Random.CreateFromIndex((uint)entity.Index)
            });
        }
    }
}
