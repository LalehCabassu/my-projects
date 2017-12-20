
package localalignment;
import java.util.*;
/**
 *
 * @author Laleh Rostami Hosoori
 * A01772483
 * 09/18/2012
 */
public class Main {
static Scanner input = new Scanner(System.in);
    
    public static void main(String[] args) {
        System.out.println("\nFirst Test >> Compare 3 pairs of strings of 20 characters");
        firstTest();

        System.out.println("\n----------------------------------------------------------------------------------------");
        System.out.println("Second Test >> Total time to solve 1000 problems of random strings with an alphabet of 4 characters of length 128, 256, 512 and 1024");
        secondTest();

        System.out.println("\n----------------------------------------------------------------------------------------");
        System.out.println("Third Test >> Match proteins with affine gap penalties");
        thirdTest();

    }

    static void firstTest(){
        
        String str1_1 = "gcgcgtgcgcggaaggagcc";
        String str1_2 = "aaggtgaagttgtagcagtg";
        String str1_3 = "tgtcagaagaggtgcgtggc";
        
        String str2_1 = "gacttgtggaacctacttcc";
        String str2_2 = "tgaaaataaccttctgtcct";
        String str2_3 = "ccgagctctccgcacccgtg";
        
        Alignment x = new Alignment();
        System.out.println("\n1st pair:");
        x.simpleMatch(str1_1, str2_1, -1, 4);
        x.printAlignment();

        System.out.println("\n2nd pair:");
        x.simpleMatch(str1_2, str2_2, -1, 4);
        x.printAlignment();

        System.out.println("\n3rd pair:");
        x.simpleMatch(str1_3, str2_3, -1, 4);
        x.printAlignment();
    }

    static void secondTest(){

        Alignment match = new Alignment();
        String alphabet = "ACGT";
        String str1, str2;
        long startTime;
        long endTime;
        long [] runTime = new long [4];
        double [] xAxis = new double [4];
        double [] yAxis = new double [4];

        System.out.printf("\n%6s %15s\n", "Length", "Run Time");
        startTime = System.nanoTime();
        for(int i = 0; i < 1000; i++){
            str1 = randomString(alphabet, 128).toString();
            str2 = randomString(alphabet, 128).toString();
            match.simpleMatch(str1, str2, -1, 4);
        }
        endTime = System.nanoTime();
        runTime[0] = endTime - startTime;
        System.out.printf("%6s %15d\n"," 128" ,(runTime[0]));

        startTime = System.nanoTime();
        for(int i = 0; i < 1000; i++){
            str1 = randomString(alphabet, 256).toString();
            str2 = randomString(alphabet, 256).toString();
            match.simpleMatch(str1, str2, -1, 4);
        }
        endTime = System.nanoTime();
        runTime[1] = endTime - startTime;
        System.out.printf("%6s %15d\n"," 256" ,(runTime[1]));

        startTime = System.nanoTime();
        for(int i = 0; i < 1000; i++){
            str1 = randomString(alphabet, 512).toString();
            str2 = randomString(alphabet, 512).toString();
            match.simpleMatch(str1, str2, -1, 4);
        }
        endTime = System.nanoTime();
        runTime[2] = endTime - startTime;
        System.out.printf("%6s %15d\n"," 512" ,(runTime[2]));

        startTime = System.nanoTime();
        for(int i = 0; i < 1000; i++){
            str1 = randomString(alphabet, 1024).toString();
            str2 = randomString(alphabet, 1024).toString();
            match.simpleMatch(str1, str2, -1, 4);
        }
        endTime = System.nanoTime();
        runTime[3] = endTime - startTime;
        System.out.printf("%6s %15d\n"," 1024" ,(runTime[3]));


        for(int i = 0; i < 4; i++){
            xAxis[i] = Math.log(Math.pow(2, 7 + i));
            yAxis[i] = Math.log(runTime[i]);
        }
    }

