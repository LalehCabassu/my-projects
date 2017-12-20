/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package exhaustive.algorithms;

/**
 *
 * @author Tulip
 */
public class ExhaustiveAlgorithms {

    /**
     * @param args the command line arguments
     */
    //static String [] DNA = new String [18];
    
    public static void main(String[] args) {
        // TODO code application logic here
        
        //int [] List = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        //int [] List = {1, 2, 3, 4, 5, 6};
        
        //int [] List = {2, 998, 1000};
        //PartialDigest test = new PartialDigest(List);
        //test.BruteForcePDP();
        
        //int [] List_2 = {2, 2, 3, 3, 4, 5, 6, 7, 8, 10};
        //PartialDigest test_2 = new PartialDigest(List_2);
        //test_2.BruteForcePDP();
        //test_2.PartialDigest();
        
        //MofitFinding MofitSearch = new MofitFinding(10);
        //MofitSearch.BruteForceMofitsearch();
        //System.out.println(MofitSearch.bestMofit);
        
        //MofitSearch.BruteForceMedianSearch();
        //System.out.println(MofitSearch.bestWord);
        
        
        PDP_Testing();
        mofitFinding_Testing();
        
    }
    
    private static void PDP_Testing()
    {
        int [] nSizes = {3, 4, 5, 6, 7};
        int [] LSizes = new int [nSizes.length];
        
        PartialDigest pdp;
        double [] runTimes_BF = new double [LSizes.length];
        double [] runTimes_BB = new double [LSizes.length];
        long startTime, endTime;
        
        // Create problem sizes
        for(int i = 0; i < nSizes.length; i++)
            LSizes[i] = nSizes[i] * (nSizes[i] - 1) / 2;
     
        // Run
        for(int i = 0; i < LSizes.length; i++)
        {
            int [] L = new int [LSizes[i]];
            for(int j = 0; j < LSizes[i]; j++)
                L[j] = j + 1;
            
            pdp = new PartialDigest(L);
            startTime = System.nanoTime();
            pdp.BruteForcePDP();
            endTime = System.nanoTime();
            runTimes_BF[i] = Math.log10(endTime - startTime);
        
            pdp = new PartialDigest(L);
            startTime = System.nanoTime();
            pdp.PartialDigest();
            endTime = System.nanoTime();
            runTimes_BB[i] = Math.log10(endTime - startTime);
        }
        
        // Print out runtimes
        System.out.print("\nPartial Digest\nProblem Sizes: ");
        for(int i = 0; i < LSizes.length; i++)
            System.out.print(LSizes[i] + "\t");
        
        System.out.println("\nRun Times BF: ");
        for(int i = 0; i < LSizes.length; i++)
            System.out.print(runTimes_BF[i] + "\t");
        
        System.out.println("\nRun Times BB: ");
        for(int i = 0; i < LSizes.length; i++)
            System.out.print(runTimes_BB[i] + "\t");
        System.out.println();
    }
    
    private static void mofitFinding_Testing()
    {
        String [] DNA = new String[18];
        int t = 0;
        
        DNA[t++] = "taatgtttgtgctgg";
        DNA[t++] = "gacaaaaacgcgtaa";
        DNA[t++] = "acaaatcccaataac";
        DNA[t++] = "cacaaagcgaaagct";
        DNA[t++] = "acggtgctacacttg";
        DNA[t++] = "agtgaattatttgaa";
        DNA[t++] = "gcgcataaaaaacgg";
        DNA[t++] = "gctccggcggggttt";
        DNA[t++] = "aacgcaattaatgtg";
        DNA[t++] = "acattaccgccaatt";
        DNA[t++] = "ggaggaggcgggagg";
        DNA[t++] = "gatcagcgtcgtttt";
        DNA[t++] = "gctgacaaaaaagat";
        DNA[t++] = "ttttttaaacattaa";
        DNA[t++] = "cccatgagagtgaaa";
        DNA[t++] = "ctggcttaactatgc";
        DNA[t++] = "ctgtgacggaagatc";
        DNA[t++] = "gatttttatacttta";
        
        int [] tSizes = {3, 4, 5, 6, 7};
        int mofitSize = 3;
        
        double [] runTimes_Mofit = new double [tSizes.length];
        double [] runTimes_Median = new double [tSizes.length];
        long startTime, endTime;
        
        String [] dna;
        MofitFinding mofitSearch;
        
        for(int i = 0; i < tSizes.length; i++)
        {
            // Create test ceses
            t = tSizes[i];
            dna = new String[t];
            for(int j = 0; j < t; j++)
                dna[j] = DNA[j];
            
            // Run test cases
            mofitSearch = new MofitFinding(dna, mofitSize);
            
            startTime = System.nanoTime();
            mofitSearch.BruteForceMofitsearch();
            endTime = System.nanoTime();
            runTimes_Mofit[i] = Math.log10(endTime - startTime);
            
            startTime = System.nanoTime();
            mofitSearch.BruteForceMedianSearch();
            endTime = System.nanoTime();
            runTimes_Median[i] = Math.log10(endTime - startTime);
        }
        
        // Print out runtimes
        System.out.println("\nMofit Finding\nProblem Sizes: ");
        for(int i = 0; i < tSizes.length; i++)
            System.out.print(tSizes[i] + "\t");
        
        System.out.println("\nRun Times Mofit Search: ");
        for(int i = 0; i < tSizes.length; i++)
            System.out.print(runTimes_Mofit[i] + "\t");
        
        System.out.println("\nRun Times Median Search: ");
        for(int i = 0; i < tSizes.length; i++)
            System.out.print(runTimes_Median[i] + "\t");
    }
}