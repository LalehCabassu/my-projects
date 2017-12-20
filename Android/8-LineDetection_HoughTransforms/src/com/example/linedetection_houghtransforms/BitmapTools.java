package com.example.linedetection_houghtransforms;

import android.graphics.Bitmap;
import android.graphics.Color;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;

public class BitmapTools {

    public static Bitmap convertArrayToBitmap(short[][] imageArray, int height, int width)
    {
        Bitmap bitmapImage = Bitmap.createBitmap(width, height, Bitmap.Config.ARGB_8888);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                int val = imageArray[i][j];
                bitmapImage.setPixel(j, i, Color.argb(255, val, val, val));
            }
        }
        return bitmapImage;
    }

    public static void writeImageArray(short[][] imageArray, int height, int width, 
    								String directory, String filename)
    {
        Bitmap bitmap = convertArrayToBitmap(imageArray, height, width);
        writeBitmap(bitmap, directory, filename);
        bitmap.recycle();
        bitmap = null;
    }

    public static void writeBitmap(Bitmap bitmap, String address, String filename)
    {
        File destinationFolder = new File(address);
        destinationFolder.mkdirs();

        try {
            FileOutputStream out = new FileOutputStream(address + filename + ".png");
            bitmap.compress(Bitmap.CompressFormat.PNG, 90, out);
            out.close();
        }
        catch (IOException ex){
            ex.printStackTrace();
        }
    }

    public static void getPixels(Bitmap bitmap, short[][] pixels) {
        for (int i = 0, height = bitmap.getHeight(); i < height; i++){
            for (int j = 0, width = bitmap.getWidth(); j < width; j++){
                pixels[i][j] = (short) Color.green(bitmap.getPixel(j, i));
            }
        }
    }
}

