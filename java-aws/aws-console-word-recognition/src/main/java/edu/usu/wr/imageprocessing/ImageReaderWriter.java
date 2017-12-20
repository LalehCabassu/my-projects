package edu.usu.wr.imageprocessing;

import edu.usu.wr.cloudutils.Converter;

import java.awt.image.BufferedImage;
import java.io.File;
import java.io.FileFilter;
import java.io.IOException;
import java.util.Locale;

import javax.imageio.ImageIO;

public class ImageReaderWriter {

	// Put all images(.bmp, .jpg, .png) in an array of Files
	public static File[] readImageFiles(String sourceDirectory) {
		File sourceFolder = new File(sourceDirectory);
		File[] imageFiles = sourceFolder.listFiles(
			new FileFilter() {
				public boolean accept(File file) {
					String fileName = file.getName().toLowerCase(Locale.getDefault());
					if (fileName.endsWith(".bmp") || fileName.endsWith(".jpg") || fileName.endsWith(".png"))
						return true;
					return false;
				}
			}
		);
		return imageFiles;
	}
	
	public static void writeImageFiles(File[] imageFiles, String destDirectory)
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
		File[] files = readImageFiles(directory);
		for(File file: files)
			file.delete();
	}
}
