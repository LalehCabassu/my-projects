package com.example.imagebinarization;

import android.graphics.Bitmap;

public class Pixels {
	
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
   
    public void writePixelsToFile(String outputDirectory, String name) {
        BitmapTools.writeImageArray(mBinaryPixels, mHeight, mWidth, outputDirectory, name);
    }
}