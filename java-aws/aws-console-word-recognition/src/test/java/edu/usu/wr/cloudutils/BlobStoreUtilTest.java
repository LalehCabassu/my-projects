package edu.usu.wr.cloudutils;

import static org.junit.Assert.*;

import java.util.List;

import org.jclouds.blobstore.domain.StorageMetadata;
import org.junit.Test;

import edu.usu.wr.handlers.BlobStoreHandler;

public class BlobStoreUtilTest {

	private BlobStoreUtil blobStoreUtil;
	private String containerName = BlobStoreHandler.CONTAINER_NAME;
	private String directoryName = BlobStoreHandler.INPUT_DIRECTORY;
	
	public BlobStoreUtilTest()
	{
		blobStoreUtil = new BlobStoreUtil();
	}
	
	public void testListBlobsInContainer()
	{
		List<StorageMetadata> blobs = blobStoreUtil.listBlobsInContainer(containerName);
		for(StorageMetadata rs: blobs)
			System.out.printf("Blob> %s\n", rs);
		assertNotEquals("Number of blobs?", 0, blobs.size());
	}
	
	public void testListBlobsInDirectory()
	{
		List<StorageMetadata> blobs = blobStoreUtil.listBlobsInDirectory(containerName, directoryName);
		for(StorageMetadata rs: blobs)
			System.out.printf("Blob> %s\n", rs.getName());
		assertNotEquals("Number of blobs?", 0, blobs.size());
	}
	
	@Test
	public void testListBlobNamesInDirectory()
	{
		directoryName = BlobStoreHandler.BLOB_DIRECTORY + "/" + "nt_29_line_07";
		List<String> blobs = blobStoreUtil.listBlobNamesInDirectory(containerName, directoryName);
		for(String rs: blobs)
			System.out.printf("Blob> %s\n", rs);
		assertNotEquals("Number of blobs?", 0, blobs.size());

	}
	
	public void testDirectoryExists()
	{
		assertTrue("If '" + directoryName + "' exists?" , blobStoreUtil.directoryExists(containerName, directoryName));
	}
	
	public void testClearDirectory()
	{
		blobStoreUtil.clearDirectory(containerName, directoryName);
		assertTrue("Does directory exist?", blobStoreUtil.directoryExists(containerName, directoryName));
		assertEquals("Is blobstore directory empty?", 0, blobStoreUtil.countBlobsInDirectory(containerName, directoryName));
	}

}
