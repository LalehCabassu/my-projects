package edu.usu.wr.cloudutils;

import static org.junit.Assert.*;

import java.awt.image.BufferedImage;
import java.io.File;
import java.util.List;

import org.junit.Test;

import edu.usu.wr.cloudutils.BlobStoreUtil;
import edu.usu.wr.handlers.BlobStoreHandler;
import edu.usu.wr.imageprocessing.ImageReaderWriter;

public class BlobStoreHandlerTest {

	private BlobStoreHandler handler;
	private BlobStoreUtil blobStoreUtil;
	private String containerName = BlobStoreHandler.CONTAINER_NAME;
	private String directoryName = BlobStoreHandler.INPUT_DIRECTORY;
	
	public BlobStoreHandlerTest()
	{
		handler = new BlobStoreHandler();
		blobStoreUtil = handler.getBlobStoreUtil();
	}
	
	// Passed
	public void testUploadingImages() {
		File [] files = ImageReaderWriter.readImageFiles(BlobStoreHandler.INPUT_LOCAL_PATH);
		long numberOfBlobs = handler.uploadImages(null);
		
		assertTrue("Does container exists?", blobStoreUtil.containerExists(containerName));
		assertEquals("Number of blobs in directory?", files.length, numberOfBlobs);
		assertNotEquals("Size of directory?", (long)0, blobStoreUtil.getDirectorySize(containerName, directoryName));
	}
	
	// Passed
	public void testDownloadingImages() {
		
		long numberOfImages = handler.downloadImages();
		File [] files = ImageReaderWriter.readImageFiles(BlobStoreHandler.OUTPUT_LOCAL_PATH);
		
		assertEquals("Number of images in the directory?", numberOfImages, files.length);
	}
	
	public void testDownloadWordBlobs_EmptyList()
	{
		List<BufferedImage> images = handler.downloadWordBlobs("nt_29_line_07");
	
		assertEquals("Is the images empty?", 0, images.size());
		for(BufferedImage img: images)
			System.out.println("Blob> " + img);
	}
	
	@Test
	public void testDownloadWordBlobs_NonEmpty()
	{
		List<BufferedImage> images = handler.downloadWordBlobs("nt_34_line_03");
	
		assertNotEquals("Is the images empty?", 0, images.size());
		for(BufferedImage img: images)
			System.out.println("Blob> " + img);
	}
}
