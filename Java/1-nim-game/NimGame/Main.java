/**
 *
 * @author Laleh Rostami Hosoori
 * A01772483
 * 04/09/2012
 *
 * Bioinformatics I- Assignment I: Nim Game
 *
 */

package NimGame;

public class Main {

    public static void main(String[] args) {

        Nim myGame = new Nim(10, 20);

        myGame.play();

        //System.out.println("win("+ myGame.getN1() + ", " + myGame.getN2() + ", " + myGame.getN3() + ")");

        //System.out.print("winCache: ");
        //System.out.println(myGame.winCache());
        
        //System.out.print("winRec: ");
        //System.out.println(myGame.winRec());

        //System.out.println("XOR: " + (myGame.getN1() ^ myGame.getN2() ^ myGame.getN3()));

    }
}

