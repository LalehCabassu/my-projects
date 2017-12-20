/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package globalalignment;

/**
 *
 * @author Tulip
 */
public class Alignment {
    private String A, B;
    private int [] alignTemp;
    int [][] linearCache;
    private int [] alignment;
    private int n , m;
    private int gapCost;
    private int misMatchCost;
    private int matchScore;
    StringBuilder subSeq_A;
    StringBuilder subSeq_B;
    StringBuilder symbol;

    Alignment(String str1, String str2, int match, int mismatch, int gap){
        A = str1.toUpperCase();
        B = str2.toUpperCase();
        n = A.length();
        m = B.length();
        gapCost = gap;
        misMatchCost = mismatch;
        matchScore = match;
        subSeq_A = new StringBuilder("");
        subSeq_B = new StringBuilder("");
        symbol = new StringBuilder("");
    }

    public void simpleMatch() {
        int i, j;
        int [][] C = new int [n + 1][m + 1];
        subSeq_A = new StringBuilder("");
        subSeq_B = new StringBuilder("");
        symbol = new StringBuilder("");

        //Fill base cases
        for(i = 0; i <= n; i++)
            C[i][0] = i * gapCost;
        for(j = 0; j <= m; j++)
            C[0][j] = j * gapCost;

        //Fill the cache (C)
        for(i = 1; i <= n; i++)
            for(j = 1; j <= m; j++)
                if(A.charAt(i - 1) == B.charAt(j - 1))
                    C[i][j] = max(C[i - 1][j - 1] + matchScore, C[i - 1][j] + gapCost, C[i][j - 1] + gapCost);
                else
                    C[i][j] = max(C[i - 1][j - 1], C[i - 1][j] + gapCost, C[i][j - 1] + gapCost);
        /*
        //Find the largest value in C
        int maxIndex_i = 1;
        int maxIndex_j = 1;
        for(i = 1; i <= n; i++)
            for(j = 1; j <= m; j++)
                if(C[i][j] > C[maxIndex_i][maxIndex_j] && !(i == n && j == m)){
                    maxIndex_i = i;
                    maxIndex_j = j;
                }
         *
         */

        //Trace back in C
        //i = maxIndex_i;
        //j = maxIndex_j;
        i = n; j = m;
        while(i > 0 && j > 0){

            if(C[i][j] == C[i - 1][j - 1] + matchScore && A.charAt(i - 1) == B.charAt(j - 1)){
                subSeq_A.insert(0, A.charAt(i - 1));
                subSeq_B.insert(0, B.charAt(j - 1));
                symbol.insert(0, ':');
                i = i - 1; j = j - 1;
            }
            else if(C[i][j] == C[i - 1][j] + gapCost){
                subSeq_A.insert(0, A.charAt(i - 1));
                subSeq_B.insert(0, '-');
                symbol.insert(0, ' ');
                i = i - 1;
            }
            else if(C[i][j] == C[i][j - 1] + gapCost){
                subSeq_A.insert(0, '-');
                subSeq_B.insert(0, B.charAt(j - 1));
                symbol.insert(0, ' ');
                j = j - 1;
            }
            else if(C[i][j] == C[i - 1][j - 1]&& A.charAt(i - 1) != B.charAt(j - 1)){
                subSeq_A.insert(0, A.charAt(i - 1));
                subSeq_B.insert(0, B.charAt(j - 1));
                symbol.insert(0, '.');
                i = i - 1; j = j - 1;
            }
        }
    }

