package aspects.testing;

import java.security.GeneralSecurityException;
import personalHealthInfo.*;
import aspects.*;
import aspects.encrypt.Encryption;


public aspect TestEncryptedFields {

	pointcut encryptPC(String value):
		call(@Encrypt * personalHealthInfo.*.Set*(String)) && args(value);
	
	pointcut decryptPC():
		call(@Encrypt * personalHealthInfo.*.Get*());
	
	
	void around(String value): encryptPC(value)
	{
		String encryptedValue = Encryption.encrypt(value.toString());
		proceed(encryptedValue);
		System.out.println("**EncryptedValue: " + encryptedValue);
		return;
	}
	
	String around(): decryptPC()
	{
		String decryptedValue = proceed();
		System.out.println("*DecryptedValue: " + decryptedValue);
		return Encryption.decrypt(decryptedValue);
	}
	
	
//	public static void main(String[] args) {
//		Name name = new Name(Name.NameTypeEnum.LEGAL, "", "Laleh", "", "Rostami", "");
//		String salutation = "MISS";
//		System.out.println("Entered a salutation: " + salutation);
//		name.SetSalutation(salutation);
//		System.out.println("First name:" + name.GetFirstName());
//	}

}
