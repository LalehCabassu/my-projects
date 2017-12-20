package personalHealthInfo;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.Iterator;
import java.util.List;
import java.util.Locale;

public class Patient extends Person
{
	private static int idSquence = 1;
	private int id;
	private String gender;
	private Date birthdate;
	private List<Physician> physicians;
	private List<EmergencyContact> emergencyContacts;
	private List<Surgery> surgeries;
	private List<Diagnosis> diagnosises;
	private List<Allergy> allergies;
	private List<Perscription> perscriptions;
	private List<HealthIssue> healthIssues;
	
	public Patient(Person person, String gender, String birthdate)
	{
		super(person);
		this.id = Patient.idSquence++;
		SetGender(gender);
		SetBirthdate(birthdate);
		physicians = new ArrayList<Physician>();
		emergencyContacts = new ArrayList<EmergencyContact>();
		surgeries = new ArrayList<Surgery>();
		diagnosises = new ArrayList<Diagnosis>();
		allergies = new ArrayList<Allergy>();
		perscriptions = new ArrayList<Perscription>();
		healthIssues = new ArrayList<HealthIssue>();
	}
	
	public Patient(Person person, String gender)
	{
		super(person);
		this.id = Patient.idSquence++;
		SetGender(gender);
		SetBirthdate("");
		this.physicians = new ArrayList<Physician>();
		this.emergencyContacts = new ArrayList<EmergencyContact>();
		this.surgeries = new ArrayList<Surgery>();
		this.diagnosises = new ArrayList<Diagnosis>();
		this.allergies = new ArrayList<Allergy>();
		this.perscriptions = new ArrayList<Perscription>();
		this.healthIssues = new ArrayList<HealthIssue>();
	}
	
	public Patient(Person person)
	{
		super(person);
		this.id = Patient.idSquence++;
		SetGender("");
		SetBirthdate("");
		this.physicians = new ArrayList<Physician>();
		this.emergencyContacts = new ArrayList<EmergencyContact>();
		this.surgeries = new ArrayList<Surgery>();
		this.diagnosises = new ArrayList<Diagnosis>();
		this.allergies = new ArrayList<Allergy>();
		this.perscriptions = new ArrayList<Perscription>();
		this.healthIssues = new ArrayList<HealthIssue>();
	}
	
	public int GetId()
	{
		return this.id;
	}
	
	public String GetGender()
	{
		return this.gender;
	}
	
	public Date GetBirthdate()
	{
		return this.birthdate;
	}
	
	public void SetGender(String gender)
	{
		this.gender = gender;
	}
	
	public void SetBirthdate(String birthdate)
	{
		SimpleDateFormat df = new SimpleDateFormat("mm/dd/yyyy", Locale.US);
		
		try {
			this.birthdate = df.parse(birthdate);
		} catch (ParseException e) {
			this.birthdate = new Date();
		}
	}
	
	public void AddPhysician(Physician newPhysician)
	{
		this.physicians.add(newPhysician);
	}
	
	public void RemovePhysician(Physician physician)
	{
		if(this.physicians.size() > 0)
			this.physicians.remove(physician);
	}
	
	public void AddEmergencyContact(Person person, String relationship)
	{
		EmergencyContact newContact = new EmergencyContact(person, relationship);
		this.emergencyContacts.add(newContact);
	}
	
	public void RemoveEmergencyContact(Person person)
	{
		EmergencyContact ec = findEmegencyContact(person);
		if(ec != null && this.emergencyContacts.size() > 0)
			this.emergencyContacts.remove(ec);
	}
	
	public void AddSurgery(Surgery newSurgery)
	{
		this.surgeries.add(newSurgery);
	}
	
	public void RemoveSurgery(Surgery surgery)
	{
		if(this.surgeries.size() > 0)
			this.surgeries.remove(surgery);
	}
	
	public void AddDiagnosis(Diagnosis newDiagnosis)
	{
		this.diagnosises.add(newDiagnosis);
	}
	
	public void RemoveDiagnosis(Diagnosis diagnosis)
	{
		if(this.diagnosises.size() > 0)
			this.diagnosises.remove(diagnosis);
	}

	public void AddAllergy(Allergy newAllergy)
	{
		this.allergies.add(newAllergy);
	}
	
	public void RemoveAllergy(Allergy allergy)
	{
		if(this.allergies.size() > 0)
			this.allergies.remove(allergy);
	}
	
