
package exactstringmatching;
import java.io.*;
import java.util.logging.Level;
import java.util.logging.Logger;
import java.util.Random;

/**
 *  Assignment 4: Exact String Match
 * 
 * @author Laleh Rostami Hosoori
 *         A01772483
 */

public class ExactStringMatching {
    
    static String DNA = "";
    static long time_BM, time_KMP, time_RK;
    
    public static void main(String[] args) {
        
        try {
            firstTest();
        } catch (IOException ex) {
            Logger.getLogger(ExactStringMatching.class.getName()).log(Level.SEVERE, null, ex);
        }
        secondTest();
        
    }

    static void firstTest() throws FileNotFoundException, IOException{
        
        InputDNA();
        
        String pattern1 = "TCCAGTCAGGAATAATACTTTTCATTTTTAGTTTACTGCTCCCCCAGTAAGAGCGATATATTCCGAACAGTCTTGTCTGGGA";
        String pattern2 = "CAGGCTGGCGCCTGCCGGAAACGCGGGGTGCAGCGCCTGCGGCGCTGCCG";
        String pattern3 = "TGTGTTTGGCAGCGGGGCGTACAGTAAACCAGCACAAATCAGCCTGGAATGTGAACATTACTCGCTGACCAGCGAT";
        String pattern4 = "AGTCTGAAACAGTACGCCTGCCGTCATTTTGCCGAACGCTTACCGGCAATGGCGCGCAATGGTGAACTGAAAAG";
        String pattern5 = "TGATGTAATTCACTGATATTCAAAATCCGGAACGCTCCGCGAAAATCGATCCCCGCAGGGG";
        String pattern6 = "GCTTACGGGTGACAATGCCGTTTTCATATAGTGCCGCTTCGGTAATATTTTTTGTGTCAAATGTAAGTGAGGTT";
        String pattern7 = "ATGCAGCATGTCCCTGATT";
        
        ExactMatch pair1 = new ExactMatch(DNA, pattern1);
        ExactMatch pair2 = new ExactMatch(DNA, pattern2);
        ExactMatch pair3 = new ExactMatch(DNA, pattern3);
        ExactMatch pair4 = new ExactMatch(DNA, pattern4);
        ExactMatch pair5 = new ExactMatch(DNA, pattern5);
        ExactMatch pair6 = new ExactMatch(DNA, pattern6);
        ExactMatch pair7 = new ExactMatch(DNA, pattern7);
        
        int index;
        
        System.out.println("\n\nFirst Test");
        System.out.print("| Col1: Pattern number | Col2: Position | Col3: BM Time | Col4: KMB Time | Col5: Rabin–Karp Time |");
        
        //1st Pair
        BooyerMoore(pair1);
        KMP(pair1);
        index = RabinKarp(pair1);
        
        System.out.printf("\n| %20d | %14d | %13d | %14d | %21d |", 1, index, time_BM, time_KMP, time_RK);
        
        //2nd Pair
        BooyerMoore(pair2);
        KMP(pair2);
        index = RabinKarp(pair2);
        
        System.out.printf("\n| %20d | %14d | %13d | %14d | %21d |", 2, index, time_BM, time_KMP, time_RK);
        
        //3rd Pair
        BooyerMoore(pair3);
        KMP(pair3);
        index = RabinKarp(pair3);
        
        System.out.printf("\n| %20d | %14d | %13d | %14d | %21d |", 3, index, time_BM, time_KMP, time_RK);
        
        //4th Pair
        BooyerMoore(pair4);
        KMP(pair4);
        index = RabinKarp(pair4);
        
        System.out.printf("\n| %20d | %14d | %13d | %14d | %21d |", 4, index, time_BM, time_KMP, time_RK);
        
        //5th Pair
        BooyerMoore(pair5);
        KMP(pair5);
        index = RabinKarp(pair5);
        
        System.out.printf("\n| %20d | %14d | %13d | %14d | %21d |", 5, index, time_BM, time_KMP, time_RK);
        
        //6th Pair
        BooyerMoore(pair6);
        KMP(pair6);
        index = RabinKarp(pair6);
        
        System.out.printf("\n| %20d | %14d | %13d | %14d | %21d |", 6, index, time_BM, time_KMP, time_RK);
        
        //7th Pair
        BooyerMoore(pair7);
        KMP(pair7);
        index = RabinKarp(pair7);
        
        System.out.printf("\n| %20d | %14d | %13d | %14d | %21d |", 7, index, time_BM, time_KMP, time_RK);
    }
    
    
    static void secondTest(){
        
        int [] patternLength = {128, 256, 512, 1024, 2048};
        //String [] patterns = new String [patternLength.length];
        ExactMatch pair;
        
        double [] runTime_BM = new double [patternLength.length];
        double [] runTime_KMP = new double [patternLength.length];
        double [] runTime_RK = new double [patternLength.length];
        
        System.out.println("\n\nSecond Test");
        //System.out.print("| Col1: Pattern Length | Col2: BM Position | Col3: KMB Positin | Col4: Rabin–Karp Position |");
        System.out.print("| Col1: Pattern number | Col2: BM Time | Col3: KMB Time | Col4: Rabin–Karp Time |");
        
        
        for(int i = 0; i < patternLength.length; i++){
            
            long sum_BM = 0, sum_KMP = 0, sum_RK = 0;
            for(int j = 0; j < 5; j++){
                pair = new ExactMatch(DNA, randomPattern(patternLength[i]));
                
                BooyerMoore(pair);
                KMP(pair);
                RabinKarp(pair);
                
                sum_BM += time_BM;
                sum_KMP += time_KMP;
                sum_RK += time_RK;
            }
            runTime_BM[i] = sum_BM / 5;
            runTime_KMP[i] = sum_KMP / 5;
            runTime_RK[i] = sum_RK / 5;
            
            //System.out.printf("\n| %20d | %17d | %17d | %25d |", patternLength[i], index_BM, index_KMP, index_RK);
            System.out.printf("\n| %20.3f | %13.3f | %14.3f | %21.3f |", Math.log10(patternLength[i]) , Math.log10(time_BM), Math.log10(time_KMP), Math.log10(time_RK));
        }
     
    }   
    
