using DependencyInjection;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityServiceLocator;

public class GOAPFactory : MonoBehaviour, IDependencyProvider
{
    void Awake()
    {
        ServiceLocator.Global.Register(this);
    }

    [Provide] public GOAPFactory ProvideFactory() => this;

    public IGOAPPlanner CreatePlanner()
    {
        return new GOAPPlanner();
    }
}
