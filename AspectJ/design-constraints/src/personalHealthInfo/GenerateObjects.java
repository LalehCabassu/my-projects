package personalHealthInfo;

import java.util.ArrayList;
import java.util.BitSet;
import java.util.List;
import java.util.Random;

public class GenerateObjects {
	
	private static Random mRandom = new Random();
		
	public static Patient createPatient() {
		
		Person person = createPerson();
		return new Patient(person, "male", generateBirthdate());
	}

	private static Person createPerson() {
		Name name = generateName();
		PhoneNumber phone = generatePhoneNumber();
		Address address = generateAddress();
		
		return new Person(name, phone, address);
	}
	
	private static String generateBirthdate() {
		String birthdates [] = {"12/13/1993", "2/23/1985", "6/17/1996", "8/15/1983", "1/30/1961", "5/27/1978",
				"4/29/1965", "11/17/1999", "10/16/1989", "7/28/1977"};

		return birthdates[mRandom.nextInt(birthdates.length)];
	}

	private static Address generateAddress() {
		Address[] addresses = createAddresses();
		
		return addresses[mRandom.nextInt(addresses.length)];
	}

	private static PhoneNumber generatePhoneNumber() {
		PhoneNumber.PhoneTypeEnum phoneTypes [] = PhoneNumber.PhoneTypeEnum.values();
		String areaCodes[] = {"435", "480", "501", "209", "702", "603", "201", "505", "303", "212"};
		String exchanges[] = {"512", "554", "834", "423", "245", "325", "321", "876", "532", "877"};
		String detailNumbers[] = {"8987", "5187", "0293", "6878", "9874", "0743", "5362", "9825", "0305", "0245"};
		
		int ptIndex = mRandom.nextInt(phoneTypes.length);
		int acIndex = mRandom.nextInt(areaCodes.length);
		int eIndex = mRandom.nextInt(exchanges.length);
		int dnIndex = mRandom.nextInt(detailNumbers.length);
		return new PhoneNumber(phoneTypes[ptIndex], areaCodes[acIndex], exchanges[eIndex],detailNumbers[dnIndex] , "");
	}
	
	private static Name generateName() {
		String firstNames[] = {"Dylan", "Kevin", "Karl", "Shaun", "Mario", "Nathan", "Anthony", "Brenton", "Tyrel", "Spencer"};
		String middleNames[] = {"James", "Joseph", "Edward", "Thomas", "Lee", "Joshua", "Michael", "William", "Ray", "Daniel"};
		String secondNames[] = {"Anderson", "Beutler", "Calderwood", "Hawley", "Matos", "Merkley", "Johnson", "Hawley", "Bronson", "Baker"};
		
		int fnIndex = mRandom.nextInt(firstNames.length);
		int mnIndex = mRandom.nextInt(middleNames.length);
		int snIndex = mRandom.nextInt(secondNames.length);
		
		return new Name(Name.NameTypeEnum.LEGAL, "Mr", firstNames[fnIndex], middleNames[mnIndex], secondNames[snIndex], "");	
	}
	
	public static List<Patient> generatePatients(int numberOfObjects) {
		
		List<Person> persons = generatePersons(numberOfObjects);
		
		String birthdates [] = {"12/13/1993", "2/23/1985", "6/17/1996", "8/15/1983", "1/30/1961", "5/27/1978",
								"4/29/1965", "11/17/1999", "10/16/1989", "7/28/1977"};
		
		List<Patient> patients = new ArrayList<Patient> ();
		for(int i = 0; i < numberOfObjects; i++)
		{
			int bdIndex = i % birthdates.length;
			patients.add(new Patient(persons.get(i), "male", birthdates[bdIndex]));
		}
		return patients;
	}

	public static List<Person> generatePersons(int numberOfObjects) {
		Name[] names = createNames(numberOfObjects);
		
		PhoneNumber[] phones = createPhones(numberOfObjects);
		
		Address[] addresses = createAddresses();
		
		List<Person> persons = new ArrayList<Person> ();
		for(int i = 0; i < numberOfObjects; i++)
		{
			int addIndex = i % addresses.length;
			persons.add(new Person(names[i], phones[i], addresses[addIndex]));
		}
		return persons;
	}

