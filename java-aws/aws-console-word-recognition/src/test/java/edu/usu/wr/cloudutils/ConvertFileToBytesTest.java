package edu.usu.wr.cloudutils;

import static org.junit.Assert.*;

import java.io.File;
import java.io.IOException;

import org.junit.Test;

import com.google.common.io.ByteSource;

import edu.usu.wr.cloudutils.Converter;
import edu.usu.wr.handlers.BlobStoreHandler;
import edu.usu.wr.imageprocessing.ImageReaderWriter;

public class ConvertFileToBytesTest {

	@Test
	public void test() {
		
		File [] imageList = 
				ImageReaderWriter.readImageFiles(BlobStoreHandler.INPUT_LOCAL_PATH);
		if(imageList.length > 0)
		{
			ByteSource bytes = Converter.imageFileToBytes(imageList[0]);
			assertNotEquals("Is ByteSoure null?", bytes, null);
			try {
				assertNotEquals("Is the size of ByeSource ZERO?", bytes.size(), (long)0);
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
	}

}
