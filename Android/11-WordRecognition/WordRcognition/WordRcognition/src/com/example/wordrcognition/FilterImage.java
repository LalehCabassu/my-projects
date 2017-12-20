package com.example.wordrcognition;

import android.graphics.Bitmap;

public class FilterImage {

	 static Pixels rgbImageIn;
	 static Pixels rgbImageOut;
	
	public static Bitmap averageFilter(Bitmap bm){
	
		Pixels inImage = new Pixels("middleZone", bm);
		Pixels outImage = new Pixels("blurred", bm);
		
		// Gaussian filter
		//double [][] filter = 
			//{{0.00761, 0.036075, 0.10959}, 
				//{0.21345, 0.2666, 0.21345},
				//{0.10959, 0.03608, 0.00761}};
		
		// average filter
		double [][] filter =
			{{1, 1, 1, 1, 1}, {1, 1, 1, 1, 1}, {1, 1, 1, 1, 1}, {1, 1, 1, 1, 1}, {1, 1, 1, 1, 1}};
		
		int gaussianSize = filter.length;
		
		int width = inImage.getWidth();
		int height = inImage.getHeight();
		
		rgbImageIn = inImage;
		rgbImageOut = outImage;
		
		//genPixelImage();
		rgbImageIn.genBinaryPixels();
		rgbImageOut.genBinaryPixels();
		
		for(int row = 0; row < height; row++){
			for(int col = 0; col < width; col++){
				rgbImageOut.setBinaryPixel(row, col, (short)rgbImageIn.getBinaryPixel(row, col));
				
				// each pixels
				double sum = 0, count = 0;
				for(int gr = -(gaussianSize / 2); gr <= (gaussianSize / 2); gr++){
					for(int gc = -(gaussianSize / 2); gc <= (gaussianSize / 2); gc++){	
						
						// each average value
						if( (row + gr > 0 && row + gr < height) && (col + gc > 0 && col + gc < width) ){
							sum += filter[gr + (gaussianSize / 2)][gc + (gaussianSize / 2)] * 
									rgbImageIn.getBinaryPixel(row + gr, col + gc);
							count ++;
						}
					}
				}
				if(count > 0)
					rgbImageOut.setBinaryPixel(row, col, (short) (sum / count));
			}
		}
		return rgbImageOut.convertToBitmap();
	}
	
	// Create an image
	static void genPixelImage()
	{
		int numCols = rgbImageIn.getHeight(), numRows = rgbImageIn.getWidth();
		int col = 0, row = 0;
		
		while (row != numRows)
		{
			col = col % numCols;
			short val = (short) rgbImageIn.getPixel(col, row); 
			rgbImageIn.setBinaryPixel(col, row, val);
			rgbImageOut.setBinaryPixel(col, row, val);
			if (col == numCols - 1)
				row ++;
			col ++;
		}
	}
	
}
