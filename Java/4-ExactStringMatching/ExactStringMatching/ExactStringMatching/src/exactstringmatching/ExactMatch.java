
package exactstringmatching;

/**
 *  Assignment 4: Exact String Match
 * 
 * @author Laleh Rostami Hosoori
 *         A01772483
 */
public class ExactMatch {
    
    private String text;
    private String pattern;
    
    //BM Algorithm
    private String alphabet;
    
    //KMP Algorithm
    private int [] KMP_table;
    
    //Rolling hash
    private double lastValue;
    private String lastString;
    private int [] hashTable;
    
    ExactMatch(String myText, String myPattern){
        
        pattern = myPattern;
        alphabet = "ACGT";
        
        StringBuilder tempText = new StringBuilder("");
        for(int i = 0; i < myText.length(); i++){
            if(alphabet.indexOf(myText.charAt(i)) != -1){
                tempText.append(myText.charAt(i));
            }
        }
        text = tempText.toString();
        
        /*
        //For Boyer Moore Algorithm: Build up alphabet
        StringBuilder tempAlphabet = new StringBuilder("");
        for(int i = 0; i < text.length(); i++){
            if(tempAlphabet.lastIndexOf(text.substring(i, i + 1)) == -1){
                tempAlphabet.append(text.charAt(i));
            }
        }
        for(int i = 0; i < pattern.length(); i++){
            if(tempAlphabet.lastIndexOf(pattern.substring(i, i + 1)) == -1){
                tempAlphabet.append(pattern.charAt(i));
            }
        }
        alphabet = tempAlphabet.toString();

*/
        
        
        //For Rabin Karp Algorithm
        lastValue = -1;
        lastString = "";
    }
    
    
    // Implementation of Boyer Moore Algorithm (Bad character rule) from the definition in Wikipedia 
    // at this url: http://en.wikipedia.org/wiki/Boyer%E2%80%93Moore_string_search_algorithm
    public int BoyerMoore(){
        
        // Text shoerter than Pattern
        if(text.length() < pattern.length())
            return -1;
        
        // Build up mapping table
        int [][] mappingTable = new int [alphabet.length()][pattern.length()];
        for(int i = 0; i < alphabet.length(); i++){
            for(int j = 0; j < pattern.length(); j++){

                // Default: No match
                mappingTable[i][j] = -1;
                
                // Never fail happens when two character are match
                if(alphabet.charAt(i) == pattern.charAt(j))
                    continue;
                
                // Search for the highest index match for mappingTable[i][j]
                for(int k = j - 1; k >= 0; k--){
                    if(alphabet.charAt(i) == pattern.charAt(k)){
                        mappingTable[i][j] = k;
                        break;
                    }
                }
            }
        }
        
        int match_end = pattern.length() - 1;
        boolean found = false;              // True : A match found
        
        while(match_end < text.length()){
            
            int i, j; 
            for(i = match_end, j = pattern.length() - 1; j >= 0 ; i--, j--){
                
                // Mismatch
                if(text.charAt(i) != pattern.charAt(j)){
                    
                    // No match in subsequence of pattern -> Shift over the whole LEFT CHARACTERS in the pattern
                    if(mappingTable[alphabet.indexOf(text.substring(i, i + 1))][j] == -1)
                        //match_end += pattern.length();
                        match_end += (j + 1);
                    else
                        // A match exists -> Shift pattern slightly
                        match_end += (j - mappingTable[alphabet.indexOf(text.substring(i, i + 1))][j]);
                    break;
                }
                // A match found
                else if(j == 0)
                    found = true;
            }
            
            // A match found
            if(found)
                return (match_end - pattern.length() + 1);
        }
        
        // No match
        return -1;
    }
    
    
    // Implementation of Knuth–Morris–Pratt Algorithm from the definition in Wikipedia 
    // at this url: http://en.wikipedia.org/wiki/Knuth%E2%80%93Morris%E2%80%93Pratt_algorithm
    private void Create_KMP_Table()
    {
        KMP_table = new int [pattern.length()];
        int pos = 2, cnd = 0;
        
        //The first few values are fixed but different from what the algorithm might suggest
        KMP_table[0] = -1;
        KMP_table[1] = 0;
        
        while (pos < pattern.length())
        {
            //First case: the substring continues
            if(pattern.charAt(pos - 1) == pattern.charAt(cnd))
            {
                cnd++;
                KMP_table[pos] = cnd;
                pos++;
            }
            else if(cnd > 0)
                cnd = KMP_table[cnd];
            else
            {
                KMP_table[pos] = 0;
                pos++;
            }
        }
    }
    
    public int KMP()
    {
        int m = 0;    // The beginning of the current match in text
        int i = 0;    // The position of the current character in pattern
        
        Create_KMP_Table();
        
        while((m + i) < text.length())
        {
            if(pattern.charAt(i) == text.charAt(m + i))
            {
                if(i == pattern.length() - 1)
                    return m;
                i++;
            }
            else
            {
                m = m + i - KMP_table[i];
                if(KMP_table[i] > -1)
                    i = KMP_table[i];
                else
                    i = 0;
            }
        }
        
        //If we reach here, we have searched all of S unsuccessfully
        return -1;
    }
    
    // Rolling hash ny one character
    private double RollingHash(String newString){
        
        double base = 101.0;
        int index;
        double value;

        //First time
        if(lastString.equals("") && lastValue == -1){
            lastValue = 0;
            for(int i = 0; i < newString.length(); i++){
                index = alphabet.indexOf(newString.charAt(i));
                value = hashTable[index] * Math.pow(base, i);
                lastValue += value;
            }

        }
        else {
            String lastString_suffix = lastString.substring(1);
            String newString_prefix = newString.substring(0, newString.length() - 1);
            
            if(lastString_suffix.equals(newString_prefix)){
                index = alphabet.indexOf(newString.charAt(newString.length() - 1));
                lastValue = Math.floor(lastValue / base) + hashTable[index] * Math.pow(base, newString.length() - 1);
                //lastValue += ((lastValue - hashTable[alphabet.indexOf(lastString.charAt(0))]) / base) + hashTable[index] * Math.pow(base, newString.length() - 1);
            }
               
            else{
                lastValue = 0;
                for(int i = 0; i < newString.length(); i++){
                    index = alphabet.indexOf(newString.charAt(i));
                    value = hashTable[index] * Math.pow(base, i);
                    lastValue += value;
                }
            }
            //lastString = newString;
        }
        lastString = newString;
        return lastValue;
    }
    
    public int RabinKarp(){
        int n = text.length();
        int m = pattern.length();

        int startValue = 26;
        hashTable = new int [alphabet.length()];
        for(int i = 0; i < alphabet.length(); i++){
            hashTable[i] = (startValue++);
        }

        double patternHash = RollingHash(pattern);
        double textHash = RollingHash(text.substring(0, m));
        
        for(int i = 0; i < n - m; i++) {
            if(patternHash == textHash){
                if(pattern.equals(text.substring(i, m + i)))
                    return i;
            }
            String newSub = text.substring(i + 1, m + i + 1);
            textHash = RollingHash(newSub);
        }
        return -1;
    }
}
