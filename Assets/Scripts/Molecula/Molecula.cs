using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using Common;

public class Graph2D
{
    public readonly List<Edge> edges;

    public readonly List<Node> nodes;

    private Graph2D(List<Edge> edges, List<Node> nodes)
    {
        this.edges = edges;
        this.nodes = nodes;
    }

    public static Graph2D fromEdges(List<Edge> edges)
    {
        var nodes = edges.SelectMany((edge) => new Node[] { edge.first, edge.second }).Distinct().ToList();
        return new Graph2D(edges, nodes);
    }

    public static Graph2D mapToSubgraph(Graph2D thisGraph, Graph2D otherGraph)
    {
        int j = 0;
        var thisGraphNodes = thisGraph.nodes.OrderBy((node) => node.NodeCode).ToList();
        var otherNodes = otherGraph.nodes.OrderBy((node) => node.NodeCode);
        var nodesMapping = new Dictionary<int, Node>();
        foreach (Node otherNode in otherNodes)
        {
            if(j >= thisGraphNodes.Count)
            {
                break;
            }
            if(thisGraphNodes[j].NodeCode == otherNode.NodeCode)
            {
                nodesMapping[thisGraphNodes[j].NodeNumber] = otherNode;
                j++;
            }
        }

        var edgesMapping = new List<Edge>();
        foreach(Edge thisEdge in thisGraph.edges)
        {
            var first = nodesMapping[thisEdge.first.NodeNumber];
            var second = nodesMapping[thisEdge.second.NodeNumber];
            edgesMapping.Add(new Edge(first, second, thisEdge.payload));
        }
        return Graph2D.fromEdges(edgesMapping);
    }
}

public interface Node
{
    int NodeNumber { get; }
    string NodeCode { get; }
}

[Serializable]
public class BasicAtom : Node
{
    [SerializeField]
    private string _nodeCode;
    [SerializeField]
    private int _nodeNumber;

    public string NodeCode { get { return _nodeCode; } private set { _nodeCode = value; } }
    public int NodeNumber { get { return _nodeNumber; } private set { _nodeNumber = value; } }
    public BasicAtom(string code, int number)
    {
        NodeCode = code;
        NodeNumber = number;
    }

    public override bool Equals(object obj)
    {
        var other = obj as BasicAtom;
        if (other == null) return false;
        return other.NodeCode == NodeCode && other.NodeNumber == NodeNumber;
    }

    public override int GetHashCode()
    {
        return NodeNumber * 31 + NodeCode.GetHashCode();
    }
}

public class Edge
{
    public Node first;
    public Node second;
    public EdgePayload payload;

    public Edge(Node first, Node second, EdgePayload edgePayload)
    {
        this.first = first;
        this.second = second;
        this.payload = edgePayload;
    }

    public override bool Equals(object obj)
    {
        var other = obj as Edge;
        return other != null
            && (first.NodeCode.Equals(other.first.NodeCode) && second.NodeCode.Equals(other.second.NodeCode)
            || other.first.NodeCode.Equals(second.NodeCode) && first.NodeCode.Equals(other.second.NodeCode));
    }

    public override int GetHashCode()
    {
        return first.GetHashCode() + 31 * second.GetHashCode();
    }
}

public class Molecula
{
    public Graph2D graph;
    public MoleculaInfo info;
    public Molecula(Graph2D graph, MoleculaInfo info)
    {
        this.graph = graph;
        this.info = info;
    }
}