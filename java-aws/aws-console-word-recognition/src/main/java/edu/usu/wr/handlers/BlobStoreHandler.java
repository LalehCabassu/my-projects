package edu.usu.wr.handlers;

import java.util.ArrayList;
import java.util.List;
import java.awt.image.BufferedImage;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;

import com.google.common.io.ByteSource;

import edu.usu.wr.cloudutils.BlobStoreUtil;
import edu.usu.wr.cloudutils.Converter;
import edu.usu.wr.cloudutils.ProviderAPI;
import edu.usu.wr.imageprocessing.ImageReaderWriter;

public class BlobStoreHandler {
	
	public final static ProviderAPI BLOBSTORE_PROVIDER = ProviderAPI.AMAZON_S3;
	public final static String CONTAINER_NAME = "word-recognition-app";
	public final static String INPUT_DIRECTORY = "input_images";
	public final static String BLOB_DIRECTORY = "word_blobs";
	
	public final static String INPUT_LOCAL_PATH = "/Users/Life/Applications/wordrecognition/input-images/";
	public final static String LIBRARY_LOCAL_PATH = "/Users/Life/Applications/wordrecognition/";
	public final static String LIBRARY_FILE_NAME = "library.txt";
	public final static String BLOBS_LOCAL_PATH = "/Users/Life/Applications/wordrecognition/word-blobs/";
	public final static String WORDS_LOCAL_PATH = "/Users/Life/Applications/wordrecognition/recognized_words/";
	public final static String OUTPUT_LOCAL_PATH =  "/Users/Life/Applications/wordrecognition/output-images/";
	
	private BlobStoreUtil blobStoreUtil;
	
	public BlobStoreHandler()
	{
		this.blobStoreUtil = new BlobStoreUtil(BLOBSTORE_PROVIDER);
	}
	
	public long uploadImages(String path)
	{
		int numberOfUpladedImages = 0;
		blobStoreUtil.createContainer(CONTAINER_NAME);
		
		File [] imageList;
		if(path != null && !path.isEmpty())
			imageList = ImageReaderWriter.readImageFiles(path);
		else
			imageList = ImageReaderWriter.readImageFiles(INPUT_LOCAL_PATH);
        
		for(File imageFile: imageList) {
        	ByteSource blobBytes = Converter.imageFileToBytes(imageFile);
        	String destPath = INPUT_DIRECTORY + "/" + imageFile.getName();
        	blobStoreUtil.pushBlob(blobBytes, CONTAINER_NAME, destPath);
        	numberOfUpladedImages++;
        }
		return numberOfUpladedImages;
	}
	
