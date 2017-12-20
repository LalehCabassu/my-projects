package edu.usu.cloud.wr;

import java.util.ArrayList;
import java.util.List;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.net.URI;

import com.google.common.io.ByteSource;

import edu.usu.cloud.wr.cloudutils.BlobAccessOptions;
import edu.usu.cloud.wr.cloudutils.BlobStoreUtil;
import edu.usu.cloud.wr.cloudutils.ContainerAccessOptions;
import edu.usu.cloud.wr.cloudutils.ProviderAPI;
import edu.usu.cloud.wr.common.Converter;
import edu.usu.cloud.wr.imageprocessing.ImageReaderWriter;
import edu.usu.cloud.wr.model.Image;

public class BlobStoreHandler {
	
	private final static ProviderAPI BLOBSTORE_PROVIDER = ProviderAPI.AMAZON_S3;
	private final static String CONTAINER_NAME = "word-recognition-app";
	private final static String INPUT_DIRECTORY = "input_images";
	private final static String LIBRARY_FILE_NAME = "library.txt";
	private static ContainerAccessOptions CONTAINER_ACCESS_OPTIONS = ContainerAccessOptions.FULL_ACCESS;
	private static BlobAccessOptions BLOB_ACCESS_OPTIONS = BlobAccessOptions.FULL_ACCESS;
	private BlobStoreUtil blobStoreUtil;
	
	public BlobStoreHandler()
	{
		this.blobStoreUtil = new BlobStoreUtil(BLOBSTORE_PROVIDER);
	}
	
	public boolean init(String initialImagesPath, String libraryPath)
	{
		boolean result = false;
		
		// a new container is created
		if(blobStoreUtil.createContainer(CONTAINER_NAME, CONTAINER_ACCESS_OPTIONS))
		{
			System.out.println("yes");
			result = (addImages(initialImagesPath) != 0) ? true : false;
			result &= uploadTemplateLibrary(libraryPath);
		}
		return result;
	}
	
	public long addImages(String path)
	{
		int numberOfUpladedImages = 0;
		
		List<File> imageList = ImageReaderWriter.readImageFiles(path);
	    
		for(File imageFile: imageList) {
        	ByteSource blobBytes = Converter.imageFileToBytes(imageFile);
        	String destPath = INPUT_DIRECTORY + "/" + imageFile.getName();
        	blobStoreUtil.pushBlob(blobBytes, CONTAINER_NAME, destPath, BLOB_ACCESS_OPTIONS);
        	numberOfUpladedImages++;
		}
		
		return numberOfUpladedImages;
	}
	
	public boolean uploadTemplateLibrary(String path)
	{
		boolean result = false;
		
		File libFile= new File(path);
		if(libFile.exists())
		{
			ByteSource blobBytes = Converter.textFileToBytes(libFile);
			blobStoreUtil.pushBlob(blobBytes, CONTAINER_NAME, LIBRARY_FILE_NAME);
		}
		
		return result;
	}
	
	public List<String> downloadTemplateLibrary()
	{
		List<String> list = new ArrayList<String>();
		InputStream is = blobStoreUtil.getBlobStream(CONTAINER_NAME, LIBRARY_FILE_NAME);
		String text = Converter.readTextFromStream(is);
		if(text != null)
		{
			String[] lines = text.split("\n");
			for(String line: lines)
				list.add(line);
		}
		return list;
	}
	
	public BufferedImage downloadImage(Image image)
	{
		BufferedImage bufferedImage = null;
		
		InputStream is = blobStoreUtil.getBlobStream(CONTAINER_NAME, INPUT_DIRECTORY, image.getName());
		bufferedImage = Converter.readImageFromStream(is);
		
		return bufferedImage;
	}
	
	public void removeImage(Image image)
	{
		if(blobStoreUtil.blobExists(CONTAINER_NAME, INPUT_DIRECTORY, image.getName()))
			blobStoreUtil.removeBlob(CONTAINER_NAME, INPUT_DIRECTORY, image.getName());
	}
		
	public void removeAllImages()
	{
		blobStoreUtil.clearDirectory(CONTAINER_NAME, INPUT_DIRECTORY);
	}
	
	public List<Image> listImages()
	{
		List<Image> result = new ArrayList<Image>();
		List<String> imageNames = blobStoreUtil.listBlobNamesInDirectory(CONTAINER_NAME, INPUT_DIRECTORY); 
		
		for(String name: imageNames)
		{
			URI uri = blobStoreUtil.getBlobUri(CONTAINER_NAME, INPUT_DIRECTORY, name);
			Image img = new Image(name, uri);
			result.add(img);
		}
		
		return result; 
	}
	
	public boolean imageExists(Image image)
	{
		return blobStoreUtil.blobExists(CONTAINER_NAME, INPUT_DIRECTORY, image.getName());
	}
	
	public BlobStoreUtil getBlobStoreUtil() {
		return blobStoreUtil;
	}

	public void setBlobStoreUtil(BlobStoreUtil blobStoreUtil) {
		this.blobStoreUtil = blobStoreUtil;
	}	
}