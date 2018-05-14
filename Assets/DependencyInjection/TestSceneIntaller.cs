using UnityEngine;
using Zenject;
using System.Collections.Generic;
using System.Linq;

public class TestSceneIntaller : MonoInstaller<TestSceneIntaller>
{
    [Inject, HideInInspector]
    public List<MoleculaScriptableObject> molecules;

    public override void InstallBindings()
    {
        Container.BindInstance(new MoleculesGraphModel(new List<Molecula>(molecules.Select(molecula => molecula.MapToGraph2D()).ToList())));
        Container.BindInstance(molecules.Select(m => m.info).ToList()).WhenInjectedInto<TestSceneController>();
    }
}