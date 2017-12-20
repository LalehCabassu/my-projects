/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package exhaustive.algorithms;

/**
 *
 * @author Tulip
 */
public class MofitFinding {
    private String [] DNA;
    private int t;  //Number of Sequences
    private int n;  //Lenght of each sequence
    private int l;  //Length of Mofit
    private int[] bestSolution;
    private int bestScore = 0;
    public String bestMofit;
    private StringBuilder currentMofit;
    
    public String bestWord;
    private int bestDistance;
    
    private String alphabet = "acgt";
    
    MofitFinding(String [] dna, int mofitLength)
    {
        
        /*
        DNA = new String[6];
        
        DNA[t++] = "taatgt";
        DNA[t++] = "gacaaa";
        DNA[t++] = "acaaat";
        DNA[t++] = "cacaaa";
        DNA[t++] = "acggtg";
        DNA[t++] = "agtgaa";
        */
        
        DNA = dna;
        t = dna.length;
        n = DNA[0].length();
        l = mofitLength;
        bestSolution = new int[t];
        
    }
    //==========================================================================
    public void BruteForceMedianSearch()
    {
        StringBuilder words = new StringBuilder("");
        for(int i = 0; i < l; i++)
            words.append("a");
        
        bestDistance = totalDistance(words);
        bestWord = words.toString();
        words = new StringBuilder("");
        MedianSearch(words, 0);
        System.out.println("Median search -> Best Word: " + bestWord);
    }
    
    private int MedianSearch(StringBuilder word, int depth)
    {
        int score;
        if(depth == l)
            return totalDistance(word);
        
        for(int i = 0; i < alphabet.length(); i++)
        {
            word.append(alphabet.charAt(i));
            score = MedianSearch(word, depth + 1);
            if(score < bestDistance)
            {
                bestDistance = score;
                bestWord = word.toString();
            }
            word.deleteCharAt(depth);
        }
        return bestDistance;
        
    }
    
    private int totalDistance(StringBuilder words)
    {
        int total = 0;
        int distance, best = 0;
        
        for(int k = 0; k < t; k++)
        {
            for(int i = 0; i < n - l; i++)
            {
                distance = 0;
                for(int j = 0; j < l; j++)
                    if(DNA[k].charAt(i + j) != words.charAt(j))
                        distance++;
                if(i == 0)
                    best = distance;
                else if(distance < best)
                    best = distance;
            }
            //total += best;
        }
        return total;
    }
    
    //==========================================================================
    public void BruteForceMofitsearch()
    {
        int [] solution = new int [t];
        MofitSearch(solution, 0);
        System.out.println("Mofit Finding -> Best Mofit: " + bestMofit);
    }
    
    private int MofitSearch(int [] sol, int depth)
    {
        if(depth == t)
            return score(sol);
        for(int i = 0; i < n - l + 1; i++)
        {
            sol[depth] = i;
            int score = MofitSearch(sol, depth + 1);
            if(score > bestScore)
            {
                bestScore = score;
                copyBestSolution(sol);
                bestMofit = currentMofit.toString();
            }
        }
        return bestScore;
    }
    
    private int score(int [] sol)
    {
        // [0]:a , [1]:c , [2]:g , [3]:t
        int [] count = new int[alphabet.length()];
        int s = 0;
        currentMofit = new StringBuilder("");
        
        for(int i = 0; i < l; i++)
        {
            for(int k = 0; k < alphabet.length(); k++)
            {
                count[k] = 0;
                for(int j = 0; j < t; j++)
                {
                    if(DNA[j].charAt((sol[j] + i)) == alphabet.charAt(k))
                        count[k]++;
                }
            }
            currentMofit.append(alphabet.charAt(maxIndex(count)));
            s += count[maxIndex(count)];
        }
        return s;
    }
    
    private void copyBestSolution(int [] sol)
    {
        for (int i = 0; i < bestSolution.length; i++)
            bestSolution[i] = sol[i];
    }
    
    private int maxIndex(int [] list)
    {
        int max = 0;
        for(int i = 0; i < list.length; i++)
            if(list[max] < list[i])
                max = i;
        return max;
    }    
}
