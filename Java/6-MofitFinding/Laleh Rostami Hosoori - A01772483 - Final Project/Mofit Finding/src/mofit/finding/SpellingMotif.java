/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package mofit.finding;

import java.util.ArrayList;
import java.util.List;
import java.util.ListIterator;

/**
 *
 * @author Tulip
 */
class SpellingMotif {
    String DNA;
    int k;   // Mofit length 
    int e;   // Maximum of Hamming error
    int q;   // Number of occurances
    String alpha = "acgt";
    List<String> models = new ArrayList<String>();
    SuffixTree tree;

    SpellingMotif(String dna, int motifLength, int numOfOccurance) {
        DNA = dna;
        alpha = "acgt";
        models = new ArrayList<String>();
        tree = new SuffixTree(dna);

        k = motifLength;
        q = numOfOccurance;
        e = 1;
    }
    
    public void SpellModels()
    {
        List<Node> Occ = new ArrayList<Node>();
        Occ.add(tree.root);

        String Ext;
        if(e > 0)
            Ext = alpha;
        else
            Ext = nextSymbols(tree.root);

        spellModels(0, "", Occ, Ext);
        //printModels();
    }
    
    void spellModels(int l, String model, List<Node> Occ, String Ext)
    {
        //if(l >= k)
        //{
          //  if(l > k)
            //{
              //  models.clear();
                //k = l;
            //}
        
        if(l == k)
        {
            models.add(model);
        }
        else if(l < k)
        {
            for(int i = 0; i < Ext.length(); i++)
            {
                char a = Ext.charAt(i);
                int nbocc = 0;              // Number of real occurances of the current model
                String Ext_a = "";          // Symbol for extending the model
                List<Node> Occ_a = new ArrayList<Node>();    // Node occurances of the model
                
                ListIterator OccIterator = Occ.listIterator();;
                while(OccIterator.hasNext())
                {
                    Node x = (Node) OccIterator.next();
                    if(!(x instanceof Leaf))
                    {
                        Node x_prime = hasLabel(x, a);
                        // Match
                        if(x_prime != null)
                        {
                            x_prime.error = x.error;
                            Occ_a.add(x_prime);

                            nbocc += x_prime.leaves;

                            if(x.error == e)
                                Ext_a = nextSymbols(x_prime);
                            else
                                Ext_a = alpha; 
                        }
                        // Mismatch
                        if(x.error < e)
                        {
                            ListIterator EdgeIterator = x.edgeList.listIterator();
                            while(EdgeIterator.hasNext())
                            {
                                Edge currEdge = (Edge) EdgeIterator.next();
                                if(currEdge.child != x_prime)
                                {
                                    currEdge.child.error++;
                                    Occ_a.add(currEdge.child);

                                    nbocc += currEdge.child.leaves;

                                    if(x.error == e - 1)
                                        Ext_a = nextSymbols(currEdge.child);
                                    else
                                        Ext_a = alpha; 
                                }
                            }
                        }
                    }
                }
                if(nbocc >= q)
                {
                    StringBuilder model_a = new StringBuilder(model);
                    model_a.append(a);
                    spellModels(l + 1, model_a.toString(), Occ_a, Ext_a);
                }
            }
        }
    }
    
    private Node hasLabel(Node x, char a)
    {
        
        ListIterator EdgeIterator = x.edgeList.listIterator();
        while(EdgeIterator.hasNext())
        {
            Edge currEdge = (Edge) EdgeIterator.next();
            if(currEdge.label == a)
                return currEdge.child;
        }
        return null;
    }
    
    private String nextSymbols(Node x_prime)
    {
        StringBuilder symbols = new StringBuilder("");
        if(!(x_prime instanceof Leaf))
        {
            ListIterator EdgeIterator = x_prime.edgeList.listIterator();
            while(EdgeIterator.hasNext())
            {
                Edge currEdge = (Edge) EdgeIterator.next();
                symbols.append(currEdge.label);
            }
        }
        return symbols.toString();
    }
    
    void printModels()
    {
        System.out.print("Suffix Tree -> All Motif: ");
        ListIterator modelsIterator = models.listIterator();
        while(modelsIterator.hasNext())
            System.out.print(modelsIterator.next().toString() + " ");
        System.out.println();
    }
}
