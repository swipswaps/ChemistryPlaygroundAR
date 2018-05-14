using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class MoleculesController : MonoBehaviour
{

    [Inject]
    MoleculesGraphModel moleculesGraphModel;

    bool recalculate = false;

    public event Action<MoleculaInfo> onMoleculaFound;

    public void RegisterAtom(AtomView atom)
    {
        moleculesGraphModel.AddAtom(atom);
        recalculate = true;
    }

    public void UnregisterAtom(AtomView atom)
    {
        moleculesGraphModel.RemoveAtom(atom);
        recalculate = true;
    }

    void Update()
    {
        if (recalculate)
        {
            var moleculaFull = moleculesGraphModel.CalculateGraph();
            moleculesGraphModel.atoms.ForEach((atom) => atom.ResetBindings());
            if (moleculaFull != null)
            {
                var molecula = moleculaFull.graph;
                foreach (Edge edge in molecula.edges)
                {
                    var firstAtom = edge.first as AtomView;
                    var secondAtom = edge.second as AtomView;
                    if (firstAtom != null && secondAtom != null)
                    {
                        firstAtom.Bind(secondAtom, edge.payload.electronsFirst);
                        secondAtom.Bind(firstAtom, edge.payload.electronsSecond);
                    }
                }
                if (onMoleculaFound != null)
                    onMoleculaFound(moleculaFull.info);
            }
            else
            {
                if (onMoleculaFound != null)
                    onMoleculaFound(null);
            }
            recalculate = false;
        }
    }
}
