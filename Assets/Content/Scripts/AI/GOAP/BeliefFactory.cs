using System.Collections.Generic;
using System;
using UnityEngine;

/// <summary>
/// BeliefFactory
/// </summary>

public class BeliefFactory
{
    readonly GOAPAgent agent;
    readonly Dictionary<string, AgentBelief> beliefs;

    public BeliefFactory(GOAPAgent agent, Dictionary<string, AgentBelief> beliefs)
    {
        this.agent = agent;
        this.beliefs = beliefs;
    }

    public void AddBelief(string key, Func<bool> condition)
    {
        beliefs.Add(key, new AgentBelief.Builder(key).WithCondition(condition).Build());
    }

    public void AddSensorBelief(string key, Sensor sensor)
    {
        beliefs.Add(key, new AgentBelief.Builder(key)
            .WithCondition(() => sensor.IsTargetInRange)
            .WithLocation(() => sensor.TargetPosition)
            .Build());
    }

    public void AddLocationBelief(string key, float distance, Transform locationCondition)
    {
        AddLocationBelief(key, distance, locationCondition.position);
    }

    public void AddLocationBelief(string key, float distance, Vector3 locationCondition)
    {
        beliefs.Add(key, new AgentBelief.Builder(key).WithCondition(() => InRangeOf(locationCondition, distance)).WithLocation(() => locationCondition).Build());
    }

    bool InRangeOf(Vector3 pos, float range) => Vector3.Distance(agent.transform.position, pos) < range;
}
