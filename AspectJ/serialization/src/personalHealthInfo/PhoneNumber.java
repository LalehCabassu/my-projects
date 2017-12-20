package personalHealthInfo;
import java.util.List;

public class PhoneNumber {
	public static enum PhoneTypeEnum {
		HOME, CELL_PHONE, WORK, FAX, OTHER
	};
	
	private PhoneTypeEnum phoneType;
	private String areaCode;
	private String exchange;
	private String detailNumber;
	private String extension;
	
	public PhoneNumber(PhoneTypeEnum pt, String ac, String e, String dn, String ex) {
		this.phoneType = pt;
		this.areaCode = ac;
		this.exchange = e;
		this.detailNumber = dn;
		this.extension = ex;
	}
	
	public PhoneTypeEnum GetPhoneType(){
		return this.phoneType;
	}
	
	public String GetAreaCode(){
		return this.areaCode;
	}
	
	public String GetExchange(){
		return this.exchange;
	}
	
	public String GetDetailNumber(){
		return this.detailNumber;
	}
	public String GetExtension(){
		return this.extension;
	}

	public void SetPhoneType(PhoneTypeEnum pt){
		this.phoneType = pt;
	}
	
	public void SetAreaCode(String ac){
		this.areaCode = ac;
	}
	
	public void SetExchange(String e){
		this.exchange = e;
	}
	
	public void SetDetailNumber(String dn){
		this.detailNumber = dn;
	}
	public void SetExtension(String ex){
		this.extension = ex;
	}
	
	public String GetFormatedNumber()
	{
		return "(" + this.areaCode + ") " + this.exchange + "-" + this.detailNumber + "-" + this.extension;
	}
	
	public int Compare(PhoneNumber phone)
	{
		// -1: not equal, 0: equal
		int result = -1;
		
		String phoneStr_1 = this.GetFormatedNumber();
		String phoneStr_2 = phone.GetDetailNumber();
		
		if(phoneStr_1 == phoneStr_2)
			result = 0;
		
		return result;
	}
	
	public float Match(PhoneNumber phone)
	{
		float result = 0;
		
		String phoneStr_1 = this.GetFormatedNumber();
		String phoneStr_2 = phone.GetFormatedNumber();
		
		List<char[]> bigram_1 = FuzzyMatch.Bigram(phoneStr_1);
		List<char[]> bigram_2 = FuzzyMatch.Bigram(phoneStr_2);
		
		result = FuzzyMatch.Dice(bigram_1, bigram_2);
		
		return result;
	}
	
	public @Override String toString()
	{
		String result = "Phone number: ";
		
		result += (!phoneType.toString().isEmpty()) ? phoneType.toString() + ": " : "";
		result += (!areaCode.isEmpty()) ? "(" + areaCode + ") - " : "";
		result += (!exchange.isEmpty()) ? exchange + " " : "";
		result += (!detailNumber.isEmpty()) ? detailNumber + " " : "";
		result += (!extension.isEmpty()) ? " - " + extension : ""; 
		
		return result;
	}
	
}
