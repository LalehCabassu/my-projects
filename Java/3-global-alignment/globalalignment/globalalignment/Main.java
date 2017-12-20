/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package globalalignment;
import java.util.*;
/**
 *
 * @author Tulip
 */
public class Main {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        
        System.out.println("\nFirst Test >> Compare 3 pairs of strings of 20 characters");
        firstTest();


    }

    static void firstTest(){

        String str1_1 = "gcgc";    //4
        //String str1_1 = "gcgcgtgcgcggaaggagcc";
        String str1_2 = "aaggtgaagttgtagcagtg";
        String str1_3 = "tgtcagaagaggtgcgtggc";

        String str2_1 = "gact";    //4
        //String str2_1 = "gacttgtggaacctacttcc";
        String str2_2 = "tgaaaataaccttctgtcct";
        String str2_3 = "ccgagctctccgcacccgtg";

        Alignment x1 = new Alignment(str1_1, str2_1, 4, -2, -2);
        System.out.println("\n1st pair:");
        x1.simpleMatch();
        x1.printAlignment();
        x1.linearAlign();
        x1.printAlignment();
/*
        Alignment x2 = new Alignment(str1_2, str2_2, 4, -2, -2);
        System.out.println("\n2nd pair:");
        x2.simpleMatch();
        x2.printAlignment();
        x2.linearAlign();
        x2.printAlignment();

        Alignment x3 = new Alignment(str1_3, str2_3, 4, -2, -2);
        System.out.println("\n3rd pair:");
        x3.simpleMatch();
        x3.printAlignment();
        x3.linearAlign();
        x3.printAlignment();
 *
 */
    }

}
