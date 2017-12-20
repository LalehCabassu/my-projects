package edu.usu.wr.cloudutils;

import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.List;

import org.jclouds.blobstore.BlobStore;
import org.jclouds.blobstore.domain.Blob;
import org.jclouds.blobstore.domain.StorageMetadata;
import org.jclouds.blobstore.domain.StorageType;
import org.jclouds.blobstore.options.ListContainerOptions;

import com.google.common.io.ByteSource;

public class BlobStoreUtil {
	
	private AuthenticationUtil authentication;
	private BlobStore blobStore;
	
	public BlobStoreUtil()
	{
		this.authentication = new AuthenticationUtil();
		this.blobStore = authentication.getBlobStore();
	}
	
	public BlobStoreUtil(@NonNull ProviderAPI provider)
	{
		this.authentication = new AuthenticationUtil(provider);
		this.blobStore = authentication.getBlobStore();
	}
	
	public BlobStoreUtil(@NonNull ProviderAPI provider, @NonNull String credentialPath)
	{
		this.authentication = new AuthenticationUtil(provider, credentialPath);
		this.blobStore = authentication.getBlobStore();
	}
	
	public void createContainer(@NonNull String containerName)
	{
		if(!this.blobStore.containerExists(containerName))
			this.blobStore.createContainerInLocation(null, containerName);
	}
	
	public void createEmptyContainer(@NonNull String containerName)
	{
		if(!this.blobStore.containerExists(containerName))
			this.blobStore.createContainerInLocation(null, containerName);
		else
			blobStore.clearContainer(containerName);
	}
	
	public void clearContainer(@NonNull String containerName)
	{
		blobStore.clearContainer(containerName);
	}
	
	public void clearDirectory(@NonNull String containerName, @NonNull String directoryName)
	{
		// It does not work
//		if(directoryExists(containerName, directoryName))
//		{
//			ListContainerOptions options = new ListContainerOptions();
//			options.inDirectory(directoryName);
//			this.blobStore.clearContainer(containerName, options);
//		}
		
		List<String> blobs = listBlobNamesInDirectory(containerName, directoryName); 
		for(String blobName: blobs)
			removeBlob(containerName, directoryName, blobName);
	}
	
	public boolean containerExists(@NonNull String containerName)
	{
		return this.blobStore.containerExists(containerName);
	}
	
	public boolean directoryExists(@NonNull String containerName, @NonNull String directoryName)
	{
		boolean result = false;
		
		List<StorageMetadata> resources = listBlobsInContainer(containerName);
		for(StorageMetadata rs: resources)
		{
			StorageType type = rs.getType();
			if(type == StorageType.RELATIVE_PATH && rs.getName().equals(directoryName))
			{
				result = true;
				break;
			}
		}
				
		return result;
//		return this.blobStore.directoryExists(containerName, directoryName);
	}
	
	public boolean blobExists(@NonNull String containerName, @NonNull String blobName)
	{
		return this.blobStore.blobExists(containerName, blobName);
	}
	
	public boolean blobExists(@NonNull String containerName, @NonNull String directoryName, @NonNull String blobName)
	{
		String fullPath = directoryName + "/" + blobName;
		return this.blobStore.blobExists(containerName, fullPath);
	}
		
	public long countBlobsInContainer(@NonNull String containerName)
	{
		return this.blobStore.countBlobs(containerName);
	}
	
	public long countBlobsInDirectory(@NonNull String containerName, @NonNull String directoryName)
	{
		long result = 0;
		
		ListContainerOptions options = new ListContainerOptions();
		options.inDirectory(directoryName);
		result = this.blobStore.countBlobs(containerName, options);
		
		return result;
	}
	
	public long getContainerSize(@NonNull String containerName)
	{
		long result = 0;
		
		for(StorageMetadata blob: this.blobStore.list(containerName))
			result += getBlobSize(containerName, blob.getName());
		
		return result;
	}
	
	public long getDirectorySize(@NonNull String containerName, @NonNull String directoryName)
	{
		long result = 0;
		
		ListContainerOptions options = new ListContainerOptions();
		options.inDirectory(directoryName);

		for(StorageMetadata blobSM: this.blobStore.list(containerName, options))
			result += getBlobSize(containerName, blobSM.getName());
		
		return result;
	}

