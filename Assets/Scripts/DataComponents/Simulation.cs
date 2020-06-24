using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent] 
public struct Simulation : IComponentData
{
   public float endSimulationTimer;
}
