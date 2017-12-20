package edu.usu.wr.imageprocessing;

public class NiblackBinarizer {
	
	private static int binaryThreshold = 127;

    public static void binarizeImage(Pixels image, int blockSize, double stdThreshold, double niblackConstant){
        int height = image.getHeight();
        int width  = image.getWidth();
        for (int i = 0; i < height; i = i + blockSize){
            for (int j = 0; j < width; j = j + blockSize) {

                int m = i + blockSize;
                if (m >= height)
                    m = height - 1;

                int n = j + blockSize;
                if (n >= width)
                    n = width - 1;

                double meanVal = 0;
                double stdVal = 0;
                int p = 0;

                for (int k = i; k < m; k++){
                    for (int l = j; l < n; l++){
                        meanVal = meanVal + image.getPixel(k, l);
                        p++;
                    }
                }
                meanVal = meanVal/p;

                for (int k = i; k < m; k++){
                    for (int l = j; l < n; l++){
                        stdVal = stdVal + Math.pow(image.getPixel(k, l) - meanVal, 2.0);
                    }
                }
                stdVal = Math.sqrt(stdVal/p);
                double threshold = binaryThreshold;
                if (stdVal > stdThreshold)  {
                    threshold = (meanVal + niblackConstant * stdVal);
                }

                for (int k = i; k < m; k++){
                    for (int l = j; l < n; l++){
                        if (image.getPixel(k, l) <= threshold)
                            image.setBinaryPixel(k, l, (short) 255);
                        else
                            image.setBinaryPixel(k, l, (short) 0);
                    }
                }
            }
        }
    }
}