    public void linearAlign(){
        alignTemp = new int [m];
        alignment = new int [n];
        linearCache = new int [2][m + 1];
        subSeq_A = new StringBuilder("");
        subSeq_B = new StringBuilder("");
        symbol = new StringBuilder("");

        linearAlign(0, n, 0, m);
    }
/*
    private void linearAlign(int A_start, int A_length, int B_start, int B_length) {
        int i_start = A_start, i_end = (A_start + A_length);
        int j_start = B_start,


        if(A_length == 2){

            for(int i = 0; i <= A_length; i++)
                quadCache[i][0] = i * gapCost;
            for(int j = 0; j <= B_length; j++)
                quadCache[0][j] = j * gapCost;

            for(int i = A_start + 1; i <= i_end; i++)
                for(int j = B_start + 1; j <= j_end; j++)
                    if(A.charAt(i - 1) == B.charAt(j - 1))
                        quadCache[i][j] = max(quadCache[i - 1][j - 1] + matchScore, quadCache[i - 1][j] + gapCost, quadCache[i][j - 1] + gapCost);
                    else
                        quadCache[i][j] = max(quadCache[i - 1][j - 1], quadCache[i - 1][j] + gapCost, quadCache[i][j - 1] + gapCost);

            int i = i_end, j = j_end;
            while(i > A_start && j > B_start){

                if(quadCache[i][j] == 0)
                    break;
                if(quadCache[i][j] == quadCache[i - 1][j - 1] + matchScore && A.charAt(i - 1) == B.charAt(j - 1)){
                    subSeq_A.insert(0, A.charAt(i - 1));
                    subSeq_B.insert(0, B.charAt(j - 1));
                    symbol.insert(0, ':');
                    i = i - 1; j = j - 1;
                }
                else if(quadCache[i][j] == quadCache[i - 1][j] + gapCost){
                    subSeq_A.insert(0, A.charAt(i - 1));
                    subSeq_B.insert(0, '-');
                    symbol.insert(0, ' ');
                    i = i - 1;
                }
                else if(quadCache[i][j] == quadCache[i][j - 1] + gapCost){
                    subSeq_A.insert(0, '-');
                    subSeq_B.insert(0, B.charAt(j - 1));
                    symbol.insert(0, ' ');
                    j = j - 1;
                }
                else if(quadCache[i][j] == quadCache[i - 1][j - 1]&& A.charAt(i - 1) != B.charAt(j - 1)){
                    subSeq_A.insert(0, A.charAt(i - 1));
                    subSeq_B.insert(0, B.charAt(j - 1));
                    symbol.insert(0, '.');
                    i = i - 1; j = j - 1;
                }
            }
        }
        else{
            //quadCache Indexes Initialization
            //indexA_end + 2 -> 2: Two columns for base cases
            //int iIndex_s = indexA_start, iIndex_end = indexA_end + 2;
            //int jIndex_s = indexB_start, jIndex_end = indexB_end + 1;

            //indexA_end + 1 -> 1: A column for base cases
            forwardAlignment(A_start, (i_end / 2) + 1, B_start, j_end);
            backwardAlignment(i_end / 2, i_end, B_start, j_end);

            //Find the index of the maximum value in the cache
            int max_j = B_start;
            for(int j = B_start; j <= j_end; j++){
                alignTemp[0][j] += alignTemp[1][j];
                if(alignTemp[0][j] > alignTemp[0][max_j])
                    max_j = j;
            }
            //Print
            if(A.charAt((i_end / 2) - 1) == B.charAt(max_j - 1)){
                subSeq_A.insert(0, A.charAt(i_end / 2));
                subSeq_B.insert(0, B.charAt(max_j));
                symbol.insert(0, ':');
            }
            else if(max_j == j_end){
                subSeq_A.insert(0, A.charAt(i_end / 2));
                subSeq_B.insert(0, '-');
                symbol.insert(0, ' ');
            }
            else if(A.charAt(i_end / 2) == B.charAt(max_j)){
                subSeq_A.insert(0, A.charAt(i_end / 2));
                subSeq_B.insert(0, B.charAt(max_j));
                symbol.insert(0, '.');
            }

            linearAlign(A_start, i_end / 2, B_start, max_j);
            linearAlign(i_end / 2, i_end, max_j, j_end);
        }
    }

     private void forwardAlignment(int preA_s, int preA_e, int preB_s, int preB_e){



         //Fill base cases for Left-to-Right swap - first column
            for(int j = preB_s; j <= preB_e; j++)
                quadCache[0][j] = j * gapCost;

            //Calculate cache for Left-to-Right swap - second column to n/2 column
            for(int i = preA_s + 1; i <= preA_e; i++)
            {
                quadCache[1][preB_s] = i * gapCost;

                for(int j = preB_s + 1; j <= preB_e; j++)
                {
                    //Cache indexes are greater than string indexes by 1
                    if(A.charAt(i - 1) == B.charAt(j - 1))
                        quadCache[1][j] = max(quadCache[0][j - 1] + matchScore, quadCache[0][j] + gapCost, quadCache[1][j - 1] + gapCost);
                    else
                        quadCache[1][j] = max(quadCache[0][j - 1] + misMatchCost, quadCache[0][j] + gapCost, quadCache[1][j - 1] + gapCost);
                }

                //Copy the last column to the first one, ready to roll
                for(int j = preB_s; j <= preB_e; j++)
                    quadCache[0][j] = quadCache[1][j];
            }

            //Write the n/2 column
            for(int j = preB_s; j <= preB_e; j++)
                alignTemp[0][j] = quadCache[1][j];
     }

     private void backwardAlignment(int sufA_s, int sufA_e, int sufB_s, int sufB_e){

         //int [][] quadCache = new int [2][(sufB_e - sufB_s) + 1];

         //Fill base cases for Right-to-Left upsidedown swap - nth column
            for(int j = sufB_e; j >= sufB_s; j--)
                quadCache[1][j] = (m - j) * gapCost;

            //Calculate cache for Right-to-Left upsidedown swap - (n - 1)th column to n/2 column
            for(int i = sufA_e - 1; i >= sufA_s; i--)
            {
                quadCache[1][sufB_e] = (n - i) * gapCost;

                for(int j = sufB_e - 1; j >= sufB_s; j--)
                {
                    //Cache indexes are greater than string indexes by 1
                    if(A.charAt(i) == B.charAt(j))
                        quadCache[0][j] = max(quadCache[1][j + 1] + matchScore, quadCache[1][j] + gapCost, quadCache[0][j + 1] + gapCost);
                    else
                        quadCache[0][j] = max(quadCache[1][j + 1] + misMatchCost, quadCache[1][j] + gapCost, quadCache[0][j + 1] + gapCost);
                }

                //Copy the first column to the last one, ready to roll
                for(int j = sufB_e; j >= sufB_s; j--)
                    quadCache[1][j] = quadCache[0][j];
            }

            //Write the n/2 column
            for(int j = sufB_e; j >= sufB_s; j--)
                alignTemp[1][j] = quadCache[0][j];
     }
 *
 */


