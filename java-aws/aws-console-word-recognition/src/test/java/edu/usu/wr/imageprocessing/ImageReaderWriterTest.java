package edu.usu.wr.imageprocessing;

import static org.junit.Assert.*;

import java.io.File;

import org.junit.Test;

import edu.usu.wr.handlers.BlobStoreHandler;
import edu.usu.wr.imageprocessing.ImageReaderWriter;

public class ImageReaderWriterTest {

	@Test
	public void test() {
		File [] readImageFiles = 
				ImageReaderWriter.readImageFiles(BlobStoreHandler.INPUT_LOCAL_PATH);
		assertNotEquals("Number of read images?", readImageFiles.length, 0);
		
		String destDirectory = BlobStoreHandler.OUTPUT_LOCAL_PATH;
		ImageReaderWriter.writeImageFiles(readImageFiles, destDirectory);
		
		File [] writtenImageFiles = 
				ImageReaderWriter.readImageFiles(destDirectory);
				
		assertNotEquals("Written image list is not empty?", writtenImageFiles, null);
		assertNotEquals("Number of read images?", writtenImageFiles.length, 0);	
	}
}
