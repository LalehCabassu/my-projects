package personalHealthInfo;
import java.util.List;


public class Address {
	public static enum AddressTypeEnum
	{
		HOME, WORK, OTHER
	};
	
	private AddressTypeEnum addressType;
	private String streetLine1;
	private String streetLine2;
	private String city;
	private String state;
	private String postalCode;
	
	public Address(AddressTypeEnum at, String sl1, String sl2, String c, String s, String pc)
	{
		this.addressType = at;
		this.streetLine1 = sl1;
		this.streetLine2 = sl2;
		this.city = c;
		this.state = s;
		this.postalCode = pc;
	}
	
	public AddressTypeEnum GetAddressType()
	{
		return this.addressType;
	}
	
	public String GetStreesLine1()
	{
		return this.streetLine1;
	}
	
	public String GetStreetLine2()
	{
		return this.streetLine2;
	}

	public String GetCity()
	{
		return this.city;
	}

	public String GetState()
	{
		return this.state;
	}

	public String GetPostalCode()
	{
		return this.postalCode;
	}
	
	public void SetAddressType(AddressTypeEnum at)
	{
		this.addressType = at;
	}
	
	public void SetStreesLine1(String st1)
	{
		this.streetLine1 = st1;
	}
	
	public void SetStreetLine2(String st2)
	{
		this.streetLine2 = st2;
	}

	public void SetCity(String c)
	{
		this.city = c;
	}

	public void SetState(String s)
	{
		this.state = s;
	}

	public void SetPostalCode(String pc)
	{
		this.postalCode = pc;
	}
	
	public String GetFormatedAddress()
	{
		return this.addressType + ": " + this.streetLine1 + "\n" + this.streetLine2 + "\n"+
				this.city + " " + this.state + " " + this.postalCode;
	}
	
	public int Compare(Address address)
	{
		// -1: not equal 0:equal
		int result = -1;
		
		String addressStr_1 = this.GetFormatedAddress();
		String addressStr_2 = address.GetFormatedAddress();
		
		if(addressStr_1 == addressStr_2)
			result = 0;
		
		return result;
	}

	public float Match(Address address)
	{
		float result = 0;

		String addressStr_1 = this.GetFormatedAddress();
		String addressStr_2 = address.GetFormatedAddress();

		List<char []> bigram_1 = FuzzyMatch.Bigram(addressStr_1);
		List<char []> bigram_2 = FuzzyMatch.Bigram(addressStr_2);
		
		result = FuzzyMatch.Dice(bigram_1, bigram_2);
		
		return result;
	}
	
	public @Override String toString()
	{
		String result = "Address";
		
		result += (!addressType.toString().isEmpty()) ? "(" + addressType.toString() + "): " : ": ";
		result += (!streetLine1.isEmpty()) ? streetLine1 + " " : "";
		result += (!streetLine2.isEmpty()) ? streetLine2 + " " : "";
		result += (!city.isEmpty()) ? city + " " : "";
		result += (!state.isEmpty()) ? state + " " : "";
		result += (!postalCode.isEmpty()) ? postalCode + " " : "";
		
		return result;
	}

}