    private void linearAlign(int i_start, int i_end, int j_start, int j_end) {
        if(i_end <= i_start && j_end <= j_start);
        
        else if((i_end - i_start) <= 1){

            for(int i = 0; i <= (i_end - i_start) - 1; i++)
                linearCache[i][0] = i * gapCost;
            for(int j = j_start; j <= j_end; j++)
                linearCache[0][j] = j * gapCost;

            //for(int i = 1; i <= (i_end - i_start) - 1; i++)
                for(int j = j_start + 1; j <= j_end; j++)
                    if(A.charAt(i_start) == B.charAt(j - 1))
                        linearCache[1][j] = max(linearCache[0][j - 1] + matchScore, linearCache[0][j] + gapCost, linearCache[1][j - 1] + gapCost);
                    else
                        linearCache[1][j] = max(linearCache[0][j - 1], linearCache[0][j] + gapCost, linearCache[1][j - 1] + gapCost);

            int i = i_end - i_start, j = j_end;
            while(i > 0 && j > j_start){

                if(linearCache[i][j] == linearCache[i - 1][j - 1] + matchScore && A.charAt(i_start) == B.charAt(j - 1)){
                    alignment[i_start]= j - 1;
                    //subSeq_A.insert(0, A.charAt(i_start));
                    //subSeq_B.insert(0, B.charAt(j - 1));
                    //symbol.insert(0, ':');
                    i = i - 1; j = j - 1;
                }
                else if(linearCache[i][j] == linearCache[i - 1][j] + gapCost){
                    alignment[i_start] = -1;
                    //subSeq_A.insert(0, A.charAt(i_start));
                    //subSeq_B.insert(0, '-');
                    //symbol.insert(0, ' ');
                    i = i - 1;
                }
                else if(linearCache[i][j] == linearCache[i][j - 1] + gapCost){

                    //subSeq_A.insert(0, '-');
                    //subSeq_B.insert(0, B.charAt(j - 1));
                    //symbol.insert(0, ' ');
                    j = j - 1;
                }
                else if(linearCache[i][j] == linearCache[i - 1][j - 1]&& A.charAt(i_start) != B.charAt(j - 1)){
                    alignment[i_start]= j - 1;
                    //subSeq_A.insert(0, A.charAt(i_start));
                    //subSeq_B.insert(0, B.charAt(j - 1));
                    //symbol.insert(0, '.');
                    i = i - 1; j = j - 1;
                }
            }
            
        }

/*
 *         else if((j_end - j_start) <= 1){
            forwardAlignment(i_start, i_end, j_start, j_end);



        }

            
 *
 */
        
        else{
            //quadCache Indexes Initialization
            //indexA_end + 2 -> 2: Two columns for base cases
            //int iIndex_s = indexA_start, iIndex_end = indexA_end + 2;
            //int jIndex_s = indexB_start, jIndex_end = indexB_end + 1;

            //indexA_end + 1 -> 1: A column for base cases
            if((i_end / 2) <= i_start)
                for(int j = j_start + 1; j <= j_end; j++)
                    alignTemp[j - 1] = 0;
            else
                forwardAlignment(i_start, (i_end / 2), j_start, j_end);
            if(i_end <= (i_end / 2) - 1)
                for(int j = j_end; j >= j_start; j--)
                    linearCache[0][j] = 0;
            else
                backwardAlignment((i_end / 2) - 1, i_end, j_start, j_end);

            //Find the index of the maximum value in the cache
            int max_j = j_start;
            for(int j = j_start; j < j_end; j++){
                alignTemp[j] += linearCache[0][j];
                if(alignTemp[j] > linearCache[0][max_j])
                    max_j = j;
            }
            alignment[(i_end / 2) - 1] = max_j;
            /*
             //Print
            if(A.charAt((i_end / 2) - 1) == B.charAt(max_j)){
                subSeq_A.insert((i_end / 2) - 1, A.charAt((i_end / 2)- 1));
                subSeq_B.insert((i_end / 2) - 1, B.charAt(max_j));
                symbol.insert((i_end / 2) - 1, ':');
            }
            else if(max_j == j_end){
                subSeq_A.insert((i_end / 2) - 1, A.charAt((i_end / 2) - 1));
                subSeq_B.insert((i_end / 2) - 1, '-');
                symbol.insert((i_end / 2) - 1, ' ');
            }
            else if(A.charAt((i_end / 2) - 1) != B.charAt(max_j)){
                subSeq_A.insert((i_end / 2) - 1, A.charAt((i_end / 2)- 1));
                subSeq_B.insert((i_end / 2) - 1, B.charAt(max_j));
                symbol.insert((i_end / 2) - 1, '.');
            }
             *
             * 
             */

            linearAlign(i_start, (i_end / 2) - 1, j_start, max_j);
            linearAlign(i_end / 2, i_end, max_j + 1, j_end);
        }
    }
     
