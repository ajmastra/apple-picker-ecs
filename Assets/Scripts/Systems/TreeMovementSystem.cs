using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.SceneManagement;

public partial struct TreeMovementSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        // Check if the current scene is "Hard" outside of any Burst-compiled code
        bool enableVerticalMovement = SceneManager.GetActiveScene().name == "Hard";

        // Call the Burst-compiled job with the result of the scene check
        new MoveTreeJob
        {
            DeltaTime = SystemAPI.Time.DeltaTime,
            EnableVerticalMovement = enableVerticalMovement
        }.Schedule();
    }

    [BurstCompile]
    private partial struct MoveTreeJob : IJobEntity
    {
        public float DeltaTime;
        public bool EnableVerticalMovement;

        [BurstCompile]
        private void Execute(ref LocalTransform transform, ref AppleTreeSpeed speed, in AppleTreeBounds bounds)
        {
            // Horizontal movement
            transform.Position.x += speed.HorizontalSpeed * DeltaTime;
            if (transform.Position.x > bounds.Right)
            {
                speed.HorizontalSpeed = -math.abs(speed.HorizontalSpeed);
            }
            else if (transform.Position.x < bounds.Left)
            {
                speed.HorizontalSpeed = math.abs(speed.HorizontalSpeed);
            }

            // Vertical movement only if in the "Hard" scene
            if (EnableVerticalMovement)
            {
                transform.Position.y += speed.VerticalSpeed * DeltaTime;
                if (transform.Position.y > bounds.Top)
                {
                    speed.VerticalSpeed = -math.abs(speed.VerticalSpeed);
                }
                else if (transform.Position.y < bounds.Bottom)
                {
                    speed.VerticalSpeed = math.abs(speed.VerticalSpeed);
                }
            }
        }
    }
}
