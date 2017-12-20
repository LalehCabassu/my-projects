package personalHealthInfo;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Date;
import java.util.Iterator;
import java.util.List;
import java.util.Locale;
import java.util.Random;

public class TestClass {

	private static Random mRandom = new Random();
	
	public static void main(String[] args) throws ParseException {
		
		// Problem 3-a: Creating a meaning set of Patient objects
		List<Patient> patients = CreatePatientObjects(10);
		System.out.println("Created patients:");
		printPatients(patients);
		
		// Problem 3-b: Exercise every method with representative object states and input parameters
		TestPatientMethods(patients.get(0));
	
		// Problem 4-a: Retrieve a sorted list of known names (primary or other) for a person
		Person person = createPersonWithFiveNames();
		System.out.println("\nUnsorted names:");
		printStringArray(person.GetSortNames());
		String[] sortedNames = sortNames(person);
		System.out.println("\nSorted names:");
		printStringArray(sortedNames);
		System.out.println("\n");
		
		// Problem 4-b: Find all persons with a diagnosed that is for a specified condition
		CreateDiagnosisObjects(patients);
		System.out.println("Add conditions:");
		printPatients(patients);
		List<Patient> ptns = FindPatientDiagnosisedWith(patients, "Cancer");
		System.out.println("Diagnosed patients with CANCER:");
		printPatients(ptns);
	}

	private static void printStringArray(String[] sortedNames) {
		for(int i = 0; i < sortedNames.length; i++)
			System.out.println(sortedNames[i]);
	}

	private static void printPatients(List<Patient> patients) {
		for(Iterator<Patient> p = patients.iterator(); p.hasNext();)
			System.out.println(p.next().toString());
	}

	private static List<Patient> CreatePatientObjects(int numberOfObjects) {
		
		Name[] names = createNames(numberOfObjects);
		
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
		
		List<Person> persons = new ArrayList<Person> ();
		for(int i = 0; i < numberOfObjects; i++)
		{
			int addIndex = i % addresses.length;
			persons.add(new Person(names[i], phones[i], addresses[addIndex]));
		}
		
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

	private static void TestPatientMethods(Patient patient)
	{
		// Create a person object for an emergency contact
		Name nameEC = new Name(Name.NameTypeEnum.LEGAL, "Ms", "Shannon", "Valerie", "Wertman", "");
		PhoneNumber phoneEC = new PhoneNumber(PhoneNumber.PhoneTypeEnum.CELL_PHONE, "435", "512", "4352", "");
		Address addressEC = new Address(Address.AddressTypeEnum.WORK, "1750 N Main St", "", "Logan", "Utah", "84341");
		Person personEC = new Person(nameEC, phoneEC, addressEC);
		
		// Create a Physician object
		Name namePH = new Name(Name.NameTypeEnum.LEGAL, "Dr", "Steven", "Watt", "Wynn", "MD");
		PhoneNumber phonePH = new PhoneNumber(PhoneNumber.PhoneTypeEnum.WORK, "801", "786", "7500", "");
		Address addressPH = new Address(Address.AddressTypeEnum.WORK, "2400 N 400 E", "", "Ogden", "Utah", "84414");
		
		Person personPH = new Person(namePH, phonePH, addressPH);
		Physician physician = new Physician(personPH, "1962510040", "Family Medicine");
		physician.AddPatient(patient);
		
		// Add those objects to the given patient
		System.out.println("\nAdd an emergency contact and a physician to a patient:");
		patient.AddEmergencyContact(personEC, "Sibling");
		patient.AddPhysician(physician);
		System.out.println(patient.toString());
		
		// Remove the emergency contact and the physician
		System.out.println("\nRemove the emergency contact and physician from the patient:");
		patient.RemoveEmergencyContact(personEC);
		patient.RemovePhysician(physician);
		System.out.println(patient.toString());
	}

	private static Person createPersonWithFiveNames()
	{
		Person result = new Person();
		Name[] names = createNames(5);
		
		for(int i = 0; i < names.length; i++)
			result.AddName(names[i]);
			
		return result;
	}
	
	private static String[] sortNames(Person person)
	{
		String[] result = person.GetSortNames();
		Arrays.sort(result);
		
		return result;
	}
	
	private static void CreateDiagnosisObjects(List<Patient> patients) throws ParseException
	{
		int numOfDiagnosis = 20;
		List<Diagnosis> diagnosises = new ArrayList<Diagnosis>();
		SimpleDateFormat df = new SimpleDateFormat("mm/dd/yyyy", Locale.US);
		
		String strDates [] = 
			{"9/4/2011", "5/9/2013", "1/5/2012", "11/24/2011", "7/21/2011", "2/13/2014", 
				"10/18/2010", "3/19/2009", "4/22/2010", "8/17/2013"};
		Date dates [] = new Date [strDates.length];
		for(int i = 0; i < dates.length; i++)
			dates[i] = df.parse(strDates[i]);
		
		String conditions [] = 
			{"Alzheimers Disease", 
				"Amyotrophic Lateral Sclerosis (ALS)", 
				"Arrhythmia", 
				"Arthritis", 
				"Atrial Fibrillation",
				"Autism", 
				"Cancer", 
				"Cholesterol", 
				"Chronic Pain", 
				"Chronic Kidney Disease"};
		
		for(int i = 0; i < numOfDiagnosis; i++)
		{
			int dateIndex = mRandom.nextInt(dates.length);
			int conditionIndex = mRandom.nextInt(conditions.length);
			diagnosises.add(new Diagnosis(dates[dateIndex], conditions[conditionIndex], ""));
		}
		
		for(Iterator<Patient> p = patients.iterator(); p.hasNext();)
		{
			int diagnosisIndex = mRandom.nextInt(numOfDiagnosis);
			Patient ptn = p.next();
			ptn.AddDiagnosis(diagnosises.get(diagnosisIndex));
		}
		
	}
	
	private static List<Patient> FindPatientDiagnosisedWith(List<Patient> patients, String condition)
	{
		List<Patient> result = new ArrayList<Patient>();
		
		for(Iterator<Patient> p = patients.iterator(); p.hasNext();)
		{
			Patient cp = p.next();
			if(cp.DiagnosisedWith(condition))
				result.add(cp);
		}
		return result;
	}
	
	
}
