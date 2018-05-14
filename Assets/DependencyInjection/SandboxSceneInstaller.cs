using UnityEngine;
using Zenject;
using System.Collections.Generic;
using System.Linq;
using SubjectNerd.Utilities;

public class SandboxSceneInstaller : MonoInstaller<SandboxSceneInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInstance(new MoleculesGraphModel(new List<Molecula>(molecules.Select(molecula => molecula.MapToGraph2D()).ToList())));
    }

    [Reorderable]
    public List<MoleculaScriptableObject> molecules;
}