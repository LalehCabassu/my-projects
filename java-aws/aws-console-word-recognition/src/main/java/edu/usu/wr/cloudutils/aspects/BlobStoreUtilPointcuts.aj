package edu.usu.wr.cloudutils.aspects;

import java.io.InputStream;
import java.util.List;

import org.jclouds.blobstore.domain.StorageMetadata;

import edu.usu.wr.cloudutils.BlobStoreUtil;

public abstract aspect BlobStoreUtilPointcuts {
	
	pointcut blobStoreMethods():
		 (execution(* BlobStoreUtil.*(..)) && !execution(BlobStoreUtil.new(..)))
	        && within(BlobStoreUtil+);
	 
	pointcut blobStoreMethodsInit():
	        (execution(* BlobStoreUtil.*(..)) || execution(BlobStoreUtil.new(..)))
	        && within(BlobStoreUtil+);
	
	pointcut blobStoreUtilConstructor(BlobStoreUtil blobStoreUtil):
		this(blobStoreUtil) && execution(public BlobStoreUtil.new(..));
	
	pointcut blobStoreVoidMethods(): 
		execution(void BlobStoreUtil.*(..)) && within(BlobStoreUtil);
	
	pointcut blobStoreBooleanMethods():
		execution(boolean BlobStoreUtil.*(..)) && within(BlobStoreUtil);
	
	pointcut blobStoreLongMethods():
		execution(long BlobStoreUtil.*(..)) && within(BlobStoreUtil);
	
	pointcut blobStoreInputStreamMethods():
		execution(InputStream BlobStoreUtil.*(..)) && within(BlobStoreUtil);
	
	pointcut blobStoreListMetadataMethods():
		execution(List<StorageMetadata> BlobStoreUtil.*(..)) && within(BlobStoreUtil);
	
	pointcut blobStoreListStringMethods():
		execution(List<String> BlobStoreUtil.*(..)) && within(BlobStoreUtil);
	
}
