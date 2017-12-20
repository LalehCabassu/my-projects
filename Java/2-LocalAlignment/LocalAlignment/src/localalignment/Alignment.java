package localalignment;

/**
 *
 * @author Laleh Rostami Hosoori
 * A01772483
 * 09/18/2012
 */
class Alignment {
    private String A, B;
    private int [][] C;
    private StringBuilder subSeq_A, subSeq_B, symbol;

    public void simpleMatch(String str1, String str2, int gapCost, int matchScore) {
        int i, j;
        A = str1.toUpperCase();
        B = str2.toUpperCase();
        int n = A.length();
        int m = B.length();
        C = new int [n + 1][m + 1];
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
                    C[i][j] = max(0, C[i - 1][j - 1] + matchScore, C[i - 1][j] + gapCost, C[i][j - 1] + gapCost);
                else
                    C[i][j] = max(0, C[i - 1][j - 1], C[i - 1][j] + gapCost, C[i][j - 1] + gapCost);

        //Find the largest value in C
        int maxIndex_i = 1;
        int maxIndex_j = 1;
        for(i = 1; i <= n; i++)
            for(j = 1; j <= m; j++)
                if(C[i][j] > C[maxIndex_i][maxIndex_j] && !(i == n && j == m)){
                    maxIndex_i = i;
                    maxIndex_j = j;
                }

        //Trace back in C
        i = maxIndex_i;
        j = maxIndex_j;
        while(i > 0 && j > 0){

            if(C[i][j] == 0)
                break;
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
    
    public void affineMatch(String str1, String str2, int openGap, int extendGap) {
        int i, j;
        A = str1.toUpperCase();
        B = str2.toUpperCase();
        int n = A.length();
        int m = B.length();
        C = new int [n + 1][m + 1];
        int [][] D = new int [n + 1][m + 1];
        int [][] I = new int [n + 1][m + 1];
        subSeq_A = new StringBuilder("");
        subSeq_B = new StringBuilder("");
        symbol = new StringBuilder("");

        int [][] BLOSUM80= {
        {5,-2,-2,-2,-1,-1,-1,0,-2,-2,-2,-1,-1,-3,-1,1,0,-3,-2,0,-2,-2,-1,-1},
        {-2,6,-1,-2,-4,1,-1,-3,0,-3,-3,2,-2,-4,-2,-1,-1,-4,-3,-3,-1,-3,0,-1},
        {-2,-1,6,1,-3,0,-1,-1,0,-4,-4,0,-3,-4,-3,0,0,-4,-3,-4,5,-4,0,-1},
        {-2,-2,1,6,-4,-1,1,-2,-2,-4,-5,-1,-4,-4,-2,-1,-1,-6,-4,-4,5,-5,1,-1},
        {-1,-4,-3,-4,9,-4,-5,-4,-4,-2,-2,-4,-2,-3,-4,-2,-1,-3,-3,-1,-4,-2,-4,-1},
        {-1,1,0,-1,-4,6,2,-2,1,-3,-3,1,0,-4,-2,0,-1,-3,-2,-3,0,-3,4,-1},
        {-1,-1,-1,1,-5,2,6,-3,0,-4,-4,1,-2,-4,-2,0,-1,-4,-3,-3,1,-4,5,-1},
        {0,-3,-1,-2,-4,-2,-3,6,-3,-5,-4,-2,-4,-4,-3,-1,-2,-4,-4,-4,-1,-5,-3,-1},
        {-2,0,0,-2,-4,1,0,-3,8,-4,-3,-1,-2,-2,-3,-1,-2,-3,2,-4,-1,-4,0,-1},
        {-2,-3,-4,-4,-2,-3,-4,-5,-4,5,1,-3,1,-1,-4,-3,-1,-3,-2,3,-4,3,-4,-1},
        {-2,-3,-4,-5,-2,-3,-4,-4,-3,1,4,-3,2,0,-3,-3,-2,-2,-2,1,-4,3,-3,-1},
        {-1,2,0,-1,-4,1,1,-2,-1,-3,-3,5,-2,-4,-1,-1,-1,-4,-3,-3,-1,-3,1,-1},
        {-1,-2,-3,-4,-2,0,-2,-4,-2,1,2,-2,6,0,-3,-2,-1,-2,-2,1,-3,2,-1,-1},
        {-3,-4,-4,-4,-3,-4,-4,-4,-2,-1,0,-4,0,6,-4,-3,-2,0,3,-1,-4,0,-4,-1},
        {-1,-2,-3,-2,-4,-2,-2,-3,-3,-4,-3,-1,-3,-4,8,-1,-2,-5,-4,-3,-2,-4,-2,-1},
        {1,-1,0,-1,-2,0,0,-1,-1,-3,-3,-1,-2,-3,-1,5,1,-4,-2,-2,0,-3,0,-1},
        {0,-1,0,-1,-1,-1,-1,-2,-2,-1,-2,-1,-1,-2,-2,1,5,-4,-2,0,-1,-1,-1,-1},
        {-3,-4,-4,-6,-3,-3,-4,-4,-3,-3,-2,-4,-2,0,-5,-4,-4,11,2,-3,-5,-3,-3,-1},
        {-2,-3,-3,-4,-3,-2,-3,-4,2,-2,-2,-3,-2,3,-4,-2,-2,2,7,-2,-3,-2,-3,-1},
        {0,-3,-4,-4,-1,-3,-3,-4,-4,3,1,-3,1,-1,-3,-2,0,-3,-2,4,-4,2,-3,-1},
        {-2,-1,5,5,-4,0,1,-1,-1,-4,-4,-1,-3,-4,-2,0,-1,-5,-3,-4,5,-4,0,-1},
        {-2,-3,-4,-5,-2,-3,-4,-5,-4,3,3,-3,2,0,-4,-3,-1,-3,-2,2,-4,3,-3,-1},
        {-1,0,0,1,-4,4,5,-3,0,-4,-3,1,-1,-4,-2,0,-1,-3,-3,-3,0,-3,5,-1},
        {-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1}
        };

        String BLOSUMIndex = "ARNDCQEGHILKMFPSTWYVBJZX";

        //Fill base cases in C, D and I
        for(i = 0; i <= n; i++)
            I[i][0] = openGap;

        for(j = 0; j <= m; j++)
            D[0][j] = openGap;

        for(i = 0; i <= n; i++)
            C[i][0] = I[i][0];
        for(j = 0; j <= m; j++)
            C[0][j] = D[0][j];

        //Fill caches D, I and C
        for(i = 1; i <= n; i++)
            for(j = 1; j <= m; j++)
                D[i][j] = Math.max(D[i - 1][j] + extendGap, C[i - 1][j] + openGap + extendGap);

        for(i = 1; i <= n; i++)
            for(j = 1; j <= m; j++)
                I[i][j] = Math.max(I[i][j - 1] + extendGap, C[i][j - 1] + openGap + extendGap);

        for(i = 1; i <= n; i++)
            for(j = 1; j <= m; j++){
                int B_i = BLOSUMIndex.indexOf(A.charAt(i - 1));
                int B_j = BLOSUMIndex.indexOf(B.charAt(j - 1));
                C[i][j] = max(C[i - 1][j - 1] + BLOSUM80[B_i][B_j], I[i][j], D[i][j]);
            }

        //Find the largest value in C
        int maxIndex_i = 1;
        int maxIndex_j = 1;
        for(i = 1; i <= n; i++)
            for(j = 1; j <= m; j++)
                if(C[i][j] > C[maxIndex_i][maxIndex_j] && !(i == n && j == m)){
                    maxIndex_i = i;
                    maxIndex_j = j;
                }

        //Trace back in C
        i = maxIndex_i;
        j = maxIndex_j;
        while(i > 0 && j > 0){

            if(C[i][j] == 0)
                break;
            
            int B_i = BLOSUMIndex.indexOf(A.charAt(i - 1));
            int B_j = BLOSUMIndex.indexOf(B.charAt(j - 1));
            if(C[i][j] == C[i - 1][j - 1] + BLOSUM80[B_i][B_j]){
                subSeq_A.insert(0, A.charAt(i - 1));
                subSeq_B.insert(0, B.charAt(j - 1));
                if(A.charAt(i - 1) == B.charAt(j - 1))
                    symbol.insert(0, ':');
                else
                    symbol.insert(0, '.');
                i = i - 1; j = j - 1;
            }
            else if(C[i][j] == I[i][j]){
                subSeq_A.insert(0, A.charAt(i - 1));
                subSeq_B.insert(0, '-');
                symbol.insert(0, ' ');
                i = i - 1;
            }
            else if(C[i][j] == D[i][j]){
                subSeq_A.insert(0, '-');
                subSeq_B.insert(0, B.charAt(j - 1));
                symbol.insert(0, ' ');
                j = j - 1;
            }
        }
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

    private int max(int w, int x, int y, int z){
        int temp;
        temp = Math.max(w, x);
        temp = Math.max(temp, y);
        return Math.max(temp, z);
    }
}
