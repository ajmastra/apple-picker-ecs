using Unity.Burst;
using Unity.Entities;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
public partial struct RandomDirectionChangeSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        new ChangeDirectionJob().Schedule();
    }

    [BurstCompile]
    private partial struct ChangeDirectionJob : IJobEntity
    {
        private void Execute(ref AppleTreeRandom random, in AppleTreeDirectionChangeChance chance, ref AppleTreeSpeed speed)
        {
            // Randomly change horizontal direction
            if (random.Value.NextFloat() < chance.Value)
            {
                speed.HorizontalSpeed *= -1;
            }

            // Randomly change vertical direction
            if (random.Value.NextFloat() < chance.Value)
            {
                speed.VerticalSpeed *= -1;
            }
        }
    }
}
