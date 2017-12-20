package mofit.finding;
import java.util.*;
/**
 *
 * @author Laleh Rostami Hosoori    A01772483
 */

class Edge {
    char label;
    //Node parent;
    Node child;
    
    Edge(char l, Node c)
    {
        label = l;
        //parent = p;
        child = c;
    }
}

class Node {
    List<Edge> edgeList;
    Node parent;
    public int leaves;
    int error;
    
    Node(Node p)
    {
        edgeList = new ArrayList<Edge>();
        parent = p;
        leaves = 0;
        error = 0;
    }
    
    public void AddEdge(char label, Node child)
    {
        Edge newEdge = new Edge(label, child);
        edgeList.add(newEdge);
    }
    
    public Node GetChild(char label)
    {
        ListIterator elI = edgeList.listIterator();
        while(elI.hasNext())
        {
            Edge curEdge = (Edge)elI.next();
            if(curEdge.label == label)
                return curEdge.child;
        }
        return null;
    }
    
    public int IncrementError()
    {
        return ++error;
    }
}

class Leaf extends Node
{
    int id;
    
    Leaf(Node p)
    {
        super(p);
        edgeList = null;
        
    }
    
    Leaf(Node p, int index)
    {
        super(p);
        edgeList = null;
        id = index;
    }
    
    void setID(int index)
    {
        id = index;
    }
}

public class SuffixTree {
    Node root;
    String sequence;
    String endSign = "$";
    int leafID;
    List<Leaf> leavesList;
    
    SuffixTree(String dna)
    {
        root = new Node(null);
        leavesList = new ArrayList<Leaf>();
        
        sequence = dna.concat(endSign);
        BuildTree();
        CalcAllLeaves();
        //PrintTree();
    }
    
    public Node GetRoot()
    {
        return root;
    }
    
    public void BuildTree()
    {
        for(int i = sequence.length() - 1; i >= 0; i--)
        {
            leafID = i;
            recursiveTreeBuilder(root, sequence.substring(i));
        }
    }
    
    private void recursiveTreeBuilder(Node node, String suffix)
    {
        if(!suffix.isEmpty())
        {
            Node child = GetChild(node, suffix.charAt(0));
            // New edge
            if(child == null)
            {
                // New leaf
                if(suffix.equalsIgnoreCase(endSign))
                {
                    child = new Leaf(node, leafID);
                    node.AddEdge(suffix.charAt(0), child);
                    leavesList.add((Leaf)child);
                }
                
                // New inner node
                else
                {
                    child = new Node(node);
                    node.AddEdge(suffix.charAt(0), child);
                }
            }
            recursiveTreeBuilder(child, suffix.substring(1));
        }
    }
    
    // Suffix starts at the index of i
    public String Leaf_i(int i)
    {
        return sequence.substring(i);
    }
    
    public Node GetChild(Node curNode, char edgeLabel)
    {
        if(curNode instanceof Leaf)
            return null;
        else
            return curNode.GetChild(edgeLabel);
    }
    
    public void CalcAllLeaves()
    {
        ListIterator listIterator = leavesList.listIterator();
        while(listIterator.hasNext())
        {
            Leaf curLeaf = (Leaf)listIterator.next();
            calcLeaves(curLeaf.parent);
        }
    }
    
    private void calcLeaves(Node node)
    {
        if(node != null)
        {
            node.leaves++;
            calcLeaves(node.parent);
        }
    }
    
    public void PrintTree()
    {
        printTree(root);
    }
    
    private void printTree(Node node)
    {
        if(node != null)
        {
            System.out.println("\n* " + node.leaves);

            ListIterator childIterator = node.edgeList.listIterator();
            while(childIterator.hasNext())
            {
                Edge curEdge = (Edge)childIterator.next();
                System.out.print(curEdge.label + "\t");
                if(curEdge.child instanceof Leaf)
                    System.out.println("\n* " + node.leaves);
                else
                    printTree(curEdge.child);
            }
        } 
    }
}



