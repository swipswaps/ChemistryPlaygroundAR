using System.Collections;
using System.Collections.Generic;
using System.Linq;
public static class SciptableObjectToGraphMapper
{
    public static Molecula MapToGraph2D(this MoleculaScriptableObject molecula)
    {
        var edges = molecula.edges.Select((edge) => new Edge(edge.first, edge.second, edge.payload)).ToList();
        var graph = Graph2D.fromEdges(edges);
        return new Molecula(graph, molecula.info);
    }
}
