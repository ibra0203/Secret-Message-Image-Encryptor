# Secret Message Image Encryptor
## Introduction
This is an ASP.NET web application that encrypts a message using a key and saves the output to an image. The image can then be decrypted using the same key.

To try the application:
http://messageencryptor.azurewebsites.net

## Encryption/Decryption Explained
If you tried to encrypt a message using this app, you'll get a similar result to this:

![Alt text](Screenshots/exm.png?raw=true "example")

But how did we encrypt a string into an image like this?
### Step 1: Encrypting The Message
The Encryption process is fairly straightforward. The steps are: Raw message => Hash with the key as the salt using the MD5 algorithm => Result is base64 byte array => Convert to a binary string => Draw an image using the binary string where 0=white pixel, 1=black pixel.  

First, the string is hashed using the key as salt through the MD5 hashing algorithm. The result will be a base64 byte array, you can use that to generate a string of a binary representation of the data, which will be used to draw the pixels of the image. You can read the Encryption code in **"Helpers/Encryptor.cs"** to follow along. The methods **FinalEncrypt** and **FinalDecrypt** perform all the steps by calling other methods in the class.

Decryption is done by reversing the steps. Which should be:
Read every pixel in the image and convert it to a binary string => convert binary string to base64 string => decrypt the base64 string using the key.

### Step 2: Generating The Image

The code for generating the image from a binary string can be found in ***"Helpers/Helpers.cs".*** 
The function ***"GenerateImageFromBinary"*** is fully responsible for that. Every step is documented there through comments.

The first step is calculating the width and height of the image. Since every character in the binary string is going to be represented, the initial width and height are decided by getting the square root of the binary string's length then ceiling it. For example if the length is 108, the square root of that will be around 10.4. if you ceiled it, it's 11. So the image will end up being 11\*11 which is 121. That means we get more pixels that we won't be using. You calculate the number of extra pixels by substracting the binary string length from rSquared(r*r). So rSquared-binaryLength. Which means we have around 13 extra pixels in this case.

To calculate the number of extra rows we can remove due to them being unnecessary, we can do Height = Height - Math.Floor(extraPixels/width) .. which is going to be 13/11 in this case. This gives us 1 row to remove. 
Notice that Floor is necessary in this case so you won't remove a row in use. 



The image is generated using ***System.Drawing.Bitmap***. Which represents a ***GDI+ bitmap***.

When the binary string ends **a red pixel is set right afterwards** to mark the end of the string. This is done to tell the decryptor that reading should be over. You can notice it in the previous image. Usually it's followed by the color black until the end of the image, which just indicates no color.

### Encryption/Decryption image rules

#### **=Different length messages and keys will generate different sized images**
The image on the left here is generated using around a 7200 characters message and a 20 characters key.
The one on the right is generated with around a 2400 characters message and a 10 characters key.
![Alt text](Screenshots/comparison.png?raw=true "Comparison")
#### **=Image can't contain colors other than white, black and red**

# ----

## Walkthrough

You can either Encode a message into an image or Decode a message from an image. To encode a message choose **"Encode"** from the side. 
You'll then be taken to a screen where you can Encode a message. The corresponding view is **"Views/Encode.aspx"**, with the controller being **"Controllers/Encode.aspx.cs"**. The key is used to hash your message, so make sure you use the exact same key when decoding. 
This app also makes use of captcha to prevent spam.

![Alt text](Screenshots/2.png?raw=true "Main page")

If there was a problem with the fields, warning will display letting you know. If not, you'll find yourself in another view. This one is located in **"Views/DisplayImage.aspx"**, corresponding controller is in the **"Controllers"** folder.
Here you can view your generated image or download it.

![Alt text](Screenshots/3.png?raw=true "Displaying generated image")

By clicking on the image, you can see a close-up preview.

![Alt text](Screenshots/4.png?raw=true "Displaying generated image")

If you head to the **"Decode"** section of the website, you can decode the image by using the same key you used. 
Using a different key will render the process impossible, so make sure it exactly matches.

![Alt text](Screenshots/5.png?raw=true "Decoding image")

And here's our decoded message:
![Alt text](Screenshots/6.png?raw=true "Decoding image")
