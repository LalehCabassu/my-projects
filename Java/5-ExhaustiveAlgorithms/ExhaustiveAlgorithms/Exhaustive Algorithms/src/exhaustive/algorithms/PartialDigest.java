/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package exhaustive.algorithms;

/**
 *
 * @author Tulip
 */
public class PartialDigest {
    
    private int [] List;
    private int n;
    
    // Used in Brute Force
    private int subsetIndex = 0;
    private int [][] subsetIndexes;
    
    // Used in Partial Digest
    private int [] L;
    private int width;
    private int [] X = {0};
    private int [] deltaX;
    
    PartialDigest(int [] lengths)
    {
        List = lengths;
        n = findN();
        width = maxItem(List);
    }
    
    //Part 1-> Problem 1-> B
    public void PartialDigest()
    {
        L = List; 
        
        deleteItem(width);
        X = insertItem(width);
        Place();
        
        System.out.print("Partial Digest: ");
        for(int i = 0; i < X.length; i++)
            System.out.print(X[i] + " ");
        System.out.println();
        
    }
    
    private boolean Place()
    {
        boolean right = false;
        boolean left = false;
        int [] oldX = X; 
        int [] oldL = L;
        
        if(L.length == 0)
        {    
            //?????X_2 = X;
            return true;
        }
        
        int y = maxItem(L);
        deltaX = new int [X.length];
        
        if(delta(width - y))
        {
            //oldX = X;
            //oldL = L;
            X = insertItem(width - y);
            deleteItems(deltaX);
            right = Place();
            if(!right)
            {
                L = oldL;
                X = oldX;
            }
        }
        if(!right && delta(y))
        {
            //X = oldX;
            //L = oldL;
            X = insertItem(y);
            deleteItems(deltaX);
            left = Place();
            if(!left)
            {
                L = oldL;
                X = oldX;
            }
        }
        return right || left;
    }

    private boolean delta(int y)
    {
        // Creat multiset of pairwise distances
        for (int i = 0; i < X.length; i++)
            deltaX[i] = Math.abs(y - X[i]);
        
        for(int i = 0; i < deltaX.length; i++)
            if(!isItemInList(deltaX[i]))
                return false;
        return true;
    }
    
    private int [] insertItem(int item)
    {
        int [] newList = new int [X.length + 1];
        int j = 0;
        boolean inserted = false;
        for(int i = 0; i < X.length; i++)
        {
            if(!inserted && item < X[i])
            {   newList[j++] = item;
                inserted = true;
            }
            newList[j++] = X[i];
        }
        if(j == newList.length - 1)
            newList[j] = item;
        return newList;
    }
    
    private void deleteItems(int [] itemsList)
    {
        for(int i = 0; i < itemsList.length; i++)
            if(isItemInList(itemsList[i]))
                deleteItem(itemsList[i]);
    }
    
    private boolean isItemInList(int item)
    {
        for(int i = 0; i < L.length; i++)
            if(item == L[i])
                return true;
        return false;
    }
    
    private void deleteItem(int item)
    {
        int [] newList = new int [L.length - 1];
        int j = 0;
        for(int i = 0; i < L.length; i++)
        {
            if(L[i] == item && j == i)
                continue;
            newList[j++] = L[i];
        }
        L = newList;
    }
    
    private int maxItem(int [] list)
    {
        int max = list[0];
        for(int i = 1; i < list.length; i++)
        {
            if(list[i] > max)
                max = list[i];
        }
        return max;
    }
    
    
    //==========================================================================
    public int [] BruteForcePDP()
    {
        int numberOfSubsets = combination(List.length - 1, n - 2);
        subsetIndexes = new int [numberOfSubsets][n - 2];
        int [] set = new int [n - 2];
        
        L = List; 
        deleteItem(width);
               
        createSubsetIndexes(set, 0, List.length, n - 2);
        int [][] subsets = createSubsets();
        
        for(int i = 0; i < numberOfSubsets; i++)
        {
            if(delta(subsets[i]))
            {
                
                System.out.print("Brute Force: ");
                for(int j = 0; j < subsets[i].length; j++)
                    System.out.print(subsets[i][j] + " ");
                System.out.println();
                return subsets[i];
            }
        }
        return null;
    }
    
    private boolean delta(int [] X)
    {
        int [] deltaX = new int [List.length];
        int k = 0;
        for (int i = 0; i < X.length - 1; i++)
            for(int j = i + 1; j < X.length; j++)
                deltaX[k++] = Math.abs(X[i] - X[j]);
        
        for(int i = 0; i < List.length; i++)
            if(!isItemInList(deltaX, List[i]))
                return false;
        return true;
    }
    
    private boolean isItemInList(int [] set, int item)
    {
        for(int i = 0; i < set.length; i++)
            if(item == set[i])
                return true;
        return false;
    }
    
    private int [][] createSubsets()
    {
        int [][] subsets = new int [subsetIndexes.length][n];
        
        for(int i = 0; i < subsetIndexes.length; i++)
        {
            int [] curSet = new int [n];
            curSet[0] = 0;
            for(int j = 0; j < n - 2; j++)
                curSet[j + 1] = L[subsetIndexes[i][j]];
            curSet[n - 1] = width;
            subsets [i] = curSet;
        }
        return subsets;
    }
    
    private void createSubsetIndexes(int [] set, int firstIndex, int lastIndex, int setLength)
    {
        if(setLength == 0)
            copyToSubsets(set);
        else
        {
            int end = lastIndex - setLength;
            int j = (n - 2) - setLength;
            for(int i = firstIndex; i < end ; i++)
            {
                set[j] = i;
                createSubsetIndexes(set, i + 1, lastIndex, setLength - 1);
            }
        }
    }
    
    private void copyToSubsets(int [] s)
    {
        for(int i = 0; i < s.length; i++)
            subsetIndexes[subsetIndex][i] = s[i];
        subsetIndex ++;
        //System.out.print(subsetIndex + " " + List.length + " " + (n - 2) + " ");
        //System.out.println(combination(List.length - 1, n - 2));
    }
    
    private int findN()
    {
        for(int i = 2; i <= List.length; i++)
            if((i * (i - 1)) == 2 * List.length)
                return i;
        return 0;
    }
    
    private int combination(int n, int m)
    {
        if (n < m)
        {
            int t = m;
            m = n;
            n = t;
        }
        return (int) (factorial(n) / (factorial(m) * factorial(n - m)));
    }
    
    private long factorial(int n)
    {
        long fact = 1;
        for(int i = 1; i <= n; i++)
            fact *= i;
        return fact;
    }
    
}
