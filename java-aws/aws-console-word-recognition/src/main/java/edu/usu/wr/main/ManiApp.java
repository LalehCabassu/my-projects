package edu.usu.wr.main;

import java.awt.image.BufferedImage;
import java.io.File;
import java.util.List;
import java.util.Scanner;

import javax.swing.ImageIcon;
import javax.swing.JFrame;
import javax.swing.JLabel;

import edu.usu.wr.imageprocessing.TemplateLibrary;
import edu.usu.wr.imageprocessing.WordRecognition;
import edu.usu.wr.handlers.BlobStoreHandler;

public class ManiApp {

    private static BlobStoreHandler handler;
	private static Scanner scanner;
	
	public static void main(String[] args) throws Exception {
	
		init();
		
		int command;
		
		System.out.println("\n--------------Welcome to Word Recognition Application--------------");
		
		if(!handler.uploadTemplateLibrary())
		{
			System.out.printf("Template library is missing. Please put '%s' at '%s' and restart the application.\n", 
								BlobStoreHandler.LIBRARY_FILE_NAME, BlobStoreHandler.LIBRARY_LOCAL_PATH);
			scanner.close();
			return;
		}
		
		do
		{
			System.out.println("\nPlease select an actions:");
			System.out.println("[1] List all images");
			System.out.println("[2] Recognise words in an image");
			System.out.println("[3] Upload image(s)");
			System.out.println("[4] Display an image");
			System.out.println("[5] Remove an image");
			System.out.println("[6] Remove all images");
			System.out.println("[7] Exit");
			System.out.println("Please enter the number of your choice.");
			System.out.print("Command> ");
			
			command = scanner.nextInt();
			
			switch(command)
			{
				case 2:
					recogniseWords();
					break;
				case 3:
					uploadImages();
					break;
				case 4:
					displayImage();
					break;
				case 5:
					removeImage();
					break;
				case 6:
					removeAllImages();
					break;
				case 7:
					break;	
				default:
					listAllImages();
					break;
			}
		}while(command != 7);
		
		scanner.close();
    }
	  
	public static void init()
	{
		handler = new BlobStoreHandler();
		scanner = new Scanner(System.in);
	}
	
	public static void listAllImages()
	{
		List<String> imageNames = handler.listImages();
		
		if(imageNames.size() == 0)
			System.out.println("There is no image available.");
		else
		{
			System.out.printf("All %d images:\n", imageNames.size());
			
			int i = 1;
			for(String imageName: imageNames)
				System.out.printf("image %d> %s\n", i++, imageName);
		}
	}
	
	public static void uploadImages()
	{
		System.out.println("The default folder for uploading images located at");
		System.out.println(BlobStoreHandler.INPUT_LOCAL_PATH);
		System.out.print("Do you want choose another folder (Y/N)? ");
		String ans = scanner.next();
		String path = BlobStoreHandler.INPUT_LOCAL_PATH;
		
		if(ans.equalsIgnoreCase("Y"))
		{
			File inputDir;
			do
			{
				System.out.print("Full path of folder? ");
				path = scanner.next();
				inputDir = new File(path);
				if(!inputDir.exists())
				{	
					System.out.println("The directory does not exist!");
					System.out.print("Do you want choose another folder (Y/N)? ");
					ans = scanner.next();
				}
			} while(!inputDir.exists() && ans.equalsIgnoreCase("Y"));
		}
		
		System.out.println("Uploading images from " + path);
		long num = handler.uploadImages(path);
		System.out.printf("%d images have been uploaded successfully.\n", num);
	}
    
	public static void recogniseWords()
	{
		List<String> library = handler.downloadTemplateLibrary();
		TemplateLibrary.loadLibrary(library);
		
		WordRecognition wr = new WordRecognition();
		
		String command;
		do
		{
			System.out.print("Image name? ");
			command = scanner.next().trim();
			
			if(handler.imageExists(command))
			{
				System.out.println("Downloading image: " + command);
				BufferedImage image = handler.downloadImage(command);
				String imageName = command.substring(0, command.indexOf("."));
				String recWords = wr.recognizeWord(image, imageName);
				handler.uploadRecognisedWords(imageName, recWords);
				displayImage(image);
				System.out.println("Recognised words: " + recWords);
			}
			else
				System.out.println("The image does not exist!");
			
			System.out.print("Try another image (Y/N)? ");
			command = scanner.next();
		
		} while(command.equalsIgnoreCase("Y"));		
	}
	
	private static void displayImage(BufferedImage image)
	{
		JFrame frame = new JFrame();
        frame.setSize(300, 300);
        JLabel label = new JLabel(new ImageIcon(image));
        frame.add(label);
        frame.setVisible(true);
	}
	
	public static void displayImage()
	{
		String command;
		do
		{
			System.out.print("Image name? ");
			command = scanner.next().trim();
			
			if(handler.imageExists(command))
			{
				System.out.println("Downloading image: " + command);
				BufferedImage image = handler.downloadImage(command);
				displayImage(image);
			}
			else
				System.out.println("The image does not exist!");
		
			System.out.println("Try another image (Y/N)? ");
			command = scanner.next();
		
		} while(command.equalsIgnoreCase("Y"));		
	}
	
	public static void removeImage()
	{
		String command;
		do
		{
			System.out.print("Image name? ");
			command = scanner.next().trim();
			
			if(handler.imageExists(command))
			{
				System.out.println("Deleting image: " + command);
				handler.removeImage(command);
				handler.removeImage(command);
			}
			else
				System.out.println("The image does not exist!");
		
			System.out.println("Try another image (Y/N)? ");
			command = scanner.next();
		
		} while(command.equalsIgnoreCase("Y"));		
	}
	
	public static void removeAllImages()
	{
		System.out.print("Are you sure you want to delete all images (Y/N)? ");
		String command = scanner.next();
		if(command.equalsIgnoreCase(command))
		{	
			handler.removeAllImages();
			handler.removeAllWordBlobs();
		}
	}
}