    static void thirdTest(){
        
        String p53_1 = "meeqqsdlsiepplsqetfsdlwkllpqnnvlstplspnsmedlllspedvanwlddpdealqvpaaaitgdpvtetsapvapppatpwplsssvpsqktyqgsygfrlgflhsgtaksvtctyspplnklfcqlaktcpvqlwvdstpppgtrvramaiykksqhmtevvkrcphhercsdsdglappqhlirvegnlraeylddkhtfrhsvvvpyeppevgsdcttihynymcnsscmggmnrrpiltiitledssgnllgrnsfevrvcacpgrdrrteeenfrkkgelcpelppgstkralptgtssspqpkkkpldgeyftlkirgrerfemfrelnealelkdtqaekdsgesrahssylkskkgqstsrhkklmikregpdsd";
        String p53_2 = "mqeppleltiepplsqetfselwnllpennvlsselssamnelplsedvanwldeapddasgmsavpapaapapatpapaiswplssfvpsqktypgaygfhlgflqsgtaksvtctyspplnklfcqlaktcpvqlwvrsppppgtcvramaiykksefmtevvrrcphhercpdssdglappqhlirvegnlhakylddrntfrhsvvvpyeppevgsdcttihynfmcnsscmggmnrrpiitiitledsngkllgrnsfevrvcacpgrdrrteeenfrkkgepcpepppgstkralppstsstppqkkkpldgeyftlqirgrerfemfrelnealelkdaqsgkepggsrahsshlkakkgqstsrhkkpmlkregldsd";
        String p53_3 = "meesqaelgvepplsqetfsdlwnllpennllsselsapvddllpysedvvtwldecpneapqmpeppaqaalapatswplssfvpsqktypgnygfrlgflhsgtaksvtctyspslnklfcqlaktcpvqlwvdsppppgtrvramaiykklehmtevvrrsphherssdysdglappqhlirvegnlraeyfddrntfrhsvvvpyespeiesecttihynfmcnsscmggmnrrpiltiitledsrgnllgrssfevrvcacpgrdrrteeenfrkkgqscpepppgstkralpsstssspqqkkkpldgeyftlqirgrkrfemfrelnealelmdaqagrepgesrahsshlkskkgpspschkkpmlkregpdsd";
        String p53_4 = "meepqsdlsielplsqetfsdlwkllppnnvlstlpssdsieelflsenvtgwledsggalqgvaaaaastaedpvtetpapvasapatpwplsssvpsyktyqgdygfrlgflhsgtaksvtctyspslnklfcqlaktcpvqlwvnstpppgtrvramaiykklqymtevvrrcphherssegdslappqhlirvegnlhaeylddkqtfrhsvvvpyeppevgsdcttihynymcnsscmggmnrrpiltiitledpsgnllgrnsfevricacpgrdrrteeknfqkkgepcpelppksakralptntssspppkkktldgeyftlkirgherfkmfqelnealelkdaqaskgsedngahssylkskkgqsasrlkklmikregpdsd";
        String p53_5 = "rartllpsrvsrspedwitvmedsqsdmsielplsqetfsclwkllppddilpttatgspnsmedlflpqdvaellegpeealqvsapaaqepgteapapvapasatpwplsssvpsqktyqgnygfhlgflqsgtaksvmctysislnklfcqlaktcpvqlwvtstpppgtrvramaiykksqhmtevvrrcphhercsdgdglappqhlirvegnpyaeylddrqtfrhsvvvpyeppevgsdyttihykymcnsscmggmnrrpiltiitledssgnllgrdsfevrvcacpgrdrrteeenfrkkeehcpelppgsakralptstssspqqkkkpldgeyftlkirgrerfemfrelnealelkdaraaeesgdsrahssypktkkgqstsrhkkpmikkvgpdsd";
        String p53_6 = "meepqsdlsiepplsqetfsdlwkllppknllsalepmedlllpqdvtswlgdadealpvctapaegpapeapapaapappaswplssfvpshktfqgnygfrlgflqsgtaksvtctyspslnklfcqlaktcpvqlwvssapppgtrvramaiyknsqhmtevvrrcphhercseneasdprgrappqhlirvegnlhaeyvddrqtfrhsvlvpyespevgsdcttihynymcnsscmggmnrrpiltiitledpsgnllgrnsfevrvcacpgrdrrteeenlrkkqrcpelpqgsakralptntssspqskrkpadgeyftlkirgrkrfevfrelnealelkdaqaagesgdgraqasclktkkdkstsprknpmikreepdsd";
        String p53_7 = "mtameesqsdislelplsqetfsglwkllppedilpsphcmddlllpqdveeffegpsealrvsgapaaqdpvtetpgpvapapatpwplssfvpsqktyqgnygfhlgflqsgtaksvmctyspplnklfcqlaktcpvqlwvsatppagsrvramaihkksqhmtgvvrrcphhercsdgdglappqhlirvegnlypeyledrqtfrhsvvvpyeppeagseyttihykyicnsscmggmnrrpiltiitledssgnllgrdsfevrvcacpgrdrrteeenfrkkevlcpelppgsakralptctsasppqkkkpldgeyftlkirgrkrfemfrelnealelkdahateesgdsrahssylktkkgqstsrhkktmvkkvgpdsd";
        String p53_8 = "lplsqetfqrlwkllppeavlseaspnsmdnmflspdvvnllegpeealqvsaapaaqdpvtetpapaapapatpwplssfvpsqktyqgsygfhlgflqsgtaksvmctyspslnklfcqlaktcpvqlwvsdtppagsrvramaiykksqhmtevvrrcphherctdgdglappqhlirvegnlnaeylddkqtfrhsvvvpyeppevgsdyttihykymcnsscmggmnrrpiltiitledssgnllgrdsfevricacpgrdrrteeenfrkkeepcpelplgsakralptgtsaspqqkkkrldgeyftlkirgrerfemfrelnealelkdaraaeelgdsrahssylktkrgqssshhkkpmvkkvgpdsd";

        Alignment match_b = new Alignment();
        
        System.out.println("\n1st Pair: ");
        match_b.affineMatch(p53_1, p53_2, -14, -4);
        match_b.printAlignment();

        System.out.println("\n2nd Pair");
        match_b.affineMatch(p53_3, p53_4, -14, -4);
        match_b.printAlignment();

        System.out.println("\n3rd Pair");
        match_b.affineMatch(p53_5, p53_6, -14, -4);
        match_b.printAlignment();

        System.out.println("\n4th Pair");
        match_b.affineMatch(p53_7, p53_8, -14, -4);
        match_b.printAlignment();

        System.out.println("\n5th Pair");
        match_b.affineMatch(p53_1, p53_8, -14, -4);
        match_b.printAlignment();

        System.out.println("\n6th Pair");
        match_b.affineMatch(p53_2, p53_7, -14, -4);
        match_b.printAlignment();

        System.out.println("\n7th Pair");
        match_b.affineMatch(p53_3, p53_6, -14, -4);
        match_b.printAlignment();

        System.out.println("\n8th Pair");
        match_b.affineMatch(p53_4, p53_5, -14, -4);
        match_b.printAlignment();

        System.out.println("\n9th Pair");
        match_b.affineMatch(p53_2, p53_4, -14, -4);
        match_b.printAlignment();

        System.out.println("\n10th Pair");
        match_b.affineMatch(p53_6, p53_8, -14, -4);
        match_b.printAlignment();

    }

    static StringBuilder randomString(String alphabet, int length){
        Random rand = new Random();
        StringBuilder str = new StringBuilder("");
        str = new StringBuilder("");

        for(int j = 0; j < length; j++){
            str.append(alphabet.charAt(rand.nextInt(alphabet.length())));
        }
        return str;
    }
}
