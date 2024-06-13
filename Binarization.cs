    using System;
using System.Drawing;

public class Binarization{

    public static Bitmap Static_Binarization(Bitmap image, int threshold){
        Bitmap newImage = new Bitmap(image.Width, image.Height);

        for(int y = 0; y<image.Height; y++){
            for(int x = 0; x<image.Width; x++){
                Color pixel = image.GetPixel(x,y);

                int avgpix = (pixel.R + pixel.G + pixel.B)/3;

                if(avgpix >= threshold){
                    newImage.SetPixel(x,y, Color.White);
                }else{
                    newImage.SetPixel(x,y, Color.Black);
                }

            }
        }

        return newImage;
    }

    public static int MeanIntensity(Bitmap image){
        long sum = 0;
        long numpix = 0;
        for(int y=0; y<image.Height; y++){
            for(int x=0; x<image.Width; x++){

                Color pixel = image.GetPixel(x,y);
                numpix++;
                sum += (pixel.R + pixel.G + pixel.B)/3;
            }
        }

        int meanIntensity = (int)(sum/numpix);

        return meanIntensity;

    }

    public static Bitmap MeanBinarization(Bitmap image){
        int threshold = MeanIntensity(image);
        return Static_Binarization(image, threshold);
    }

    public static void Main(string[] args){
        string imagePath = args[0];
        string method = args[1];

        Bitmap inputImage;

        string uuid = Guid.NewGuid().ToString();
        string savePath = $"{method}_{uuid}.jpg";

        using (var inputTemp = Image.FromFile(imagePath)){
            inputImage = new Bitmap(inputTemp);
        }

        if(method== "static"){
            Console.Write("Enter the threshold Value: ");
            int threshold = int.Parse(Console.ReadLine());
            Bitmap output = Static_Binarization(inputImage, threshold);
            output.Save(savePath);

        }else if(method == "mean"){
            Bitmap output = MeanBinarization(inputImage);
            output.Save(savePath);
        }else{
            Console.WriteLine("Invalid method specified. Use 'static' or 'mean'.");
            return;
        }

        

    }
}