     private void forwardAlignment(int preA_s, int preA_e, int preB_s, int preB_e){

         

         //Fill base cases for Left-to-Right swap - first column
            for(int j = preB_s; j <= preB_e; j++)
                linearCache[0][j] = j * gapCost;

            //Calculate cache for Left-to-Right swap - second column to n/2 column
            for(int i = preA_s + 1; i <= preA_e; i++)
            {
                linearCache[1][preB_s] = i * gapCost;

                for(int j = preB_s + 1; j <= preB_e; j++)
                {
                    //Cache indexes are greater than string indexes by 1
                    if(A.charAt(i - 1) == B.charAt(j - 1))
                        linearCache[1][j] = max(linearCache[0][j - 1] + matchScore, linearCache[0][j] + gapCost, linearCache[1][j - 1] + gapCost);
                    else
                        linearCache[1][j] = max(linearCache[0][j - 1] + misMatchCost, linearCache[0][j] + gapCost, linearCache[1][j - 1] + gapCost);
                }
                if(i != preA_e)
                    //Copy the last column to the first one, ready to roll
                    for(int j = preB_s; j <= preB_e; j++)
                        linearCache[0][j] = linearCache[1][j];
            }

            //Write the n/2 column
            // Start at the second row because of the base cases
            for(int j = preB_s + 1; j <= preB_e; j++)
                alignTemp[j - 1] = linearCache[1][j];
     }

     private void backwardAlignment(int sufA_s, int sufA_e, int sufB_s, int sufB_e){

         //int [][] quadCache = new int [2][(sufB_e - sufB_s) + 1];

         //Fill base cases for Right-to-Left upsidedown swap - nth column
            for(int j = sufB_e; j >= sufB_s; j--)
                linearCache[1][j] = (m - j) * gapCost;

            //Calculate cache for Right-to-Left upsidedown swap - (n - 1)th column to n/2 column
            for(int i = sufA_e - 1; i >= sufA_s; i--)
            {
                linearCache[0][sufB_e] = (n - i) * gapCost;

                for(int j = sufB_e - 1; j >= sufB_s; j--)
                {
                    //Cache indexes are greater than string indexes by 1
                    if(A.charAt(i) == B.charAt(j))
                        linearCache[0][j] = max(linearCache[1][j + 1] + matchScore, linearCache[1][j] + gapCost, linearCache[0][j + 1] + gapCost);
                    else
                        linearCache[0][j] = max(linearCache[1][j + 1] + misMatchCost, linearCache[1][j] + gapCost, linearCache[0][j + 1] + gapCost);
                }

                if(i != sufA_s)
                    //Copy the first column to the last one, ready to roll
                    for(int j = sufB_e; j >= sufB_s; j--)
                        linearCache[1][j] = linearCache[0][j];
            }

            //Write the n/2 column
            // Start at the second row because of the base cases
            //for(int j = sufB_e - 1; j >= sufB_s; j--)
              //  alignTemp[1][j] = quadCache[0][j];
     }

     public void printAlignment(){
        System.out.println(subSeq_A);
        System.out.println(symbol);
        System.out.println(subSeq_B);
    }

    private int max(int x, int y, int z){
        int temp;
        temp = Math.max(x, y);
        return Math.max(temp, z);
    }

}