    static String randomPattern(int length){
        
        String alphabet = "ACGT";
        
        Random rand = new Random();
        StringBuilder str = new StringBuilder("");

        for(int j = 0; j < length; j++){
            str.append(alphabet.charAt(rand.nextInt(alphabet.length())));
        }
        
        return str.toString();
    }
    
    static void InputDNA() throws FileNotFoundException, IOException {
        String currentDir = new File("").getAbsolutePath();
        boolean i = true;
        BufferedReader in = new BufferedReader( new FileReader(currentDir + "/src/exactstringmatching/DNA.txt"));
        String line; 
        
        while((line = in.readLine())!= null)
            DNA += line;
        in.close();
    }
    
    static int BooyerMoore(ExactMatch pair){
        
        long startTime_BM, endTime_BM;
        int index;
        
        startTime_BM = System.nanoTime();
        index = pair.BoyerMoore();
        endTime_BM = System.nanoTime();
        time_BM = endTime_BM - startTime_BM;
        
        return index;
    }
    
    static int KMP(ExactMatch pair){
        
        long startTime_KMP, endTime_KMP;
        int index;
        
        startTime_KMP = System.nanoTime();
        index = pair.KMP();
        endTime_KMP = System.nanoTime();
        time_KMP = endTime_KMP - startTime_KMP;
        
        return index;
    }
    
    static int RabinKarp(ExactMatch pair){
        
        long startTime_RK, endTime_RK;
        int index;
        
        startTime_RK = System.nanoTime();
        index = pair.RabinKarp();
        endTime_RK = System.nanoTime();
        time_RK = endTime_RK - startTime_RK;
        
        return index;
    }
    
}