	private static Address[] createAddresses() {
		Address addresses[] = new Address[10];
		addresses[0] = new Address(Address.AddressTypeEnum.HOME, "8 Aggie Village APT D", "", "Logan", "Utah", "84341");
		addresses[1] = new Address(Address.AddressTypeEnum.WORK, "28 Aggie Village APT E", "", "Logan", "Utah", "84341");
		addresses[2] = new Address(Address.AddressTypeEnum.OTHER, "678 N Main St", "", "Logan", "Utah", "84321");
		addresses[3] = new Address(Address.AddressTypeEnum.HOME, "118 North Main Street,", "", "Logan", "Utah", "84321");
		addresses[4] = new Address(Address.AddressTypeEnum.WORK, "1550 N Main St", "", "Logan", "Utah", "84341");
		addresses[5] = new Address(Address.AddressTypeEnum.OTHER, "442 N 175 E", "", "Logan", "Utah", "84321");
		addresses[6] = new Address(Address.AddressTypeEnum.HOME, "555 E 1400 N", "", "Logan", "Utah", "84341");
		addresses[7] = new Address(Address.AddressTypeEnum.WORK, "682 S Main St", "", "Logan", "Utah", "84321");
		addresses[8] = new Address(Address.AddressTypeEnum.OTHER, "1165 N Main St", "", "Logan", "Utah", "84341");
		addresses[9] = new Address(Address.AddressTypeEnum.HOME, "2281 N Main St", "", "Logan", "Utah", "84341");
		return addresses;
	}

	private static PhoneNumber[] createPhones(int numberOfObjects) {
		PhoneNumber.PhoneTypeEnum phoneTypes [] = PhoneNumber.PhoneTypeEnum.values();
		String areaCodes[] = {"435", "480", "501", "209", "702", "603", "201", "505", "303", "212"};
		String exchanges[] = {"512", "554", "834", "423", "245", "325", "321", "876", "532", "877"};
		String detailNumbers[] = {"8987", "5187", "0293", "6878", "9874", "0743", "5362", "9825", "0305", "0245"};
		PhoneNumber phones[] = new PhoneNumber[numberOfObjects];
		for(int i = 0; i < numberOfObjects; i++)
		{
			int ptIndex = mRandom.nextInt(phoneTypes.length);
			int acIndex = mRandom.nextInt(areaCodes.length);
			int eIndex = mRandom.nextInt(exchanges.length);
			int dnIndex = mRandom.nextInt(detailNumbers.length);
			phones[i] = new PhoneNumber(phoneTypes[ptIndex], areaCodes[acIndex], exchanges[eIndex],detailNumbers[dnIndex] , "");
		}
		return phones;
	}

	private static Name[] createNames(int numberOfObjects) {
		String firstNames[] = {"Dylan", "Kevin", "Karl", "Shaun", "Mario", "Nathan", "Anthony", "Brenton", "Tyrel", "Spencer"};
		String middleNames[] = {"James", "Joseph", "Edward", "Thomas", "Lee", "Joshua", "Michael", "William", "Ray", "Daniel"};
		String secondNames[] = {"Anderson", "Beutler", "Calderwood", "Hawley", "Matos", "Merkley", "Johnson", "Hawley", "Bronson", "Baker"};
		Name names[]  = new Name [numberOfObjects];
		for(int i = 0; i < numberOfObjects; i++)
		{
			int fnIndex = mRandom.nextInt(firstNames.length);
			int mnIndex = mRandom.nextInt(middleNames.length);
			int snIndex = mRandom.nextInt(secondNames.length);
			names[i] = new Name(Name.NameTypeEnum.LEGAL, "Mr", firstNames[fnIndex], middleNames[mnIndex], secondNames[snIndex], "");
		}
		return names;
	}
	
	public static void createUserAccounts()
	{
		String[] usernames = {"laleh", "anthony", "dylan", "josh", "spencer", "shannon", "kristin", "alex", "kensey", "andrea"};
		String[] passwords = {"lh%0", "ay%1", "dn%2", "jh%3", "sr%4", "sn%5", "kn%6", "ax%7", "ky%8", "aa%9"};
		String[] firstNames = {"Laleh", "Anthony", "Dylan", "Josh", "Spencer", "Shannon", "Kristin", "Alex", "Kensey", "Andrea"};
		String[] lastNames = {"Rostami", "Johnson", "Anderson", "Furman", "Baker", "Wertman", "Nelson", "Desser", "Hasen", "Fjeldsted"};
		
		BitSet [] accesses = new BitSet [16] ;
		
		for(int i = 0; i < accesses.length; i++)
		{
			accesses[i] = BitSet.valueOf(new long[] {i});
		}
			
		for(int i = 0; i < usernames.length; i++)
		{
			int randIndx = mRandom.nextInt(accesses.length);
		
			boolean canViewBirthDate = accesses[randIndx].get(0);
			boolean canModifyBirthDate = accesses[randIndx].get(1);
			boolean canViewNames = accesses[randIndx].get(2);
			boolean canModifyNames = accesses[randIndx].get(3);
			
			UserAccount newUser = 
					new UserAccount(usernames[i], passwords[i], firstNames[i], lastNames[i],
							canViewBirthDate, canModifyBirthDate, canViewNames, canModifyNames);
			UserAccountList.add(newUser);
		}
	}	
}