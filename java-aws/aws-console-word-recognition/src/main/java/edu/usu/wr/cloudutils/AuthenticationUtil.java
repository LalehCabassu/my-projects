package edu.usu.wr.cloudutils;

import org.jclouds.ContextBuilder;
import org.jclouds.blobstore.BlobStore;
import org.jclouds.blobstore.BlobStoreContext;

public class AuthenticationUtil {
	
	private Credentials credentials;
	
	public AuthenticationUtil()
	{
		this.credentials = new Credentials();
	}
	
	public AuthenticationUtil(ProviderAPI provider)
	{
		this.credentials = new Credentials(provider);
	}
	
	public AuthenticationUtil(ProviderAPI provider, String credentialPath)
	{
		this.credentials = new Credentials(provider, credentialPath);
	}
	
	public BlobStore getBlobStore()
	{
		BlobStore result = null;
		String identity = credentials.getAccessKeyId();
		String credential = credentials.getSecretKey(); 
		
		String providerId;
		switch(credentials.getProvider())
		{
			case OPENSTACK_SWIFT:
				providerId = "swift";
				break;
				
			default:
				providerId = "aws-s3";
				break;
		}
		
		BlobStoreContext context = ContextBuilder.newBuilder(providerId)
                .credentials(identity, credential)
                .buildView(BlobStoreContext.class);
		if(context != null)
			result = context.getBlobStore();
		
		return result;
	}

	public Credentials getCredentials() {
		return credentials;
	}
		
}
