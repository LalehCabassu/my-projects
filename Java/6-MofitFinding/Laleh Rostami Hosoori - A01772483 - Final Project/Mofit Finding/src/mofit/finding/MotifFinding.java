package mofit.finding;
import java.util.*;

/**
 *
 * @author Laleh Rostami Hosoori    A01772483
 */
public class MotifFinding {
    
    static int k;   // Mofit length 
    static int e;   // Maximum of Hamming error
    static int q;   // Number of occurances
    static String alpha = "acgt";
    static List<String> models = new ArrayList<String>();

    public static void main(String[] args) {
        
        motifFinding_Testing();
        
    }
    
    private static void motifFinding_Testing()
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
        
        double [] runTimes_Exhausive = new double [tSizes.length];
        double [] runTimes_SpellingMotif = new double [tSizes.length];
        long startTime, endTime;
        
        String [] dna;
        Exhaustive exhaustiveApproach;
        
        
        for(int i = 0; i < tSizes.length; i++)
        {
            // Create test ceses
            t = tSizes[i];
            dna = new String[t];
            StringBuilder dnaStr = new StringBuilder("");
            for(int j = 0; j < t; j++)
            {
                dna[j] = DNA[j];
                dnaStr.append(dna[j]);
            }
            
            // Run test cases
            exhaustiveApproach = new Exhaustive(dna, mofitSize);
            
            startTime = System.nanoTime();
            exhaustiveApproach.BruteForceMofitsearch();
            endTime = System.nanoTime();
            runTimes_Exhausive[i] = Math.log10(endTime - startTime);
            
            startTime = System.nanoTime();
            SpellingMotif suffixTreeApproach = new SpellingMotif(dnaStr.toString(), mofitSize, t);
            suffixTreeApproach.SpellModels();
            endTime = System.nanoTime();
            runTimes_SpellingMotif[i] = Math.log10(endTime - startTime);
        }
        
        // Print out runtimes
        System.out.println("\nMofit Finding\nProblem Sizes: ");
        for(int i = 0; i < tSizes.length; i++)
            System.out.print(tSizes[i] + "\t");
        
        System.out.println("\nRun Times Exhaustive Approach: ");
        for(int i = 0; i < tSizes.length; i++)
            System.out.print(runTimes_Exhausive[i] + "\t");
        
        System.out.println("\nRun Times Suffix Tree Approach: ");
        for(int i = 0; i < tSizes.length; i++)
            System.out.print(runTimes_SpellingMotif[i] + "\t");
    }
}
