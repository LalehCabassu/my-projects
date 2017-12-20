package aspects.testing;

import personalHealthInfo.*;

import java.text.ParseException;
import java.util.BitSet;
import java.util.Date;
import java.util.Iterator;
import java.util.List;
import java.util.Random;

import aspects.accessrights.Authentication;
import aspects.accessrights.UserSession;



public aspect TestAccessRights {	
	
	private Patient curPatient;
	pointcut createPatientPC(): execution(Patient GenerateObjects.createPatient()); 
	
	pointcut viewBirthDatePC(): call(Date Patient.GetBirthdate());
	pointcut modifyBirthDatePC(String date): args(date) && call(void Patient.SetBirthdate(String));

	pointcut viewNamesPC(): call(String Person.GetNamesString()) || call(List<Name> Person.getNames());
	pointcut modifyNamesPC(Name name): args(name) && (call(void Person.AddName(Name)) || call(void Person.RemoveName(Name)));
	
	Patient around(): createPatientPC()
	{
		System.out.println(">> Creating a new patient...");
		curPatient = proceed();
		System.out.println(">> The patient information:");
		System.out.println(curPatient);
		return curPatient;
	}
	
	Date around(): viewBirthDatePC()
	{
		Date result = null;
		
		UserSession curSession = Authentication.getCurrentSesstion();
		if(curSession != null && curSession.canViewBirthDate())
			result =  proceed();
		else
			System.out.println(">>> Access denied to view 'birth date'.");
		return result;
	}
	
	void around(String date): modifyBirthDatePC(date)
	{
		UserSession curSession = Authentication.getCurrentSesstion();
		if(curSession != null && curSession.canModifyBirthDate())
		{	
			proceed(date);
			System.out.println(">>> Birth date has been changed to " + date + ".");
		}
		else
			System.out.println(">>> Access denied to modify 'birth date'.");
	}
	
	Object around(): viewNamesPC()
	{
		Object names = null;
		
		UserSession curSession = Authentication.getCurrentSesstion();
		if(curSession != null && curSession.canViewNames())
			names = proceed();
		else
			System.out.println(">>> Access denied to view 'names'.");
		return names;
	}
	
	void around(Name name): modifyNamesPC(name)
	{
		UserSession curSession = Authentication.getCurrentSesstion();
		if(curSession != null && curSession.canModifyNames())
		{	
			proceed(name);
			System.out.println(">>> Add/remove a name '" + name + "'.");
		}
		else
			System.out.println(">>> Access denied to add/remove a 'name'.");
	}
	
//	public static void main(String[] args) throws ParseException {
//		GenerateObjects.createUserAccounts();
//		
//		for(Iterator<UserAccount> u = UserAccountList.getIterator(); u.hasNext();)
//		{
//			UserAccount user = u.next();
//			UserSession session = UserSession.Login(user.getUsername(), user.getPassword());
//			if(session != null)
//			{
//				// Modify names and birth date in Patient constructor
//				// View names and birth date in Patient.toString()
//				Patient patient = GenerateObjects.createPatient();
//				
//				// Modify names
//				Name name = new Name(Name.NameTypeEnum.LEGAL, "", "Laleh", "", "Rostami", "");
//				patient.RemoveName(name);
//				patient.GetNamesString();
//				
//				UserSession.LogOut(session);
//			}	
//		}
//	}
	
}
