package com.example.linedetection_houghtransforms;

import android.graphics.Bitmap;

public class HoughTransform  {
	
	protected static int LOWER_RHO_BOUND = 0;
	protected static int UPPER_RHO_BOUND = 1;
	protected static int LOWER_THETA_BOUND = 0;
	protected static int UPPER_THETA_BOUND = 180;
	protected static int THRESHOLD_SUPPORT_LEVEL = 200;
	protected static Bitmap inBitmap = null;
		
	public static Bitmap detectLines(Bitmap bm){
		inBitmap = bm;
		Pixels inImage = new Pixels("image", inBitmap);
		Pixels outImage = new Pixels("image", inBitmap);
		int [][] HT_table;
		
		UPPER_RHO_BOUND = (int) Math.floor(Math.sqrt( Math.pow(inImage.getHeight(), 2) + Math.pow(inImage.getWidth(), 2)));
		HT_table = new int [UPPER_RHO_BOUND - LOWER_RHO_BOUND + 1][UPPER_THETA_BOUND - LOWER_THETA_BOUND + 1]; 
		
		int height = inImage.getHeight();
		int width = inImage.getWidth();
		
		EdgeDetector.rgbImage = inImage;
		EdgeDetector.rgbImageOut = outImage;
		EdgeDetector.genPixelImage();
		
		for (int row = 0; row < height; row++) {
			for (int col = 0; col < width; col++) {
				double ed_magnitude = EdgeDetector.gradientMagnitude(col, row);
				double ed_theta = EdgeDetector.gradientDirection(col, row);
				
				if (ed_magnitude >= EdgeDetector.magnitudeThreshold && 
						Math.abs(ed_theta) <= EdgeDetector.thetaThreshold){
					for(int theta = LOWER_THETA_BOUND; theta <= UPPER_THETA_BOUND; theta++){
						int rho = (int) (col * Math.cos(theta) + row * Math.sin(theta));
						if(rho >= LOWER_RHO_BOUND && rho <= UPPER_RHO_BOUND)
							HT_table[rho][theta] ++;
					}
				}
			}
		}
		for(int rho = LOWER_RHO_BOUND; rho <= UPPER_RHO_BOUND; rho++){
			for(int theta = LOWER_THETA_BOUND; theta <= UPPER_THETA_BOUND; theta++){
				if(HT_table[rho][theta] > THRESHOLD_SUPPORT_LEVEL)
					drawLines(rho, theta);
			}
		}
		return inBitmap;
	}
	
	public static void drawLines(int rho, int theta){
		
		int height = inBitmap.getHeight();
		int width = inBitmap.getWidth();
		
		int x = (int) Math.abs((rho * Math.cos(theta)));
		int y = (int) Math.abs((rho * Math.sin(theta)));
		
		// Find the line equation in X,Y system (y = mx + b)
		if (y != 0){
			int m = (int) (Math.ceil(- x / y));
			int b = (int) (Math.ceil(x * x / y + y));
			
			for (int row = 0; row < height; row++) {
				for (int col = 0; col < width; col++) {
					if(row == m * col + b) // (col, row) is on the line
						inBitmap.setPixel(col, row, 255);
				}
			}
		}
		else{
			// Vertical lines: X = x (col = x)
			for (int row = 0; row < height; row++) {
				inBitmap.setPixel(x, row, 255);
			}
		}
	}
}
