package edu.usu.cloud.wr;

import java.awt.image.BufferedImage;
import java.util.ArrayList;
import java.util.List;

import edu.usu.cloud.wr.model.Image;

public class ImageHandler {
	private static final String INITIAL_IMAGES_PATH = "/initial-images/";
	private static final String INITIAL_LIBRARY_PATH = "/library.txt";
	
	private List<Image> images;
	private BlobStoreHandler blobStoreHandler;
	
	public ImageHandler()
	{
		blobStoreHandler = new BlobStoreHandler();
		images = new ArrayList<Image>();
	}
	
	public void initialImages(String resourcePath)
	{
		String initialImgPath = resourcePath + INITIAL_IMAGES_PATH;
		String libraryPath = resourcePath + INITIAL_LIBRARY_PATH;
		blobStoreHandler.init(initialImgPath, libraryPath);		
		images = blobStoreHandler.listImages();
	}
	
	public List<Image> getImages() {
		return images;
	}
	
	public Image getImage(int index)
	{
		return images.get(index);
	}
	
	public BufferedImage downloadImage(int index)
	{
		return blobStoreHandler.downloadImage(images.get(index));
	}
	
	public List<String> downloadTemplateLibrary()
	{
		return blobStoreHandler.downloadTemplateLibrary();
	}
	
	//coucou bilou bilou

}
