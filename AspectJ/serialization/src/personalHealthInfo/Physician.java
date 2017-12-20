package personalHealthInfo;
import java.util.ArrayList;
import java.util.List;

public class Physician extends Person{

	private String nationalProviderId;
	private String specialization;
	private List<Patient> patients;
	private List<Surgery> surgeries;
	private List<Diagnosis> diagnosises;
	
	public Physician(Person person, String nationalProviderId, String specialization) {
		super(person);
		SetSpecialization(specialization);
		SetNationalProviderId(nationalProviderId);
		patients = new ArrayList<Patient>();
		surgeries = new ArrayList<Surgery>();
		diagnosises = new ArrayList<Diagnosis>();
	}
	
	public String GetSpecialization()
	{
		return this.specialization;
	}
	
	public void SetSpecialization(String spec)
	{
		this.specialization = spec;
	}
	
	public String GetNationalProviderId()
	{
		return this.nationalProviderId;
	}
	
	public void SetNationalProviderId(String nationalProviderId)
	{
		this.nationalProviderId = nationalProviderId;
	}
	
	public void AddPatient(Patient newPatient)
	{
		this.patients.add(newPatient);
	}
	
	public void RemovePatient(Patient patient)
	{
		if(this.patients.size() > 0)
			this.patients.remove(patient);
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
	
	public @Override String toString()
	{
		String result = "Physician    ";
		
		result += this.specialization + " ";
		result += this.nationalProviderId + "\n";
		result += super.toString() + "\n";
		//result += this.GetPatients() + "\n";
		//result += this.GetDiagnosises();
		
		return result;
	}
	
	public String GetPatients()
	{
		String result = "";
		
		if(this.patients.size() > 0)
		{
			result += "Patients:\n";
			for(int i = 0; i < this.patients.size(); i++)
				result += "[" + i + "]: " + this.patients.get(i).toString(); 
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
}
