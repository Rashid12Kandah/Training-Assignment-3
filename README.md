# Binarization task (static/mean)

csc Binarization.cs

mono Binarization.exe "path/to/image" method (static/mean)


### Original Image - Dog Image
<img src = "https://github.com/Rashid12Kandah/Training-Assignment-3/blob/master/Cute_dog.jpg" alt="Original Image" width="300" height="200">

# Static Output
### Static binarization with threshold 70 - Dog

<img src = "https://github.com/Rashid12Kandah/Training-Assignment-3/blob/master/static_70_Dog.jpg" alt = "Static thresholding, Dog, thr 70" width="300" height="200">


# Mean Thresholding
> In mean thresholding I had a problem of the mean being too high, so I tried to adjust the scaling factor of the mean intensity to have a better result.

> Mean Binarization no scaling altered.

<img src="https://github.com/Rashid12Kandah/Training-Assignment-3/blob/master/mean_normal.jpg" alt="Mean Binarization, Dog" width="300" height="200">

> Mean Binarization scaling the mean intensity by 80%

<img src="https://github.com/Rashid12Kandah/Training-Assignment-3/blob/master/mean_dog_0.8.jpg" alt="Mean Binarization, Dog, S.F. 0.8" width="300" height="200">

>> In the mean binarization of the dog image the change of the S.F did not show significant differences, by looking at it, to the normal mean.

### Original Image - Cheque Image
<img src = "https://github.com/Rashid12Kandah/Training-Assignment-3/blob/master/24bit_cheque.bmp" alt = "24-bit cheque bmp" width = "748" height="352">

# Static Output

### Static binarization with threshold 70 - Cheque

<img src = "https://github.com/Rashid12Kandah/Training-Assignment-3/blob/master/static_70_thr.jpg" alt = "Static Thresholding, Cheque, thr 70" width="748" height="352">

### Static Binarization with threshold 75 - Cheque

<img src = "https://github.com/Rashid12Kandah/Training-Assignment-3/blob/master/static_75_thr.jpg" alt = "Static Thresholding, Cheque, thr 75" width = "748" height="352">


# Mean Output

>In the follwoing example I tried different S.F. (0.7 and 0.8).

> Mean Binarization no Scaling Factors altered

<img src="https://github.com/Rashid12Kandah/Training-Assignment-3/blob/master/mean_not_normalized.jpg" alt="Mean Thresholding not Scaled" width = "748" height="352">

> Mean Binarization Scaling Factor = 0.7

<img src="https://github.com/Rashid12Kandah/Training-Assignment-3/blob/master/mean_0.7_normalize.jpg" alt="Mean Thresholding, S.F. 0.7" width = "748" height="352">

> Mean Binarization Scaling Factor = 0.8

<img src = "https://github.com/Rashid12Kandah/Training-Assignment-3/blob/master/mean_0.8_normalize.jpg" alt="Mean Thresholding, S.F. 0.8" width = "748" height="352">