	public long downloadImages()
	{
		int numberOfDownloadedImages = 0;
		for(String imageName: blobStoreUtil.listBlobNamesInDirectory(CONTAINER_NAME, INPUT_DIRECTORY))
		{
			InputStream is = blobStoreUtil.getBlobStream(CONTAINER_NAME, INPUT_DIRECTORY, imageName);
			BufferedImage bi = Converter.readImageFromStream(is);
			
			ImageReaderWriter.writeBufferedImage(bi, OUTPUT_LOCAL_PATH, imageName);
			numberOfDownloadedImages ++;
			try {
				is.close();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		return numberOfDownloadedImages;
	}
	
	public long uploadWordBlobs(List<BufferedImage> blobs, String subDirectory, String extension)
	{
		int numberOfUpladedImages = 0;
		blobStoreUtil.createContainer(CONTAINER_NAME);
		
		for(BufferedImage blob: blobs) {
			String blobName = "blob_" + numberOfUpladedImages  + "." + extension;
	        ByteSource blobBytes = Converter.bufferedImageToBytes(blob, extension);
        	String destPath = BLOB_DIRECTORY + "/" +  subDirectory + "/" + blobName;
        	blobStoreUtil.pushBlob(blobBytes, CONTAINER_NAME, destPath);
        	numberOfUpladedImages++;
        }
		return numberOfUpladedImages;
	}
	
	public List<BufferedImage> downloadWordBlobs(String subDirectory)
	{
		List<BufferedImage> list = new ArrayList<BufferedImage>();
		String destPath = BLOB_DIRECTORY + "/" + subDirectory;
		for(String imageName: blobStoreUtil.listBlobNamesInDirectory(CONTAINER_NAME, destPath))
		{
			InputStream is = blobStoreUtil.getBlobStream(CONTAINER_NAME, destPath, imageName);
			BufferedImage bi = Converter.readImageFromStream(is);
			if(bi != null)
				list.add(bi);
		}
		return list;
	}
	
	public boolean uploadTemplateLibrary()
	{
		boolean result = false;
		
		File libFile= new File(LIBRARY_LOCAL_PATH + LIBRARY_FILE_NAME);
		if(libFile.exists())
		{
			result = true;
			blobStoreUtil.createContainer(CONTAINER_NAME);
			
			ByteSource blobBytes = Converter.textFileToBytes(libFile);
			blobStoreUtil.pushBlob(blobBytes, CONTAINER_NAME, LIBRARY_FILE_NAME);
		}
		else
			result = blobStoreUtil.blobExists(CONTAINER_NAME, LIBRARY_FILE_NAME);
		
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
	
	public BufferedImage downloadImage(String imageName)
	{
		BufferedImage image = null;
		
		InputStream is = blobStoreUtil.getBlobStream(CONTAINER_NAME, INPUT_DIRECTORY, imageName);
		image = Converter.readImageFromStream(is);
		
		return image;
	}
	
	public void uploadRecognisedWords(String subDirectory, String recWords)
	{
		blobStoreUtil.createContainer(CONTAINER_NAME);
		
		ByteSource blobBytes = Converter.stringToBytes(recWords);
		String destPath = BLOB_DIRECTORY + "/" +  subDirectory + "/recognised_words.txt";
    	blobStoreUtil.pushBlob(blobBytes, CONTAINER_NAME, destPath);
	}
	
	public void removeImage(String imageName)
	{
		if(blobStoreUtil.blobExists(CONTAINER_NAME, INPUT_DIRECTORY, imageName))
			blobStoreUtil.removeBlob(CONTAINER_NAME, INPUT_DIRECTORY, imageName);
	}
	
	public void removeWordBlobsOfImage(String imageName)
	{
		List<String> blobs = listWordBlobs(imageName);
		for(String blobName: blobs)
		{
			String destPath = imageName + "/" + blobName ;
	    	blobStoreUtil.removeBlob(CONTAINER_NAME, BLOB_DIRECTORY, destPath);
		}
	}
	
	public void removeAllImages()
	{
		blobStoreUtil.clearDirectory(CONTAINER_NAME, INPUT_DIRECTORY);
	}
	
	public void removeAllWordBlobs()
	{
		blobStoreUtil.clearDirectory(CONTAINER_NAME, BLOB_DIRECTORY);
	}
	
	public List<String> listWordBlobs(String subDirectory)
	{
		String destPath = BLOB_DIRECTORY + "/" +  subDirectory;
    	return blobStoreUtil.listBlobNamesInDirectory(CONTAINER_NAME, destPath);
	}
	
	public List<String> listImages()
	{
		return blobStoreUtil.listBlobNamesInDirectory(CONTAINER_NAME, INPUT_DIRECTORY);
	}
	
	public boolean imageExists(String name)
	{
		return blobStoreUtil.blobExists(CONTAINER_NAME, INPUT_DIRECTORY, name);
	}
	
	public BlobStoreUtil getBlobStoreUtil() {
		return blobStoreUtil;
	}

	public void setBlobStoreUtil(BlobStoreUtil blobStoreUtil) {
		this.blobStoreUtil = blobStoreUtil;
	}
	
}