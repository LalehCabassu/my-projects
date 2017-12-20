package edu.usu.cloud.wr.cloudutils;

import org.jclouds.ContextBuilder;
import org.jclouds.blobstore.BlobStore;
import org.jclouds.blobstore.BlobStoreContext;
import org.jclouds.s3.domain.AccessControlList;
import org.jclouds.s3.domain.CanonicalUser;
import org.jclouds.s3.domain.AccessControlList.CanonicalUserGrantee;
import org.jclouds.s3.domain.AccessControlList.Grantee;

public class AuthenticationUtil {
	
	private Credentials credentials;
	private BlobStoreContext blobStoreContext;
	
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
	
	private void buildBlobStoreContext()
	{
		if(blobStoreContext == null)
		{
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
			
			blobStoreContext = ContextBuilder.newBuilder(providerId)
	                .credentials(identity, credential)
	                .buildView(BlobStoreContext.class);
		}
	}
	
	public BlobStoreContext getBlobStoreContext()
	{
		buildBlobStoreContext();
		return this.blobStoreContext;
	}
	
	public BlobStore getBlobStore()
	{
		BlobStore result = null;
		
		buildBlobStoreContext();
		if(blobStoreContext != null)
			result = blobStoreContext.getBlobStore();
		
		return result;
	}
	
	public AccessControlList createBlobACL(BlobAccessOptions accessOptions) {
		AccessControlList acl = new AccessControlList();
		CanonicalUser owner = new CanonicalUser(credentials.getCanonicalUserId());
		acl.setOwner(owner);
		
		Grantee grantee = new CanonicalUserGrantee(credentials.getCanonicalUserId());
		
		switch(accessOptions)
		{
		case FULL_ACCESS:
			acl.addPermission(grantee, AccessControlList.Permission.FULL_CONTROL);
			break;
		case READ_ACL:
			acl.addPermission(grantee, AccessControlList.Permission.READ_ACP);
			break;
		case WRITE_ACL:
			acl.addPermission(grantee, AccessControlList.Permission.WRITE_ACP);
			break;
		default:
			acl.addPermission(grantee, AccessControlList.Permission.READ);
			break;
		}
		return acl;
	}
	
	public AccessControlList createContainerACL(ContainerAccessOptions accessOptions) {
		AccessControlList acl = new AccessControlList();
		CanonicalUser owner = new CanonicalUser(credentials.getCanonicalUserId());
		acl.setOwner(owner);
		
		Grantee grantee = new CanonicalUserGrantee(credentials.getCanonicalUserId());
		
		switch(accessOptions)
		{
		case FULL_ACCESS:
			acl.addPermission(grantee, AccessControlList.Permission.FULL_CONTROL);
			break;
		case READ_ACL:
			acl.addPermission(grantee, AccessControlList.Permission.READ_ACP);
			break;
		case WRITE_ACL:
			acl.addPermission(grantee, AccessControlList.Permission.WRITE_ACP);
			break;
		case WRITE:
			acl.addPermission(grantee, AccessControlList.Permission.WRITE);
			break;
		default:
			acl.addPermission(grantee, AccessControlList.Permission.READ);
			break;
		}
		return acl;
	}
	
		
}
