using UnityEngine;
using Unity.Entities;

public class EndSimulationSystem : SystemBase
{
    private Simulation simulation;

    protected override void OnCreate() 
    {
        // End simulation after 45 seconds
        simulation.endSimulationTimer = 45f;
    }
    protected override void OnUpdate()
    {
        simulation.endSimulationTimer -= Time.DeltaTime;

        // 45 seconds after start end the simulation
        if (simulation.endSimulationTimer <= 0f)
        {
            Debug.Log("QUIT!!!!!!");
            Application.Quit();
        }
    }
    
}
