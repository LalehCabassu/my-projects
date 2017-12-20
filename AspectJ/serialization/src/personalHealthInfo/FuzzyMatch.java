package personalHealthInfo;
import java.util.ArrayList;
import java.util.List;

public class FuzzyMatch {

	public static List<char[]> Bigram(String input)
	{
	    ArrayList<char[]> bigram = new ArrayList<char[]>();
	    for (int i = 0; i < input.length() - 1; i++)
	    {
	        char[] chars = new char[2];
	        chars[0] = input.charAt(i);
	        chars[1] = input.charAt(i+1);
	        bigram.add(chars);
	    }
	    return bigram;
	}
	
	public static float Dice(List<char[]> bigram1, List<char[]> bigram2)
	{
		//1.0 meaning the strings are exactly equal
		//0.0 meaning the strings have nothing in common
		
	    List<char[]> copy = new ArrayList<char[]>(bigram2);
	    int matches = 0;
	    for (int i = bigram1.size(); --i >= 0;)
	    {
	        char[] bigram = bigram1.get(i);
	        for (int j = copy.size(); --j >= 0;)
	        {
	            char[] toMatch = copy.get(j);
	            if (bigram[0] == toMatch[0] && bigram[1] == toMatch[1])
	            {
	                copy.remove(j);
	                matches += 2;
	                break;
	            }
	        }
	    }
	    return (float) matches / (bigram1.size() + bigram2.size());
	}

}
