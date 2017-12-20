package edu.usu.cloud.wr.imageprocessing;

import edu.usu.cloud.wr.common.Converter;

import java.awt.image.BufferedImage;
import java.io.File;
import java.io.FileFilter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;

import javax.imageio.ImageIO;

public class ImageReaderWriter {

	// Put all images(.bmp, .jpg, .png) in an array of Files
	public static List<File> readImageFiles(String sourceDirectory) {
		List<File> result = new ArrayList<File>();
		
		File sourceFolder = new File(sourceDirectory);
		if(sourceFolder.exists())
		{
			File[] imageList = sourceFolder.listFiles(
				new FileFilter() {
					public boolean accept(File file) {
						String fileName = file.getName().toLowerCase(Locale.getDefault());
						if (fileName.endsWith(".bmp") || fileName.endsWith(".jpg") || fileName.endsWith(".png"))
							return true;
						return false;
					}
				}
			);
			if(imageList != null)
				for(File img: imageList)
					result.add(img);
		}
		return result;
	}
	
	public static void writeImageFiles(List<File> imageFiles, String destDirectory)
	{
		for(File imageFile: imageFiles)
		{
			BufferedImage bufferedImage = Converter.imageFileToBufferedImage(imageFile);
			writeBufferedImage(bufferedImage, destDirectory, imageFile.getName());
		}
	}
	
	public static File writeBufferedImage(BufferedImage bufferedImage, String destDirectory, String fileName)
	{
		String destFilePath = destDirectory + fileName;
		String extension = fileName.substring(fileName.indexOf(".") + 1, fileName.length());
        
		File destDirFile = new File(destDirectory);
		destDirFile.mkdir();
		File outImageFile = new File(destFilePath);
		try {
				ImageIO.write(bufferedImage, extension, outImageFile);
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
		return outImageFile;
	}
	
	public static void clearImageDirectory(String directory)
	{
		List<File> files = readImageFiles(directory);
		for(File file: files)
			file.delete();
	}
}
