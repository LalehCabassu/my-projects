package edu.usu.wr.cloudutils.aspects;

import java.io.InputStream;
import java.util.List;

import org.jclouds.blobstore.domain.StorageMetadata;

import edu.usu.wr.cloudutils.BlobStoreUtil;

public aspect AthenticationAspect extends BlobStoreUtilPointcuts {
	
	private boolean connectedToBlobStore = false;
	
	after(BlobStoreUtil blobStoreUtil) returning: blobStoreUtilConstructor(blobStoreUtil) 
	{
		if(blobStoreUtil.getBlobStore() != null)
			connectedToBlobStore = true;
	}
	
//	Object around(): blobStoreMethods()
//	{
//		Object result = null;
//		if(connectedToBlobStore)
//			result = proceed();
//		return result;
//	}
	
	void around(): blobStoreVoidMethods()
	{
		if(connectedToBlobStore)
			proceed();
		return;
	}
	
	boolean around(): blobStoreBooleanMethods()
	{
		boolean result = false;
		if(connectedToBlobStore)
			result = proceed();
		return result;
	}
	
	long around(): blobStoreLongMethods()
	{
		long result = 0;
		if(connectedToBlobStore)
			result = proceed();
		return result;
	}
	
	InputStream around(): blobStoreInputStreamMethods()
	{
		InputStream result = null;
		if(connectedToBlobStore)
			result = proceed();
		return result;
	}
	
	List<StorageMetadata> around(): blobStoreListMetadataMethods()
	{
		List<StorageMetadata> result = null;
		if(connectedToBlobStore)
			result = proceed();
		return result;
	}
	
	List<String> around(): blobStoreListStringMethods()
	{
		List<String> result = null;
		if(connectedToBlobStore)
			result = proceed();
		return result;
	}
}
