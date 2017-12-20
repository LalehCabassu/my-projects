package edu.usu.cloud.wr.imageprocessing;

import java.awt.Color;
import java.awt.image.BufferedImage;

public class BitmapTools {

    public static BufferedImage convertArrayToBufferedImage(short[][] imageArray, int height, int width)
    {
    	BufferedImage bufferedImage = new BufferedImage(width, height, BufferedImage.TYPE_4BYTE_ABGR);
//    			Bitmap.createBitmap(width, height, Bitmap.Config.ARGB_8888);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                int val = imageArray[i][j];
                Color color = new Color(val, val, val, 255);
                bufferedImage.setRGB(j, i, color.getRGB());
//                bitmapImage.setPixel(j, i, Color.argb(255, val, val, val));
            }
        }
        return bufferedImage;
    }

    public static void writeImageArray(short[][] imageArray, int height, int width, 
    								String directory, String filename)
    {
//        Bitmap bitmap = convertArrayToBitmap(imageArray, height, width);
    	BufferedImage bufferedImage = convertArrayToBufferedImage(imageArray, height, width);
    	ImageReaderWriter.writeBufferedImage(bufferedImage, directory, filename);
//        writeBufferedImage(bufferedImage, directory, filename);
        bufferedImage.flush();
        bufferedImage = null;
    }

//    public static void writeBufferedImage(BufferedImage bufferedImage, String address, String filename)
//    {
//        File destinationFolder = new File(address);
//        destinationFolder.mkdirs();
//
//        try {
//            FileOutputStream out = new FileOutputStream(address + filename + ".png");
//            bufferedImage.compress(Bitmap.CompressFormat.PNG, 90, out);
//            out.close();
//        }
//        catch (IOException ex){
//            ex.printStackTrace();
//        }
//    }

    public static void getPixels(BufferedImage bufferedImage, short[][] pixels) {
        for (int i = 0, height = bufferedImage.getHeight(); i < height; i++){
            for (int j = 0, width = bufferedImage.getWidth(); j < width; j++){
            	Color color = new Color(bufferedImage.getRGB(j, i));
            	pixels[i][j] = (short) color.getGreen();
//                pixels[i][j] = (short) Color.green(bufferedImage.getPixel(j, i));
            }
        }
    }
}

