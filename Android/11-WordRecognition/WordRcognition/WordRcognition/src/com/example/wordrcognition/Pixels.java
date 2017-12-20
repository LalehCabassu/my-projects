package com.example.wordrcognition;

import android.graphics.Bitmap;

public class Pixels {
	
	final String LOGCAT = Pixels.class.getSimpleName() + "LOGCAT";
	private String mName;
    private int mHeight;
    private int mWidth;
    private short[][] mPixels;
    private short[][] mBinaryPixels;

    public Pixels(String name, Bitmap bitmap)
    {
        this.mName = name;

        this.mHeight = bitmap.getHeight();
        this.mWidth = bitmap.getWidth();

        this.mPixels = new short[mHeight][mWidth];
        this.mBinaryPixels = new short[mHeight][mWidth];

        BitmapTools.getPixels(bitmap, mPixels);
    }

    public String getName() {
        return mName;
    }

    public int getHeight() {
        return mHeight;
    }

    public int getWidth() {
        return mWidth;
    }

    public int getPixel(int y, int x) {
        return mPixels[y][x];
    }

    public void setBinaryPixel(int y, int x, short val) {
        mBinaryPixels[y][x] = val;
    }
    
    public short getBinaryPixel(int y, int x)
    {
    	return mBinaryPixels[y][x];
    }
    
    public short[][] getBinaryPixels()
    {
    	return mBinaryPixels;
    }
    
    public Bitmap convertToBitmap()
    {
    	return BitmapTools.convertArrayToBitmap(mBinaryPixels, mHeight, mWidth);
    }
   
    public void writePixelsToFile(String outputDirectory, String name) {
    	
        BitmapTools.writeImageArray(mBinaryPixels, mHeight, mWidth, outputDirectory, name);
    }
    
    public void cropImage(int ystart, int yend, int xstart, int xend){
    	short [][] croppedBinaryPix = new short [yend - ystart][xend - xstart];
   	
		for(int i = ystart; i < yend; i++)
			for(int j = xstart; j < xend; j++){
				croppedBinaryPix[i - ystart][j - xstart] = mBinaryPixels[i][j];
			}
		mBinaryPixels = croppedBinaryPix;
		mHeight =  yend - ystart;
		mWidth = xend - xstart;
				
	}
    
    // Generate binary pixels
 	public void genBinaryPixels()
 	{
 		int height = getHeight(); 
 		int width = getWidth();
 		for(int row = 0; row < height; row++){
 			for(int col = 0; col < width; col++){
 				short val = (short) getPixel(row, col); 
 	 			setBinaryPixel(row, col, val);
 			}
 		}
 	}
}
