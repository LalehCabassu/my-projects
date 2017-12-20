package mofit.finding;

/**
 *
 * @author Laleh Rostami Hosoori    A01772483
 */
public class Exhaustive {
    private String [] DNA;
    private int t;  //Number of Sequences
    private int n;  //Lenght of each sequence
    private int l;  //Length of Mofit
    private int[] bestSolution;
    private int bestScore = 0;
    public String bestMofit;
    private StringBuilder currentMofit;
    private String alphabet = "acgt";
    
    Exhaustive(String [] dna, int mofitLength)
    {
        DNA = dna;
        t = dna.length;
        n = DNA[0].length();
        l = mofitLength;
        bestSolution = new int[t];    
    }
    
    public void BruteForceMofitsearch()
    {
        int [] solution = new int [t];
        MofitSearch(solution, 0);
        //System.out.println("Exhaustive Approach -> Best Motif: " + bestMofit);
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
