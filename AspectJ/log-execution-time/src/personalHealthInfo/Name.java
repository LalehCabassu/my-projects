package personalHealthInfo;
import java.util.*;
	
public class Name {
	public static enum NameTypeEnum {
		LEGAL, COMMOM, ALIAS 
	};
	
	private NameTypeEnum nameType;
	private String salutation;
	private String firstName;
	private String middleName;
	private String lastName;
	private String suffix;
	
	public Name(NameTypeEnum nt, String s, String fn, String mn, String ln, String sf)
	{
		this.nameType = nt;
		this.salutation = s;
		this.firstName = fn;
		this.middleName = mn;
		this.lastName = ln;
		this.suffix = sf;
	}
	
	public NameTypeEnum GetNameType() {
		return this.nameType;
	}
	
	public String GetSalutation() {
		return this.salutation;
	}
	
	public String GetFirstName() {
		return this.firstName;
	}
	
	public String GetMiddleName(){
		return this.middleName;
	}
	
	public String GetLastName(){
		return this.lastName;
	}
	
	public String GetSuffix(){
		return this.suffix;
	}
	
	public void SetNameType(NameTypeEnum nt) {
		this.nameType = nt;
	}
	
	public void SetSalutation(String s) {
		this.salutation = s;
	}
	
	public void SetFirstName(String fn) {
		this.firstName = fn;
	}
	
	public void SetMiddleName(String mn){
		this.middleName = mn;
	}
	
	public void SetLastName(String ln){
		this.lastName = ln;
	}
	
	public void SetSuffix(String s){
		this.suffix = s;
	}
	
	public String GetSortName(){
		String result = this.salutation + " " + 
						this.lastName  + " " + 
						this.firstName + " " + 
						"(" + this.middleName  + ") " + 
						this.suffix;	
		return result;
	}
	
	public String GetFormattedName(){
		String result = "";
		
		result += this.salutation + " " + this.firstName + " " + this.middleName + " " + this.lastName;
		return result;
	}
	
	public int Compare(Name name){
		
		//-1: lower, 0: equal, 1:greater
		int result = this.lastName.compareTo(name.lastName);
		
		if(result == 0){
			result = this.firstName.compareTo(name.firstName);
			if(result == 0)
				result = this.middleName.compareTo(name.middleName);
		}
		
		return result;
	}
	
	public float Match(Name name){
		float result = 0;
		
		String fullName_1 = this.lastName + " " + this.firstName;
		String fullName_2 = name.lastName + " " + name.firstName;
		
		List<char[]> bigram_1 = FuzzyMatch.Bigram(fullName_1);
		List<char[]> bigram_2 = FuzzyMatch.Bigram(fullName_2);
		
		result = FuzzyMatch.Dice(bigram_1, bigram_2);
		
		return result;
	}
	
	public @Override String toString()
	{
		String result = "Full name: ";
		
		result += (!nameType.toString().isEmpty()) ? nameType.toString() + ": " : "";
		result += (!salutation.isEmpty()) ? salutation + " " : "";
		result += (!firstName.isEmpty()) ? firstName + " " : "";
		result += (!middleName.isEmpty()) ? middleName.toUpperCase().charAt(0) + ". " : "";
		result += (!lastName.isEmpty()) ? lastName + " " : "";
		
		return result;
	}
}
