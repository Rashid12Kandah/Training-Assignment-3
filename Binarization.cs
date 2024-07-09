using System;
using System.Drawing;
using System.Drawing.Imaging;

public class Binarization{

    public static Bitmap GrayScale(Bitmap image){
        int width = image.Width, height = image.Height;

        Bitmap tempImg = new Bitmap(width,height, PixelFormat.Format8bppIndexed);

        ColorPalette palette = tempImg.Palette;
        for (int i = 0; i < 256; i++)
        {
            palette.Entries[i] = Color.FromArgb(i, i, i);
        }
        tempImg.Palette = palette;

        BitmapData imgData = image.LockBits(new Rectangle(0,0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb); 

        BitmapData tempData = tempImg.LockBits(new Rectangle(0,0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

        int imgStride = imgData.Stride;
        int imgOffset = imgStride - (width*3);

        int tempStride = tempData.Stride;
        int tempOffset = tempStride - width;


        unsafe{

            byte * imgPtr = (byte *)(void *) imgData.Scan0;
            byte * tempPtr = (byte *)(void *) tempData.Scan0;
            byte red, green, blue;

            for(int y = 0; y < height; y++){
                for(int x = 0; x<width; x++){
                    red = imgPtr[0];
                    green = imgPtr[1];
                    blue = imgPtr[2];

                    tempPtr[0] = (byte)(.299 * red + .587 * green + .114 * blue);

                    imgPtr +=3;
                    tempPtr += 1;
                }
                imgPtr += imgOffset;
                tempPtr += tempOffset;

            }
            }

        image.UnlockBits(imgData);
        tempImg.UnlockBits(tempData);

        return tempImg;

        // BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format8bppRgb);

		// 	int stride = bmData.Stride;
		// 	System.IntPtr Scan0 = bmData.Scan0;

		// 	unsafe
		// 	{
		// 		byte * p = (byte *)(void *)Scan0;

		// 		int nOffset = stride - b.Width*3;

		// 		byte red, green, blue;

		// 		for(int y=0;y<b.Height;++y)
		// 		{
		// 			for(int x=0; x < b.Width; ++x )
		// 			{
		// 				blue = p[0];
		// 				green = p[1];
		// 				red = p[2];

		// 				p[0] = p[1] = p[2] = (byte)(.299 * red + .587 * green + .114 * blue);

		// 				p += 3;
		// 			}
		// 			p += nOffset;
		// 		}
		// 	}

		// 	b.UnlockBits(bmData);

		// 	return b;

    }

    public static Bitmap Static_Binarization(Bitmap image, long threshold){
        if(image.PixelFormat != PixelFormat.Format8bppIndexed){
            image = GrayScale(image);
        }

        Bitmap newImage = (Bitmap)image.Clone();
        int width = newImage.Width, height = newImage.Height;

        // BitmapData imgData = image.LockBits(new Rectangle(0,0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
        BitmapData newData = newImage.LockBits(new Rectangle(0,0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

        // int imgStride = imgData.Stride;
        // int imgOffset = imgStride - imgData.Width;

        int newStride = newData.Stride;
        // int newOffset = newStride - ((newData.Width+7)/8);


        unsafe
        {
            // byte* imgPtr = (byte*)(void*)imgData.Scan0;
            byte* newPtr = (byte*)(void*)newData.Scan0;

            for(int y = 0; y<height; y++)
            {
                // byte* linePtr = imgPtr + (y*imgStride);
                byte* newlinePtr = newPtr + (y*newStride);


                for(int x = 0; x<width; x++)
                {
                    // int byteIndx = x/8;
                    // int bitIndx = x%8;

                    int pixelValue = newlinePtr[x];

                    // if(pixelValue > threshold){
                    //     newlinePtr[byteIndx] |= (byte)(1 << (7-bitIndx)); // Left shift the 0000 0001 by the difference between 7 and the index of the bit in the current byte to place the 1 in the position of the pixel in the 1-bit image in the byte index. Then the OR operator is used to add the white pixel to the correct position. 
                    // }else{
                    //     newlinePtr[byteIndx] &= (byte)~(1 << (7-bitIndx)); // Left shift the 0000 0001 by the difference between 7 and the index of the bit in the current byte to place the 1 in the position of the pixel in the 1-bit image in the byte index. Then the AND operator is used to keep the zero in that pixel, because the original pixel is lower than the threshold.
                    // }

                    newlinePtr[x] = (byte)(pixelValue > threshold ? 255 : 0);

                }
            }
        }

        // image.UnlockBits(imgData);
        newImage.UnlockBits(newData);

        return newImage;



    }

    public static long MeanIntensity(Bitmap image){
        int width = image.Width, height = image.Height;
        long sum = 0;
        int numpix = width * height;

        BitmapData imgData = image.LockBits(new Rectangle(0,0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);

        int imgStride = imgData.Stride;

        unsafe
        {
            byte* imgPtr = (byte*)(void*)imgData.Scan0;

            for(int y = 0; y<height; y++)
            {
                byte* linePtr = imgPtr + (y*imgStride);

                for(int x = 0; x<width; x++)
                {

                    sum += linePtr[x];
                }
            }

        }

        image.UnlockBits(imgData);
//
        long meanIntensity = (long)(sum/numpix);

        return meanIntensity;

    }

    public static Bitmap MeanBinarization(Bitmap image){
        if(image.PixelFormat != PixelFormat.Format8bppIndexed){
            image = GrayScale(image);
        }
        long meanIntensity = MeanIntensity(image);

        long threshold = (long)(meanIntensity*0.7);
        Console.WriteLine($"Threshold: {threshold}");
        return Static_Binarization(image, threshold);
    }

    public static void Main(string[] args){
        string imagePath = args[0];
        string method = args[1];
        var watch = new System.Diagnostics.Stopwatch();
        string uuid = Guid.NewGuid().ToString();
        string savePath = $"{method}_{uuid}.jpg";

        Bitmap inputImage = new Bitmap(imagePath);

        if(method== "static"){

            Console.Write("Enter the threshold Value: ");
            int threshold = int.Parse(Console.ReadLine());
            watch.Start();
            Bitmap output = Static_Binarization(inputImage, threshold);
            watch.Stop();
            output.Save(savePath);
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Original Image Format: {inputImage.PixelFormat}");
            Console.WriteLine($"Binarized Image Format: {output.PixelFormat}");

        }else if(method == "mean"){

            watch.Start();
            Bitmap output = MeanBinarization(inputImage);
            watch.Stop();
            output.Save(savePath);
            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Original Image Format: {inputImage.PixelFormat}");
            Console.WriteLine($"Binarized Image Format: {output.PixelFormat}");

        }else{

            Console.WriteLine("Invalid method specified. Use 'static' or 'mean'.");
            return;

        }



    }
}
