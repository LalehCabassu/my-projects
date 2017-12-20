package com.example.alignednutritiontablelocalization;

import android.graphics.Color;

public class LineDetection {
	
	public static double magnitudeThreshold = 20;
	public static double thetaThreshold = 360;
	
	private static Pixels rgbImage;
	private static Pixels rgbImageOut;
	private static double rcoeff = 0.2126;
	private static double gcoeff = 0.7152;
	private static double bcoeff = 0.0722;
	private static double defaultDelta = 1.0;
	private static double defaultTheta = -200;
	
	// Detect horizontal lines
	public static void detectVerticalLines(Pixels inImage, Pixels outImage)
	{
		rgbImage = inImage;
		rgbImageOut = outImage;
		
		genPixelImage();
		
		for(int row = 0; row < rgbImageOut.getWidth(); row++)
			for(int col = 0; col < rgbImageOut.getHeight(); col++)
			{
				//double magnitude = gradientMagnitude(col, row);
				double theta = gradientDirection(col, row);
				
				//if (magnitude >= magnitudeThreshold && Math.abs(theta) <= thetaThreshold)
				if (Math.abs(90 - theta) <= thetaThreshold)
					rgbImageOut.setBinaryPixel(col, row, (short)255);
				else
					rgbImageOut.setBinaryPixel(col, row, (short)0);
			}
	}
	
	// Detect vertical lines
	public static void detectHorizontalLines(Pixels inImage, Pixels outImage)
	{
		rgbImage = inImage;
		rgbImageOut = outImage;
		
		genPixelImage();
		
		for(int row = 0; row < rgbImageOut.getWidth(); row++)
			for(int col = 0; col < rgbImageOut.getHeight(); col++)
			{
				double magnitude = gradientMagnitude(col, row);
				double theta = gradientDirection(col, row);
				
				//if (magnitude >= magnitudeThreshold && Math.abs(theta) <= thetaThreshold)
				if (Math.abs(theta - 0) <= thetaThreshold && magnitude >= magnitudeThreshold)
					rgbImageOut.setBinaryPixel(col, row, (short)255);
				else
					rgbImageOut.setBinaryPixel(col, row, (short)0);
			}
	}
		
	// Create an image
	static void genPixelImage()
	{
		int numCols = rgbImage.getHeight(), numRows = rgbImage.getWidth();
		int col = 0, row = 0;
		
		while (row != numRows)
		{
			col = col % numCols;
			short val = (short) rgbImage.getPixel(col, row); 
			rgbImageOut.setBinaryPixel(col, row, val);
			if (col == numCols - 1)
				row ++;
			col ++;
		}
	}
	
	// Calculate the gradient direction of a pixel
	static double gradientDirection(int col, int row)
	{
		if (!isInRange(col, row))
			return defaultTheta;
		
		double pdy = lumChangeY(col, row), pdx = lumChangeX(col, row);
		if (pdy == defaultDelta && pdx == defaultDelta)
			return defaultTheta;
		
		double theta = Math.atan2(pdy, pdx) * (180 / Math.PI);
		if (theta < 0)
			return Math.floor(theta);
		else if (theta > 0)
			return Math.ceil(theta);
		return theta;
	}
	
	// Calculate the gradient magnitude of a pixel
	static double gradientMagnitude(int col, int row)
	{
		double pdx = lumChangeX(col, row);
		double pdy = lumChangeY(col, row);
		return Math.sqrt(Math.pow(pdx, 2.0) + Math.pow(pdy, 2.0));
	}
	
	// Compute luminosity change in the y direction
	static double lumChangeX(int col, int row)
	{
		if (!isInRange(col, row))
			return defaultDelta; 
		
		double dx = luminosity(rgbImage.getPixel(col + 1, row)) - 
					luminosity(rgbImage.getPixel(col - 1, row));
		if (dx == 0.0)
			return defaultDelta;
		return dx;
	}
		
	// Compute luminosity change in the Y direction
	static double lumChangeY(int col, int row)
	{
		if (!isInRange(col, row))
			return defaultDelta; 
		
		double dy = luminosity(rgbImage.getPixel(col, row - 1)) - 
					luminosity(rgbImage.getPixel(col, row + 1));
		if (dy == 0.0)
			return defaultDelta;
		return dy;
	}
	
	// Return true if a pixel is not on borders
	static boolean isInRange(int col, int row)
	{
		return (col > 0) && (col < rgbImage.getHeight() - 1) && 
				(row > 0) && (row < rgbImage.getWidth() - 1);
	}
	
	// Calculate luminosity of a pixel
	public static double luminosity(int rgb)
	{
		return rcoeff * Color.red(rgb) + gcoeff * Color.green(rgb) + bcoeff * Color.blue(rgb);
	}

}
	
	/*
	
	
	//private static int blackThreshold = 15;
	//private static int lineThreshold = 10;
	//private static int lengthThreshold; 
	
	public static void horizontalLines(Pixels image, int stdBlackThreshold, int stdLengthThreshold){
        int height = image.getHeight();
        int width  = image.getWidth();
        
        //short [][] binPix = image.getBinaryPixels();
        int lengthThreshold = (int) Math.ceil(width / stdLengthThreshold);
        int [][] lines = new int [height][2]; //0: start of a line, 1: end of a line
        
        for (int row = 0; row < height; row++){
        	lines[row][0] = 0;
        	lines[row][1] = 0;
        	
        	// Current pixel and the following one are both black , keep them in line!!!
            for (int col = 0; col < width - 1; col++) {
            	// Start a line
            	if(image.getPixel(row, col) == 0  && 
            			image.getPixel(row, col + 1) == 0 && lines[row][0] == 0)
            		lines[row][0] = col;
            	// Omit a line
            	else if(col - lines[row][0] < lengthThreshold)
            		lines[row][0] = 0;
            	// End a line
            	else if (lines[row][0] != 0)
            		lines[row][1] = col;
            }
            
            // Set pixels in a line to 255 and others to 0
            for (int col = 0; col < width; col++) {
            	if(col >= lines[row][0] && col <= lines[row][1])
            		image.setBinaryPixel(row, col, (short) 255); 
            	else
            		image.setBinaryPixel(row, col, (short) 0);
            }
        }        
	}
*/
