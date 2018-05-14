using System.Collections.Generic;
using Common;
using System.Linq;
using UnityEngine;

public class MoleculesGraphModel
{
    public readonly List<AtomView> atoms;
    private List<Molecula> molecules;

    public MoleculesGraphModel(List<Molecula> molecules)
    {
        this.molecules = molecules;
        atoms = new List<AtomView>();
    }

    public void AddAtom(AtomView atom)
    {
        atoms.Add(atom);
    }

    public void RemoveAtom(AtomView atom)
    {
        atoms.Remove(atom);
    }

    public Molecula CalculateGraph()
    {
        var list = new List<Edge>();
        foreach (AtomView atom1 in atoms)
        {
            foreach (AtomView atom2 in atoms)
            {
                if (atom1.NodeNumber != atom2.NodeNumber)
                    list.Add(new Edge(atom1, atom2, null));
            }
        }
        var graph = Graph2D.fromEdges(list);
        var bestMolecule = molecules.FindLast((molecule) => molecule.graph.nodes.IsSubsetListOf(graph.nodes, (node) => node.NodeCode));
        if (bestMolecule == null) return null;
        var subgraph = Graph2D.mapToSubgraph(bestMolecule.graph, graph);
        return new Molecula(subgraph, bestMolecule.info);
    }
}
