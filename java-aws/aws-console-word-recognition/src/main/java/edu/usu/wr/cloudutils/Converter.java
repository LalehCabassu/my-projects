package edu.usu.wr.cloudutils;

import java.awt.image.BufferedImage;
import java.io.BufferedReader;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.ObjectInputStream;

import javax.imageio.ImageIO;

import com.google.common.base.Charsets;
import com.google.common.io.ByteSource;
import com.google.common.io.CharStreams;

public class Converter {
		
	public static ByteSource imageFileToBytes(File imageFile)
	{
		ByteSource result = null;
		
		BufferedImage image = imageFileToBufferedImage(imageFile);
		String fileName = imageFile.getName();
        String extension = fileName.substring(fileName.indexOf(".") + 1, fileName.length());
		
		// Convert image to ByteSource
		try {
			ByteArrayOutputStream stream = new ByteArrayOutputStream();
			ImageIO.write(image, extension, stream);
			stream.flush();
			byte[] byteArray = stream.toByteArray();
			stream.close();
			result = ByteSource.wrap(byteArray);
		} catch (IOException e) {
			e.printStackTrace();
		}
        
        return result;
	}
	
	public static ByteSource textFileToBytes(File textFile)
	{
		ByteSource result = null;
		
		//Read text from file
		StringBuilder records = new StringBuilder();

		try {
		    BufferedReader br = new BufferedReader(new FileReader(textFile));
		    String line;

		    while ((line = br.readLine()) != null)
		        records.append(line + '\n');
		    br.close();
		}
		catch (IOException e) {
			 e.printStackTrace();
		}
		
		result = stringToBytes(records.toString());
		
        return result;
	}
	
	// Convert String to bytes
	public static ByteSource stringToBytes(String text)
	{
		return ByteSource.wrap(text.getBytes(Charsets.UTF_8));
	}
	
	// Convert File to BufferedImage
	public static BufferedImage imageFileToBufferedImage(File imageFile) {
		BufferedImage bufferedImage = null;
		
		try {
			bufferedImage = ImageIO.read(imageFile);			
		} catch (IOException e) {
			e.printStackTrace();
		}
		
		return bufferedImage;
	}
		
	// Convert BufferedImage to ByteSource
	public static ByteSource bufferedImageToBytes(BufferedImage bufferedImage, String extension)
	{
		ByteSource result = null;
		
		try {
			ByteArrayOutputStream stream = new ByteArrayOutputStream();
			ImageIO.write(bufferedImage, extension, stream);
			stream.flush();
			byte[] byteArray = stream.toByteArray();
			stream.close();
			result = ByteSource.wrap(byteArray);
		} catch (IOException e) {
			e.printStackTrace();
		}
        
        return result;
	}
	
	public static String readTextFromStream(InputStream is)
	{
		String result = null;
		
		try {
			result = CharStreams.toString(new InputStreamReader(is, Charsets.UTF_8));
//	        System.out.println("The retrieved payload is: " + result);
	    } catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} finally {
			if (is != null) {
                try {
                    is.close();
                } catch (IOException e) {
                }
            }
		}
		
		return result;
	}
	
	public static BufferedImage readImageFromStream(InputStream is)
	{
		BufferedImage result = null;
		
		try {
			result = ImageIO.read(is);
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} finally {
			if (is != null) {
                try {
                    is.close();
                } catch (IOException e) {
                }
            }
		}
		
		return result;
	}
	
	public static Object readObjectFromStream(InputStream is)
	{
		Object result = null;
		
		ObjectInputStream ois = null;
        try {
            ois = new ObjectInputStream(is);
            result = ois.readObject();
        } catch (IOException e) {
            e.printStackTrace();
        } catch (ClassNotFoundException e) {
            e.printStackTrace();
        } finally {
            if (ois != null) {
                try {
                    ois.close();
                } catch (IOException e) {
                }
            }

            if (is != null) {
                try {
                    is.close();
                } catch (IOException e) {
                }
            }
        }
        
		return result;
	}
}
