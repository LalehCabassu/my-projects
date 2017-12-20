package aspects.serialization;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.nio.charset.Charset;
import java.nio.file.FileSystems;
import java.nio.file.Files;
import java.nio.file.Path;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Locale;

import personalHealthInfo.*;

public aspect SerializePatients {

	private static final String objectDELIMETER = "\n";
	private static final String fieldDELIMETER = ";";
	private static final String elementDELIMETER = ",";
	public static final String DELIMETER = "/";
	
	private Path file = FileSystems.getDefault().getPath("patients.txt");
	private static List<Patient> patients = new ArrayList<Patient>();
	
	pointcut mainPC(): execution(void main(String[]));
	
	void around(): mainPC()
	{
		loadPatients();
		proceed();
		savePatients();
	}
	
	public static void main(String[] args) {
		
		if(patients.size() == 0)
			patients = GenerateObjects.CreatePatientObjects(5);
	}

	private void loadPatients()
	{
		Charset charset = Charset.forName("US-ASCII");
		try (BufferedReader reader = Files.newBufferedReader(file, charset)) {
		    String line = null;
		    while ((line = reader.readLine()) != null) {
	        	Patient patient = Patient.deserialize(line);
	        	patients.add(patient);
		    }
		} catch (IOException x) {
		    System.err.format("IOException: %s%n", x);
		}
	}
	
	private void savePatients()
	{
		String s = "";
		for(Patient p: patients)
		{
			s += p.serialize();
			s += objectDELIMETER;
		}
		
		Charset charset = Charset.forName("US-ASCII");
		try (BufferedWriter writer = Files.newBufferedWriter(file, charset)) {
		    writer.write(s, 0, s.length());
		} catch (IOException x) {
		    System.err.format("IOException: %s%n", x);
		}	
	}
	
	public static Patient Patient.deserialize(String serializedPatient)
	{
		String[] patientStr = serializedPatient.split(fieldDELIMETER);
		String
			namesStr = patientStr[0], 
			phoneNumbersStr = patientStr[1], 
			addressesStr = patientStr[2], 
			genderStr = patientStr[3], 
			birthDateStr = patientStr[4];
		
		String personStr = namesStr + fieldDELIMETER + phoneNumbersStr + 
				fieldDELIMETER + addressesStr + fieldDELIMETER;
		
		Person person = Person.deserialize(personStr);
		Patient patient = new Patient(person, genderStr, birthDateStr);
		
		if(patientStr.length >= 6)
		{
			String physiciansStr = patientStr[5];
			String[] physicianListStr = physiciansStr.split(elementDELIMETER);
			for(String serializedPhysician: physicianListStr)
			{
				Physician physician = Physician.deserialize(serializedPhysician);
				patient.AddPhysician(physician);
			}
		}
		
		if(patientStr.length >= 7)
		{
			String emergencyContactsStr = patientStr[6];
			String[] emergencyContactListStr = emergencyContactsStr.split(elementDELIMETER);
			for(String serializedEmergencyContact: emergencyContactListStr)
			{
				EmergencyContact emergencyContact = EmergencyContact.deserialize(serializedEmergencyContact);
				patient.AddEmergencyContact(emergencyContact);
			}
		}
		
		if(patientStr.length >= 8)
		{
			String surgeriesStr = patientStr[7];
			String[] surgeryListStr = surgeriesStr.split(elementDELIMETER);
			for(String serializedSurgery: surgeryListStr)
			{
				Surgery surgery = Surgery.deserialize(serializedSurgery);
				patient.AddSurgery(surgery);
			}
		}
		
		if(patientStr.length >= 9)
		{
			String diagnosisesStr = patientStr[8]; 
			String[] diagnosisListStr = diagnosisesStr.split(elementDELIMETER);
			for(String serializedDiagnosis: diagnosisListStr)
			{
				Diagnosis diagnosis = Diagnosis.deserialize(serializedDiagnosis);
				patient.AddDiagnosis(diagnosis);
			}
		}
		
		if(patientStr.length >= 10)
		{
			String allergiesStr = patientStr[9];
			String[] allergyListStr = allergiesStr.split(elementDELIMETER);
			for(String serializedAllergy: allergyListStr)
			{
				Allergy allergy = Allergy.deserialize(serializedAllergy);
				patient.AddAllergy(allergy);
			}
		}
		
		if(patientStr.length >= 11)
		{
			String 	perscriptionsStr = patientStr[10]; 
			String[] perscriptionListStr = perscriptionsStr.split(elementDELIMETER);
			for(String serializedPerscription: perscriptionListStr)
			{
				Perscription perscription = Perscription.deserialize(serializedPerscription);
				patient.AddPerscription(perscription);
			}
		}
		
		if(patientStr.length >= 12)
		{
			String 	healthIssuesStr = patientStr[11];
			String[] healthIssueListStr = healthIssuesStr.split(elementDELIMETER);
			for(String serializedHealthIssue: healthIssueListStr)
			{
				HealthIssue healthIssue = HealthIssue.deserialize(serializedHealthIssue);
				patient.AddHealthIssue(healthIssue);
			}
		}
		return patient;
	}
	
	public String Patient.serialize()
	{
		String result = "";
		
		result += super.serialize();
		
		result += serializeField(this.GetGender());
		result += serializeField(this.GetBirthdate());

		List<Physician> physicians = this.getPhysicians();
		int listSize = physicians.size();
		for(int i = 0; i < listSize; i++)
		{
			if(i != listSize - 1)
				result += physicians.get(i).serialize() + elementDELIMETER;
			else
				result += physicians.get(i).serialize() + fieldDELIMETER;
		}
		
		List<EmergencyContact> emergencyContacts = this.getEmergencyContacts();
		listSize = emergencyContacts.size();
		for(int i = 0; i < listSize; i++)
		{
			if(i != listSize - 1)
				result += emergencyContacts.get(i).serialize() + elementDELIMETER;
			else
				result += emergencyContacts.get(i).serialize() + fieldDELIMETER;
		}
		
		List<Surgery> surgeries = this.getSurgeries();
		listSize = surgeries.size();
		for(int i = 0; i < listSize; i++)
		{
			if(i != listSize - 1)
				result += surgeries.get(i).serialize() + elementDELIMETER;
			else
				result += surgeries.get(i).serialize() + fieldDELIMETER;
		}
		
		List<Diagnosis> diagnosises = this.getDiagnosises();
		listSize = diagnosises.size();
		for(int i = 0; i < listSize; i++)
		{
			if(i != listSize - 1)
				result += diagnosises.get(i).serialize() + elementDELIMETER;
			else
				result += diagnosises.get(i).serialize() + fieldDELIMETER;
		}
		
		List<Allergy> allergies = this.getAllergies();
		listSize = allergies.size();
		for(int i = 0; i < listSize; i++)
		{
			if(i != listSize - 1)
				result += allergies.get(i).serialize() + elementDELIMETER;
			else
				result += allergies.get(i).serialize() + fieldDELIMETER;
		}

		List<Perscription> perscriptions = this.getPerscriptions();
		listSize = perscriptions.size();
		for(int i = 0; i < listSize; i++)
		{
			if(i != listSize - 1)
				result += perscriptions.get(i).serialize() + elementDELIMETER;
			else
				result += perscriptions.get(i).serialize() + fieldDELIMETER;
		}

		List<HealthIssue> healthIssues = this.getHealthIssues();
		listSize = healthIssues.size();
		for(int i = 0; i < listSize; i++)
		{
			if(i != listSize - 1)
				result += healthIssues.get(i).serialize() + elementDELIMETER;
			else
				result += healthIssues.get(i).serialize() + fieldDELIMETER;
		}

		return result;
	}
	
	public String Address.serialize()
	{
		return this.GetAddressType() + DELIMETER + this.GetStreetLine1() + DELIMETER + this.GetStreetLine2() + DELIMETER +
				this.GetCity() + DELIMETER + this.GetState() + DELIMETER + this.GetPostalCode();		
	}
	
	public static Address Address.deserialize(String serializedAddress)
	{
		String[] addressStr = serializedAddress.split(DELIMETER);
		String addressTypeStr = addressStr[0],
				streetLine1Str = addressStr[1],
				streetLine2Str = addressStr[2],
				cityStr = addressStr[3],
				stateStr = addressStr[4],
				postalCodeStr = addressStr[5];
		
		Address.AddressTypeEnum addressType;
		switch(addressTypeStr)
		{
			case "HOME":
				addressType = Address.AddressTypeEnum.HOME;
				break;
			case "WORK":
				addressType = Address.AddressTypeEnum.WORK;
				break;
			default:
				addressType = Address.AddressTypeEnum.OTHER;
				break;
		}
		
		return new Address(addressType, streetLine1Str, streetLine2Str, cityStr, stateStr, postalCodeStr);
	}
	
	public String Allergy.serialize()
	{
		return this.GetAllergen() + DELIMETER + this.GetSeverity();
	}
	
	public static Allergy Allergy.deserialize(String serializedAllergy)
	{
		String[] allergyStr = serializedAllergy.split(DELIMETER);
		String allergenStr = allergyStr[0],
				severityStr = allergyStr[1];
		
		return new Allergy(allergenStr, Integer.parseInt(severityStr));
	}
	
	public String Diagnosis.serialize()
	{
		return this.GetDate() + DELIMETER + this.GetCondition() + DELIMETER + this.GetNotes();
	}
	
	public static Diagnosis Diagnosis.deserialize(String serializedDiagnosis)
	{
		String[] diagnosisStr = serializedDiagnosis.split(DELIMETER);
		String dateStr = diagnosisStr[0],
				conditionStr = diagnosisStr[1],
				notesStr = diagnosisStr[2];
		
		return new Diagnosis(parseDate(dateStr), conditionStr, notesStr);
	}
	
	public String EmergencyContact.serialize()
	{
		return this.GetRelationship() + DELIMETER + super.serialize();
	}
	
	public static EmergencyContact EmergencyContact.deserialize(String serializedEmergencyContact)
	{
		String[] emergencyContactStr = serializedEmergencyContact.split(DELIMETER, 2);
		String relationshipStr = emergencyContactStr[0],
				personStr = emergencyContactStr[1];
		
		Person person = Person.deserialize(personStr);
		
		return new EmergencyContact(person, relationshipStr);
	}
	
	public String HealthIssue.serialize()
	{
		return this.GetBeganOn() + DELIMETER + this.GetEndedOn() + DELIMETER + this.GetSymptomOrObservation();
	}
	
	public static HealthIssue HealthIssue.deserialize(String serializedHealthIssue)
	{
		String[] healthIssueStr = serializedHealthIssue.split(DELIMETER);
		String beganOnStr = healthIssueStr[0],
				endedonStr = healthIssueStr[1],
				symptomOrObservation = healthIssueStr[2];
		
		return new HealthIssue(parseDate(beganOnStr), parseDate(endedonStr), symptomOrObservation);
	}
	
	public String Name.serialize()
	{
		return this.GetNameType() + DELIMETER + this.GetSalutation() + DELIMETER + 
				this.GetFirstName() + DELIMETER + this.GetMiddleName() + DELIMETER + 
					this.GetLastName() + DELIMETER + this.GetSuffix();
	}
	
	public static Name Name.deserialize(String serializedName)
	{
		String[] nameStr = serializedName.split(DELIMETER);
		String nameTypeStr = nameStr[0],
				salutation = nameStr[1],
				firstName = nameStr[2],
				middleName = nameStr[3],
				lastName = nameStr[4],
				suffix = (nameStr.length >= 6)? nameStr[5] : "";
				
		Name.NameTypeEnum nameType;
		switch(nameTypeStr)
		{
			case "COMMON":
				nameType = Name.NameTypeEnum.COMMON;
				break;
			case "ALIAS":
				nameType = Name.NameTypeEnum.ALIAS;
				break;
			default:
				nameType = Name.NameTypeEnum.LEGAL;
				break;
		}
		
		return new Name(nameType, salutation, firstName, middleName, lastName, suffix);
	}
	
	public String Perscription.serialize()
	{
		return this.GetStartDate() + DELIMETER + this.GetEndDate() + DELIMETER + 
				this.GetMedication() + DELIMETER + this.GetDosage() + DELIMETER + 
					this.GetFrequency();
	}
	
	public static Perscription Perscription.deserialize(String serializedPerscription)
	{
		String[] perscriptionStr = serializedPerscription.split(DELIMETER);
		String startDateStr = perscriptionStr[0],
				endDateStr = perscriptionStr[1],
				medication = perscriptionStr[2],
				dosage = perscriptionStr[3],
				frequency = perscriptionStr[4];
		
		return new Perscription(parseDate(startDateStr), parseDate(endDateStr),
					medication, dosage, frequency);
	}

	public String PhoneNumber.serialize()
	{
		return this.GetPhoneType() + DELIMETER + this.GetAreaCode() + DELIMETER + this.GetExchange() + DELIMETER + 
					this.GetDetailNumber() + DELIMETER + this.GetExtension();
	}
	
	public static PhoneNumber PhoneNumber.deserialize(String serializedPhoneNumber)
	{
		String[] phoneNumberStr = serializedPhoneNumber.split(DELIMETER);
		String phoneTypeStr = phoneNumberStr[0],
				areaCode = phoneNumberStr[1],
				exchange = phoneNumberStr[2],
				detailNumber = phoneNumberStr[3],
				extension = (phoneNumberStr.length >= 5) ? phoneNumberStr[4] : "";
		
		PhoneNumber.PhoneTypeEnum phoneType;
		switch(phoneTypeStr)
		{
			case "HOME":
				phoneType = PhoneNumber.PhoneTypeEnum.HOME;
				break;
			case "CELL_PHONE":
				phoneType = PhoneNumber.PhoneTypeEnum.CELL_PHONE;
				break;
			case "WORK":
				phoneType = PhoneNumber.PhoneTypeEnum.WORK;
				break;
			case "FAX":
				phoneType = PhoneNumber.PhoneTypeEnum.FAX;
				break;
			default:
				phoneType = PhoneNumber.PhoneTypeEnum.OTHER;
				break;	
		}
		
		return new PhoneNumber(phoneType, areaCode, exchange, detailNumber, extension);
	}
	
	public String Physician.serialize()
	{
		return this.GetSpecialization() + DELIMETER + this.GetNationalProviderId() + DELIMETER +
				super.serialize();
	}
	
	public static Physician Physician.deserialize(String serializedPhysician)
	{
		String[] physicianStr = serializedPhysician.split(DELIMETER, 3);
		String specialization = physicianStr[0],
				nationalProviderId = physicianStr[1],
				personStr = physicianStr[2];
				
		Person person = Person.deserialize(personStr);
		
		return new Physician(person, nationalProviderId, specialization);
	}
	
	public String Surgery.serialize()
	{
		return this.GetDate() + DELIMETER + this.GetNotes();
	}	
	
	public static Surgery Surgery.deserialize(String serializedSurgery)
	{
		String[] surgeryStr = serializedSurgery.split(DELIMETER);	
		String dateStr = surgeryStr[0],
				notes = surgeryStr[1];
		
		return new Surgery(parseDate(dateStr), notes);
	}
	
	public String Person.serialize()
	{
		String result = "";
		
		List<Name> names = this.getNames();
		int listSize = names.size();
		for(int i = 0; i < listSize; i++)
		{
			if(i != listSize - 1)
				result += names.get(i).serialize() + elementDELIMETER;
			else
				result += names.get(i).serialize() + fieldDELIMETER;
		}

		List<PhoneNumber> phoneNumbers = this.getPhoneNumbers();
		listSize = phoneNumbers.size();
		for(int i = 0; i < listSize; i++)
		{
			if(i != listSize - 1)
				result += phoneNumbers.get(i).serialize() + elementDELIMETER;
			else
				result += phoneNumbers.get(i).serialize() + fieldDELIMETER;
		}
		
		List<Address> addresses = this.getAddresses();
		listSize = addresses.size();
		for(int i = 0; i < listSize; i++)
		{
			if(i != listSize - 1)
				result += addresses.get(i).serialize() + elementDELIMETER;
			else
				result += addresses.get(i).serialize() + fieldDELIMETER;
		}
		
		return result;
	}
	
	public static Person Person.deserialize(String deserializedPerson)
	{
		String[] personStr = deserializedPerson.split(fieldDELIMETER);
		String namesStr = personStr[0],
				numbersStr = personStr[1],
				addressesStr = personStr[2];
		
		String[] nameListStr = namesStr.split(elementDELIMETER);
		List<Name> names = new ArrayList<Name>();
		for(String serializedName: nameListStr)
		{
			Name name = Name.deserialize(serializedName);
			names.add(name);
		}
		
		String[] numberListStr = numbersStr.split(elementDELIMETER);
		List<PhoneNumber> phoneNumbers = new ArrayList<PhoneNumber>();
		for(String serializedNumber: numberListStr)
		{
			PhoneNumber number = PhoneNumber.deserialize(serializedNumber);
			phoneNumbers.add(number);
		}
		
		String[] addressListStr = addressesStr.split(elementDELIMETER);
		List<Address> addresses = new ArrayList<Address>();
		for(String serializedAddress: addressListStr)
		{
			Address address = Address.deserialize(serializedAddress);
			addresses.add(address);
		}
		
		return new Person(names, phoneNumbers, addresses);
	}
	
	public static <T> String serializeField(T field)
	{
		return field + fieldDELIMETER;
	}
	
	private static Date parseDate(String dateString)
	{
		Date result;
		SimpleDateFormat df = new SimpleDateFormat("mm/dd/yyyy", Locale.US);
		
		try {
			result = df.parse(dateString);
		} catch (ParseException e) {
			result = new Date();
		}
		
		return result;
	}
}
