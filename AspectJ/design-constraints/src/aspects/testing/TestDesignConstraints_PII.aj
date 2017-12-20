package aspects.testing;

import personalHealthInfo.*;

public aspect TestDesignConstraints_PII {

	declare warning:
			call(@PIISubject * personalHealthInfo.*.*(..)):
			"Caution: Accessing PII";
			
		
//	declare warning:
//		(get(!private * personalHealthInfo.*.*) ||
//			set(!private * personalHealthInfo.*.*)):
//			"Caution: Field access modifier should be private";
	
	
	public static void main(String[] args) {
		Name name = new Name();
		name.SetFirstName("Laleh");
		name.SetLastName("Rostami");
		System.out.println("New name:" + name.GetFormattedName());
	}
}