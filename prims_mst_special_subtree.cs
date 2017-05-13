// 5 6
// 1 2 3
// 1 3 4
// 4 2 6
// 5 2 2
// 2 3 5
// 3 5 7
// 1

using System;
using System.Linq;
using System.Collections.Generic;

class Solution {
    public static void Main(string[] args) {
        string[] nm = Console.ReadLine().Split(' ');
        int n = int.Parse(nm[0]);
        int m = int.Parse(nm[1]);
        Dictionary<int, Dictionary<int, int>> edges = new Dictionary<int, Dictionary<int, int>>();
        for (int i = 0; i < m; i++) {
            string[] xyr = Console.ReadLine().Split(' ');
            int x = int.Parse(xyr[0]);
            int y = int.Parse(xyr[1]);
            int r = int.Parse(xyr[2]);
            if (!edges.ContainsKey(x)) {
                edges[x] = new Dictionary<int, int>();
            }
            edges[x][y] = r;
            if (!edges.ContainsKey(y)) {
                edges[y] = new Dictionary<int, int>();
            }
            edges[y][x] = r;
        }
        int s = int.Parse(Console.ReadLine());

        List<KeyValuePair<int, int>> selectedEdges = primEdges(s, n, edges);
        int weight = 0;
        foreach (KeyValuePair<int, int> e in selectedEdges) {
            weight += edges[e.Key][e.Value];
        }
        Console.WriteLine(weight);
    }

    private static List<KeyValuePair<int, int>> primEdges(int start, int numNodes, Dictionary<int, Dictionary<int, int>> edges) {
        List<KeyValuePair<int, int>> treeEdges = new List<KeyValuePair<int, int>>();
        HashSet<int> treeNodes = new HashSet<int>();
        treeNodes.Add(start);

        while (treeNodes.Count < numNodes) {
            List<KeyValuePair<int, int>> nextEdges = new List<KeyValuePair<int, int>>();
            foreach (int n in treeNodes) {
                foreach (KeyValuePair<int, int> p in edges[n]) {
                    if (!treeNodes.Contains(p.Key)) {
                        nextEdges.Add(new KeyValuePair<int, int>(n, p.Key));
                    }
                }
            }

            KeyValuePair<int, int> nextEdge = nextEdges.First();
            foreach (KeyValuePair<int, int> e in nextEdges) {
                // Console.WriteLine("curr min is {0}->{1}, checking {2}->{3}", nextEdge.Key, nextEdge.Value, e.Key, e.Value);
                if (edges[e.Key][e.Value] < edges[nextEdge.Key][nextEdge.Value]) {
                    nextEdge = e;
                }
            }
            treeNodes.Add(nextEdge.Value);
            treeEdges.Add(nextEdge);
            // Console.WriteLine("adding edge {0}-{1} with weigth {2}", nextEdge.Key, nextEdge.Value, edges[nextEdge.Key][nextEdge.Value]);
        }

        return treeEdges;
    }
}
