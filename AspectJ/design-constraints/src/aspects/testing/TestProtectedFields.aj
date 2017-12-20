package aspects.testing;

import java.text.ParseException;
import java.util.Iterator;

import aspects.accessrights.Authentication;
import aspects.accessrights.UserSession;
import aspects.protect.Protected;
import aspects.protect.Protected.accessRight;

import personalHealthInfo.GenerateObjects;
import personalHealthInfo.Name;
import personalHealthInfo.Patient;
import personalHealthInfo.UserAccount;
import personalHealthInfo.UserAccountList;

public aspect TestProtectedFields
{
	pointcut protectedPC(Protected annotation):
		(call(@Protected * personalHealthInfo.*.*(..)) || 
			call(@Protected * personalHealthInfo.*.*()) ) &&
		args(annotation);
	
	Object around(Protected annotation): protectedPC(annotation)
	{
		Object result = null;
		UserSession curSession = Authentication.getCurrentSesstion();
		
		String username = annotation.getUsername();
		Protected.accessRight accessRight = annotation.getAccessRight();
		if(username == curSession.getUsername())
		{
			if(accessRight == Protected.accessRight.VIEW && curSession.canView() ||
					accessRight == Protected.accessRight.MODIFY && curSession.canModify())
			{
				System.out.println(">>> Access granted to view/modify " + thisJoinPointStaticPart.getSignature().getName());
				result = proceed(annotation);
			}
			else
				System.out.println(">>> Access denied to view/modify " + thisJoinPointStaticPart.getSignature().getName());
		}
		else
			System.out.println(">>> Access denied to view/modify " + thisJoinPointStaticPart.getSignature().getName());
		
		return result;
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