	public void AddPerscription(Perscription newPerscription)
	{
		this.perscriptions.add(newPerscription);
	}
	
	public void RemovePerscription(Perscription perscription)
	{
		if(this.perscriptions.size() > 0)
			this.perscriptions.remove(perscription);
	}
	
	public void AddHealthIssue(HealthIssue newHealthIssue)
	{
		this.healthIssues.add(newHealthIssue);
	}
	
	public void RemoveHealthIssue(HealthIssue healthIssue)
	{
		if(this.healthIssues.size() > 0)
			this.healthIssues.remove(healthIssue);
	}
	
	public @Override String toString()
	{
		String result = "Patient: ";
		
		result += "Id: " + this.id + "\n"; 
		result += super.toString();
		result += "Gender: " + this.gender + " ";
		result += "Birthdate: " + this.birthdate.toString() + "\n";
		result += this.GetEmergencyContacts();
		result += this.GetPhysicians();
		result += this.GetDiagnosises();
		result += this.GetAllergies();
		result += this.GetSurgeries();
		result += this.GetPerscriptions();
		result += this.GetHealthIssues();
		
		return result;
	}
	
	public String GetPhysicians()
	{
		String result = "";
		
		if(this.physicians.size() > 0)
		{
			result += "Physicians:\n";
			for(int i = 0; i < this.physicians.size(); i++)
				result += "[" + i + "]: " + this.physicians.get(i).toString(); 
		}
		
		return result;
	}
	
	public String GetEmergencyContacts()
	{
		String result = "";
		
		if(this.emergencyContacts.size() > 0)
		{
			result += "Emergency Contacts:\n";
			for(int i = 0; i < this.emergencyContacts.size(); i++)
				result += "[" + i + "]: " + this.emergencyContacts.get(i).toString(); 
		}
		
		return result;
	}
	
	public String GetSurgeries()
	{
		String result = "";
		
		if(this.surgeries.size() > 0)
		{
			result += "Surgeries:\n";
			for(int i = 0; i < this.surgeries.size(); i++)
				result += "[" + i + "]: " + this.surgeries.get(i).toString(); 
		}
		
		return result;
	}
	
	public String GetDiagnosises()
	{
		String result = "";
		
		if(this.diagnosises.size() > 0)
		{
			result += "Diagnosises:\n";
			for(int i = 0; i < this.diagnosises.size(); i++)
				result += "[" + i + "]: " + this.diagnosises.get(i).toString(); 
		}
		
		return result;
	}
	
	public String GetAllergies()
	{
		String result = "";
		
		if(this.allergies.size() > 0)
		{
			result += "Allergies:\n";
			for(int i = 0; i < this.allergies.size(); i++)
				result += "[" + i + "]: " + this.allergies.get(i).toString(); 
		}
		
		return result;
	}
	
	public String GetPerscriptions()
	{
		String result = "";
		
		if(this.perscriptions.size() > 0)
		{
			result += "Perscriptions:\n";
			for(int i = 0; i < this.perscriptions.size(); i++)
				result += "[" + i + "]: " + this.perscriptions.get(i).toString(); 
		}
		
		return result;
	}
	
	public String GetHealthIssues()
	{
		String result = "";
		
		if(this.healthIssues.size() > 0)
		{
			result += "Health Issues:\n";
			for(int i = 0; i < this.healthIssues.size(); i++)
				result += "[" + i + "]: " + this.healthIssues.get(i).toString(); 
		}
		
		return result;
	}
	
	public boolean DiagnosisedWith(String condition)
	{
		boolean result = false;
		
		for(Iterator<Diagnosis> d = this.diagnosises.iterator(); d.hasNext();)
		{
			Diagnosis diagnosis = d.next();
			if(diagnosis.MatchCondition(condition))
			{
				result = true;
				break;
			}
		}
		return result;
	}
	
	private EmergencyContact findEmegencyContact(Person person)
	{
		EmergencyContact contact = null;
		
		for(Iterator<EmergencyContact> c = this.emergencyContacts.iterator(); c.hasNext();)
		{
			EmergencyContact ec = c.next();
			Person p = (Person)ec;
			if(p.Match(person) == 1.0)
			{
				contact = ec;
				break;
			}
		}		
		return contact;
	}
}