	public void pushBlob(@NonNull ByteSource blobBytes, @NonNull String containerName, @NonNull String destPath)
	{
		Blob blob = null;
		try {
			blob = this.blobStore.blobBuilder(destPath)
			        .payload(blobBytes)
			        .contentLength(blobBytes.size())
			        .build();
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		if(blob != null)
			this.blobStore.putBlob(containerName, blob);
	}
	
	public void removeBlob(@NonNull String containerName, @NonNull String blobName)
	{
		if(blobExists(containerName, blobName))
			blobStore.removeBlob(containerName, blobName);
	}
	
	public void removeBlob(@NonNull String containerName, @NonNull String directoryName, @NonNull String blobName)
	{
		if(blobExists(containerName, directoryName, blobName))
		{
			String destPath = directoryName + "/" + blobName;
			blobStore.removeBlob(containerName, destPath);
		}
	}
	
	public InputStream getBlobStream(@NonNull String containerName, @NonNull String blobName)
	{
		InputStream result = null;
		
		// Retrieve the blob
	    Blob blob = this.blobStore.getBlob(containerName, blobName);
	    if(blob != null)
	    {
		    try {
				result = blob.getPayload().openStream();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
	    }
	    return result;
	}
	
	public InputStream getBlobStream(@NonNull String containerName, @NonNull String directoryName, @NonNull String blobName)
	{
		InputStream result = null;
		
		// Retrieve the blob
		String blobFullName = directoryName + "/" + blobName;
	    Blob blob = this.blobStore.getBlob(containerName, blobFullName);

	    if(blob != null)
	    {
		    try {
				result = blob.getPayload().openStream();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
	    }
	    return result;
	}
	
	public long getBlobSize(@NonNull String containerName, @NonNull String blobName)
	{
		return this.blobStore.getBlob(containerName, blobName).getPayload().getContentMetadata().getContentLength();
	}
	
	public long getBlobSize(@NonNull String containerName, @NonNull String directoryName, @NonNull String blobName)
	{
		String blobFullName = directoryName + "/" + blobName;
		return this.blobStore.getBlob(containerName, blobFullName).getPayload().getContentMetadata().getContentLength();
	}
	
	public List<StorageMetadata> listContainers()
    {
		List<StorageMetadata> result = new ArrayList<StorageMetadata>();
        
		// List Container Metadata
        for (StorageMetadata resourceMd : this.blobStore.list())
            result.add(resourceMd);
        
        return result;
    }

	public List<StorageMetadata> listBlobsInContainer(@NonNull String containerName)
    {
    	List<StorageMetadata> result = new ArrayList<StorageMetadata>();
    	
        // List all folders and files in the container
        for (StorageMetadata resourceMd : this.blobStore.list(containerName))
        	result.add(resourceMd);
        
        return result;
    }
    
	public List<StorageMetadata> listBlobsInDirectory(@NonNull String containerName, @NonNull String directoryName)
    {
    	List<StorageMetadata> result = new ArrayList<StorageMetadata>();
    	
    	ListContainerOptions options = new ListContainerOptions();
		options.inDirectory(directoryName);

        // List all folders and files in the directory
        for (StorageMetadata resourceMd : this.blobStore.list(containerName, options))
        	result.add(resourceMd);
        
        return result;
    }
    
	public List<String> listResourceNamesInContainer(@NonNull String containerName)
    {
    	List<String> result = new ArrayList<String>();
    	
        // List all folders and files in the container
        for (StorageMetadata blob : this.blobStore.list(containerName)){
   			String blobName = blob.getName(); 
   			String fileName = blobName.substring(blobName.indexOf("/") + 1, blobName.length());
   			result.add(fileName);
   		}
    	
    	return result;
    }
    
	public List<String> listBlobNamesInDirectory(@NonNull String containerName, @NonNull String directoryName)
    {
    	List<String> result = new ArrayList<String>();
    	ListContainerOptions options = new ListContainerOptions();
		options.inDirectory(directoryName);

        // List all folders and files in the directory
        for (StorageMetadata blob : this.blobStore.list(containerName, options)){
   			String blobName = blob.getName(); 
   			String fileName = blobName.substring(blobName.lastIndexOf("/") + 1, blobName.length());
   			result.add(fileName);
   		}
    	return result;
    }

	public BlobStore getBlobStore() {
		return blobStore;
	}
	
	
}