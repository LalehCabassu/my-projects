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

import java.util.*;


class Nim {

    //Numbers of pieces in piles 1, 2 and 3
    private int n1, n2, n3;

    //[0]: Pile number    [1]: Number of pieces
    private int [] myMove;

    // Two arrays to use in cache approach
    private boolean [][][] win;
    private boolean [][][] know;

    Nim(int min, int max){
        // min: The minimum number of pieces in each pile
        // max: The maximum number of pieces in each pile
        // The size of each pile is generated randomly.

        n1 = getRandom(min, max);
        n2 = getRandom(min, max);
        n3 = getRandom(min, max);

        myMove = new int [2];    
        win = new boolean [n1 + 1][n2 + 1][n3 + 1];
        know = new boolean [n1 + 1][n2 + 1][n3 + 1];


        //Initialization of know[]
        for(int i = 0; i <= n1; i++)
            for(int j = 0; j <= n2; j++)
                for(int k = 0; k <= n3; k++)
                    know[i][j][k] = false;
    }

    public int getN1() { return n1;}
    public int getN2() { return n2;}
    public int getN3() { return n3;}

    private static int getRandom(int from, int to) {
        //Gerenates a random integer

        if (from < to)
            return from + new Random().nextInt(Math.abs(to - from));
        return from - new Random().nextInt(Math.abs(to - from));
    }

    private void printPiles(int n1, int n2, int n3){
        
        System.out.printf("%9s %2d %3s ","\nPile 1 (", n1,"): ");
        for(int i = 1; i <= n1; i++)
            System.out.print("o");
        System.out.printf("%9s %2d %3s ","\nPile 2 (", n2, "): ");
        for(int i = 1; i <= n2; i++)
            System.out.print("o");
        System.out.printf("%9s %2d %3s ","\nPile 3 (", n3, "): ");
        for(int i = 1; i <= n3; i++)
            System.out.print("o");
        System.out.println("\n===============================");
    }
    
   
    public void play(){
        //Run NIM game

        Scanner in = new Scanner(System.in);
        boolean turn = false;   //It's your opponent's turn.
        int pile, pieces;        //Which pile and how many pieces the opponent has chosen

        System.out.println("\n===============================");
        System.out.println("NIM Game");
        System.out.println("To make your move, just choose a pile and how many pieces you want to take.");
        printPiles(n1, n2, n3);

        while(n1 + n2 + n3 != 1){
            if(!turn){
                System.out.print("Pile? ");
                pile = in.nextInt();
                System.out.print("Number? ");
                pieces = in.nextInt();
                
                if(pile == 1 && pieces <= n1){
                    n1 -= pieces;
                    turn = true;
                }
                else if(pile == 2 && pieces <= n2){
                    n2 -= pieces;
                    turn = true;
                }
                else if(pile == 3 && pieces <= n3){
                    n3 -= pieces;
                    turn = true;
                }
                else
                    System.out.println("A wrong number has been choosed for the pile or pieces.");
            }
            else{
                winCache();
                System.out.println("Computer Move -> pile " + myMove[0] + ": " + myMove[1]);
                if(myMove[0] == 1)
                    n1 -= myMove[1];
                else if(myMove[0] == 2)
                    n2 -= myMove[1];
                else
                    n3 -= myMove[1];
                turn = false;
            }
            printPiles(n1, n2, n3);
        }
        if(!turn)
            System.out.println("\n   *** You LOSE :( ***\n");
        else
            System.out.println("\n   *** You WIN :) ***\n");
        in.close();
    }

    public boolean winCache(){
        return winCache(n1, n2, n3);
    }
    public boolean winCache(int n1, int n2, int n3){
        //Cache Approach

        boolean flag = false;
        
        if((n1 + n2 + n3) == 1){
            win[n1][n2][n3] = false;
            know[n1][n2][n3] = true;
            return false;
        }

        for(int i = 0 ; i <= n1 && !flag; i++)
            if(i == 0)
                for(int j = 0; j <= n2 && !flag; j++)
                    if(j == 0)
                        for(int k  = 1; k <= n3 & !flag; k++)
                        {
                            if(know[n1][n2][n3 - k])
                                flag = !win[n1][n2][n3 - k];
                            else {
                                flag = !winCache(n1, n2, n3 - k);
                                win[n1][n2][n3 - k] = !flag;
                                know[n1][n2][n3 - k] = true;
                            }
                            if(flag){
                                myMove[0] = 3;
                                myMove[1] = k;
                            }
                        }
                    else {
                        if(know[n1][n2 - j][n3])
                            flag = !win[n1][n2 - j][n3];
                        else {
                            flag = !winCache(n1, n2 - j, n3);
                            win[n1][n2 - j][n3] = !flag;
                            know[n1][n2 - j][n3] = true;
                        }
                        if(flag){
                            myMove[0] = 2;
                            myMove[1] = j;
                        }
                    }
            else{
                if(know[n1 - i][n2][n3])
                    flag = !win[n1 - i][n2][n3];
                else {
                    flag = !winCache(n1 - i, n2, n3);
                    win[n1 - i][n2][n3] = !flag;
                    know[n1 - i][n2][n3] = true;
                }
                if(flag){
                    myMove[0] = 1;
                    myMove[1] = i;
                }
            }

        if(flag){
            win[n1][n2][n3] = true;
            know[n1][n2][n3] = true;
            return true;
        }
        else{
            win[n1][n2][n3] = false;
            know[n1][n2][n3] = true;
            return false;
        }
    }

    public boolean winRec(){
        return winRec(n1, n2, n3);
    }

    public boolean winRec(int n1, int n2, int n3){
        //Recursive approach

        boolean flag = false;

        if(n1 + n2 + n3 == 1)   return false;

        for(int i = 0; i <= n1 && !flag; i++)
            if(i == 0)
                for(int j = 0; j <= n2 && !flag; j++)
                    if(j == 0)
                        for(int k = 1; k <= n3 && !flag; k++)
                            flag = !winRec(n1, n2, n3 - k);
                    else
                        flag = !winRec(n1, n2 - j, n3);
            else
                flag = !winRec(n1 - i, n2, n3);

        if(flag)    return true;
        else    return false;
    }
}


