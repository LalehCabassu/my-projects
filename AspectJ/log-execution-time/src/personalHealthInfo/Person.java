package personalHealthInfo;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

public class Person {
	
	private List<Name> names;
	private List<PhoneNumber> phoneNumbers;
	private List<Address> addresses;
	
	public Person()
	{
		names = new ArrayList<Name>();
		phoneNumbers = new ArrayList<PhoneNumber>();
		addresses = new ArrayList<Address>();
	}
	
	public Person(Person newPerson)
	{
		this.names = newPerson.names;
		this.phoneNumbers = newPerson.phoneNumbers;
		this.addresses = newPerson.addresses;
	}

	public Person(Name name)
	{
		this();
		AddName(name);
	}
	
	public Person(PhoneNumber phoneNumber)
	{
		this();
		AddPhoneNumber(phoneNumber);
	}
	
	public Person(Address address)
	{
		this();
		AddAddress(address);
	}
	
	public Person(Name name, PhoneNumber phone)
	{
		this();
		AddName(name);
		AddPhoneNumber(phone);
	}
	
	public Person(Name name, Address address)
	{
		this();
		AddName(name);
		AddAddress(address);
	}

	public Person(PhoneNumber phone, Address address)
	{
		this();
		AddPhoneNumber(phone);
		AddAddress(address);
	}
	
	public Person(Name name, PhoneNumber phone, Address address)
	{
		this();
		AddName(name);
		AddPhoneNumber(phone);
		AddAddress(address);
	}
		
	public void AddName(Name name)
	{
		names.add(name);
	}
	
	public void RemoveName(Name name)
	{
		names.remove(name);
	}
	
	public void AddPhoneNumber(PhoneNumber phoneNumber)
	{
		phoneNumbers.add(phoneNumber);
	}
	
	public void RemovePhoneNumber(PhoneNumber phoneNumber)
	{
		phoneNumbers.remove(phoneNumber);
	}
	
	public void AddAddress(Address address)
	{
		addresses.add(address);
	}
	
	public void RemoveAddress(Address address)
	{
		addresses.remove(address);
	}
	
	public float Match(Person p)
	{
		float result = 0;
		int i  = 0;
		
		for(Iterator<Name> n1 = this.names.iterator(); n1.hasNext();)
		{
			Name name_first = n1.next();
			for(Iterator<Name> n2 = p.names.iterator(); n2.hasNext();)
			{
				result += name_first.Match(n2.next());
				i++;
			}
		}
		
		for(Iterator<PhoneNumber> p1 = this.phoneNumbers.iterator(); p1.hasNext();)
		{
			PhoneNumber number_first = p1.next();
			for(Iterator<PhoneNumber> p2 = phoneNumbers.iterator(); p2.hasNext();)
			{
				result += number_first.Match(p2.next());
				i++;
			}
		}
		
		for(Iterator<Address> a1 = this.addresses.iterator(); a1.hasNext();)
		{
			Address address_first = a1.next();
			for(Iterator<Address> a2 = addresses.iterator(); a2.hasNext();)
			{
				result += address_first.Match(a2.next());
				i++;
			}
		}
		
		return (i != 0) ? (result / i) : result;
	}
	
	public @Override String toString()
	{
		String result = "";
		
		result += this.GetNames() + "\n";
		result += this.GetPhoneNumbers() + "\n";
		result += this.GetAddresses() + "\n";
		
		return result;
	}
	
	public String GetNames()
	{
		String result = "Names:\n";
		
		if(this.names.size() > 0)
			for(int i = 0; i < this.names.size(); i++)
				result += "[" + i + "]: " + this.names.get(i).toString(); 
		
		return result;
	}
	
	public String GetPhoneNumbers()
	{
		String result = "Phone numbers:\n";
		
		if(this.phoneNumbers.size() > 0)
			for(int i = 0; i < this.phoneNumbers.size(); i++)
				result += "[" + i + "]: " + this.phoneNumbers.get(i).toString(); 
		
		return result;
	}
	
	public String GetAddresses()
	{
		String result = "Addresses:\n";
		
		if(this.addresses.size() > 0)
			for(int i = 0; i < this.addresses.size(); i++)
				result += "[" + i + "]: " + this.addresses.get(i).toString(); 
		
		return result;
	}
	
	public String[] GetSortNames()
	{
		String[] result = new String[this.names.size()];
		
		int i = 0;
		for(Iterator<Name> n = this.names.iterator(); n.hasNext();)
		{
			String sortName = n.next().GetSortName();
			result[i++] = sortName;
		}
		return result;
	}
	
	
}
