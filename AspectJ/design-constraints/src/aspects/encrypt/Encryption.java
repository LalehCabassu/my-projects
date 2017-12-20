package aspects.encrypt;

import java.nio.charset.Charset;
import java.security.GeneralSecurityException;

import javax.crypto.Cipher;
import javax.crypto.spec.IvParameterSpec;
import javax.crypto.spec.SecretKeySpec;

public class Encryption {
	
	public static String encrypt(String value)
	{
		return new StringBuffer(value).reverse().toString();
	}
	
	public static String decrypt(String encryptedValue)
	{
		return new StringBuffer(encryptedValue).reverse().toString();
	}

//	private static String key = "MySecretKey;)";

//	public static String encrypt(String value)
//	      throws GeneralSecurityException {
//
//		String result = "";
//		
//	    byte[] raw = key.getBytes(Charset.forName("US-ASCII"));
//	    if (raw.length != 16) {
//	      throw new IllegalArgumentException("Invalid key size.");
//	    }
//
//	    SecretKeySpec skeySpec = new SecretKeySpec(raw, "AES");
//	    Cipher cipher = Cipher.getInstance("AES/CBC/PKCS5Padding");
//	    cipher.init(Cipher.ENCRYPT_MODE, skeySpec, new IvParameterSpec(new byte[16]));
//	    byte[] encrypted =  cipher.doFinal(value.getBytes(Charset.forName("US-ASCII")));
//	    result = new String(encrypted, Charset.forName("US-ASCII"));
//	    return result;
//	  }

//	  public static String decrypt(String key, byte[] encrypted)
//	      throws GeneralSecurityException {
//
//	    byte[] raw = key.getBytes(Charset.forName("US-ASCII"));
//	    if (raw.length != 16) {
//	      throw new IllegalArgumentException("Invalid key size.");
//	    }
//	    SecretKeySpec skeySpec = new SecretKeySpec(raw, "AES");
//
//	    Cipher cipher = Cipher.getInstance("AES/CBC/PKCS5Padding");
//	    cipher.init(Cipher.DECRYPT_MODE, skeySpec,
//	        new IvParameterSpec(new byte[16]));
//	    byte[] original = cipher.doFinal(encrypted);
//
//	    return new String(original, Charset.forName("US-ASCII"));
//	  }
}
