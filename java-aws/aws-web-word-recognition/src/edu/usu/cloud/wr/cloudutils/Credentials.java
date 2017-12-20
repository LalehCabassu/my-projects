package edu.usu.cloud.wr.cloudutils;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;

public class Credentials {
	
	private ProviderAPI provider;
	private String accessKeyId;
	private String secretKey;
	private String credentialFile;
	private String canonicalUserId;
	
	public Credentials()
	{
		this.provider = ProviderAPI.AMAZON_S3;
		this.credentialFile = "/Users/Life/.aws/credentials/rootkey";
		initCredentials(credentialFile);
	}
	
	public Credentials(ProviderAPI provider)
	{
		this.provider = provider;
		this.credentialFile = "/Users/Life/.aws/credentials/rootkey";
		initCredentials(credentialFile);
	}
	
	public Credentials(ProviderAPI provider, String credentialFile)
	{
		this.provider = provider;
		this.credentialFile = credentialFile;
		initCredentials(credentialFile);
	}
	
	private void initCredentials(String filePath)
	{
		File credFile = new File(filePath);
		
		if(credFile.exists())
		{
			BufferedReader br;
			try {
				br = new BufferedReader(new FileReader(filePath));
				String line = br.readLine();
			    this.accessKeyId = line.substring(line.indexOf('=') + 1, line.length());
			    line = br.readLine();
			    this.secretKey = line.substring(line.indexOf('=') + 1, line.length());
			    line = br.readLine();
			    this.canonicalUserId = line.substring(line.indexOf('=') + 1, line.length());
			    br.close();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
	}

	public ProviderAPI getProvider() {
		return provider;
	}

	public String getAccessKeyId() {
		return accessKeyId;
	}

	public String getSecretKey() {
		return secretKey;
	}

	public String getCredentialFile() {
		return credentialFile;
	}

	public String getCanonicalUserId() {
		return canonicalUserId;
	}
}
