using UnityEngine;
using Unity.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using System.Collections;

public class PathFinding : MonoBehaviour {

    public const int MOVE_DIAGONAL_COST = 14;
    public const int MOVE_STRAIGHT_COST = 10;

    public bool debug;

    public int2 grid;

    public int2 startPosition;
    public int2 endPosition;


    private void Start() {
        debug = false;

        grid = new int2(3, 3);

        startPosition = new int2(1, 0);
        endPosition = new int2(1, 2);

        FindPath();
    }

    public struct PathNode {
        public int x;
        public int y;

        public int index;

        // Custo de andar um nó
        public int gCost;
        // Custo da heurística até o endNode
        public int hCost;
        // Custo total do nó
        private int fCost;
        public int FCost { 
            get { return gCost + hCost; } 
        }

        public bool isWalkable;

        public int cameFromNodeIndex;

        public override string ToString() {
            return $"x: {x} - y: {y} - hCost: {hCost}";
        }
    }
    
    public void FindPath() {
        var gridNodes = new NativeArray<PathNode>(grid.y * grid.x, Allocator.Temp);

        for(int x = 0; x < grid.x; x++) {
            for(int y = 0; y < grid.y; y++) {
                var gridIndex = y + x * grid.y;
                var pathNode = new PathNode() {
                    x = x,
                    y = y,
                    index = gridIndex,
                    gCost = int.MaxValue,
                    hCost = CalculateDistanceCost(new int2(x, y), endPosition),
                    isWalkable = true,
                    cameFromNodeIndex = -1
                };

                if(debug) Debug.Log(pathNode);

                gridNodes[gridIndex] = pathNode;
            }
        }

        var openNodes = new List<int>();
        var closedNodes = new List<int>();

        var startNode = gridNodes[GridIndex(startPosition.x, startPosition.y)];
        startNode.gCost = 0;

        openNodes.Add(startNode.index);

        while(openNodes.Count > 0) {
            var currentNode = GetNodeWithLowestCost(gridNodes, openNodes);

            openNodes.Remove(currentNode.index);

            openNodes.AddRange(OpenNeighbors(currentNode, gridNodes, closedNodes));

            // TODO: Atualiza o gcost para os vizinhos

            // TODO: Adicionar o nó pesquisado à closedList

            // TODO: Iterar sobre os vizinhos se eles já não estão na lista closedList
        }
    }

    private IEnumerable<int> OpenNeighbors(PathNode currentNode, NativeArray<PathNode> gridNodes, List<int> closedNodes) {
        var neighbors = new List<int2>() {
            new int2(1, 0), // up
            new int2(0, 1), // right
            new int2(-1, 0), // down
            new int2(0, -1), // left
            new int2(1, 1), // up right
            new int2(-1, 1), // down right
            new int2(-1, -1), // down left
            new int2(1, -1) // up left
        };
        foreach(var neighbor in neighbors) {
            var neighborPos = new int2(currentNode.x + neighbor.x, currentNode.y + neighbor.y);
            if(!IsInsideGrid(neighborPos.x, neighborPos.y)) continue;

            var neighborIndex = GridIndex(neighborPos.x, neighborPos.y);
            if(closedNodes.Contains(neighborIndex)) continue;

            if(!gridNodes[neighborIndex].isWalkable) continue;

            // TODO: atualizar o gcost baseado no valor do currentNode, talvez o melhor será separar os vizinhos entre diagonal e straight

            yield return GridIndex(currentNode.x + neighbor.x, currentNode.y + neighbor.y);
        }
    }

    private bool IsInsideGrid(int x, int y) {
        return x >= 0 && x < grid.x
            && y >= 0 && y < grid.y;
    }

    public PathNode GetNodeWithLowestCost(NativeArray<PathNode> gridNodes, List<int> openNodes) {
        var node = gridNodes[openNodes.First()];
        for(int index = 0; index < openNodes.Count; index++) {
            if(gridNodes[index].FCost < node.FCost) {
                node = gridNodes[index];
            }
        }

        if(debug) Debug.Log("LowestCost: " + node);

        return node;
    }

    private int GridIndex(int x, int y) {
        return y + x * grid.y;
    }

    private int CalculateDistanceCost(int2 startPosition, int2 endPosition) {
        var distanceX = Math.Abs(endPosition.x - startPosition.x);
        var distanceY = Math.Abs(endPosition.y - startPosition.y);

        var distanceStraight = Math.Abs(distanceX - distanceY);

        return MOVE_DIAGONAL_COST * Math.Min(distanceX, distanceY) + MOVE_STRAIGHT_COST * distanceStraight;
    }
}
