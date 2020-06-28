using UnityEngine;
using Unity.Entities;

public class EndSimulationSystem : SystemBase
{
    private Simulation simulation;

    protected override void OnCreate() 
    {
        // Initialize framesCounter
        simulation.framesCounter = 1;
    }
    protected override void OnUpdate()
    {
        simulation.framesCounter ++;

        // When the frames reach 600 end the simulation
        if (simulation.framesCounter == 600)
        {
            Application.Quit();
        }
    }
    
}
