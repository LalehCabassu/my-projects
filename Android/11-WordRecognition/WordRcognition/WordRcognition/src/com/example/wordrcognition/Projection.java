package com.example.wordrcognition;

public class Projection {
	
	// On BLACK pixels
	public static int [] verticalProjection(Pixels image){
		int width = image.getWidth();
		int height = image.getHeight();
		int [] verticalProj = new int [width];
		
		for(int col = 0; col < width; col++){
			int gx = 0;	
	    	for(int row = 0; row < height; row++){
	    		gx += (255 - image.getPixel(row, col));	  
	    	}
	    	verticalProj[col] = gx;
		}   
		return verticalProj;
	}
	
	// On BLACK pixels
	public static int [] horizontalProjection(Pixels image){
		int width = image.getWidth();
		int height = image.getHeight();
		int [] horizonProj = new int [height];
		
		for(int row = 0; row < height; row++){
			int fx = 0;	
	    	for(int col = 0; col < width; col++){
	    		fx += (255 - image.getPixel(row, col));
		    	}
	    	horizonProj[row] = fx;
		}
		return horizonProj;
	}

	public static int meanVerticalProjection(Pixels image, int [] verticalProj){
		int width = image.getWidth();
		int sumPro = 0;
		
		for(int col = 0; col < width; col++)
			sumPro += verticalProj[col];
		return (int) Math.ceil(sumPro / width);
	}
	
	public static int meanHorizontalProjection(Pixels image, int [] horizonProj){
		int height = image.getHeight();
		int sumPro = 0;
		
		for(int row = 0; row < height; row++)
			sumPro += horizonProj[row];
		return (int) Math.ceil(sumPro / height);
	}
}
